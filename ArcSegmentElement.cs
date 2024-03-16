using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RectangleApp
{
    internal class ArcSegmentElement
    {
        public double X { get; set; } // Координата X
        public double Y { get; set; } // Координата Y
        public double R { get; set; }
        public double StartAngle { get; set; } // Угол наклона в градусах
        public double EndAngle { get; set; } // Угол наклона в градусах
        public double StartAngleRadians { get; set; }
        public double EndAngleRadians { get; set; }
        public double ArcAngleRadians { get; set; }
        public double ArcLength { get; set; }

        public Color Color { get; set; }
        public double Thickness { get; set; }
        public double PointPositionFromStart { get; set; }
        public double PointX_Position { get; set; }
        public double PointY_Position { get; set; }
        public double PointNormalAngle { get; set; }

        public ArcSegmentElement(double x, double y, double r, double startAngle, double endAngle, Color color = default, double thickness = 1)
        {
            if (r <= 0)
            {
                throw new ArgumentOutOfRangeException("Radius must be positive");
            }

            if (startAngle < 0 || startAngle > 360)
            {
                throw new ArgumentOutOfRangeException("Start angle must be between 0 and 360 degrees");
            }

            if (endAngle < 0 || endAngle > 360)
            {
                throw new ArgumentOutOfRangeException("End angle must be between 0 and 360 degrees");
            }

            if (endAngle < startAngle)
            {
                throw new ArgumentOutOfRangeException("End angle must be greater than or equal to start angle");
            }

            X = x;
            Y = y;
            R = r;
            StartAngle = startAngle;
            EndAngle = endAngle;
            StartAngleRadians = StartAngle * Math.PI / 180.0;
            EndAngleRadians = EndAngle * Math.PI / 180.0;
            ArcAngleRadians = Math.Abs(EndAngleRadians - StartAngleRadians);
            ArcLength = R * ArcAngleRadians;
            Color = color != default ? color : Colors.DarkGray;
            Thickness = thickness;
        }

        // Метод для вычисления координат точки на дуге
        public void CalculatePointPosition()
        {
            // Вычисление координат точки на дуге
            double angleToPoint = StartAngleRadians + ArcAngleRadians * (PointPositionFromStart / (2 * Math.PI * R));
            PointX_Position = R * Math.Cos(angleToPoint);
            PointY_Position = R * Math.Sin(angleToPoint);
        }

        // Метод для вычисления угла нормали
        public void CalculatePointNormalAngle()
        {
            // Вычисление угла нормали
            PointNormalAngle = StartAngleRadians + ArcAngleRadians * (PointPositionFromStart / (2 * Math.PI * R));
        }
    }

}
