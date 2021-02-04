using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EChat.Models
{
    public class LoginViewModel
    {
        [DisplayName("ユーザーID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "※必須入力です。")]
        public string UserId { get; set; }

        [DisplayName("パスワード")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "※必須入力です。")]
        public string Password { get; set; }
    }
}
