using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    abstract class Shape
    {
        public abstract string Name { get; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Radius { get; set; }
        public double Area { get; set; }

        public abstract double CalculateArea();
        

        public override string ToString()
        {
            return "[" + Name + "] : " + CalculateArea();
        }
    }
}
