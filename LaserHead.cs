using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RectangleApp
{
    public class LaserHead
    {
        public double X { get; set; } // Координата X начальной точки луча
        public double Y { get; set; } // Координата Y начальной точки луча
                                      // public double Angle { get; set; } // Угол наклона в градусах
        public double AngleRadians { get; set; }
        public double Length { get; set; }
        public Color Color { get; set; }
        public double Thickness { get; set; }

        public double End_X_Position { get; set; }
        public double End_Y_Position { get; set; }


        public LaserHead(LaserVector laserVector, double length, Color color = default, double thickness = 1)
        {
            X = laserVector.X;
            Y = laserVector.Y;
            AngleRadians = laserVector.Angle;
            Length = length;
            Color = color != default ? color : Colors.Black;
            Thickness = thickness;
            End_X_Position = X + (Length * Math.Cos(AngleRadians));
            End_Y_Position = Y + (Length * Math.Sin(AngleRadians));
        }

        public void Draw(Canvas canvas, double x, double y)
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

            /*           TextBlock textBlock = new TextBlock();
                       textBlock.FontSize = 9;
                       textBlock.VerticalAlignment = VerticalAlignment.Center;
                       textBlock.TextAlignment = TextAlignment.Center;
                       textBlock.Text = $"N:{elementNumder}\nL:{Length:F2}\ntL{totalLenght:F2}";
                       Canvas.SetLeft(textBlock, (x + (X + End_X_Position) / 2) + textBlock.ActualWidth);
                       Canvas.SetTop(textBlock, y + (Y + End_Y_Position) / 2);
                       canvas.Children.Add(textBlock);*/
        }

    }
}
