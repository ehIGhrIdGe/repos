﻿using System;
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
        //public Dictionary<int,string[]> AdsDictionary { get; private set; }
        public List<string[]> AddressList { get; private set; }

        public AddressInfo()
        {
            Name = "No Name";
            Age = "";
            PhoneNumber = "";
            Address = "";
            AddressList = new List<string[]>();
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
