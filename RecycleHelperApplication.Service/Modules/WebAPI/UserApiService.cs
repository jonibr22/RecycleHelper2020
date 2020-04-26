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
    public interface IUserApiService
    {
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<ExecuteResult> InsertUpdate(User user);
    }
    public class UserApiService : IUserApiService
    {
        private readonly IUserRepository userRepository;
        public UserApiService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<User> GetById(int id)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@Id", id)
            };
            return await userRepository.ExecSPToSingleAsync("User_GetById @Id ", Param);
        }
        public async Task<User> GetByUsername(string username)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@Username", username)
            };
            return await userRepository.ExecSPToSingleAsync("User_GetByUsername @Username ", Param);
        }
        public async Task<ExecuteResult> InsertUpdate(User user)
        {
            ExecuteResult ReturnValue = new ExecuteResult();
            List<StoredProcedure> Data = new List<StoredProcedure>();

            Data.Add(new StoredProcedure
            {
                SPName = "User_InsertUpdate @Id, @Username, @Password, @Name, @RoleId ",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@Id", user.Id),
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@Password", user.Password),
                    new SqlParameter("@Name", user.Name),
                    new SqlParameter("@RoleId", user.RoleId)
                }
            });

            ReturnValue = await userRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
    }
}
