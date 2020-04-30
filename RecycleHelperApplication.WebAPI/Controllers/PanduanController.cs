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
    [RoutePrefix("Panduan")]
    public class PanduanController : ApiController
    {
        private readonly IPanduanHandler panduanHandler;

        public PanduanController(IPanduanHandler panduanHandler)
        {
            this.panduanHandler = panduanHandler;
        }
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllPanduan()
        {
            try
            {
                return Json(await panduanHandler.GetAllPanduan());
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("MultipleBahan/{ids}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetListByMultipleBahan(string ids)
        {
            try
            {
                return Json(await panduanHandler.GetListByMultipleBahan(ids));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("User/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetListByUser(int id)
        {
            try
            {
                return Json(await panduanHandler.GetListByUser(id));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            try
            {
                return Json(await panduanHandler.GetById(id));
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
                return Json(await panduanHandler.Insert(body));
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
                return Json(await panduanHandler.Update(body));
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
        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                return Json(await panduanHandler.Delete(id));
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
