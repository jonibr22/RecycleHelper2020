using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using UnityDependencyResolver.Lib;
using RecycleHelperApplication.Data.Infrastructure;
using Unity;
using RecycleHelperApplication.Data.Repositories;
using RecycleHelperApplication.WebAPI.Handlers;
using RecycleHelperApplication.Service.Modules.WebAPI;

namespace RecycleHelperApplication.WebAPI
{
    public class Bootstrapper
    {
        public static IUnityContainer Initialize()
        {
            var container = new UnityContainer();
            container.RegisterType<IDbFactory, DBFactory>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserApiService, UserApiService>();
            container.RegisterType<IUserHandler, UserHandler>();
            return container;
        }
    }
}