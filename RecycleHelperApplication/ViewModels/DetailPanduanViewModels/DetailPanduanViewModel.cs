using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.ViewModels.DetailPanduanViewModels
{
    public class DetailPanduanViewModel
    {
        public int IdPanduan { get; set; }
        public int IdUser { get; set; }
        public string NamaPanduan { get; set; }
        public string DeskripsiPanduan { get; set; }
        public List<Bahan> ListBahan { get; set; }
        public string UserName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Kategori Bahan harus dipilih")]
        public int IdKategoriBahan { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Bahan harus dipilih")]
        public int IdBahan { get; set; }

        public List<SelectListItem> ListPilihanBahan { get; set; }
        public List<SelectListItem> ListKategoriBahan { get; set; }

        public string SelectedBahanIds { get; set; }

        public int FromRemoveBahan { get; set; }
    }
}