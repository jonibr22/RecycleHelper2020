using RecycleHelperApplication.Data.Repositories;
using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecycleHelperApplication.Service.Modules.WebAPI
{
    public interface IKategoriBahanApiService
    {
        Task<List<KategoriBahan>> GetAllKategoriBahan();
        Task<KategoriBahan> GetById(int id);
        Task<ExecuteResult> InsertUpdate(KategoriBahan kategoriBahan);
    }
    public class KategoriBahanApiService : IKategoriBahanApiService
    {
        private readonly IKategoriBahanRepository kategoriBahanRepository;
        public KategoriBahanApiService(IKategoriBahanRepository kategoriBahanRepository)
        {
            this.kategoriBahanRepository = kategoriBahanRepository;
        }
        public async Task<List<KategoriBahan>> GetAllKategoriBahan()
        {
            return (await kategoriBahanRepository.ExecSPToListAsync("KategoriBahan_GetAllKategoriBahan")).ToList();
        }
        public async Task<KategoriBahan> GetById(int id)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdKategoriBahan", id)
            };
            return await kategoriBahanRepository.ExecSPToSingleAsync("KategoriBahan_GetById @IdKategoriBahan ", Param);
        }
        public async Task<ExecuteResult> InsertUpdate(KategoriBahan kategoriBahan)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "KategoriBahan_InsertUpdate @IdKategoriBahan, @NamaKategoriBahan ",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdKategoriBahan", kategoriBahan.IdKategoriBahan),
                    new SqlParameter("@NamaKategoriBahan", kategoriBahan.NamaKategoriBahan),
                }
            });

            ReturnValue = await kategoriBahanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
    }
}
