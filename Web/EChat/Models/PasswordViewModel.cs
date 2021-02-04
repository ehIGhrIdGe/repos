using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EChat.Models
{
    public class PasswordViewModel
    {
        [DisplayName("古いパスワード")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "※必須入力です。")]
        public string OldPassword { get; set; }

        [DisplayName("新しいパスワード")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "※必須入力です。")]
        public string NewPassword { get; set; }

        [DisplayName("確認用パスワード")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "※必須入力です。")]
        public string ConfirmPassword { get; set; }
    }
}
