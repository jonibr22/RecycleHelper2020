using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Service.Helper.APIHelper
{
    public class APIResult : AsyncController
    {
        public async Task<JsonResult> GetAsync(string url)
        {
            /*
             * Controller : 
             * APIResult apiResult = new APIResult();
             * string apiResultString = (await apiResult.GetAsync(url)).Data.ToString();
             */
            try
            {
                using (var client = GetHttpClient())
                {
                    var response = await client.GetAsync(url);
                    var responseString = await response.Content.ReadAsStringAsync();
                    return Json(responseString);
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
        }

        public async Task<JsonResult> GetAsync(string controllerMethod, NameValueCollection nvc)
        {
            try
            {
                using (var client = GetHttpClient())
                {
                    var response = await client.GetAsync(controllerMethod + ToQueryString(nvc));
                    var responseString = await response.Content.ReadAsStringAsync();
                    return Json(responseString);
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
        }

        public async Task<JsonResult> PostAsync(string controllerMethod, NameValueCollection body)
        {
            try
            {
                using (var client = GetHttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body.AllKeys.ToDictionary(k => k, k => body.Get(k))), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(controllerMethod, content);
                    var responseString = await response.Content.ReadAsStringAsync();
                    return Json(responseString);
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
        }

        public async Task<JsonResult> PostAsync(string url, HttpClient client, NameValueCollection body)
        {
            Dictionary<string, string> param = body.AllKeys.ToDictionary(k => k, k => body.Get(k));
            using (client)
            {
                using (var content = new FormUrlEncodedContent(param))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    string ResponseStr = await response.Content.ReadAsStringAsync();
                    return Json(ResponseStr);
                }
            }
        }

        public JsonResult PostSync(string controllerMethod, HttpClient client, NameValueCollection body)
        {
            Dictionary<string, string> param = body.AllKeys.ToDictionary(k => k, k => body.Get(k));
            try
            {
                using (client)
                {
                    using (var content = new FormUrlEncodedContent(param))
                    {
                        content.Headers.Clear();
                        content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                        var response = client.PostAsync(controllerMethod, content).Result;
                        var responseString = response.Content.ReadAsStringAsync().Result;
                        return Json(responseString);
                    }
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*public JsonResult PostSync(string controllerMethod, HttpClient client, NameValueCollection body)
        {
            try
            {
                using (client)
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(body.AllKeys.ToDictionary(k => k, k => body.Get(k))), Encoding.UTF8, "application/json");
                    var response = client.PostAsync(controllerMethod, content).Result;
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    return Json(responseString);
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/

        public async Task<JsonResult> PostAuthAsync<T>(string url, T arg)
        {
            try
            {
                using (var client = GetHttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "<string access_token>");
                    var serializedStringParam = new StringContent(JsonConvert.SerializeObject(arg), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, serializedStringParam);
                    var responseString = await response.Content.ReadAsStringAsync();
                    return Json(responseString);
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
        }

        public async Task<JsonResult> PostRequestAsync(string controllerMethod, NameValueCollection nvc, HttpRequestBase Request)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        webClient.Headers[HttpRequestHeader.Cookie] = Request.Headers[HttpRequestHeader.Cookie.ToString()];
                        webClient.Headers["ApplicationKey"] = ConfigurationManager.AppSettings["WebAPI_KAJ_APPKEY"];

                        byte[] responseBytes = webClient.UploadValues(ConfigurationManager.AppSettings["WebAPI_KAJ_URL"].ToString() + controllerMethod + ToQueryString(nvc), "POST", nvc);
                        return Json(Encoding.UTF8.GetString(responseBytes));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
        }

        public JsonResult PostRequestSync(string controllerMethod, NameValueCollection nvc, HttpRequestBase Request)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        webClient.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                        webClient.Headers[HttpRequestHeader.Cookie] = Request.Headers[HttpRequestHeader.Cookie.ToString()];
                        webClient.Headers["ApplicationKey"] = ConfigurationManager.AppSettings["WebAPI_KAJ_APPKEY"];

                        byte[] responseBytes = webClient.UploadValues(ConfigurationManager.AppSettings["WebAPI_KAJ_URL"].ToString() + controllerMethod + ToQueryString(nvc), "POST", nvc);
                        return Json(Encoding.UTF8.GetString(responseBytes));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
        }

        public async Task<JsonResult> GetAuthBearer(string UserName, string Password)
        {
            /*
             * Mendapatkan Bearer token OAuth 2,
             * Returns : {access_token, token_type, expires_in}
             */
            try
            {
                using (var client = GetHttpClient())
                {
                    var data = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("UserName", UserName),
                    new KeyValuePair<string, string>("Password", Password),
                    new KeyValuePair<string, string>("grant_type", "password")
                });
                    var response = await client.PostAsync("/token", data);
                    if (response.IsSuccessStatusCode)
                    {
                        string ResponseString = await response.Content.ReadAsStringAsync();
                        return Json(ResponseString);
                    }
                    return null;
                }
            }
            catch (ArgumentNullException anex)
            {
                Console.WriteLine(anex.ToString());
                throw anex;
            }
            catch (HttpRequestException hrex)
            {
                Console.WriteLine(hrex.ToString());
                throw hrex;
            }
        }

        public static string DateTimeToQueryStr(DateTime paramDate)
        {
            return paramDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static object ExtractChild(string JsonParam, int Depth, params string[] KeysByDepth)
        {
            /*
             * Mengambil value dengan key yang dikirim dalam KeysByDepth
             * 
             * @param
             * *******************************************
             * JsonParam = String json dari API Result
             * Depth = Kedalaman key (index "Result" = 0)
             * KeysByDepth = Key sesuai urutan depth
             * 
             */
            try
            {
                JObject jObj = JObject.Parse(JsonParam);
                JToken tempObj = jObj.SelectToken("Result") ?? jObj.SelectToken("result");
                for (int i = 0; i < Depth; i++)
                {
                    tempObj = tempObj.SelectToken(KeysByDepth[i]);
                }
                return tempObj;
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public static T DeserializeObject<T>(string JsonParam)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(JsonParam);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient(new HttpClientHandler()
            {
                UseCookies = true,
                UseDefaultCredentials = true,
                CookieContainer = new CookieContainer()
            })
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["WebAPI_KAJ_URL"].ToString())
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private string ToQueryString(NameValueCollection nvc)
        {
            if (nvc == null)
                return "";
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key)
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }
    }

    public static class Extensions
    {
        /*
         * Pre-requisite : 
         * Struktur Result API String : 
         * {
         *   "Result" : {
         *      "Status" : {
         *          "Code" : RESULT_CODE,
         *          "Message" : MESSAGE
         *      },
         *      "IDRequest" : IDREQUEST
         *   }
         * }
        */
        public static int GetStatusCode(this string APIResultStr)
        {
            JObject jObject = JObject.Parse(APIResultStr);
            string codeStr = jObject.SelectToken("Status").SelectToken("Code").ToString();
            return Convert.ToInt32(codeStr);
        }

        public static string GetStatusMessage(this string APIResultStr)
        {
            JObject jObject = JObject.Parse(APIResultStr);
            string messageStr = jObject.SelectToken("Status").SelectToken("Message").ToString();
            return messageStr;
        }

        public static string GetMessage(this string APIResultStr)
        {
            JObject jObject = JObject.Parse(APIResultStr);
            string messageStr = jObject.SelectToken("Message").ToString();
            return messageStr;
        }

        public static string GetIDRequest(this string APIResultStr)
        {
            return APIResult.ExtractChild(APIResultStr, 1, "IDRequest").ToString();
        }

        public static string GetException(this string APIResultStr)
        {
            return APIResult.ExtractChild(APIResultStr, 1, "Exception").ToString();
        }
    }
}
