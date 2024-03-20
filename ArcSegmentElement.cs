using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Path = System.Windows.Shapes.Path;

namespace RectangleApp
{
    public class ArcSegmentElement
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
        public double PointNormalAngleRadians { get; set; }
        public double PointNormalAngle { get; set; }
        public LaserVector vector { get; set; }

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

            X = x;
            Y = y;
            R = r;
            StartAngle = startAngle;
            EndAngle = endAngle;
            StartAngleRadians = StartAngle * (Math.PI / 180.0);
            EndAngleRadians = EndAngle * (Math.PI / 180.0);
            ArcAngleRadians = (((EndAngleRadians - StartAngleRadians) + (2 * Math.PI)) % (2 * Math.PI));
            ArcLength = R * ArcAngleRadians;
            Color = color != default ? color : Colors.DarkGray;
            Thickness = thickness;
        }

        // Метод для вычисления координат точки на дуге
        public LaserVector CalculatePointPosition(double pointPositionFromStart)
        {
            PointPositionFromStart = pointPositionFromStart;
            // Вычисление координат точки на дуге
            PointNormalAngleRadians = StartAngleRadians + (ArcAngleRadians * (PointPositionFromStart / ArcLength));
            //PointNormalAngle = PointNormalAngleRadians * (2 * Math.PI);
            PointNormalAngle = PointNormalAngleRadians * (180 / Math.PI);
            PointX_Position = R * Math.Cos(PointNormalAngleRadians);
            PointY_Position = R * Math.Sin(PointNormalAngleRadians);
            return new LaserVector(PointX_Position, PointY_Position, PointNormalAngleRadians);
        }

        // Метод для вычисления угла нормали
        public void CalculatePointNormalAngle(double pointPositionFromStart)
        {
            PointPositionFromStart = pointPositionFromStart;
            // Вычисление угла нормали
            PointNormalAngleRadians = StartAngleRadians + (ArcAngleRadians * (PointPositionFromStart / ArcLength));
            PointNormalAngle = PointNormalAngleRadians * (180 / Math.PI);
        }

        public void Draw(Canvas canvas, int elementNumder, double totalLenght = 0)
        {
            // Создание Path
            Path path = new Path();
            path.Stroke = new SolidColorBrush(Color);
            path.StrokeThickness = Thickness;

            // Вычисление координат начальной и конечной точек дуги
            double startX = X + R * Math.Cos(StartAngleRadians);
            double startY = Y + R * Math.Sin(StartAngleRadians);
            double endX = X + R * Math.Cos(EndAngleRadians);
            double endY = Y + R * Math.Sin(EndAngleRadians);

            // Создание геометрии дуги
            PathGeometry geometry = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(startX, startY); // Начальная точка дуги
            ArcSegment arc = new ArcSegment();
            arc.Point = new Point(endX, endY); // Конечная точка дуги
            arc.Size = new Size(R, R);
            arc.SweepDirection = SweepDirection.Clockwise; // Направление дуги (по часовой стрелке)
            arc.IsLargeArc = false; //(ArcAngleRadians > Math.PI); // Определение большой дуги
            figure.Segments.Add(arc);
            geometry.Figures.Add(figure);

            // Установка геометрии в Path
            path.Data = geometry;

            // Добавление Path на Canvas
            Canvas.SetLeft(path, 0);
            Canvas.SetTop(path, 0);
            canvas.Children.Add(path);

            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 9;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Text = $"N:{elementNumder}\nR:{R}\nL:{ArcLength:F2}\ntL{totalLenght:F2}";
            Canvas.SetLeft(textBlock, X);
            Canvas.SetTop(textBlock, Y);
            canvas.Children.Add(textBlock);
        }

        public void DrawNormal(Canvas canvas, double normalLength, double pointPositionFromStart)
        {
            // Вычисление координат точки на кривой
            CalculatePointPosition(pointPositionFromStart);
            // Вычисление координат конца нормали
            double normalEndX = PointX_Position - (normalLength * Math.Cos(PointNormalAngleRadians + Math.PI));
            double normalEndY = PointY_Position - (normalLength * Math.Sin(PointNormalAngleRadians + Math.PI));

            // Создание линии нормали
            Line normalLine = new Line();
            normalLine.Stroke = new SolidColorBrush(Colors.Red); // Цвет нормали
            normalLine.StrokeThickness = 1;

            // Установка координат начала и конца линии нормали
            normalLine.X1 = PointX_Position;
            normalLine.Y1 = PointY_Position;
            normalLine.X2 = normalEndX;
            normalLine.Y2 = normalEndY;

            // Добавление линии нормали на Canvas
            Canvas.SetLeft(normalLine, X);
            Canvas.SetTop(normalLine, Y);
            canvas.Children.Add(normalLine);

            // Добавление точки нормали на Canvas
            Ellipse pointEllipse = new Ellipse();
            pointEllipse.Width = 4;
            pointEllipse.Height = 4;
            pointEllipse.Fill = new SolidColorBrush(Colors.Red);

            Canvas.SetLeft(pointEllipse, X + PointX_Position - 2);
            Canvas.SetTop(pointEllipse, Y + PointY_Position - 2);
            canvas.Children.Add(pointEllipse);

            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 9;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Text = $"С:{PointNormalAngle}\nСR:{PointNormalAngleRadians:F2}";
            Canvas.SetLeft(textBlock, X + normalEndX);
            Canvas.SetTop(textBlock, Y + normalEndY);
            canvas.Children.Add(textBlock);
        }
    }

}
