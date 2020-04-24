using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using RecycleHelperApplication.WebAPI.Resolvers;
using RecycleHelperApplication.WebAPI;

namespace RecycleHelperApplication.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = new UnityResolver(Bootstrapper.Initialize());

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
