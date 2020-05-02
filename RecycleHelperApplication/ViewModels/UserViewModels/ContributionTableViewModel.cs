using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.UserViewModels
{
    public class ContributionTableViewModel
    {
        public List<Panduan> ListPanduan { get; set; }
        public Dictionary<int,List<Bahan>> ListBahanByPanduan { get; set; }
    }
}