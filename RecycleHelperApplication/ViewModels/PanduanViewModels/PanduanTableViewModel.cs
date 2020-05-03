using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.PanduanViewModels
{
    public class PanduanTableViewModel
    {
        public List<Panduan> ListPanduan { get; set; }
        public List<DetailPanduan> ListDetailPanduan { get; set; }
        public List<Bahan> ListBahan { get; set; }
    }
}