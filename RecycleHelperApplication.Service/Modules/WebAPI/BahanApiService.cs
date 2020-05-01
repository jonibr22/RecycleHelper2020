using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Data.Repositories;
using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper.APIHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Service.Modules.WebAPI
{
    public interface IBahanApiService
    {
        Task<List<Bahan>> GetAllBahan();
        Task<List<Bahan>> GetListByKategori(int idKategori);
        Task<List<Bahan>> GetListByMultipleKategori(string idsKategori);
        Task<List<Bahan>> GetListByPanduan(int idPanduan);
        Task<Bahan> GetById(int id);
        Task<ExecuteResult> InsertUpdate(Bahan bahan);
        Task<int> Delete(string ids);
        Task<ExecuteResult> DeleteMultiple(string ids);
        Task<int> Insert(Bahan bahan);
        Task<int> Update(Bahan bahan);
    }
    public class BahanApiService : IBahanApiService
    {
        private readonly IBahanRepository bahanRepository;
        public BahanApiService(IBahanRepository bahanRepository)
        {
            this.bahanRepository = bahanRepository;
        }
        public async Task<List<Bahan>> GetAllBahan()
        {
            return (await bahanRepository.ExecSPToListAsync("Bahan_GetAllBahan")).ToList();
        }
        public async Task<List<Bahan>> GetListByKategori(int idKategori)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdKategori", idKategori)
            };
            return (await bahanRepository.ExecSPToListAsync("Bahan_GetListByKategori @IdKategori ", Param)).ToList();
        }
        public async Task<List<Bahan>> GetListByMultipleKategori(string idsKategori)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@ListIdKategori", idsKategori)
            };
            return (await bahanRepository.ExecSPToListAsync("Bahan_GetListByMultipleKategori @ListIdKategori ", Param)).ToList();
        }
        public async Task<List<Bahan>> GetListByPanduan(int idPanduan)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdPanduan", idPanduan)
            };
            return (await bahanRepository.ExecSPToListAsync("Bahan_GetListByPanduan @IdPanduan ", Param)).ToList();
        }
        public async Task<Bahan> GetById(int id)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdBahan", id)
            };
            return await bahanRepository.ExecSPToSingleAsync("Bahan_GetById @IdBahan ", Param);
        }
        public async Task<ExecuteResult> InsertUpdate(Bahan bahan)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "Bahan_InsertUpdate @IdBahan, @NamaBahan, @IdKategoriBahan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdBahan", bahan.IdBahan),
                    new SqlParameter("@NamaBahan", bahan.NamaBahan),
                    new SqlParameter("@IdKategoriBahan", bahan.IdKategoriBahan)
                }
            });

            ReturnValue = await bahanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }

        public async Task<int> Delete(string ids)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            var result = (await new APICall().Execute($"Bahan/Multiple/{ids}", client, HttpMethod.Delete)).Data.ToString();
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
        public async Task<ExecuteResult> DeleteMultiple(string ids)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "Bahan_DeleteMultiple @ListIdBahan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@ListIdBahan", ids)
                }
            });

            ReturnValue = await bahanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
        public async Task<int> Insert(Bahan bahan)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("Bahan", bahan);

            var result = (await new APICall().Execute($"Bahan", client, HttpMethod.Post, Body)).Data.ToString();
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
        public async Task<int> Update(Bahan bahan)
        {
            HttpClient client = new APICall.HttpClientBuilder()
               .SetBaseURL(ConfigurationManager.AppSettings["API_BASE_URL"])
               .SetMediaTypeWithQualityHeaderValue(APICall.APPLICATIONJSON)
               .Build();

            Dictionary<string, dynamic> Body = new Dictionary<string, dynamic>();
            Body.Add("Bahan", bahan);

            var result = (await new APICall().Execute($"Bahan", client, HttpMethod.Put, Body)).Data.ToString();
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
