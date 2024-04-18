using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace RectangleApp
{
    public class LaserVector
    {
        public double X { get; set; } // Координата X
        public double Y { get; set; } // Координата Y
        public double AngleRadians { get; set; }
        public double Angle { get; set; }
        public double Length { get; set; }
        public double End_X_Position { get; set; }
        public double End_Y_Position { get; set; }

        public LaserVector(double x, double y, double angle, double length)
        {
            X = x;
            Y = y;
            AngleRadians = angle;
            Angle = AngleRadians * (180 / Math.PI);
            Length = length;
            End_X_Position = X + (Length * Math.Cos(AngleRadians));
            End_Y_Position = Y + (Length * Math.Sin(AngleRadians));
        }
    }
}
