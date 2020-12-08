using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Shape
    {
        public string Name { get { return this.GetType().Name; } }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Radius { get; set; }
        public double Area { get; set; }

        public virtual double CalculateArea ()
        {
            return Area = 0;
        }

        public override string ToString()
        {
            return "[" + Name + "] : " + CalculateArea();
        }
    }
}
