using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Modules.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RecycleHelperApplication.ViewModels.UserViewModels;
using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Service.Helper;

namespace RecycleHelperApplication.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IPanduanService panduanService;
        private readonly IBahanService bahanService;
        private List<AlertMessage> ListAlert = new List<AlertMessage>();
        public UserController(IUserService userService, IRoleService roleService, IPanduanService panduanService, IBahanService bahanService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.panduanService = panduanService;
            this.bahanService = bahanService;
        }
        // GET: User
        public async Task<ActionResult> Index()
        {
            List<User> listUser = await userService.GetAllUser();
            List<Role> listRole = await roleService.GetAllRole();
            return View(new IndexViewModel{
                ListUser = listUser,
                ListRole = listRole
            });
        }
        public async Task<ActionResult> Detail(int id = 0)
        {
            id = (id == 0) ? Convert.ToInt32(Session[SessionEnum.USER_ID]) : id;
            User user = await userService.GetById(id);
            string path = UserProfilePictureManager.GetProfilePictureOrDefaultIfFileNameIsNull(user.PhotoUrl);
            return View(new DetailViewModel
            {
                IdUser = user.IdUser,
                Name = user.Name,
                Username = user.Username,
                PhotoUrl = path
            });
        }
        public async Task<ActionResult> _ContributionTable(int idUser)
        {
            List<Panduan> listPanduan = await panduanService.GetListByUser(idUser);
            Dictionary<int, List<Bahan>> listBahanByPanduan = new Dictionary<int, List<Bahan>>();
            foreach(Panduan panduan in listPanduan)
            {
                int idPanduan = panduan.IdPanduan;
                List<Bahan> listBahan = await bahanService.GetListByPanduan(idPanduan);
                listBahanByPanduan.Add(idPanduan, listBahan);
            }
            return PartialView(new ContributionTableViewModel
            {
                ListPanduan = listPanduan,
                ListBahanByPanduan = listBahanByPanduan
            });
        }
        public async Task<ActionResult> _PasswordManagement(int idUser)
        {
            return PartialView(new PasswordManagementViewModel
            {
                IdUser = idUser
            });
        }
        public async Task<JsonResult> _ChangeName(int id,string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new Exception("Nama tidak boleh kosong");
                }
                User user = await userService.GetById(id);
                user.Name = name;
                int result = await userService.Edit(user);
                return Json(new { status = "success", message = "", name = name });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", message = e.Message });
            }
        }
        public async Task<ActionResult> _SavePassword(PasswordManagementViewModel passwordManagementViewModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_PasswordManagement", passwordManagementViewModel);
            }
            try
            {
                User user = await userService.GetById(passwordManagementViewModel.IdUser);
                if (user.Password != passwordManagementViewModel.OldPassword)
                {
                    ModelState.AddModelError("OldPassword", "Password lama salah");
                    return PartialView("_PasswordManagement", passwordManagementViewModel);
                }
                if (passwordManagementViewModel.NewPassword != passwordManagementViewModel.ConfirmNewPassword)
                {
                    ModelState.AddModelError("ConfirmNewPassword", "Konfirmasi Password tidak sesuai");
                    return PartialView("_PasswordManagement", passwordManagementViewModel);
                }
                user.Password = passwordManagementViewModel.NewPassword;
                int id = await userService.Edit(user);
                ViewBag.ChangePasswordSuccess = 1;
            }
            catch(Exception e)
            {
                ViewBag.ChangePasswordSuccess = 0;
                ViewBag.ErrorMessage = e.Message;
            }
            return PartialView("_PasswordManagement", passwordManagementViewModel);
        }
        public async Task<ActionResult> UploadPhoto(HttpPostedFileBase File)
        {
            int idUser = Convert.ToInt32(Session[SessionEnum.USER_ID]);
            try
            {
                string fileName = UserProfilePictureManager.Upload(File, idUser);
                if(fileName != null)
                {
                    User user = await userService.GetById(idUser);
                    user.PhotoUrl = fileName;
                    int result = await userService.Edit(user);
                    ListAlert.Add(new AlertMessage("success", "Upload file sukses"));
                    TempData["ListAlert"] = ListAlert;
                    return Redirect("Detail/" + idUser);
                }
                throw new Exception("File not found");
            }
            catch(Exception e)
            {
                ListAlert.Add(new AlertMessage("error", e.Message));
                TempData["ListAlert"] = ListAlert;
            }
            return Redirect("Detail/"+idUser);
        }
    }
}