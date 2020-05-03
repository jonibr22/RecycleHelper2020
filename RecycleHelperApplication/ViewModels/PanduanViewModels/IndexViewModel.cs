using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.PanduanViewModels
{
    public class IndexViewModel
    {
        public int IdPanduan { get; set; }
        [Required(ErrorMessage = "Nama Panduan harus diisi")]
        public string NamaPanduan { get; set; }
        [Required(ErrorMessage = "Deskripsi Panduan harus diisi")]
        public string DeskripsiPanduan { get; set; }
        public int IdUser { get; set; }
        public string NamaBahan { get; set; }
        //[Required(ErrorMessage = "Id Bahan harus diisi")]
        public int IdBahan { get; set; }
    }
}