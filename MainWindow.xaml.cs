using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Path = System.Windows.Shapes.Path;

namespace RectangleApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public LaserVector laserVector;
        private Point currentPoint; // Текущая позиция вектора
        private Point startPoint; // Текущая позиция вектора
        private double _canvasWidth;
        private double _canvasHeight;
        private double _rectangle_Width;
        private double _rectangleHeight;
        private double _radius1;
        private double _radius2;
        private double _radius3;
        private double _radius4;
        private double _circleRadius;
        private double _circleCenterX_offset;
        private double _circleCenterY_offset;
        private double _markerAngle;
        private double _rotationSpeed;
        private double vectorLength = 50; // Длина вектора
        private double movementSpeed = 1; // Скорость движения
        private bool isCanvasLoaded = false;
        private double _c1_Angle;
        private int _startEdge;
        private double _startCuttingPosition;
        private double _endCuttingPosition;
        private double _sectionLength;
        private double _sectionCuttingLength;
        private DispatcherTimer timer;
        private DispatcherTimer timerRender;

        //  private Canvas canvas;

        public event PropertyChangedEventHandler PropertyChanged;

        public double CanvasWidth
        {
            get { return _canvasWidth; }
            set
            {
                _canvasWidth = value;
                RaisePropertyChanged(nameof(CanvasWidth));
            }
        }
        public double CanvasHeight
        {
            get { return _canvasHeight; }
            set
            {
                _canvasHeight = value;
                RaisePropertyChanged(nameof(CanvasHeight));
            }
        }
        public double RectangleWidthValue
        {
            get { return _rectangle_Width; }
            set
            {
                _rectangle_Width = value;
                RaisePropertyChanged("WidthValue");
                //UpdateRectangle();
                UpdateShapes();
            }
        }

        public double RectangleHeightValue
        {
            get { return _rectangleHeight; }
            set
            {
                _rectangleHeight = value;
                RaisePropertyChanged("HeightValue");
                //UpdateRectangle();
                UpdateShapes();
            }
        }

        public double Radius1
        {
            get { return _radius1; }
            set
            {
                _radius1 = value;
                RaisePropertyChanged("Radius1");
                //UpdateRectangle();
                UpdateShapes();
            }
        }

        public double Radius2
        {
            get { return _radius2; }
            set
            {
                _radius2 = value;
                RaisePropertyChanged("Radius2");
                //UpdateRectangle();
                UpdateShapes();
            }
        }

        public double Radius3
        {
            get { return _radius3; }
            set
            {
                _radius3 = value;
                RaisePropertyChanged("Radius3");
                //UpdateRectangle();
                UpdateShapes();
            }
        }

        public double Radius4
        {
            get { return _radius4; }
            set
            {
                _radius4 = value;
                RaisePropertyChanged("Radius4");
                //UpdateRectangle();
                UpdateShapes();
            }
        }

        public double CircleRadius
        {
            get { return _circleRadius; }
            set
            {
                _circleRadius = value;
                RaisePropertyChanged("CircleRadius");
                UpdateShapes();
            }
        }

        public double CircleCenterX_offset
        {
            get { return _circleCenterX_offset; }
            set
            {
                _circleCenterX_offset = value;
                RaisePropertyChanged("CircleCenterX");
                UpdateShapes();
            }
        }

        public double CircleCenterY_offset
        {
            get { return _circleCenterY_offset; }
            set
            {
                _circleCenterY_offset = value;
                RaisePropertyChanged("CircleCenterY");
                UpdateShapes();
            }
        }

        public double MarkerAngle
        {
            get { return _markerAngle; }
            set
            {
                _markerAngle = value;
                RaisePropertyChanged("MarkerAngle");
                UpdateShapes();
            }
        }

        public double C1_Angle
        {
            get { return _c1_Angle; }
            set
            {
                _c1_Angle = value;
                RaisePropertyChanged("C1_Angle");
                UpdateShapes();
            }
        }

        public double RotationSpeed
        {
            get { return _rotationSpeed; }
            set
            {
                _rotationSpeed = value;
                RaisePropertyChanged("RotationSpeed");
                UpdateShapes();
            }
        }

        public int StartEdge
        {
            get { return _startEdge; }
            set
            {
                _startEdge = value;
                if (_startEdge < 1 || _startEdge > 4) _startEdge = 1;
                RaisePropertyChanged("StartEdge");
                UpdateShapes();
            }
        }

        public double StartCuttingPosition
        {
            get { return _startCuttingPosition; }
            set
            {
                _startCuttingPosition = value;

                if (StartEdge == 1 || StartEdge == 3)
                {
                    if (_startCuttingPosition < 0 || _startCuttingPosition > RectangleWidthValue)
                    {
                        _startCuttingPosition = RectangleWidthValue / 2;
                    }
                }
                else
                {
                    if (_startCuttingPosition < 0 || _startCuttingPosition > RectangleHeightValue)
                    {
                        _startCuttingPosition = RectangleHeightValue / 2;
                    }
                }

                RaisePropertyChanged("StartCuttingPosition");
                UpdateShapes();
            }
        }

        public double EndCuttingPosition
        {
            get { return _endCuttingPosition; }
            set
            {
                _endCuttingPosition = value;

                if (StartEdge == 1 || StartEdge == 3)
                {
                    if (_endCuttingPosition < 0 || _endCuttingPosition > RectangleWidthValue)
                    {
                        _endCuttingPosition = RectangleWidthValue / 2;
                    }
                }
                else
                {
                    if (_endCuttingPosition < 0 || _endCuttingPosition > RectangleHeightValue)
                    {
                        _endCuttingPosition = RectangleHeightValue / 2;
                    }
                }

                RaisePropertyChanged("EndCuttingPosition");
                UpdateShapes();
            }
        }

        public double SectionLength
        {
            get { return _sectionLength; }
            set
            {
                _sectionLength = value;
                RaisePropertyChanged("SectionLength");
                UpdateShapes();
            }
        }

        public double SectionCuttingLength
        {
            get { return _sectionCuttingLength; }
            set
            {
                _sectionCuttingLength = value;
                RaisePropertyChanged("SectionCuttingLength");
                UpdateShapes();
            }
        }

        public MainWindow()
        {

            InitializeComponent();
            SetupCanvas();
            StartEdge = 1;
            StartCuttingPosition = -3;
            DataContext = this;
            RectangleWidthValue = 200;
            RectangleHeightValue = 300;
            Radius1 = 5;
            Radius2 = 5;
            Radius3 = 5;
            Radius4 = 5;
            CircleRadius = 300;
            CircleCenterX_offset = 10;
            CircleCenterY_offset = -15;
            RotationSpeed = 1; // default rotation speed
            MarkerAngle = 0; // initial angle
            startPoint = new Point(0, 0);
            currentPoint = startPoint;
            //laserVector = new LaserVector(currentPoint);

            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timerRender = new DispatcherTimer();
            timerRender.Tick += Render_Timer_Tick;
            timerRender.Interval = TimeSpan.FromMilliseconds(1000);
            timerRender.Start();

            canvas.Loaded += Canvas_Loaded;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            isCanvasLoaded = true;
            UpdateShapes();
        }

        private void UpdateShapes()
        {
            CanvasWidth = canvas.ActualWidth;
            CanvasHeight = canvas.ActualHeight;
            canvas.Children.Clear();

            double canvasCenter_X = canvas.ActualWidth / 2;
            double canvasCenter_Y = canvas.ActualHeight / 2;


            //создание линий центра координат прямоугольника
            Line verticalCrossLine_X1 = DashLine(canvasCenter_X, 0, canvasCenter_X, CanvasHeight, 2, 2);
            Line horizontalCrossLine_Y1 = DashLine(0, canvasCenter_Y, CanvasWidth, canvasCenter_Y, 2, 2);
            canvas.Children.Add(verticalCrossLine_X1);
            canvas.Children.Add(horizontalCrossLine_Y1);

            // Создание пути для прямоугольника
            Path path = CreateRoundedRectanglePathByCenter(canvasCenter_X, canvasCenter_Y, RectangleWidthValue, RectangleHeightValue, Radius1, Radius2, Radius3, Radius4);
            canvas.Children.Add(path);

            // Создание окружности оси С1
            double circleCenterX = canvasCenter_X + CircleCenterX_offset;
            double circleCenterY = canvasCenter_Y + CircleCenterY_offset;
            Ellipse circle = CreateCircle(circleCenterX, circleCenterY, CircleRadius);

            //Path c1Axis = C1_Axis_CrossLines(circleCenterX, circleCenterY, CircleRadius, C1_Angle);
            Path c1Axis = CreateCrossLines(circleCenterX, circleCenterY, CircleRadius, C1_Angle);
            canvas.Children.Add(c1Axis);

            Path laserHead = LaserHead(circleCenterX, circleCenterY, 200, C1_Angle);
            canvas.Children.Add(laserHead);

            //создание линий центра координат окружности оси C1
            /*          Line verticalCrossLine_C1 = DashLine(circleCenterX, circleCenterY - 10, circleCenterX, circleCenterY + 10, 2, 0);
            Line horizontalCrossLine_C1 = DashLine(circleCenterX - 10, circleCenterY, circleCenterX + 10, circleCenterY, 2, 0);
            canvas.Children.Add(verticalCrossLine_C1);
            canvas.Children.Add(horizontalCrossLine_C1);*/

            // Создание маркера
            double markerX = CircleCenterX_offset + CircleRadius * Math.Cos(Math.PI * MarkerAngle / 180);
            double markerY = CircleCenterY_offset + CircleRadius * Math.Sin(Math.PI * MarkerAngle / 180);
            Ellipse marker = CreateMarker(markerX, markerY);


            /*            // Находим точку на контуре прямоугольника, которая находится в нормали к контуру
            double vectorLength = 50; // Заданная длина вектора
            Point normalPoint = FindPointOnNormalToRectangle(CircleCenterX, CircleCenterY, RectangleWidthValue, RectangleHeightValue, Radius1, Radius2, Radius3, Radius4, vectorLength, MarkerAngle);

            // Находим вектор от центра окружности к этой точке
            double dx = normalPoint.X - CircleCenterX;
            double dy = normalPoint.Y - CircleCenterY;

            // Нормализуем вектор
            double length = Math.Sqrt(dx * dx + dy * dy);
            dx /= length;
            dy /= length;

            // Увеличиваем длину вектора на заданное расстояние
            dx *= vectorLength;
            dy *= vectorLength;

            // Находим конечную точку вектора
            double endX = CircleCenterX + dx;
            double endY = CircleCenterY + dy;*/



            // Создаем вектор
            //Line vectorLine = CreateVectorLine(CircleCenterX, CircleCenterY, endX, endY);

            /*          var canvas = new Canvas();
            canvasWidth = canvas.ActualWidth;
            canvasHeight = canvas.ActualHeight;*/
            //var scaleTransform = new ScaleTransform(2, 2); // Увеличиваем в 1.5 раза
            //canvas.LayoutTransform = scaleTransform;



            canvas.Children.Add(circle);
            canvas.Children.Add(marker);
            //canvas.Children.Add(vectorLine); // Добавляем вектор



            DoubleAnimation rotateAnimation = new DoubleAnimation();
            rotateAnimation.From = 0; // Угол начала анимации
            rotateAnimation.To = 360; // Угол окончания анимации
            rotateAnimation.Duration = TimeSpan.FromSeconds(2); // Продолжительность анимации
            rotateAnimation.RepeatBehavior = RepeatBehavior.Forever; // Повторять анимацию бесконечно

            // Создание вращательной трансформации
            RotateTransform rotateTransform = new RotateTransform();
            path.RenderTransform = rotateTransform; // Применение трансформации к прямоугольнику

            // Запуск анимации вращения
            //rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);


        }

        private LaserVector GetVectorNormal(double currentPosition)
        {


            laserVector = new LaserVector(3, 5, 6);
            return laserVector;
        }

        private Point FindPointOnNormalToRectangle(double centerX, double centerY, double width, double height, double radius1, double radius2, double radius3, double radius4, double vectorLength, double angle)
        {
            // Вычисляем угол нормали к контуру прямоугольника
            double angleToXAxis = angle - 90;
            if (angleToXAxis < 0)
                angleToXAxis += 360;

            // Находим координаты точки на нормали с заданной длиной
            double newX = centerX + vectorLength * Math.Cos(Math.PI * angleToXAxis / 180);
            double newY = centerY + vectorLength * Math.Sin(Math.PI * angleToXAxis / 180);

            // Проверяем, чтобы точка не выходила за пределы прямоугольника

            // Левая граница
            if (newX < radius1)
                newX = radius1;
            // Правая граница
            if (newX > width - radius3)
                newX = width - radius3;
            // Верхняя граница
            if (newY < radius1)
                newY = radius1;
            // Нижняя граница
            if (newY > height - radius4)
                newY = height - radius4;

            return new Point(newX, newY);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Increment angle for rotation
            MarkerAngle += RotationSpeed;
            C1_Angle += RotationSpeed;
            // Находим новую точку на нормали к контуру прямоугольника
            Point newPoint = FindPointOnNormalToRectangle(currentPoint.X, currentPoint.Y, RectangleWidthValue, RectangleHeightValue, Radius1, Radius2, Radius3, Radius4, vectorLength, MarkerAngle);

            // Обновляем текущую позицию вектора
            currentPoint = newPoint;
            UpdateShapes();
            // Обновляем отображение вектора
            //UpdateVector();
        }

        private void Render_Timer_Tick(object sender, EventArgs e)
        {
            CanvasWidth = canvas.ActualWidth;
            CanvasHeight = canvas.ActualHeight;
            //UpdateShapes();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Сбрасываем текущую позицию вектора и останавливаем таймер
            currentPoint = startPoint; // startPoint - начальная точка, выбранная пользователем
            UpdateShapes(); // Обновляем отображение вектора
            timer.Stop();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Tick += (s, args) => MarkerAngle += RotationSpeed;
            timer.Interval = TimeSpan.FromMilliseconds(100); // Adjust this value for desired rotation speed
            timer.Start();
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            // Приостанавливаем движение вектора
            timer.Stop();
        }

        private Path CreateRoundedRectanglePathByCenter(double X, double Y, double width, double height, double radius1, double radius2, double radius3, double radius4)
        {
            // Создание пути для прямоугольника
            // Proверка значений радиусов (Radius check)
            radius1 = Math.Min(radius1, width / 2);
            radius2 = Math.Min(radius2, height / 2);
            radius3 = Math.Min(radius3, width / 2);
            radius4 = Math.Min(radius4, height / 2);

            // Создание фигуры (Creating the figure)
            PathFigure figure = new PathFigure(new Point(0, 0 - (height / 2)), new PathSegmentCollection(), true);

            // Добавление линий и дуг (Adding lines and arcs)
            figure.Segments.Add(new LineSegment(new Point((width / 2) - radius2, 0 - (height / 2)), true));
            figure.Segments.Add(new ArcSegment(new Point(width / 2, 0 - (height / 2) + radius2), new Size(radius2, radius2), 90, false, SweepDirection.Clockwise, true));
            figure.Segments.Add(new LineSegment(new Point(width / 2, (height / 2) - radius3), true));
            figure.Segments.Add(new ArcSegment(new Point((width / 2) - radius3, height / 2), new Size(radius3, radius3), 90, false, SweepDirection.Clockwise, true));
            figure.Segments.Add(new LineSegment(new Point(0 - (width / 2) + radius4, height / 2), true));
            figure.Segments.Add(new ArcSegment(new Point(0 - (width / 2), (height / 2) - radius4), new Size(radius4, radius4), 90, false, SweepDirection.Clockwise, true));
            figure.Segments.Add(new LineSegment(new Point(0 - (width / 2), 0 - (height / 2) + radius1), true));
            figure.Segments.Add(new ArcSegment(new Point(0 - (width / 2) + radius1, 0 - (height / 2)), new Size(radius1, radius1), 90, false, SweepDirection.Clockwise, true));
            figure.Segments.Add(new LineSegment(new Point(0, 0 - (height / 2)), true));
            //figure.Segments.Add(new ArcSegment(new Point(radius1, 0), new Size(radius1, radius1), 270, false, SweepDirection.Clockwise, true));

            // Завершение фигуры (Closing the figure)
            figure.IsClosed = true;

            // Создание геометрии (Creating the geometry)
            PathGeometry geometry = new PathGeometry();
            geometry.Figures.Add(figure);

            Path path = new Path();
            path.Stroke = Brushes.DarkBlue;
            path.StrokeThickness = 2;
            path.Data = geometry;

            Canvas.SetLeft(path, X);
            Canvas.SetTop(path, Y);
            return path;
        }

        private Ellipse CreateMarker(double markerX, double markerY)
        {
            // Создание маркера
            var marker = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Green
            };

            Canvas.SetLeft(marker, markerX - 5);
            Canvas.SetTop(marker, markerY - 5);

            return marker;
        }

        private Path C1_Axis_CrossLines(double x, double y, double length, double angle)
        {
            double angleInRadians = angle * Math.PI / 180; // Переводим угол из градусов в радианы

            // Вычисляем половинную длину линии
            double halfLength = length / 8;

            // Вычисляем координаты начала и конца первой линии
            double x1Start = 0 - halfLength * Math.Cos(angleInRadians);
            double y1Start = 0 - halfLength * Math.Sin(angleInRadians);
            double x1End = 0 + halfLength * Math.Cos(angleInRadians);
            double y1End = 0 + halfLength * Math.Sin(angleInRadians);

            // Вычисляем координаты начала и конца второй линии
            double x2Start = 0 - length * Math.Cos(angleInRadians + Math.PI / 2); // Поворачиваем на 90 градусов
            double y2Start = 0 - length * Math.Sin(angleInRadians + Math.PI / 2); // Поворачиваем на 90 градусов
            double x2End = 0 + halfLength * Math.Cos(angleInRadians + Math.PI / 2); // Поворачиваем на 90 градусов
            double y2End = 0 + halfLength * Math.Sin(angleInRadians + Math.PI / 2); // Поворачиваем на 90 градусов

            // Создаем первую линию
            LineGeometry line1 = new LineGeometry(new Point(x1Start, y1Start), new Point(x1End, y1End));

            // Создаем вторую линию
            LineGeometry line2 = new LineGeometry(new Point(x2Start, y2Start), new Point(x2End, y2End));

            // Создаем геометрическую группу для объединения линий
            GeometryGroup combinedGeometry = new GeometryGroup();
            combinedGeometry.Children.Add(line1);
            combinedGeometry.Children.Add(line2);

            // Создаем фигуру на основе геометрической группы
            Path path = new Path();
            path.Stroke = Brushes.Red; // Цвет линий
            path.StrokeThickness = 1; // Толщина линий
            path.Data = combinedGeometry; // Устанавливаем геометрию фигуры

            Canvas.SetLeft(path, x);
            Canvas.SetTop(path, y);
            return path;
        }
        private Path CreateCrossLines(double x, double y, double length, double angle)
        {
            // Вычисляем половинную длину линии
            double halfLength = length / 5;

            // Создаем первую линию
            LineGeometry line1 = new LineGeometry(new Point(-halfLength, 0), new Point(halfLength, 0));

            // Создаем вторую линию
            LineGeometry line2 = new LineGeometry(new Point(0, -length), new Point(0, halfLength));

            // Создаем геометрическую группу для объединения линий
            GeometryGroup combinedGeometry = new GeometryGroup();
            combinedGeometry.Children.Add(line1);
            combinedGeometry.Children.Add(line2);

            // Создаем фигуру на основе геометрической группы
            Path path = new Path();
            path.Stroke = Brushes.Red; // Цвет линий
            path.StrokeThickness = 1; // Толщина линий
            path.Data = combinedGeometry; // Устанавливаем геометрию фигуры

            // Создаем вращающее преобразование
            RotateTransform rotateTransform = new RotateTransform(angle, 0, 0);
            path.RenderTransform = rotateTransform;
            Canvas.SetLeft(path, x);
            Canvas.SetTop(path, y);

            return path;
        }


        private Path LaserHead(double x, double y, double length, double angle)
        {
            // Вычисляем половинную длину линии
            double halfLength = length / 5;

            // Создаем первую линию
            LineGeometry line1 = new LineGeometry(new Point(0, 0), new Point(0, length));
            LineGeometry line2 = new LineGeometry(new Point(0, length), new Point(halfLength, 0));
            LineGeometry line3 = new LineGeometry(new Point(halfLength, 0), new Point(-halfLength, 0));
            LineGeometry line4 = new LineGeometry(new Point(-halfLength, 0), new Point(0, length));
            // Создаем геометрическую группу для объединения линий
            GeometryGroup combinedGeometry = new GeometryGroup();
            combinedGeometry.Children.Add(line1);
            combinedGeometry.Children.Add(line2);
            combinedGeometry.Children.Add(line3);
            combinedGeometry.Children.Add(line4);
            // Создаем фигуру на основе геометрической группы
            Path path = new Path();
            path.Stroke = Brushes.BlueViolet; // Цвет линий
            path.StrokeThickness = 1; // Толщина линий
            path.Data = combinedGeometry; // Устанавливаем геометрию фигуры

            // Создаем вращающее преобразование
            RotateTransform rotateTransform = new RotateTransform(angle, 0, 0);
            path.RenderTransform = rotateTransform;
            Canvas.SetLeft(path, x);
            Canvas.SetTop(path, y);
            return path;
        }

        private Ellipse CreateCircle(double circleCenterX, double circleCenterY, double circleRadius)
        {
            // Создание окружности
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

        private Line CreateVectorLine(double startX, double startY, double endX, double endY)
        {
            Line line = new Line();
            line.X1 = startX;
            line.Y1 = startY;
            line.X2 = endX;
            line.Y2 = endY;
            line.Stroke = Brushes.Black;
            line.StrokeThickness = 2;
            return line;
        }

        private Line DashLine(double startPointX, double startPointY, double endPointX, double endPointY, int dash1, int dash2)
        {
            // Создание новой линии
            Line line = new Line();
            line.Stroke = Brushes.Black; // Цвет линии
            line.StrokeThickness = 1; // Толщина линии
            DoubleCollection dashes = new DoubleCollection { dash1, dash2 }; // Период и интервал пунктира
            line.StrokeDashArray = dashes; // Применение пунктирной кисти к линии
                                           // Установка начальной и конечной точек линии
            line.X1 = startPointX;
            line.Y1 = startPointY;
            line.X2 = endPointX;
            line.Y2 = endPointY;
            return line;
        }

        private void SetupCanvas()
        {
            canvas.Children.Clear();
            //var scaleTransform = new ScaleTransform(2, 2); // Увеличиваем в 1.5 раза
            //canvas.LayoutTransform = scaleTransform;
        }
    }
}
