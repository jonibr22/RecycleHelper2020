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
        Task<List<User>> GetAllUser();
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
        public async Task<List<User>> GetAllUser()
        {
            return (await userRepository.ExecSPToListAsync("User_GetAllUser ")).ToList();
        }
        public async Task<User> GetById(int id)
        {
            var Param = new SqlParameter[] {
                new SqlParameter("@IdUser", id)
            };
            return await userRepository.ExecSPToSingleAsync("User_GetById @IdUser ", Param);
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
                SPName = "User_InsertUpdate @IdUser, @Username, @Password, @Name, @IdRole, @PhotoUrl ",
                SQLParam = new SqlParameter[]
                {
                    new SqlParameter("@IdUser", user.IdUser),
                    new SqlParameter("@Username", user.Username),
                    new SqlParameter("@Password", user.Password),
                    new SqlParameter("@Name", user.Name),
                    new SqlParameter("@IdRole", user.IdRole),
                    new SqlParameter("@PhotoUrl", user.PhotoUrl ?? ((object)DBNull.Value))
                }
            });

            ReturnValue = await userRepository.ExecMultipleSPWithTransaction(Data);
            return ReturnValue;
        }
    }
}
