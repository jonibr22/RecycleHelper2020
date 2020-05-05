using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.AuthViewModels;
using RecycleHelperApplication.ViewModels.DetailPanduanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class DetailPanduanController : Controller
    {
        private readonly IPanduanService panduanService;
        private readonly IBahanService bahanService;
        private readonly IUserService userService;
        private List<AlertMessage> ListAlert = new List<AlertMessage>();
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
            List<Bahan> ListBahan = await bahanService.GetListByPanduan(IdPanduan);
            User user = await userService.GetById(panduan.IdUser);
            DetailPanduanViewModel test = new DetailPanduanViewModel
            {
                IdPanduan = panduan.IdPanduan,
                ListBahan = ListBahan,
                NamaPanduan = panduan.NamaPanduan,
                DeskripsiPanduan = panduan.DeskripsiPanduan,
                UserName = user.Name,
                IdUser = user.IdUser
            };
            await SetDropdownIndex(test);
            return View(test);
        }

        private async Task SetDropdownIndex(DetailPanduanViewModel detailPanduanViewModel)
        {
            detailPanduanViewModel.ListKategoriBahan = await DropdownHelper.GetKategoriBahanDropdown();
            detailPanduanViewModel.ListPilihanBahan = await DropdownHelper.GetAllBahanDropdown();
        }
        public async Task<ActionResult> Search(DetailPanduanViewModel detailPanduanViewModel)
        {
            if (!ModelState.IsValid && detailPanduanViewModel.FromRemoveBahan == 0)
            {
                await SetDropdownIndex(detailPanduanViewModel);
                return View("Index", detailPanduanViewModel);
            }
            ModelState.Clear();
            detailPanduanViewModel.FromRemoveBahan = 0;
            string selectedBahan = detailPanduanViewModel.SelectedBahanIds;
            selectedBahan += "," + detailPanduanViewModel.IdBahan.ToString();
            //remove duplicate
            selectedBahan = selectedBahan.Split(',').Distinct().Aggregate((a, b) => a + "," + b);
            detailPanduanViewModel.SelectedBahanIds = selectedBahan;

            await SetDropdownIndex(detailPanduanViewModel);
            return View("Index", detailPanduanViewModel);
        }

        public async Task<ActionResult> Save(DetailPanduanViewModel detailPanduanViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["viewForm"] = 1;
                return View("Index", detailPanduanViewModel);
            }
            try
            {
                if (detailPanduanViewModel.IdPanduan == 0)
                {
                    // Insert
                    int id = await panduanService.Insert(new Model.Models.Panduan
                    {
                        IdPanduan = detailPanduanViewModel.IdPanduan,
                        NamaPanduan = detailPanduanViewModel.NamaPanduan,
                        DeskripsiPanduan = detailPanduanViewModel.DeskripsiPanduan,
                        IdUser = detailPanduanViewModel.IdUser,
                        ListBahan = detailPanduanViewModel.ListBahan
                    });

                    ListAlert.Add(new AlertMessage("success", "Data berhasil disimpan"));
                    TempData["ListAlert"] = ListAlert;
                }
                else
                {
                    // Update
                    int id = await panduanService.Update(new Model.Models.Panduan
                    {
                        IdPanduan = detailPanduanViewModel.IdPanduan,
                        NamaPanduan = detailPanduanViewModel.NamaPanduan,
                        DeskripsiPanduan = detailPanduanViewModel.DeskripsiPanduan,
                        IdUser = detailPanduanViewModel.IdUser,
                        ListBahan = detailPanduanViewModel.ListBahan
                    });

                    ListAlert.Add(new AlertMessage("success", "Data berhasil diubah"));
                    TempData["ListAlert"] = ListAlert;
                }
            }
            catch (Exception e)
            {
                ListAlert.Add(new AlertMessage("error", e.Message));
                TempData["ListAlert"] = ListAlert;
                return View("Index", detailPanduanViewModel);
            }
            return RedirectToAction("Index");
        }

    }
}