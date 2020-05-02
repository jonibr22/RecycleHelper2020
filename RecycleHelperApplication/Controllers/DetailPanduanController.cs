using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.DetailPanduanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class DetailPanduanController : Controller
    {
        private readonly IPanduanService panduanService;
        private readonly IBahanService bahanService;
        private readonly IUserService userService;
        public DetailPanduanController(IPanduanService panduanService, IBahanService bahanService, IUserService userService)
        {
            this.panduanService = panduanService;
            this.bahanService = bahanService;
            this.userService = userService;
        }
        // GET: Panduan
        public async System.Threading.Tasks.Task<ActionResult> Index(int IdPanduan)
        {
            Panduan panduan = await panduanService.GetById(IdPanduan);
            List<Bahan> ListBahan = await bahanService.GetBahanByPanduan(IdPanduan);
            User user = await userService.GetById(panduan.IdUser);
            DetailPanduanViewModel test = new DetailPanduanViewModel
            {
                IdPanduan = panduan.IdPanduan,
                ListBahan = ListBahan,
                NamaPanduan = panduan.NamaPanduan,
                DeskripsiPanduan = panduan.DeskripsiPanduan,
                UserName = user.Name
            };
            return View(test);
        }
    }
}