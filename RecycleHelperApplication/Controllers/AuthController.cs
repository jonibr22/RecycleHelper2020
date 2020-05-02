using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.AuthViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper;
using RecycleHelperApplication.Model.Base;

namespace RecycleHelperApplication.Controllers.Auth
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private List<AlertMessage> ListAlert = new List<AlertMessage>();

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        public async Task<ActionResult> Logout()
        {
            Session[SessionEnum.USER_ID] = null;
            return RedirectToAction("Index","Auth");
        }
        public async Task<ActionResult> Login(IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", indexViewModel);
            }
            try
            {
                int UserId = await authService.Login(new User
                {
                    Username = indexViewModel.Username,
                    Password = indexViewModel.Password
                });
                Session[SessionEnum.USER_ID] = UserId;
                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
                ListAlert.Add(new AlertMessage("error", e.Message));
                TempData["ListAlert"] = ListAlert;
            }
            return View("Index", indexViewModel);
        }
        public async Task<ActionResult> SignUp(RegistrationViewModel registrationViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration", registrationViewModel);
            }
            try
            {
                int UserId = await authService.Register(new User
                {
                    Username = registrationViewModel.Username,
                    Password = registrationViewModel.Password,
                    Name = registrationViewModel.Name,
                    IdRole = 1
                });
                ListAlert.Add(new AlertMessage("success", "Registrasi Sukses"));
                TempData["ListAlert"] = ListAlert;
                return RedirectToAction("Index", "Auth");
            }
            catch (Exception e)
            {
                ListAlert.Add(new AlertMessage("error", e.Message));
                TempData["ListAlert"] = ListAlert;
            }
            return View("Registration", registrationViewModel);
        }

        public async Task<ActionResult> Registration()
        {
            RegistrationViewModel registrationViewModel = new RegistrationViewModel();
            return View(registrationViewModel);
        }
        public async Task<ActionResult> Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            return View(indexViewModel);
        }
    }
}