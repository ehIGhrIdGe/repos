using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAddressBook.Exceptions
{
    class Exceptions
    {
        public string ErrorMsg { get; set; }

        public Exceptions(string errorMsg)
        {
            ErrorMsg = errorMsg;
        }
    }
}
