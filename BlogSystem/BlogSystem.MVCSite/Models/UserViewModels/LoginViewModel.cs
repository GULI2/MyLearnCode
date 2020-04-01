using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="登录账号（邮箱）")]
        public string LoginName { get; set; }

        [Required]
        [StringLength(50,MinimumLength =6)]
        [Display(Name = "密码")]
        [DataType(dataType:DataType.Password)]
        public string LoginPwd { get; set; }

        //是否记住
        [Display(Name = "是否记住账号")]
        public bool RememberMe { get; set; }
    } 
}