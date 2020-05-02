using RecycleHelperApplication.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.UserViewModels
{
    public class IndexViewModel
    {
        public List<User> ListUser { get; set; }
        public List<Role> ListRole { get; set; }
    }
}