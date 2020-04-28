using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace RecycleHelperApplication.ViewModels.Home
{
    public class IndexViewModel
    {
        [Range(1,int.MaxValue,ErrorMessage = "Kategori Bahan harus dipilih")]
        public int IdKategoriBahan { get; set; }
        [Range(1,int.MaxValue,ErrorMessage = "Bahan harus dipilih")]
        public int IdBahan { get; set; }

        public List<SelectListItem> ListBahan { get; set; }
        public List<SelectListItem> ListKategoriBahan { get; set; }

        public string SelectedBahanIds { get; set; }

        public List<Panduan> SearchResult { get; set; }
        public List<User> ListUser { get; set; }
    }
}