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
    public interface IPanduanHandler
    {
        Task<object> GetAllPanduan();
        Task<object> Insert(JObject body);
        Task<object> Update(JObject body);
        Task<object> Delete(JObject body);
    }
    public class PanduanHandler : IPanduanHandler
    {
        private readonly IPanduanApiService PanduanService;

        public PanduanHandler(IPanduanApiService PanduanService)
        {
            this.PanduanService = PanduanService;
        }
        public async Task<object> GetAllPanduan()
        {
            try
            {
                List<Panduan> PanduanResponse = await PanduanService.GetAllPanduan();

                if (PanduanResponse == null)
                {
                    throw new NotFoundException("Panduan tidak ada");
                }
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListPanduan = PanduanResponse
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
                Panduan PanduanRequest = body.Value<JObject>("Panduan").ToObject<Panduan>();
                List<Panduan> listAllPanduan  = await PanduanService.GetAllPanduan();
                if(listAllPanduan.Any(x => x.NamaPanduan.ToLower().Trim() == PanduanRequest.NamaPanduan.ToLower().Trim()))
                {
                    throw new NotPermittedException("Nama Panduan yang sama sudah tersedia");
                }
                ExecuteResult result = await PanduanService.InsertUpdate(PanduanRequest);

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
                Panduan PanduanRequest = body.Value<JObject>("Panduan").ToObject<Panduan>();
                Panduan PanduanResponse = await PanduanService.GetById(PanduanRequest.IdPanduan);

                if (PanduanResponse == null)
                {
                    throw new NotFoundException("Panduan dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await PanduanService.InsertUpdate(PanduanRequest);

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
        public async Task<object> Delete(JObject body)
        {
            try
            {
                Panduan PanduanRequest = body.Value<JObject>("Panduan").ToObject<Panduan>();
                Panduan PanduanResponse = await PanduanService.GetById(PanduanRequest.IdPanduan);

                if (PanduanResponse == null)
                {
                    throw new NotFoundException("Panduan dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await PanduanService.Delete(PanduanRequest);

                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = result.ReturnVariable
                };
            } catch (NotFoundException e)
            {
                throw e;
            } catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}