using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoeShop.Areas.Admin.Models
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Vui lòng nhập vào tên đăng nhập!")]
		public string Username { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập vào mật khẩu!")]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}