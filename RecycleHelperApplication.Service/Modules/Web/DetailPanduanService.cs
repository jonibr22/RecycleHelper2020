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
    public interface IDetailPanduanService
    {
        Task<int> Delete(DetailPanduan detailPanduan);
        Task<int> Insert(DetailPanduan detailPanduan);
    }
    public class DetailPanduanService : IDetailPanduanService
    {
        public async Task<int> Delete(DetailPanduan detailPanduan)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("DetailPanduan", detailPanduan);

            var result = (await new APICall().Execute($"DetailPanduan/Delete", client, HttpMethod.Post, Body)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string ReturnValueStr = jObj.SelectToken("ReturnValue").ToString();
                int status = JsonConvert.DeserializeObject<int>(ReturnValueStr);
                return status;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        } 
        public async Task<int> Insert(DetailPanduan detailPanduan)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("DetailPanduan", detailPanduan);

            var result = (await new APICall().Execute($"DetailPanduan/Insert", client, HttpMethod.Post, Body)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string ReturnValueStr = jObj.SelectToken("ReturnValue").ToString();
                int status = JsonConvert.DeserializeObject<int>(ReturnValueStr);
                return status;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
    }
}
