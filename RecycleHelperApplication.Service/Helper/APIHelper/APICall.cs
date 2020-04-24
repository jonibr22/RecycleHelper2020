using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static RecycleHelperApplication.Service.Helper.APIHelper.APICall;

namespace RecycleHelperApplication.Service.Helper.APIHelper
{
    public class APIMethod
    {
        public static int GET = 1;
        public static int POST = 2;
    }

    public interface IHttpClientBuilder
    {
        HttpClientBuilder SetBaseURL(string BaseURL);
        HttpClientBuilder AddHeader(string Key, string Value);
        HttpClientBuilder SetMediaTypeWithQualityHeaderValue(string HeaderValue);
        HttpClient Build();
    }

    public class APICall : AsyncController
    {
        #region "Statics"
        public static string APPLICATIONJSON = "application/json";
        public static string X_HTTP_URLENCODED = "application/x-www-form-urlencoded";
        #endregion

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

        public async Task<JsonResult> Execute(string URL, HttpClient client, HttpMethod httpMethod, Dictionary<string, dynamic> body = null, NameValueCollection queryParameters = null)
        {
            if (queryParameters != null && queryParameters.Count > 0)
            {
                URL = URL + ToQueryString(queryParameters);
            }
            StringContent content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            string Response = "";
            using (client)
            {
                if (httpMethod == HttpMethod.Get)
                {
                    var res = await client.GetAsync(URL);
                    Response = await res.Content.ReadAsStringAsync();
                }
                else if (httpMethod == HttpMethod.Post)
                {
                    HttpResponseMessage response = await client.PostAsync(URL, content);
                    Response = await response.Content.ReadAsStringAsync();
                }
                else if (httpMethod == HttpMethod.Put)
                {
                    HttpResponseMessage response = await client.PutAsync(URL, content);
                    Response = await response.Content.ReadAsStringAsync();
                }
                else if (httpMethod == HttpMethod.Delete)
                {
                    HttpResponseMessage response = await client.DeleteAsync(URL);
                    Response = await response.Content.ReadAsStringAsync();
                }
            }

            return Json(Response);
        }

        public class HttpClientBuilder : IHttpClientBuilder
        {
            HttpClient client;
            NameValueCollection Headers = new NameValueCollection();
            string BaseURL;
            string MediaTypeWithQualityHeaderValue;

            int APIMethod;

            public HttpClientBuilder AddHeader(string Key, string Value)
            {
                this.Headers.Add(Key, Value);
                return this;
            }

            public HttpClientBuilder SetBaseURL(string BaseURL)
            {
                this.BaseURL = BaseURL;
                return this;
            }

            public HttpClientBuilder SetMediaTypeWithQualityHeaderValue(string MediaTypeWithQualityHeaderValue)
            {
                this.MediaTypeWithQualityHeaderValue = MediaTypeWithQualityHeaderValue;
                return this;
            }

            public HttpClient Build()
            {
                this.client = new HttpClient(new HttpClientHandler()
                {
                    UseCookies = true,
                    UseDefaultCredentials = true,
                    CookieContainer = new CookieContainer()
                })
                {
                    BaseAddress = new Uri(this.BaseURL)
                };

                this.client.DefaultRequestHeaders.Accept.Clear();
                this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.MediaTypeWithQualityHeaderValue));

                // Add Headers
                foreach (string key in this.Headers)
                {
                    this.client.DefaultRequestHeaders.Add(key, this.Headers[key]);
                }

                return client;
            }
        }
    }
}
