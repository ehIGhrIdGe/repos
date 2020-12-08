using System;
using System.Collections.Generic;
using System.Text;

namespace TestInterface
{
    class Quandrilateral : Shape
    {
        public override string Name { get { return this.GetType().Name; } }
        public double Width { get; set; }
        public double Height { get; set; }

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
            return Width * Height;
        }
    }
}
