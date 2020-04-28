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
        Task<object> GetById(int id);
        Task<object> GetListByKategori(int idKategori);
        Task<object> GetListByPanduan(int idPanduan);
        Task<object> Insert(JObject body);
        Task<object> Update(JObject body);
    }
    public class BahanHandler : IBahanHandler
    {
        private readonly IBahanApiService bahanService;

        public BahanHandler(IBahanApiService bahanService)
        {
            this.bahanService = bahanService;
        }
        public async Task<object> GetAllBahan()
        {
            try
            {
                List<Bahan> BahanResponse = await bahanService.GetAllBahan();
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListBahan = BahanResponse
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
                Bahan bahan = await bahanService.GetById(id);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    Bahan = bahan
                };
            }
            catch(Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> GetListByKategori(int idKategori)
        {
            try
            {
                List<Bahan> BahanResponse = await bahanService.GetListByKategori(idKategori);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListBahan = BahanResponse
                };
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> GetListByPanduan(int idPanduan)
        {
            try
            {
                List<Bahan> BahanResponse = await bahanService.GetListByPanduan(idPanduan);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListBahan = BahanResponse
                };
            }
            catch(Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> Insert(JObject body)
        {
            try
            {
                Bahan bahanRequest = body.Value<JObject>("Bahan").ToObject<Bahan>();
                List<Bahan> listAllBahan  = await bahanService.GetAllBahan();
                if(listAllBahan.Any(x => x.NamaBahan.ToLower().Trim() == bahanRequest.NamaBahan.ToLower().Trim()))
                {
                    throw new NotPermittedException("Nama bahan yang sama sudah tersedia");
                }
                ExecuteResult result = await bahanService.InsertUpdate(bahanRequest);

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
                Bahan bahanResponse = await bahanService.GetById(bahanRequest.IdBahan);

                if (bahanResponse == null)
                {
                    throw new NotFoundException("Bahan dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await bahanService.InsertUpdate(bahanRequest);

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