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
        Task<object> DeleteMultiple(string ids);
    }
    public class KategoriBahanHandler : IKategoriBahanHandler
    {
        private readonly IKategoriBahanApiService kategoriBahanService;
        private readonly IBahanApiService bahanService;

        public KategoriBahanHandler(IKategoriBahanApiService kategoriBahanService,IBahanApiService bahanService)
        {
            this.kategoriBahanService = kategoriBahanService;
            this.bahanService = bahanService;
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
                if (result.ReturnVariable <= 0)
                {
                    throw new InternalServerErrorException("An error has occured");
                }
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
                if (result.ReturnVariable <= 0)
                {
                    throw new InternalServerErrorException("An error has occured");
                }
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
        public async Task<object> DeleteMultiple(string ids)
        {
            try
            {
                List<Bahan> bahanList = await bahanService.GetListByMultipleKategori(ids);
                if(bahanList != null && bahanList.Count > 0)
                {
                    throw new NotPermittedException("Masih ada bahan yang memakai kategori tersebut");
                }
                ExecuteResult result = await kategoriBahanService.DeleteMultiple(ids);
                if (result.ReturnVariable <= 0)
                {
                    throw new InternalServerErrorException("An error has occured");
                }
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = result.ReturnVariable
                };
            }
            catch(NotPermittedException e)
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