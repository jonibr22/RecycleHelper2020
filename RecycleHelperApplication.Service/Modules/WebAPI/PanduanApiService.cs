﻿using Newtonsoft.Json;
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
    public interface IPanduanApiService
    {
        Task<List<Panduan>> GetAllPanduan();
        Task<List<Panduan>> GetListByMultipleBahan(string bahanIds);
        Task<List<Panduan>> GetListByUser(int userId);
        Task<Panduan> GetById(int id);
        Task<ExecuteResult> InsertUpdate(Panduan Panduan);
        Task<ExecuteResult> Delete(int id);
        //Task<int> Delete(string ids);
        Task<ExecuteResult> DeleteMultiple(string ids);
        Task<int> Insert(Panduan panduan);
        Task<int> Update(Panduan panduan);
    }
    public class PanduanApiService : IPanduanApiService
    {
        private readonly IPanduanRepository panduanRepository;
        public PanduanApiService(IPanduanRepository panduanRepository)
        {
            this.panduanRepository = panduanRepository;
        }
        public async Task<List<Panduan>> GetAllPanduan()
        {
            return (await panduanRepository.ExecSPToListAsync("Panduan_GetAllPanduan")).ToList();
        }
        public async Task<List<Panduan>> GetListByMultipleBahan(string bahanIds)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@ListIdBahan", bahanIds)
            };
            return (await panduanRepository.ExecSPToListAsync("Panduan_GetListByMultipleBahan @ListIdBahan ", Param)).ToList();
        }
        public async Task<List<Panduan>> GetListByUser(int userId)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdUser", userId)
            };
            return (await panduanRepository.ExecSPToListAsync("Panduan_GetListByUser @IdUser ", Param)).ToList();
        }
        public async Task<Panduan> GetById(int id)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdPanduan", id)
            };
            return await panduanRepository.ExecSPToSingleAsync("Panduan_GetById @IdPanduan ", Param);
        }
        public async Task<ExecuteResult> InsertUpdate(Panduan Panduan)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "Panduan_InsertUpdate @IdPanduan, @NamaPanduan, @DeskripsiPanduan, @IdUser",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdPanduan", Panduan.IdPanduan),
                    new SqlParameter("@NamaPanduan", Panduan.NamaPanduan),
                    new SqlParameter("@DeskripsiPanduan", Panduan.DeskripsiPanduan),
                    new SqlParameter("@IdUser", Panduan.IdUser),
                }
            });

            ReturnValue = await panduanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
        public async Task<ExecuteResult> Delete(int id)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "DetailPanduan_DeleteByPanduan @IdPanduan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdPanduan", id)
                }
            });
            Data.Add(new StoredProcedure
            {
                SPName = "Panduan_Delete @IdPanduan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdPanduan", id)
                }
            });

            ReturnValue = await panduanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
        public async Task<ExecuteResult> DeleteMultiple(string ids)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "DetailPanduan_DeleteByMultiplePanduan @ListIdPanduan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@ListIdPanduan", ids)
                }
            });
            Data.Add(new StoredProcedure
            {
                SPName = "Panduan_DeleteMultiple @ListIdPanduan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@ListIdPanduan", ids)
                }
            });

            ReturnValue = await panduanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
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
    }
}
