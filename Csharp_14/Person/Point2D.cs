using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    class Point2D
    {
        public int X { get; }
        public int Y { get; }

        public Point2D(int x, int y) { X = x; Y = y; }

        public override bool Equals(object obj)
        {
            if(obj is Point2D)
            {
                var temp = (Point2D)obj;
                return X == temp.X && Y == temp.Y;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            var cc = X.GetHashCode() * 444 ^ Y.GetHashCode();
            return cc;
        }
    }
}
