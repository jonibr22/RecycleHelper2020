using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.BahanViewModels
{
    public class IndexViewModel
    {
        public int IdBahan { get; set; }
        [Required(ErrorMessage = "Nama Bahan harus diisi")]
        public string NamaBahan { get; set; }
        [Required(ErrorMessage = "Id Kategori Bahan harus diisi")]
        public int IdKategoriBahan { get; set; }
    }
}