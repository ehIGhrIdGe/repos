using System;
using System.Collections.Generic;
using System.Text;

namespace Square
{
    class Square
    {
        public double Width { get; set; }

        public Square()
        {
            Width = 0;
        }

        public Square(double width)
        {
            Width = width;
        }

        public double CalculationArea()
        {
            var result = Width * Width;

            return result;
        }

    }
}
