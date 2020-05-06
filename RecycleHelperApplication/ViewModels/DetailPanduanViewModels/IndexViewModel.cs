using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecycleHelperApplication.ViewModels.DetailPanduanViewModels
{
    public class IndexViewModel
    {
        public int IdPanduan { get; set; }
        public int IdUser { get; set; }
        public string NamaPanduan { get; set; }
        public string UserName { get; set; }
    }
}