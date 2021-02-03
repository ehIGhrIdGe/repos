using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EChat.Models
{
    public class UserInfo
    {
        [DisplayName("User Id")]
        public string UserId { get; private set; }

        [DisplayName("User Name")]
        public string UserName { get; private set; }

        [DisplayName("Password Type")]
        public byte PasswordType { get; private set; }

        [DisplayName("Password Salt")]
        public string PasswordSalt { get; private set; }

        [DisplayName("Password")]
        public string Password { get; private set; }

        [DisplayName("Is Administrator")]
        public bool IsAdministrator { get; private set; }

        public UserInfo(string userId, string userName, byte passwordType, string passwordSalt, string password, bool isAdministrator)
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
