﻿using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.PanduanViewModels
{
    public class PanduanTableViewModel
    {
        public List<Panduan> ListPanduan { get; set; }
        public int IdUser { get; set; }
    }
}