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
                    throw new UnauthorizedException("Bahan tidak ada");
                }
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = BahanResponse
                };
            }
            catch (UnauthorizedException e)
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