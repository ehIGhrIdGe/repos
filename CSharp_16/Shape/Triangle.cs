using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Triangle : Shape
    {
        public override string Name { get { return this.GetType().Name; } }
        public Triangle()
        {
            Width = 0;
            Height = 0;
        }

        public Triangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()
        {
            return base.Area = Width * Height / 2;
        }
    }
}
