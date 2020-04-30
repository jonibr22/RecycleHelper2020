using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.KategoriBahanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class KategoriBahanController : BaseController
    {
        private readonly IKategoriBahanService kategoriBahanService;
        private List<AlertMessage> ListAlert = new List<AlertMessage>();
        public KategoriBahanController(IKategoriBahanService kategoriBahanService)
        {
            this.kategoriBahanService = kategoriBahanService;
        }
        // GET: KategoriBahan
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            return View(indexViewModel);
        }
        public async Task<ActionResult> Save(IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", indexViewModel);
            }
            try
            {
                if (indexViewModel.IdKategoriBahan == 0)
                {
                    // Insert
                    int id = await kategoriBahanService.Insert(new Model.Models.KategoriBahan
                    {
                        IdKategoriBahan = indexViewModel.IdKategoriBahan,
                        NamaKategoriBahan = indexViewModel.NamaKategoriBahan
                    });
                    ListAlert.Add(new AlertMessage("success", "Data berhasil disimpan"));
                    TempData["ListAlert"] = ListAlert;
                }
                else
                {
                    // Update
                    int id = await kategoriBahanService.Update(new Model.Models.KategoriBahan
                    {
                        IdKategoriBahan = indexViewModel.IdKategoriBahan,
                        NamaKategoriBahan = indexViewModel.NamaKategoriBahan
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
        public async Task<ActionResult> _KategoriBahanTable()
        {
            List<KategoriBahan> listKategoriBahan = await kategoriBahanService.GetAllKategoriBahan();
            return PartialView(new KategoriBahanTableViewModel
            {
                ListKategoriBahan = listKategoriBahan
            });
        }
        public async Task<ActionResult> SelectKategoriBahanToEdit(int IdKategoriBahan)
        {
            string NamaKategoriBahan = "";
            if (IdKategoriBahan != 0)
            {
                KategoriBahan kategoriBahan = await kategoriBahanService.GetById(IdKategoriBahan);
                NamaKategoriBahan = kategoriBahan.NamaKategoriBahan;
            }
            return Json(new { id = IdKategoriBahan, name = NamaKategoriBahan });
        }
    }
}