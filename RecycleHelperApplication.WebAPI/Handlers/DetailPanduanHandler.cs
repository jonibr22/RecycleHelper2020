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
    public interface IDetailPanduanHandler
    {
        Task<object> Insert(JObject body);
        Task<object> Delete(JObject body);
    }
    public class DetailPanduanHandler : IDetailPanduanHandler
    {
        private readonly IDetailPanduanApiService detailPanduanService;
        public DetailPanduanHandler(IDetailPanduanApiService detailPanduanSerice)
        {
            this.detailPanduanService = detailPanduanSerice;
        }
        public async Task<object> Insert(JObject body)
        {
            try
            {
                DetailPanduan detailPanduanRequest = body.Value<JObject>("DetailPanduan").ToObject<DetailPanduan>();
                List<DetailPanduan> detailPanduanResponse = await detailPanduanService.GetWithParam(detailPanduanRequest);
                if(detailPanduanResponse != null && detailPanduanResponse.Count > 0)
                {
                    throw new NotPermittedException("Data yang sama telah tercatat, tidak dapat insert");
                }
                ExecuteResult result = await detailPanduanService.Insert(detailPanduanRequest);
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
        public async Task<object> Delete(JObject body)
        {
            try
            {
                DetailPanduan detailPanduanRequest = body.Value<JObject>("DetailPanduan").ToObject<DetailPanduan>();
                List<DetailPanduan> detailPanduanResponse = await detailPanduanService.GetWithParam(detailPanduanRequest);
                if (detailPanduanResponse == null || (detailPanduanResponse != null && detailPanduanResponse.Count == 0))
                {
                    throw new NotFoundException("Data tidak ditemukan, tidak dapat delete");
                }
                ExecuteResult result = await detailPanduanService.Delete(detailPanduanRequest);
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