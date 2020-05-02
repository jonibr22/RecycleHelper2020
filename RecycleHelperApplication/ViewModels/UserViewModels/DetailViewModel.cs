using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.UserViewModels
{
    public class DetailViewModel
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Upload)]
        public string File { get; set; }
        public string PhotoUrl { get; set; }
    }
}