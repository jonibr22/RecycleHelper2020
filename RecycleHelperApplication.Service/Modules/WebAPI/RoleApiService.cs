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
    public interface IRoleApiService
    {
        Task<List<Role>> GetAllRole();
        Task<Role> GetById(int id);
        Task<ExecuteResult> InsertUpdate(Role role);
    }
    public class RoleApiService : IRoleApiService
    {
        private readonly IRoleRepository roleRepository;
        public RoleApiService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        public async Task<List<Role>> GetAllRole()
        {
            return (await roleRepository.ExecSPToListAsync("Role_GetAllRole")).ToList();
        }
        public async Task<Role> GetById(int id)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdRole", id)
            };
            return await roleRepository.ExecSPToSingleAsync("Role_GetById @IdRole ", Param);
        }
        public async Task<ExecuteResult> InsertUpdate(Role role)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "Role_InsertUpdate @IdRole, @NamaRole ",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdRole", role.IdRole),
                    new SqlParameter("@NamaRole", role.NamaRole),
                }
            });

            ReturnValue = await roleRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
    }
}
