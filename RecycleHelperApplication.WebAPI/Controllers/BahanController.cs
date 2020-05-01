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
        private readonly IBahanHandler bahanHandler;

        public BahanController(IBahanHandler bahanHandler)
        {
            this.bahanHandler = bahanHandler;
        }
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAllBahan()
        {
            try
            {
                return Json(await bahanHandler.GetAllBahan());
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
                return Json(await bahanHandler.GetById(id));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("Kategori/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetListByKategori(int id)
        {
            try
            {
                return Json(await bahanHandler.GetListByKategori(id));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("Panduan/{Id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetListByPanduan(int id)
        {
            try
            {
                return Json(await bahanHandler.GetListByPanduan(id));
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
                return Json(await bahanHandler.Insert(body));
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
                return Json(await bahanHandler.Update(body));
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
        public async Task<IHttpActionResult> DeleteBahan(int idBahan)
        {
            try
            {
                return Json(await bahanHandler.DeleteBahan(idBahan));
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
        [Route("Multiple/{ids}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteMultiple(string ids)
        {
            try
            {
                return Json(await bahanHandler.DeleteMultiple(ids));
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
    }
}
