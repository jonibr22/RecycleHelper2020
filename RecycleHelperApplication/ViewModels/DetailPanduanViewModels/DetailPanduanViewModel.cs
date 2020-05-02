using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.DetailPanduanViewModels
{
    public class DetailPanduanViewModel
    {
        public int IdPanduan { get; set; }
        public string NamaPanduan { get; set; }
        public string DeskripsiPanduan { get; set; }
        public List<Bahan> ListBahan { get; set; }
        public string UserName { get; set; }
    }
}