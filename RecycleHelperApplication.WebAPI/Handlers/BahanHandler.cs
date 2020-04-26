using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Modules.WebAPI;
using RecycleHelperApplication.Service.Helper.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecycleHelperApplication.WebAPI.Handlers
{
    public interface IBahanHandler
    {
        Task<object> GetAllBahan();
        Task<object> Insert(JObject body);
        Task<object> Update(JObject body);
    }
    public class BahanHandler : IBahanHandler
    {
        private readonly IBahanApiService BahanService;

        public BahanHandler(IBahanApiService BahanService)
        {
            this.BahanService = BahanService;
        }
        public async Task<object> GetAllBahan()
        {
            try
            {
                List<Bahan> BahanResponse = await BahanService.GetAllBahan();

                if (BahanResponse == null)
                {
                    throw new NotFoundException("Bahan tidak ada");
                }
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListBahan = BahanResponse
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
        public async Task<object> Insert(JObject body)
        {
            try
            {
                Bahan bahanRequest = body.Value<JObject>("Bahan").ToObject<Bahan>();
                List<Bahan> listAllBahan  = await BahanService.GetAllBahan();
                if(listAllBahan.Any(x => x.NamaBahan.ToLower().Trim() == bahanRequest.NamaBahan.ToLower().Trim()))
                {
                    throw new NotPermittedException("Nama bahan yang sama sudah tersedia");
                }
                ExecuteResult result = await BahanService.InsertUpdate(bahanRequest);

                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = result.ReturnVariable
                };
            }
            catch (NotPermittedException e)
            {
                throw e;
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
                Bahan bahanRequest = body.Value<JObject>("Bahan").ToObject<Bahan>();
                Bahan bahanResponse = await BahanService.GetById(bahanRequest.IdBahan);

                if (bahanResponse == null)
                {
                    throw new NotFoundException("Bahan dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await BahanService.InsertUpdate(bahanRequest);

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
    }
}