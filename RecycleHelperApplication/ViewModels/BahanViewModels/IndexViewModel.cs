using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.ViewModels.BahanViewModels
{
    public class IndexViewModel
    {
        public int IdBahan { get; set; }
        [Required(ErrorMessage = "Nama Bahan harus diisi")]
        public string NamaBahan { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Kategori Bahan harus dipilih")]
        public int IdKategoriBahan { get; set; }

        public List<SelectListItem> ListKategoriBahan { get; set; }
    }
}