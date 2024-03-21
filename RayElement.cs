using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.IO;

namespace RectangleApp
{
    public class RayElement
    {
        public double X { get; set; } // Координата X начальной точки луча
        public double Y { get; set; } // Координата Y начальной точки луча
        public double Angle { get; set; } // Угол наклона в градусах
        public double AngleRadians { get; set; }
        public double Length { get; set; }
        public Color Color { get; set; }
        public double Thickness { get; set; }
        public double PointPositionFromStart { get; set; }
        public double End_X_Position { get; set; }
        public double End_Y_Position { get; set; }
        public double PointX_Position_Normal { get; set; }
        public double PointY_Position_Normal { get; set; }
        public double PointNormalAngleRadians { get; set; }
        public double PointNormalAngle { get; set; }


        public RayElement(double x, double y, double angle, double length, Color color = default, double thickness = 1)
        {
            if (length <= 0)
            {
                throw new ArgumentOutOfRangeException("Length must be positive");
            }

            X = x;
            Y = y;
            Angle = angle;
            AngleRadians = angle * (Math.PI / 180.0); // Перевод угла в радианы
            Length = length;
            Color = color != default ? color : Colors.Black; // Установка цвета по умолчанию
            Thickness = thickness;
            End_X_Position = X + (Length * Math.Cos(AngleRadians));
            End_Y_Position = Y + (Length * Math.Sin(AngleRadians));
        }



        // Метод для вычисления координат точки на луче
        public LaserVector CalculatePointPosition(double pointPositionFromStart)
        {
            PointX_Position_Normal = X + (pointPositionFromStart * Math.Cos(AngleRadians));
            PointY_Position_Normal = Y + (pointPositionFromStart * Math.Sin(AngleRadians));
            PointNormalAngleRadians = (AngleRadians - (Math.PI / 2) + 2 * Math.PI) % (2 * Math.PI);// Добавляем PI, чтобы нормаль была противоположна направлению луча
            PointNormalAngle = PointNormalAngleRadians * (180 / Math.PI); // Перевод угла в градусы
            return new LaserVector(PointX_Position_Normal, PointY_Position_Normal, PointNormalAngleRadians);
        }

        // Метод для вычисления угла нормали
        /*      public void CalculatePointNormalAngle()
              {
                  PointNormalAngleRadians = (AngleRadians - (Math.PI / 2) + 2 * Math.PI) % (2 * Math.PI);// Добавляем PI, чтобы нормаль была противоположна направлению луча
                  PointNormalAngle = PointNormalAngleRadians * (180 / Math.PI); // Перевод угла в градусы
              }*/

        public void Draw(Canvas canvas, double x, double y, int elementNumder, double totalLenght = 0)
        {
            // Создание линии, представляющей луч
            Line line = new Line();
            line.Stroke = new SolidColorBrush(Color);
            line.StrokeThickness = Thickness;
            line.X1 = X;
            line.Y1 = Y;
            line.X2 = End_X_Position;
            line.Y2 = End_Y_Position;

            // Добавление линии на канвас
            Canvas.SetLeft(line, x);
            Canvas.SetTop(line, y);
            canvas.Children.Add(line);

            TextBlock textBlock = new TextBlock();
            textBlock.FontSize = 9;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Text = $"N:{elementNumder}\nL:{Length:F2}\ntL{totalLenght:F2}";

            Canvas.SetLeft(textBlock, (x + (X + End_X_Position) / 2) + textBlock.ActualWidth);
            Canvas.SetTop(textBlock, y + (Y + End_Y_Position) / 2);
            canvas.Children.Add(textBlock);
        }

        public void DrawNormal(Canvas canvas, double x, double y, double normalLength, double pointPositionFromStart)
        {
            // Вычисление координат точки на кривой
            CalculatePointPosition(pointPositionFromStart);
            // Вычисление координат конца нормали
            double normalEndX = PointX_Position_Normal - (normalLength * Math.Cos(PointNormalAngleRadians + Math.PI));
            double normalEndY = PointY_Position_Normal - (normalLength * Math.Sin(PointNormalAngleRadians + Math.PI));

            // Создание линии нормали
            Line normalLine = new Line();
            normalLine.Stroke = new SolidColorBrush(Colors.Red); // Цвет нормали
            normalLine.StrokeThickness = 1;

            // Установка координат начала и конца линии нормали
            normalLine.X1 = PointX_Position_Normal;
            normalLine.Y1 = PointY_Position_Normal;
            normalLine.X2 = normalEndX;
            normalLine.Y2 = normalEndY;

            // Добавление линии нормали на Canvas
            Canvas.SetLeft(normalLine, x);
            Canvas.SetTop(normalLine, y);
            canvas.Children.Add(normalLine);

            // Добавление точки нормали на Canvas
            Ellipse pointEllipse = new Ellipse();
            pointEllipse.Width = 4;
            pointEllipse.Height = 4;
            pointEllipse.Fill = new SolidColorBrush(Colors.Red);

            Canvas.SetLeft(pointEllipse, x + PointX_Position_Normal - 2);
            Canvas.SetTop(pointEllipse, y + PointY_Position_Normal - 2);
            canvas.Children.Add(pointEllipse);

            /*          TextBlock textBlock = new TextBlock();
                      textBlock.FontSize = 9;
                      textBlock.VerticalAlignment = VerticalAlignment.Center;
                      textBlock.TextAlignment = TextAlignment.Center;
                      textBlock.Text = $"СN:{PointNormalAngle}\nС:{Angle:F2}\nСNR:{PointNormalAngleRadians:F2}";
                      Canvas.SetLeft(textBlock, PointX_Position_Normal);
                      Canvas.SetTop(textBlock, PointY_Position_Normal);
                      canvas.Children.Add(textBlock);*/
        }
    }
}
