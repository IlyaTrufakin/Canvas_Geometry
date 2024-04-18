using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace RectangleApp
{


    internal class Disk
    {
        public double X { get; set; } // Координата X
        public double Y { get; set; } // Координата Y
        public double R { get; set; }
        public LaserVector vector { get; set; }
        //     public double StartAngle { get; set; } // Угол наклона в градусах
        public double StartAngleRadians { get; set; }
        public Color Color { get; set; }
        public double Thickness { get; set; }
        public double PointNormalAngleRadians { get; set; }
        public double PointNormalAngle { get; set; }


        public Disk(double x, double y, double x_offset, double y_offset, LaserVector vector, Color color = default, double thickness = 1)
        {
            X = x;
            Y = y;
            R = Math.Sqrt(Math.Pow(vector.End_X_Position - x_offset, 2) + Math.Pow(vector.End_Y_Position - y_offset, 2));

            /*          EndAngleRadians = EndAngle * (Math.PI / 180.0);
                      ArcAngleRadians = (((EndAngleRadians - StartAngleRadians) + (2 * Math.PI)) % (2 * Math.PI));
                      ArcLength = R * ArcAngleRadians;*/
            Color = color != default ? color : Colors.DarkGray;
            Thickness = thickness;

            // Координаты центра окружности (точка A)
            /*          double centerX = 3.0;
                      double centerY = 4.0;*/

            // Координаты второй точки на окружности (точка B)
     /*       double pointX = 5.0;
            double pointY = 6.0;*/

            // Вычисляем разность координат точки B и центра окружности
            double diffX = vector.End_X_Position - x_offset;
            double diffY = vector.End_Y_Position - y_offset;

            // Вычисляем угол между лучом и отрезком, соединяющим центр и точку B
            double angle = Math.Atan2(diffY, diffX) * (180.0 / Math.PI);

            // Угол в радианах преобразуется в градусы с помощью множителя (180 / π)

            // Угол может быть отрицательным, поэтому приводим его к положительному значению
            if (angle < 0)
            {
                angle += 360.0;
            }
            StartAngleRadians = angle;
        }



        public void Draw(Canvas canvas, double x, double y, double lineLenght = 10)
        {
            Ellipse circle = new Ellipse
            {
                Width = R * 2,
                Height = R * 2,
                Stroke = new SolidColorBrush(Color),
                StrokeThickness = Thickness
            };

            Canvas.SetLeft(circle, x - R);
            Canvas.SetTop(circle, y - R);
            canvas.Children.Add(circle);

            LineGeometry line1 = new LineGeometry(new Point(-lineLenght, 0), new Point(lineLenght, 0));
            LineGeometry line2 = new LineGeometry(new Point(0, -lineLenght), new Point(0, lineLenght));
            GeometryGroup combinedGeometry = new GeometryGroup();
            combinedGeometry.Children.Add(line1);
            combinedGeometry.Children.Add(line2);
            Path line = new Path();
            line.Stroke = Brushes.Red; // Цвет линий
            line.StrokeThickness = 1; // Толщина линий
            line.Data = combinedGeometry; // Устанавливаем геометрию фигуры
            RotateTransform rotateTransform = new RotateTransform(0, 0, 0);// Создаем вращающее преобразование
            line.RenderTransform = rotateTransform;
            Canvas.SetLeft(line, x);
            Canvas.SetTop(line, y);
            canvas.Children.Add(line);
        }

    }
}
