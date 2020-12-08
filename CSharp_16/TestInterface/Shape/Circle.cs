using System;
using System.Collections.Generic;
using System.Text;

namespace TestInterface
{
    class Circle : Shape
    {
        public override string Name { get { return this.GetType().Name; } }
        public double Radius { get; set; }

        public Circle()
        {
            Radius = 0;
        }

        public Circle(double radisu)
        {
            Radius = radisu;
        }

        public override double CalculateArea()
        {
            return Radius * Radius * 3.14;
        }
    }
}
