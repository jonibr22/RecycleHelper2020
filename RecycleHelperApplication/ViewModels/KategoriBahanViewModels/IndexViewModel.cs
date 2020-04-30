using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.KategoriBahanViewModels
{
    public class IndexViewModel
    {
        public int IdKategoriBahan { get; set; }
        [Required(ErrorMessage = "Kategori Bahan harus diisi")]
        public string NamaKategoriBahan { get; set; }
    }
}