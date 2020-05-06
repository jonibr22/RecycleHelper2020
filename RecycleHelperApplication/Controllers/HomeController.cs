using Microsoft.Web.Mvc;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.HomeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPanduanService panduanService;
        private readonly IUserService userService;
        private readonly IBahanService bahanService;
        public HomeController(IPanduanService panduanService,IUserService userService,IBahanService bahanService)
        {
            this.panduanService = panduanService;
            this.userService = userService;
            this.bahanService = bahanService;
        }
        private async Task SetDropdownIndex(IndexViewModel indexViewModel)
        {
            indexViewModel.ListKategoriBahan = await DropdownHelper.GetKategoriBahanDropdown();
            indexViewModel.ListBahan = await DropdownHelper.GetAllBahanDropdown();   
        }
        private async Task GetSearchResult(IndexViewModel indexViewModel,string by = "")
        {
            //get panduan by selected bahan
            List<Panduan> listPanduan;
            if (by == "all")
            {
                listPanduan = await panduanService.GetAllPanduan();
            }
            else
            {
                listPanduan = await panduanService.GetListByMultipleBahan(indexViewModel.SelectedBahanIds);
            }
            for(int i = 0; i < listPanduan.Count; i++)
            {
                listPanduan[i].ListBahan = await bahanService.GetListByPanduan(listPanduan[i].IdPanduan);
            }
            indexViewModel.SearchResult = listPanduan;
            //get all user for mapping user id to user name
            List<User> listUser = await userService.GetAllUser();
            indexViewModel.ListUser = listUser;
        }
        // GET: Home
        public async Task<ActionResult> Index()
        {
            IndexViewModel indexViewModel = new IndexViewModel
            {
                SelectedBahanIds = "0"
            };
            await GetSearchResult(indexViewModel,"all");
            await SetDropdownIndex(indexViewModel);
            return View(indexViewModel);
        }
        public async Task<ActionResult> Search(IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid && indexViewModel.FromRemoveBahan == 0)
            {
                await GetSearchResult(indexViewModel,(indexViewModel.SelectedBahanIds == "0") ? "all" : "");
                await SetDropdownIndex(indexViewModel);
                return View("Index", indexViewModel);
            }
            ModelState.Clear();
            indexViewModel.FromRemoveBahan = 0;
            string selectedBahan = indexViewModel.SelectedBahanIds;
            selectedBahan += "," + indexViewModel.IdBahan.ToString();
            //remove duplicate
            selectedBahan = selectedBahan.Split(',').Distinct().Aggregate((a,b)=>a+","+b);
            indexViewModel.SelectedBahanIds = selectedBahan;
            await GetSearchResult(indexViewModel,(selectedBahan=="0")?"all":"");
            await SetDropdownIndex(indexViewModel);
            return View("Index", indexViewModel);
        }
    }
}