using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    abstract class Shape
    {
        public abstract string Name { get; }

        public abstract double CalculateArea();        

        public override string ToString()
        {
            return $"[{Name}] : {CalculateArea()}";
        }
    }
}
