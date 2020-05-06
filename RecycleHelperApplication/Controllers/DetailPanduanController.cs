using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.DetailPanduanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class DetailPanduanController : BaseController
    {
        private readonly IPanduanService panduanService;
        private readonly IBahanService bahanService;
        private readonly IUserService userService;
        private readonly IDetailPanduanService detailPanduanService;
        private List<AlertMessage> ListAlert = new List<AlertMessage>();
        public DetailPanduanController(IPanduanService panduanService, IBahanService bahanService, IUserService userService, IDetailPanduanService detailPanduanService)
        {
            this.panduanService = panduanService;
            this.bahanService = bahanService;
            this.userService = userService;
            this.detailPanduanService = detailPanduanService;
        }
        // GET: Panduan
        public async Task<ActionResult> Index(int id)
        {
            Panduan panduan = await panduanService.GetById(id);
            User user = await userService.GetById(panduan.IdUser);
            IndexViewModel indexViewModel = new IndexViewModel
            {
                IdPanduan = panduan.IdPanduan,
                NamaPanduan = panduan.NamaPanduan,
                UserName = user.Name,
                IdUser = user.IdUser
            };
            return View(indexViewModel);
        }
        public async Task<ActionResult> _TableBahan(int idPanduan)
        {
            Panduan panduan = await panduanService.GetById(idPanduan);
            TableBahanViewModel tableBahanViewModel = new TableBahanViewModel
            {
                IdPanduan = idPanduan,
                IdUser = panduan.IdUser
            };
            tableBahanViewModel.ListSelectedBahan = await bahanService.GetListByPanduan(idPanduan);
            await SetDropdownPartial(tableBahanViewModel);
            return PartialView(tableBahanViewModel);
        }
        public async Task<ActionResult> _Description(int idPanduan)
        {
            Panduan panduan = await panduanService.GetById(idPanduan);
            DescriptionViewModel descriptionViewModel = new DescriptionViewModel
            {
                IdPanduan = panduan.IdPanduan,
                DeskripsiPanduan = panduan.DeskripsiPanduan,
                IdUser = panduan.IdUser
            };
            return PartialView(descriptionViewModel);
        }
        public async Task<ActionResult> SaveDeskripsi(DescriptionViewModel descriptionViewModel)
        {
            try
            {
                Panduan panduan = await panduanService.GetById(descriptionViewModel.IdPanduan);
                if (ModelState.IsValid)
                {
                    panduan.DeskripsiPanduan = descriptionViewModel.DeskripsiPanduan;
                    int result = await panduanService.Update(panduan);
                    ViewBag.ShowAlertTableBahan = 1;
                    ViewBag.StatusAlert = "success";
                    ViewBag.MessageAlert = "Deskripsi Panduan berhasil diupdate";
                }
                descriptionViewModel.IdPanduan = panduan.IdPanduan;
                descriptionViewModel.DeskripsiPanduan = panduan.DeskripsiPanduan;
                descriptionViewModel.IdUser = panduan.IdUser;
                return PartialView("_Description",descriptionViewModel);
            }
            catch(Exception e)
            {
                ViewBag.ShowAlertTableBahan = 1;
                ViewBag.StatusAlert = "error";
                ViewBag.MessageAlert = e.Message;
                return PartialView("_Description",descriptionViewModel);
            }
        }
        private async Task SetDropdownPartial(TableBahanViewModel tableBahanViewModel)
        {
            tableBahanViewModel.ListKategoriBahan = await DropdownHelper.GetKategoriBahanDropdown();
            tableBahanViewModel.ListBahan = await DropdownHelper.GetAllBahanDropdown();
        }
        public async Task<ActionResult> _AddBahan(TableBahanViewModel tableBahanViewModel)
        {
            if (!ModelState.IsValid)
            {
                await SetDropdownPartial(tableBahanViewModel);
                tableBahanViewModel.ListSelectedBahan = await bahanService.GetListByPanduan(tableBahanViewModel.IdPanduan);
                return PartialView("_TableBahan", tableBahanViewModel);
            }
            ViewBag.ShowAlertTableBahan = 1;
            try
            {
                await detailPanduanService.Insert(new DetailPanduan
                {
                    IdBahan = tableBahanViewModel.IdBahan,
                    IdPanduan = tableBahanViewModel.IdPanduan
                });
                ViewBag.StatusAlert = "success";
                ViewBag.MessageAlert = "Data berhasil ditambah";
            }
            catch(Exception e)
            {
                ViewBag.StatusAlert = "error";
                ViewBag.MessageAlert = e.Message;
            }
            tableBahanViewModel = new TableBahanViewModel
            {
                IdPanduan = tableBahanViewModel.IdPanduan,
                IdUser = tableBahanViewModel.IdUser
            };
            tableBahanViewModel.ListSelectedBahan = await bahanService.GetListByPanduan(tableBahanViewModel.IdPanduan);
            await SetDropdownPartial(tableBahanViewModel);
            return PartialView("_TableBahan",tableBahanViewModel);
        }
        public async Task<ActionResult> _DeleteBahan(int idBahan,int idPanduan)
        {
            try
            {
                await detailPanduanService.Delete(new DetailPanduan
                {
                    IdBahan = idBahan,
                    IdPanduan = idPanduan
                });
                ListAlert.Add(new AlertMessage("success", "Data berhasil delete"));
                TempData["ListAlert"] = ListAlert;
            }
            catch(Exception e)
            {
                ListAlert.Add(new AlertMessage("error", e.Message));
                TempData["ListAlert"] = ListAlert;
            }
            return RedirectToAction("Index", new { id = idPanduan });
        }
        public async Task<JsonResult> _ChangeName(int id, string name)
        {
            try
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new Exception("Nama tidak boleh kosong");
                }
                Panduan panduan = await panduanService.GetById(id);
                panduan.NamaPanduan = name;
                int result = await panduanService.Update(panduan);
                return Json(new { status = "success", message = "", name = name });
            }
            catch (Exception e)
            {
                return Json(new { status = "error", message = e.Message });
            }
        }
    }
}