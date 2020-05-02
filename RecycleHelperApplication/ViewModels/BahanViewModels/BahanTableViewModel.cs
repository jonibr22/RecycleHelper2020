using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.ViewModels.BahanViewModels
{
    public class BahanTableViewModel
    {
        public List<Bahan> ListBahan { get; set; }
        public List<SelectListItem> ListKategoriBahan { get; set; }
    }
}