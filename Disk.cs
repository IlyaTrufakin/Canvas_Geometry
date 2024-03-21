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
        public Disk() { }



        private Path CreateCrossLines(double x, double y, double length, double angle)
        {
            double halfLength = length / 5;
            LineGeometry line1 = new LineGeometry(new Point(-halfLength, 0), new Point(halfLength, 0));
            LineGeometry line2 = new LineGeometry(new Point(0, -length), new Point(0, halfLength));
            GeometryGroup combinedGeometry = new GeometryGroup();
            combinedGeometry.Children.Add(line1);
            combinedGeometry.Children.Add(line2);
            Path path = new Path();
            path.Stroke = Brushes.Red; // Цвет линий
            path.StrokeThickness = 1; // Толщина линий
            path.Data = combinedGeometry; // Устанавливаем геометрию фигуры
            RotateTransform rotateTransform = new RotateTransform(angle, 0, 0);// Создаем вращающее преобразование
            path.RenderTransform = rotateTransform;
            Canvas.SetLeft(path, x);
            Canvas.SetTop(path, y);
            return path;
        }

        private Ellipse CreateCircle(double circleCenterX, double circleCenterY, double point_X, double point_Y)
        {
            // Создание окружности
            double circleRadius = Math.Sqrt(Math.Pow(point_X + CircleCenterX_offset, 2) + Math.Pow(point_Y + CircleCenterY_offset, 2));
            Ellipse circle = new Ellipse
            {
                Width = circleRadius * 2,
                Height = circleRadius * 2,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };

            Canvas.SetLeft(circle, circleCenterX - circleRadius);
            Canvas.SetTop(circle, circleCenterY - circleRadius);
            return circle;
        }
    }
}
