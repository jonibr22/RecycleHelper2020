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
        [HttpPost]
        public async Task<IHttpActionResult> GetAllBahan()
        {
            try
            {
                return Json(await BahanHandler.GetAllBahan());
            }
            catch (UnauthorizedException e)
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
