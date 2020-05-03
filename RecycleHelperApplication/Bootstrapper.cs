using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using RecycleHelperApplication.Data.Infrastructure;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.Data.Repositories;
using RecycleHelperApplication.Service.Modules.WebAPI;
using RecycleHelperApplication.WebAPI.Handlers;

namespace RecycleHelperApplication
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
        var container = BuildUnityContainer();
        DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        container.RegisterType<IDbFactory, DBFactory>();
        container.RegisterType<IAuthService, AuthService>();
        container.RegisterType<IBahanService, BahanService>();
        container.RegisterType<IUserService, UserService>();
        container.RegisterType<IPanduanService, PanduanService>();
        container.RegisterType<IKategoriBahanService, KategoriBahanService>();
        container.RegisterType<IRoleService, RoleService>();
        return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
    
    }
  }
}