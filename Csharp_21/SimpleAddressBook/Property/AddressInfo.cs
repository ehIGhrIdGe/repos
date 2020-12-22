using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAddressBook.Property
{
    class AddressInfo
    {
        public string Name { get; private set; }
        public string Age { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }

        public AddressInfo()
        {
            Name = "No Name";
            Age = "";
            PhoneNumber = "";
            Address = "";
        }

        public AddressInfo(string name, string age, string phoneNumber, string address)
        {
            Name = name;
            Age = age;
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}
