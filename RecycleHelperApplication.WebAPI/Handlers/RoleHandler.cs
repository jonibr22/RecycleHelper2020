using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper.APIHelper;
using RecycleHelperApplication.Service.Modules.WebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecycleHelperApplication.WebAPI.Handlers
{
    public interface IRoleHandler
    {
        Task<object> GetAllRole();
        Task<object> GetById(int id);
        Task<object> Insert(JObject body);
        Task<object> Update(JObject body);
    }
    public class RoleHandler : IRoleHandler
    {
        private readonly IRoleApiService roleService;

        public RoleHandler(IRoleApiService roleService)
        {
            this.roleService = roleService;
        }
        public async Task<object> GetAllRole()
        {
            try
            {
                List<Role> roleResponse = await roleService.GetAllRole();
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ListRole = roleResponse
                };
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> GetById(int id)
        {
            try
            {
                Role role = await roleService.GetById(id);
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    Role = role
                };
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> Insert(JObject body)
        {
            try
            {
                Role roleRequest = body.Value<JObject>("Role").ToObject<Role>();
                List<Role> listAllRole = await roleService.GetAllRole();
                if (listAllRole.Any(x => x.NamaRole.ToLower().Trim() == roleRequest.NamaRole.ToLower().Trim()))
                {
                    throw new NotPermittedException("Nama role yang sama sudah tersedia");
                }
                ExecuteResult result = await roleService.InsertUpdate(roleRequest);

                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = result.ReturnVariable
                };
            }
            catch (NotPermittedException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> Update(JObject body)
        {
            try
            {
                Role roleRequest = body.Value<JObject>("Role").ToObject<Role>();
                Role roleResponse = await roleService.GetById(roleRequest.IdRole);

                if (roleResponse == null)
                {
                    throw new NotFoundException("Role dengan ID tersebut tidak ditemukan");
                }
                ExecuteResult result = await roleService.InsertUpdate(roleRequest);

                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = result.ReturnVariable
                };
            }
            catch (NotFoundException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
    }
}