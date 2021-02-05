using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EChat.Models
{
    public class User
    {
        [DisplayName("ユーザーID")]
        [Required(AllowEmptyStrings = false ,ErrorMessage ="※必須入力です。")]
        public string UserId { get; private set; }

        [DisplayName("ユーサーネーム")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "※必須入力です。")]
        public string UserName { get; private set; }

        [DisplayName("パスワードタイプ")]
        public byte PasswordType { get; private set; }

        [DisplayName("パスワードソルト")]
        public string PasswordSalt { get; private set; }

        [DisplayName("パスワード")]
        public string Password { get; private set; }

        [DisplayName("管理者権限")]
        public bool IsAdministrator { get; private set; }

        public User(string userId, string userName, byte passwordType, string passwordSalt, string password, bool isAdministrator)
        {
            UserId = userId;
            UserName = userName;
            PasswordType = passwordType;
            PasswordSalt = passwordSalt;
            Password = password;
            IsAdministrator = isAdministrator;
        }
    }
}
