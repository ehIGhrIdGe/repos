using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAddressBook.Property
{
    class AddressInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public AddressInfo()
        {
            Name = "No Name";
            Age = -1;
            PhoneNumber = "";
            Address = "";
        }

        public AddressInfo(string name, int age, string phoneNumber, string address)
        {
            Name = name;
            Age = age;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
