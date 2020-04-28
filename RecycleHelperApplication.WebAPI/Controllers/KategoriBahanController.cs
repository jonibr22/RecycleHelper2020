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
    [RoutePrefix("KategoriBahan")]
    public class KategoriBahanController : ApiController
    {
        private readonly IKategoriBahanHandler kategoriBahanHandler;

        public KategoriBahanController(IKategoriBahanHandler kategoriBahanHandler)
        {
            this.kategoriBahanHandler = kategoriBahanHandler;
        }
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllKategoriBahan()
        {
            try
            {
                return Json(await kategoriBahanHandler.GetAllKategoriBahan());
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                return Json(await kategoriBahanHandler.GetById(id));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Insert(JObject body)
        {
            try
            {
                return Json(await kategoriBahanHandler.Insert(body));
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
        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> Update(JObject body)
        {
            try
            {
                return Json(await kategoriBahanHandler.Update(body));
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
