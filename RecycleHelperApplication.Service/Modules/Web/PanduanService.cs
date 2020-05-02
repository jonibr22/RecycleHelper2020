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
    public interface IPanduanService
    {
        Task<List<Panduan>> GetListByMultipleBahan(string ids);
        Task<List<Panduan>> GetListByUser(int userId);
    }
    public class PanduanService : IPanduanService
    {
        public async Task<List<Panduan>> GetListByMultipleBahan(string ids)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Panduan/MultipleBahan/{ids}", client, HttpMethod.Get)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string listPanduanStr = jObj.SelectToken("ListPanduan").ToString();
                List<Panduan> listPanduan = JsonConvert.DeserializeObject<List<Panduan>>(listPanduanStr);
                return listPanduan;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
        public async Task<List<Panduan>> GetListByUser(int userId)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Panduan/User/{userId}", client, HttpMethod.Get)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string listPanduanStr = jObj.SelectToken("ListPanduan").ToString();
                List<Panduan> listPanduan = JsonConvert.DeserializeObject<List<Panduan>>(listPanduanStr);
                return listPanduan;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
    }
}
