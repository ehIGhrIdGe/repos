using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EChat.Models
{
    public class Messages
    {
        public int Id { get; set; }

        public DateTime PostAt { get; set; }

        public string Message { get; set; }

        public string Name { get; set; }

        public Messages(int id, DateTime postAt, string message, string name)
        {
            Id = id;
            PostAt = postAt;
            Message = message;
            Name = name;
        }
    }
}
