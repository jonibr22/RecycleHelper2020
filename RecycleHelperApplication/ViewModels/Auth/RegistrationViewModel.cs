using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecycleHelperApplication.ViewModels.Auth
{
    public class RegistrationViewModel
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Panjang Username tidak boleh melebihi 255")]
        public string Username { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Panjang Password tidak boleh melebihi 255")]
        public string Password { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Panjang Nama tidak boleh melebihi 255")]
        public string Name { get; set; }
    }
}