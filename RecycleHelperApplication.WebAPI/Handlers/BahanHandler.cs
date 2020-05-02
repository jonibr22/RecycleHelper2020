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
        Task<object> DeleteBahan(int idBahan);
        Task<object> DeleteMultiple(string ids);
    }
    public class BahanHandler : IBahanHandler
    {
        private readonly IBahanApiService bahanService;
        private readonly IPanduanApiService panduanService;

        public BahanHandler(IBahanApiService bahanService, IPanduanApiService panduanService)
        {
            this.bahanService = bahanService;
            this.panduanService = panduanService;
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
                if(result.ReturnVariable <= 0)
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
                Bahan bahanRequest = body.Value<JObject>("Bahan").ToObject<Bahan>();
                Bahan bahanResponse = await bahanService.GetById(bahanRequest.IdBahan);

                if (bahanResponse == null)
                {
                    throw new NotFoundException("Bahan dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await bahanService.InsertUpdate(bahanRequest);
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

        public async Task<object> DeleteBahan(int IdBahan)
        {
            try
            {
                List<Bahan> BahanResponse = await bahanService.GetListByPanduan(IdBahan);
                if (BahanResponse == null || (BahanResponse != null && BahanResponse.Count == 0))
                {
                    throw new NotFoundException("Bahan tidak ditemukan, tidak dapat delete");
                }
                ExecuteResult result = await bahanService.Delete(IdBahan);
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
                List<Panduan> panduanList = await panduanService.GetListByMultipleBahan(ids);
                if(panduanList != null && panduanList.Count > 0)
                {
                    throw new NotPermittedException("Masih terdapat panduan yang menggunakan bahan tersebut");
                }
                ExecuteResult result = await bahanService.DeleteMultiple(ids);
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