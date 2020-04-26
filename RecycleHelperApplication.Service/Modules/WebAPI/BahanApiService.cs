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
    public interface IBahanApiService
    {
        Task<List<Bahan>> GetAllBahan();
        Task<ExecuteResult> InsertUpdate(Bahan bahan);
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
        public async Task<ExecuteResult> InsertUpdate(Bahan bahan)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "Bahan_InsertUpdate @IdBahan, @NamaBahan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdBahan", bahan.IdBahan),
                    new SqlParameter("@NamaBahan", bahan.NamaBahan)
                }
            });

            ReturnValue = await bahanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
    }
}
