using System;
using System.Collections.Generic;
using System.Text;

namespace Shape
{
    class Circle : Shape
    {
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
            return base.Area = Radius * Radius * 3.14;
        }
    }
}
