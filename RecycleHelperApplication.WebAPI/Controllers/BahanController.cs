using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Service.Helper.APIHelper;
using RecycleHelperApplication.WebAPI.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RecycleHelperApplication.WebAPI.Controllers
{
    [RoutePrefix("Bahan")]
    public class BahanController : ApiController
    {
        private readonly IBahanHandler BahanHandler;

        public BahanController(IBahanHandler BahanHandler)
        {
            this.BahanHandler = BahanHandler;
        }
        [Route("GetAllBahan")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBahan()
        {
            try
            {
                return Json(await BahanHandler.GetAllBahan());
            }
            catch (NotFoundException e)
            {
                return Json(APIError.From(e));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("Insert")]
        [HttpPost]
        public async Task<IHttpActionResult> Insert(JObject body)
        {
            try
            {
                return Json(await BahanHandler.Insert(body));
            }
            catch (NotPermittedException e)
            {
                return Json(APIError.From(e));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("Update")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(JObject body)
        {
            try
            {
                return Json(await BahanHandler.Update(body));
            }
            catch (NotFoundException e)
            {
                return Json(APIError.From(e));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
    }
}
