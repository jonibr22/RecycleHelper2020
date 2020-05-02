using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.UserViewModels
{
    public class PasswordManagementViewModel
    {
        public int IdUser { get; set; }
        [Required(ErrorMessage = "Field Old Password harus diisi")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Field New Password harus diisi")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Field Confirm New Password harus diisi")]
        public string ConfirmNewPassword { get; set; }
    }
}