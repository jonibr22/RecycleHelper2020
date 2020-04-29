using Microsoft.Web.Mvc;
using RecycleHelperApplication.Model.Models;
using RecycleHelperApplication.Service.Helper;
using RecycleHelperApplication.Service.Modules.Web;
using RecycleHelperApplication.ViewModels.Home;
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
        public HomeController(IPanduanService panduanService,IUserService userService)
        {
            this.panduanService = panduanService;
            this.userService = userService;
        }
        private async Task SetDropdownIndex(IndexViewModel indexViewModel)
        {
            indexViewModel.ListKategoriBahan = await DropdownHelper.GetKategoriBahanDropdown();
            indexViewModel.ListBahan = await DropdownHelper.GetAllBahanDropdown();   
        }
        private async Task GetSearchResult(IndexViewModel indexViewModel)
        {
            //get panduan by selected bahan
            List<Panduan> listPanduan = await panduanService.GetListByMultipleBahan(indexViewModel.SelectedBahanIds);
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
            await GetSearchResult(indexViewModel);
            await SetDropdownIndex(indexViewModel);
            return View(indexViewModel);
        }
        public async Task<ActionResult> Search(IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid && indexViewModel.FromRemoveBahan == 0)
            {
                await GetSearchResult(indexViewModel);
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

            await GetSearchResult(indexViewModel);
            await SetDropdownIndex(indexViewModel);
            return View("Index", indexViewModel);
        }
    }
}