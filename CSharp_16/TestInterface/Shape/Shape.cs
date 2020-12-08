using System;
using System.Collections.Generic;
using System.Text;

namespace TestInterface
{
    abstract class Shape : IName
    {
        public abstract string Name { get; }

        public abstract double CalculateArea();        

        public override string ToString()
        {
            return $"[{Name}] : {CalculateArea()}";
        }
    }
}
