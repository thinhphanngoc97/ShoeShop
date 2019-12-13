using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeShop.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập vào email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập vào mật khẩu!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}