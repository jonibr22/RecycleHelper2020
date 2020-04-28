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
    public interface IDetailPanduanApiService
    {
        Task<List<DetailPanduan>> GetWithParam(DetailPanduan detailPanduan);
        Task<ExecuteResult> Insert(DetailPanduan detailPanduan);
        Task<ExecuteResult> Delete(DetailPanduan detailPanduan);
    }
    public class DetailPanduanApiService : IDetailPanduanApiService
    {
        private readonly IDetailPanduanRepository detailPanduanRepository;
        public DetailPanduanApiService(IDetailPanduanRepository detailPanduanRepository)
        {
            this.detailPanduanRepository = detailPanduanRepository;
        }
        public async Task<List<DetailPanduan>> GetWithParam(DetailPanduan detailPanduan)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdPanduan", detailPanduan.IdPanduan),
                new SqlParameter("@IdBahan", detailPanduan.IdBahan)
            };
            return (await detailPanduanRepository.ExecSPToListAsync("DetailPanduan_GetWithParam @IdPanduan, @IdBahan ", Param)).ToList();
        }
        public async Task<ExecuteResult> Insert(DetailPanduan detailPanduan)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "DetailPanduan_Insert @IdPanduan, @IdBahan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdPanduan", detailPanduan.IdPanduan),
                    new SqlParameter("@IdBahan", detailPanduan.IdBahan),
                }
            });

            ReturnValue = await detailPanduanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
        public async Task<ExecuteResult> Delete(DetailPanduan detailPanduan)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "DetailPanduan_Delete @IdPanduan, @IdBahan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdPanduan", detailPanduan.IdPanduan),
                    new SqlParameter("@IdBahan", detailPanduan.IdBahan),
                }
            });

            ReturnValue = await detailPanduanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
    }
}
