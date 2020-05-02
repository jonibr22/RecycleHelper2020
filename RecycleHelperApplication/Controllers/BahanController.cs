using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.BahanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class BahanController : BaseController
    {
        // GET: Bahan
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult GetAllBahan()
        //{

        //}
        private readonly IBahanService bahanService;
        private List<AlertMessage> ListAlert = new List<AlertMessage>();
        public BahanController(IBahanService bahanService)
        {
            this.bahanService = bahanService;
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
                if (indexViewModel.IdBahan == 0)
                {
                    // Insert
                    int id = await bahanService.Insert(new Model.Models.Bahan
                    {
                        IdBahan = indexViewModel.IdBahan,
                        NamaBahan = indexViewModel.NamaBahan,
                        IdKategoriBahan = indexViewModel.IdKategoriBahan,
                    });
                    ListAlert.Add(new AlertMessage("success", "Data berhasil disimpan"));
                    TempData["ListAlert"] = ListAlert;
                }
                else
                {
                    // Update
                    int id = await bahanService.Update(new Model.Models.Bahan
                    {
                        IdBahan = indexViewModel.IdBahan,
                        NamaBahan = indexViewModel.NamaBahan,
                        IdKategoriBahan = indexViewModel.IdKategoriBahan,
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
        public async Task<ActionResult> _BahanTable()
        {
            List<Bahan> listBahan = await bahanService.GetAllBahan();
            return PartialView(new BahanTableViewModel
            {
                ListBahan = listBahan
            });
        }
        public async Task<ActionResult> SelectBahanToEdit(int IdBahan)
        {
            string NamaBahan = "";
            int IdKategoriBahan = 0;
            if (IdBahan != 0)
            {
                Bahan bahan = await bahanService.GetById(IdBahan);
                NamaBahan = bahan.NamaBahan;
                IdKategoriBahan = bahan.IdKategoriBahan;
            }
            return Json(new { id = IdBahan, name = NamaBahan, idKategori = IdKategoriBahan });
        }
        public async Task<ActionResult> Add()
        {
            TempData["viewForm"] = 1;
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Delete(string hddSelectedIds)
        {
            try
            {
                int result = await bahanService.Delete(hddSelectedIds);
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