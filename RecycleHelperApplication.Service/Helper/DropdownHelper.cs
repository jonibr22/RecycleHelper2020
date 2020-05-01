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
using System.Web.Mvc;

namespace RecycleHelperApplication.Service.Helper
{
    public class DropdownHelper
    {
        public static List<SelectListItem> Init(string baseMessage)
        {
            List<SelectListItem> listDropdown = new List<SelectListItem>();
            listDropdown.Insert(0, new SelectListItem() { Value = "0", Text = baseMessage });
            return listDropdown;
        }
        public static async Task<List<SelectListItem>> GetKategoriBahanDropdown()
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"KategoriBahan", client, HttpMethod.Get)).Data.ToString();
            List<SelectListItem> kategoriBahanList = new List<SelectListItem>();
            kategoriBahanList.Insert(0, new SelectListItem() { Value = "0", Text = "-- Pilih Kategori --" });

            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string listKategoriBahanStr = jObj.SelectToken("ListKategoriBahan").ToString();
                List<KategoriBahan> listKategoriBahan = JsonConvert.DeserializeObject<List<KategoriBahan>>(listKategoriBahanStr);

                foreach (var x in listKategoriBahan)
                {
                    kategoriBahanList.Add(new SelectListItem
                    {
                        Value = x.IdKategoriBahan.ToString(),
                        Text = x.NamaKategoriBahan
                    });
                }

            }
            return kategoriBahanList;
        }
        public static async Task<List<SelectListItem>> GetBahanByKategoriDropdown(int id)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Bahan/Kategori/{id}", client, HttpMethod.Get)).Data.ToString();
            List<SelectListItem> bahanList = new List<SelectListItem>();
            bahanList.Insert(0, new SelectListItem() { Value = "0", Text = "-- Pilih Bahan --" });
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string listBahanStr = jObj.SelectToken("ListBahan").ToString();
                List<Bahan> listBahan = JsonConvert.DeserializeObject<List<Bahan>>(listBahanStr);

                foreach (var x in listBahan)
                {
                    bahanList.Add(new SelectListItem
                    {
                        Value = x.IdBahan.ToString(),
                        Text = x.NamaBahan
                    });
                }
            }
            return bahanList;
        }
        public static async Task<List<SelectListItem>> GetAllBahanDropdown()
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Bahan", client, HttpMethod.Get)).Data.ToString();
            List<SelectListItem> bahanList = new List<SelectListItem>();
            bahanList.Insert(0, new SelectListItem() { Value = "0", Text = "-- Pilih Bahan --" });
            if (result.GetStatusCode() == 200)
            {
                JObject jObj = JObject.Parse(result);
                string listBahanStr = jObj.SelectToken("ListBahan").ToString();
                List<Bahan> listBahan = JsonConvert.DeserializeObject<List<Bahan>>(listBahanStr);

                foreach (var x in listBahan)
                {
                    bahanList.Add(new SelectListItem
                    {
                        Value = x.IdBahan.ToString(),
                        Text = x.NamaBahan
                    });
                }
            }
            return bahanList;
        }
    }
}
