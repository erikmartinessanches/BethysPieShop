using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BethysPieShop.ViewModels
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; }

        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
