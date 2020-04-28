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
    [RoutePrefix("DetailPanduan")]
    public class DetailPanduanController : ApiController
    {
        private readonly IDetailPanduanHandler detailPanduanHandler;
        public DetailPanduanController(IDetailPanduanHandler detailPanduanHandler)
        {
            this.detailPanduanHandler = detailPanduanHandler;
        }
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> Insert(JObject body)
        {
            try
            {
                return Json(await detailPanduanHandler.Insert(body));
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
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(JObject body)
        {
            try
            {
                return Json(await detailPanduanHandler.Delete(body));
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
