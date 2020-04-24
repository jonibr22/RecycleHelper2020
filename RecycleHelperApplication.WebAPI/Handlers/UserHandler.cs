﻿using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Modules.WebAPI;
using RecycleHelperApplication.Service.Helper.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecycleHelperApplication.WebAPI.Handlers
{
    public interface IUserHandler
    {
        Task<object> DoLogin(JObject body);
        Task<object> DoRegister(JObject body);
    }
    public class UserHandler : IUserHandler
    {
        private readonly IUserApiService userService;

        public UserHandler(IUserApiService userService)
        {
            this.userService = userService;
        }
        public async Task<object> DoLogin(JObject body)
        {
            try
            {
                User userRequest = body.Value<JObject>("User").ToObject<User>();
                User userResponse = await userService.GetByUsername(userRequest.Username);

                if (userResponse == null)
                {
                    throw new UnauthorizedException("Username tidak ada");
                }
                if (userRequest.Password != userResponse.Password)
                {
                    throw new UnauthorizedException("Password salah");
                }
                return new
                {
                    Status = Models.APIResult.ResultSuccessStatus,
                    ReturnValue = userResponse.Id
                };
            }
            catch (UnauthorizedException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new InternalServerErrorException(e.Message);
            }
        }
        public async Task<object> DoRegister(JObject body)
        {
            try
            {
                User userRequest = body.Value<JObject>("User").ToObject<User>();
                User userResponse = await userService.GetByUsername(userRequest.Username);

                if (userResponse != null)
                {
                    throw new NotPermittedException("Username telah dipakai");
                }
                ExecuteResult result = await userService.InsertUpdate(userRequest);

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
    }
}