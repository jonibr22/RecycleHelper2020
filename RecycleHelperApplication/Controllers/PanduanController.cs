using RecycleHelperApplication.Model.Base;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.PanduanViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class PanduanController : BaseController
    {
        // GET: Panduan
        //public ActionResult Index()
        //{
        //    return View();
        //}
        private readonly IPanduanService panduanService;
        private List<AlertMessage> ListAlert = new List<AlertMessage>();
        public PanduanController(IPanduanService panduanService)
        {
            this.panduanService = panduanService;
        }
        // GET: Bahan
        public ActionResult Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.IdUser = Convert.ToInt32(Session[SessionEnum.USER_ID]);
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
            return PartialView(new PanduanTableViewModel
            {
                ListPanduan = listPanduan
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
        public async Task<ActionResult> Delete(string selectedId)
        {
            try
            {
                int result = await panduanService.Delete(selectedId);
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