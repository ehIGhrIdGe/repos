using System;
using System.Collections.Generic;
using System.Text;

namespace Namespace.Test
{
    class Test
    {
        public string Temp { get; set; }

        public enum aaa
        {
            a,b,c,d
        }

        public Test()
        {
            var sun = TestEnum.DayOfWeek.Sun;
        }
    }
}
