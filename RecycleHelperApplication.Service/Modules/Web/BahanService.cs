using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper.APIHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Service.Modules.Web
{
    public interface IBahanService
    {
        Task<List<Bahan>> GetAllBahan();
    }
    public class BahanService : IBahanService
    {
        public async Task<List<Bahan>> GetAllBahan()
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Bahan", client, HttpMethod.Get)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string listBahanStr = jObj.SelectToken("ListBahan").ToString();
                List<Bahan> listBahan = JsonConvert.DeserializeObject<List<Bahan>>(listBahanStr);
                return listBahan;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
    }
}
