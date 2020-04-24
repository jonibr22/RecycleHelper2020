﻿using Newtonsoft.Json.Linq;
using RecycleHelperApplication.Service.Helper.APIHelper;
using RecycleHelperApplication.WebAPI.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RecycleHelperApplication.WebAPI.Controllers
{
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        private readonly IUserHandler userHandler;

        public UserController(IUserHandler userHandler)
        {
            this.userHandler = userHandler;
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IHttpActionResult> DoLogin(JObject body)
        {
            try
            {
                return Json(await userHandler.DoLogin(body));
            }
            catch (UnauthorizedException e)
            {
                return Json(APIError.From(e));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IHttpActionResult> DoRegister(JObject body)
        {
            try
            {
                return Json(await userHandler.DoRegister(body));
            }
            catch (NotPermittedException e)
            {
                return Json(APIError.From(e));
            }
            catch (InternalServerErrorException e)
            {
                return Json(APIError.From(e));
            }
        }
    }
}
