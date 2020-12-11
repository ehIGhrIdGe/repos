using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAddressBook.Property
{
    class TextInfo
    {
        public string Value { get; set; }
        public string[] Values { get; set; }

        public TextInfo(string value)
        {
            Value = value;
        }
    }
}
