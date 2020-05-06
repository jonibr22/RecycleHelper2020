using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.DetailPanduanViewModels
{
    public class DescriptionViewModel
    {
        public int IdPanduan { get; set; }
        [Required(ErrorMessage = "Deskripsi Panduan wajib diisi")]
        public string DeskripsiPanduan { get; set; }
        public int IdUser { get; set; }
    }
}