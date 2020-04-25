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
    
    public interface IAuthService
    {
        Task<int> Login(User user);
        Task<int> Register(User user);
    }
    public class AuthService : IAuthService
    {
        public async Task<int> Login(User user)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("User", user);

            var result = (await new APICall().Execute($"User/Login", client, HttpMethod.Post, Body)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string ReturnValueStr = jObj.SelectToken("ReturnValue").ToString();
                int Id = JsonConvert.DeserializeObject<int>(ReturnValueStr);
                return Id;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
        public async Task<int> Register(User user)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("User", user);

            var result = (await new APICall().Execute($"User/Register", client, HttpMethod.Post, Body)).Data.ToString();
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string ReturnValueStr = jObj.SelectToken("ReturnValue").ToString();
                int Id = JsonConvert.DeserializeObject<int>(ReturnValueStr);
                return Id;
            }
            string errMsg = result.GetStatusCode() + " " + result.GetStatusMessage() + " : " + result.GetMessage();
            throw new Exception(errMsg);
        }
    }
}
