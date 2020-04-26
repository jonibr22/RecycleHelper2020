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
    public interface IPanduanApiService
    {
        Task<List<Panduan>> GetAllPanduan();
        Task<ExecuteResult> InsertUpdate(Panduan Panduan);
    }
    public class PanduanApiService : IPanduanApiService
    {
        private readonly IPanduanRepository PanduanRepository;
        public PanduanApiService(IPanduanRepository PanduanRepository)
        {
            this.PanduanRepository = PanduanRepository;
        }
        public async Task<List<Panduan>> GetAllPanduan()
        {
          
            return (await PanduanRepository.ExecSPToListAsync("Panduan_GetAllPanduan")).ToList();
        }
        public async Task<ExecuteResult> InsertUpdate(Panduan Panduan)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "Panduan_InsertUpdate @IdPanduan, @NamaPanduan, @DeskripsiPanduan, @ListIdBahan",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdPanduan", Panduan.IdPanduan),
                    new SqlParameter("@NamaPanduan", Panduan.NamaPanduan),
                    new SqlParameter("@DeskripsiPanduan", Panduan.DeskripsiPanduan),
                    new SqlParameter("@ListIdBahan", Panduan.ListIdBahan),
                }
            });

            ReturnValue = await PanduanRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
    }
}
