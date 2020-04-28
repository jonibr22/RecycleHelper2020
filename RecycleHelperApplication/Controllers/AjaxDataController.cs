using RecycleHelperApplication.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.Controllers
{
    public class AjaxDataController : BaseController
    {
        public async Task<ActionResult> GetListBahanByKategori(int IDKategoriBahan)
        {
            List<SelectListItem> ListBahan = await DropdownHelper.GetBahanByKategoriDropdown(IDKategoriBahan);
            return Json(ListBahan);
        }
    }
}