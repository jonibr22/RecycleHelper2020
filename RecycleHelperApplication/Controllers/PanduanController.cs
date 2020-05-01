using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Modules.WebAPI;
using RecycleHelperApplication.ViewModels.PanduanViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class PanduanController : Controller
    {
        // GET: Panduan
        //public ActionResult Index()
        //{
        //    return View();
        //}
        private readonly IPanduanApiService panduanService;
        private readonly IBahanApiService bahanService;
        private readonly IDetailPanduanApiService detailPanduanService;
        private List<AlertMessage> ListAlert = new List<AlertMessage>();
        public PanduanController(IPanduanApiService panduanService)
        {
            this.panduanService = panduanService;
        }
        public PanduanController(IBahanApiService bahanService)
        {
            this.bahanService = bahanService;
        }
        public PanduanController(IDetailPanduanApiService detailPanduanService)
        {
            this.detailPanduanService = detailPanduanService;
        }
        // GET: Bahan
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            return View(indexViewModel);
        }
        public async Task<ActionResult> Save(IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["viewForm"] = 1;
                return View("Index", indexViewModel);
            }
            try
            {
                if (indexViewModel.IdPanduan == 0)
                {
                    // Insert
                    int id = await panduanService.Insert(new Model.Models.Panduan
                    {
                        IdPanduan = indexViewModel.IdPanduan,
                        NamaPanduan = indexViewModel.NamaPanduan,
                        DeskripsiPanduan = indexViewModel.DeskripsiPanduan,
                        IdUser = indexViewModel.IdUser,
                    });
                    //int idDetail = await detailPanduanService.Insert(new Model.Models.DetailPanduan
                    //{

                    //});
                    var idBahan = await detailPanduanService.Insert(new Model.Models.DetailPanduan
                    {
                       IdBahan = indexViewModel.IdBahan
                    });
                    ListAlert.Add(new AlertMessage("success", "Data berhasil disimpan"));
                    TempData["ListAlert"] = ListAlert;
                }
                else
                {
                    // Update
                    int id = await panduanService.Update(new Model.Models.Panduan
                    {
                        IdPanduan = indexViewModel.IdPanduan,
                        NamaPanduan = indexViewModel.NamaPanduan,
                        DeskripsiPanduan = indexViewModel.DeskripsiPanduan,
                        IdUser = indexViewModel.IdUser,
                    });
                    var idBahan = await detailPanduanService.Insert(new Model.Models.DetailPanduan
                    {
                        IdBahan = indexViewModel.IdBahan
                    });
                    ListAlert.Add(new AlertMessage("success", "Data berhasil diubah"));
                    TempData["ListAlert"] = ListAlert;
                }
            }
            catch (Exception e)
            {
                ListAlert.Add(new AlertMessage("error", e.Message));
                TempData["ListAlert"] = ListAlert;
                return View("Index", indexViewModel);
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> _PanduanTable()
        {
            List<Panduan> listPanduan = await panduanService.GetAllPanduan();
            List<Bahan> listBahan = await bahanService.GetAllBahan();
            return PartialView(new PanduanTableViewModel
            {
                ListPanduan = listPanduan,
                ListBahan = listBahan
            });
        }
        public async Task<ActionResult> SelectPanduanToEdit(int IdPanduan)
        {
            string NamaPanduan = "";
            string DeskripsiPanduan = "";
            int IdUser = 0;
            if (IdPanduan != 0)
            {
                Panduan panduan = await panduanService.GetById(IdPanduan);
                NamaPanduan = panduan.NamaPanduan;
                DeskripsiPanduan = panduan.DeskripsiPanduan;
                IdUser = panduan.IdUser;
            }
            return Json(new { id = IdPanduan, name = NamaPanduan, deskripsi = DeskripsiPanduan, idUser = IdUser });
        }
        public async Task<ActionResult> Add()
        {
            TempData["viewForm"] = 1;
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(int selectedId)
        {
            try
            {
                var result = await panduanService.Delete(selectedId);
                ListAlert.Add(new AlertMessage("success", "Data berhasil dihapus"));
                TempData["ListAlert"] = ListAlert;
            }
            catch (Exception e)
            {
                ListAlert.Add(new AlertMessage("error", e.Message));
                TempData["ListAlert"] = ListAlert;
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> DeleteMultiple(string hddSelectedId)
        {
            try
            {
                var result = await panduanService.DeleteMultiple(hddSelectedId);
                ListAlert.Add(new AlertMessage("success", "Data berhasil dihapus"));
                TempData["ListAlert"] = ListAlert;
            }
            catch (Exception e)
            {
                ListAlert.Add(new AlertMessage("error", e.Message));
                TempData["ListAlert"] = ListAlert;
            }
            return RedirectToAction("Index");
        }
    }
}