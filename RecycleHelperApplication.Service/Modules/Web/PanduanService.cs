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
        Task<List<Panduan>> GetAllPanduan();
        Task<Panduan> GetById(int id);
        Task<int> Insert(Panduan panduan);
        Task<int> Update(Panduan panduan);
        Task<int> Delete(string ids);
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

        public async Task<List<Panduan>> GetAllPanduan()
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Panduan", client, HttpMethod.Get)).Data.ToString();
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

        public async Task<Panduan> GetById(int id)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Panduan/{id}", client, HttpMethod.Get)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string panduanStr = jObj.SelectToken("Panduan").ToString();
                Panduan panduan = JsonConvert.DeserializeObject<Panduan>(panduanStr);
                return panduan;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }

        public async Task<int> Insert(Panduan panduan)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("Panduan", panduan);

            var result = (await new APICall().Execute($"Panduan", client, HttpMethod.Post, Body)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string ReturnValueStr = jObj.SelectToken("ReturnValue").ToString();
                int id = JsonConvert.DeserializeObject<int>(ReturnValueStr);
                return id;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }

        public async Task<int> Update(Panduan panduan)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("Panduan", panduan);

            var result = (await new APICall().Execute($"Panduan", client, HttpMethod.Put, Body)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string ReturnValueStr = jObj.SelectToken("ReturnValue").ToString();
                int id = JsonConvert.DeserializeObject<int>(ReturnValueStr);
                return id;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }

        public async Task<int> Delete(string ids)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Panduan/Multiple/{ids}", client, HttpMethod.Delete)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string retvalStr = jObj.SelectToken("ReturnValue").ToString();
                int retval = JsonConvert.DeserializeObject<int>(retvalStr);
                return retval;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
    }
}
