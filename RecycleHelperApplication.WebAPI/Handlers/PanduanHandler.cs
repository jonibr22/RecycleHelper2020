using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper.APIHelper;
using RecycleHelperApplication.Service.Modules.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecycleHelperApplication.WebAPI.Handlers
{
    public interface IPanduanHandler
    {
        Task<object> GetAllPanduan();
        Task<object> GetListByMultipleBahan(string bahanIds);
        Task<object> GetListByUser(int idUser);
        Task<object> GetById(int id);
        Task<object> Insert(JObject body);
        Task<object> Update(JObject body);
        Task<object> Delete(int id);
    }
    public class PanduanHandler : IPanduanHandler
    {
        private readonly IPanduanApiService panduanService;

        public PanduanHandler(IPanduanApiService panduanService)
        {
            this.panduanService = panduanService;
        }
        public async Task<object> GetAllPanduan()
        {
            try
            {
                List<Panduan> panduanResponse = await panduanService.GetAllPanduan();
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListPanduan = panduanResponse
                };
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> GetListByMultipleBahan(string bahanIds)
        {
            try
            {
                List<Panduan> panduanResponse = await panduanService.GetListByMultipleBahan(bahanIds);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListPanduan = panduanResponse
                };
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> GetListByUser(int userId)
        {
            try
            {
                List<Panduan> panduanResponse = await panduanService.GetListByUser(userId);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListPanduan = panduanResponse
                };
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> GetById(int id)
        {
            try
            {
                Panduan panduan = await panduanService.GetById(id);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    Panduan = panduan
                };
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> Insert(JObject body)
        {
            try
            {
                Panduan panduanRequest = body.Value<JObject>("Panduan").ToObject<Panduan>();
                ExecuteResult result = await panduanService.InsertUpdate(panduanRequest);

                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = result.ReturnVariable
                };
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> Update(JObject body)
        {
            try
            {
                Panduan panduanRequest = body.Value<JObject>("Panduan").ToObject<Panduan>();
                Panduan panduanResponse = await panduanService.GetById(panduanRequest.IdPanduan);

                if (panduanResponse == null)
                {
                    throw new NotFoundException("Panduan dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await panduanService.InsertUpdate(panduanRequest);

                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = result.ReturnVariable
                };
            }
            catch (NotFoundException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> Delete(int id)
        {
            try
            {
                Panduan panduan = await panduanService.GetById(id);
                if(panduan == null)
                {
                    throw new NotFoundException("Panduan dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await panduanService.Delete(id);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = result.ReturnVariable
                };
            }
            catch(NotFoundException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}