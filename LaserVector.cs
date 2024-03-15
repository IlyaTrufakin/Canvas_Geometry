using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleApp
{
    public class LaserVector
    {
        public double X { get; set; } // Координата X
        public double Y { get; set; } // Координата Y
        public double Angle { get; set; } // Угол наклона в градусах

        public LaserVector(double x, double y, double angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }
    }
}
