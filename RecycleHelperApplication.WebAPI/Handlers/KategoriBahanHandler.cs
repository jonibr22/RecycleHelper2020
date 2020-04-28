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
    public interface IKategoriBahanHandler
    {
        Task<object> GetAllKategoriBahan();
        Task<object> GetById(int id);
        Task<object> Insert(JObject body);
        Task<object> Update(JObject body);
    }
    public class KategoriBahanHandler : IKategoriBahanHandler
    {
        private readonly IKategoriBahanApiService kategoriBahanService;

        public KategoriBahanHandler(IKategoriBahanApiService kategoriBahanService)
        {
            this.kategoriBahanService = kategoriBahanService;
        }
        public async Task<object> GetAllKategoriBahan()
        {
            try
            {
                List<KategoriBahan> kategoriBahanResponse = await kategoriBahanService.GetAllKategoriBahan();
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListKategoriBahan = kategoriBahanResponse
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
                KategoriBahan kategoriBahan = await kategoriBahanService.GetById(id);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    KategoriBahan = kategoriBahan
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
                KategoriBahan kategoriBahanRequest = body.Value<JObject>("KategoriBahan").ToObject<KategoriBahan>();
                List<KategoriBahan> listAllKategoriBahan = await kategoriBahanService.GetAllKategoriBahan();
                if (listAllKategoriBahan.Any(x => x.NamaKategoriBahan.ToLower().Trim() == kategoriBahanRequest.NamaKategoriBahan.ToLower().Trim()))
                {
                    throw new NotPermittedException("Nama kategori bahan yang sama sudah tersedia");
                }
                ExecuteResult result = await kategoriBahanService.InsertUpdate(kategoriBahanRequest);

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
                KategoriBahan kategoriBahanRequest = body.Value<JObject>("KategoriBahan").ToObject<KategoriBahan>();
                KategoriBahan kategoriBahanResponse = await kategoriBahanService.GetById(kategoriBahanRequest.IdKategoriBahan);

                if (kategoriBahanResponse == null)
                {
                    throw new NotFoundException("Kategori Bahan dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await kategoriBahanService.InsertUpdate(kategoriBahanRequest);

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