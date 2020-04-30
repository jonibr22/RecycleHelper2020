using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Model.Base;
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
    public interface IKategoriBahanService
    {
        Task<List<KategoriBahan>> GetAllKategoriBahan();
        Task<KategoriBahan> GetById(int id);
        Task<int> Insert(KategoriBahan kategoriBahan);
        Task<int> Update(KategoriBahan kategoriBahan);
    }
    public class KategoriBahanService : IKategoriBahanService
    {
        public async Task<List<KategoriBahan>> GetAllKategoriBahan()
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"KategoriBahan", client, HttpMethod.Get)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string listKategoriBahanStr = jObj.SelectToken("ListKategoriBahan").ToString();
                List<KategoriBahan> listKategoriBahan = JsonConvert.DeserializeObject<List<KategoriBahan>>(listKategoriBahanStr);
                return listKategoriBahan;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
        public async Task<KategoriBahan> GetById(int id)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"KategoriBahan/{id}", client, HttpMethod.Get)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string kategoriBahanStr = jObj.SelectToken("KategoriBahan").ToString();
                KategoriBahan kategoriBahan = JsonConvert.DeserializeObject<KategoriBahan>(kategoriBahanStr);
                return kategoriBahan;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
        public async Task<int> Insert(KategoriBahan kategoriBahan)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("KategoriBahan", kategoriBahan);

            var result = (await new APICall().Execute($"KategoriBahan", client, HttpMethod.Post, Body)).Data.ToString();
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
        public async Task<int> Update(KategoriBahan kategoriBahan)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("KategoriBahan", kategoriBahan);

            var result = (await new APICall().Execute($"KategoriBahan", client, HttpMethod.Put, Body)).Data.ToString();
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
    }
}
