using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EChat.Models
{
    public class UserListViewModel
    {
        public User User { get; set; }
        public List<User> UserList { get; set; }
    }
}
