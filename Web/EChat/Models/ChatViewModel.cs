using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EChat.Models
{
    public class ChatViewModel
    {
        public Message Message { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
