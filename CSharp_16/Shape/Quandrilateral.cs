using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Quandrilateral : Shape
    {
        public Quandrilateral()
        {
            Width = 0;
            Height = 0;
        }

        public Quandrilateral(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public override double CalculateArea()
        {
            return base.Area = Width * Height;
        }
    }
}
