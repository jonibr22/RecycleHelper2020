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
        Task<Panduan> GetById(int idPanduan);
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

        public async Task <Panduan> GetById (int id)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Panduan/{id}", client, HttpMethod.Get)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string kategoriBahanStr = jObj.SelectToken("Panduan").ToString();
                Panduan Panduan = JsonConvert.DeserializeObject<Panduan>(kategoriBahanStr);
                return Panduan;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }

    }
}
