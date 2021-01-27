using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EChat.Models
{
    public class Message
    {
        public int Id { get; set; }

        public DateTime PostAt { get; set; }

        public string Msg { get; set; }

        public string Name { get; set; }

        public Message(int id, DateTime postAt, string msg, string name)
        {
            Id = id;
            PostAt = postAt;
            Msg = msg;
            Name = name;
        }

        public Message(string msg)
        {
            Msg = msg;
        }
    }
}
