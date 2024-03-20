using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private Canvas canvas1;
        private static readonly int[] elementsType = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        public RectangleElement rectangleElement;
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
        private double _currentPosition;
        private double _startCuttingPosition;
        private double _endCuttingPosition;
        private double _sectionLength;
        private double _sectionCuttingLength;
        private double _currentElement;
        private DispatcherTimer timer;
        private DispatcherTimer timerRender;
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
                //UpdateShapes();
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
                // UpdateShapes();
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
                // UpdateShapes();
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
                // UpdateShapes();
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
                // UpdateShapes();
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
                // UpdateShapes();
            }
        }
        public double CircleRadius
        {
            get { return _circleRadius; }
            set
            {
                _circleRadius = value;
                RaisePropertyChanged("CircleRadius");
                // UpdateShapes();
            }
        }
        public double CircleCenterX_offset
        {
            get { return _circleCenterX_offset; }
            set
            {
                _circleCenterX_offset = value;
                RaisePropertyChanged("CircleCenterX");
                //UpdateShapes();
            }
        }
        public double CircleCenterY_offset
        {
            get { return _circleCenterY_offset; }
            set
            {
                _circleCenterY_offset = value;
                RaisePropertyChanged("CircleCenterY");
                //UpdateShapes();
            }
        }
        public double MarkerAngle
        {
            get { return _markerAngle; }
            set
            {
                _markerAngle = value;
                RaisePropertyChanged("MarkerAngle");
                //UpdateShapes();
            }
        }
        public double C1_Angle
        {
            get { return _c1_Angle; }
            set
            {
                _c1_Angle = value;
                RaisePropertyChanged("C1_Angle");
                //UpdateShapes();
            }
        }
        public double RotationSpeed
        {
            get { return _rotationSpeed; }
            set
            {
                _rotationSpeed = value;
                RaisePropertyChanged("RotationSpeed");
                // UpdateShapes();
            }
        }
        public int StartEdge
        {
            get { return _startEdge; }
            set
            {
                _startEdge = value;
                if (_startEdge < 1 || _startEdge > 4) _startEdge = 1;
                //RaisePropertyChanged("StartEdge");
                //UpdateShapes();
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
                //UpdateShapes();
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
                //UpdateShapes();
            }
        }
        public double SectionLength
        {
            get { return _sectionLength; }
            set
            {
                _sectionLength = value;
                RaisePropertyChanged("SectionLength");
            }
        }
        public double SectionCuttingLength
        {
            get { return _sectionCuttingLength; }
            set
            {
                _sectionCuttingLength = value;
                RaisePropertyChanged("SectionCuttingLength");
            }
        }
        public double CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                _currentPosition = value;
                RaisePropertyChanged("CurrentPosition");
            }
        }
        public double CurrentElement
        {
            get { return _currentElement; }
            set
            {
                _currentElement = value;
                RaisePropertyChanged("CurrentElement");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            SetupCanvas();
            StartEdge = 1;
            StartCuttingPosition = 0;
            DataContext = this;
            CurrentPosition = 0;
            RectangleWidthValue = 300;
            RectangleHeightValue = 500;
            Radius1 = 50;
            Radius2 = 50;
            Radius3 = 50;
            Radius4 = 50;
            CircleRadius = 300;
            CircleCenterX_offset = 10;
            CircleCenterY_offset = -15;
            RotationSpeed = 1; // default rotation speed
            //MarkerAngle = 0; // initial angle
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timerRender = new DispatcherTimer();
            timerRender.Tick += Render_Timer_Tick;
            timerRender.Interval = TimeSpan.FromMilliseconds(1000);
            timerRender.Start();
            canvas1.Loaded += Canvas_Loaded;


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
            CanvasWidth = canvas1.ActualWidth;
            CanvasHeight = canvas1.ActualHeight;
            canvas1.Children.Clear();

            double canvasCenter_X = canvas1.ActualWidth / 2;
            double canvasCenter_Y = canvas1.ActualHeight / 2;

            //создание линий центра координат прямоугольника
            Line verticalCrossLine_X1 = DashLine(canvasCenter_X, 0, canvasCenter_X, CanvasHeight, 2, 2);
            Line horizontalCrossLine_Y1 = DashLine(0, canvasCenter_Y, CanvasWidth, canvasCenter_Y, 2, 2);
            canvas1.Children.Add(verticalCrossLine_X1);
            canvas1.Children.Add(horizontalCrossLine_Y1);

            rectangleElement = new RectangleElement(canvasCenter_X, canvasCenter_Y, RectangleWidthValue, RectangleHeightValue, Radius1, Radius2, Radius3, Radius4);
            rectangleElement.Draw(canvas1);

            rectangleElement.DrawNormal(canvas1, 30, CurrentPosition);
            // Создание окружности оси С1
            double circleCenterX = canvasCenter_X + CircleCenterX_offset;
            double circleCenterY = canvasCenter_Y + CircleCenterY_offset;
            Ellipse circle = CreateCircle(circleCenterX, circleCenterY, CircleRadius);
            canvas1.Children.Add(circle);

            Path c1Axis = CreateCrossLines(circleCenterX, circleCenterY, CircleRadius, C1_Angle);
            canvas1.Children.Add(c1Axis);

            Path laserHead = LaserHead(circleCenterX, circleCenterY, 200, C1_Angle);
            canvas1.Children.Add(laserHead);

            DoubleAnimation rotateAnimation = new DoubleAnimation();
            rotateAnimation.From = 0; // Угол начала анимации
            rotateAnimation.To = 360; // Угол окончания анимации
            rotateAnimation.Duration = TimeSpan.FromSeconds(2); // Продолжительность анимации
            rotateAnimation.RepeatBehavior = RepeatBehavior.Forever; // Повторять анимацию бесконечно

            // Создание вращательной трансформации
            //RotateTransform rotateTransform = new RotateTransform();
            //c1Axis.RenderTransform = rotateTransform; // Применение трансформации к прямоугольнику

            // Запуск анимации вращения
            //rotateTransform.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);

            SectionLength = rectangleElement.GetSectionLength();
            CurrentElement = rectangleElement.GetCurrentElement(CurrentPosition);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Increment angle for rotation
            MarkerAngle += RotationSpeed;
            C1_Angle += RotationSpeed;
            // Находим новую точку на нормали к контуру прямоугольника
            //Point newPoint = FindPointOnNormalToRectangle(currentPoint.X, currentPoint.Y, RectangleWidthValue, RectangleHeightValue, Radius1, Radius2, Radius3, Radius4, vectorLength, MarkerAngle);
            CurrentPosition += RotationSpeed;
            UpdateShapes();
        }
        private void Render_Timer_Tick(object sender, EventArgs e)
        {
            CanvasWidth = canvas1.ActualWidth;
            CanvasHeight = canvas1.ActualHeight;
            UpdateShapes();
        }
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Сбрасываем текущую позицию вектора и останавливаем таймер
            currentPoint = startPoint; // startPoint - начальная точка, выбранная пользователем
            CurrentPosition = 0;
            UpdateShapes(); // Обновляем отображение вектора
            timer.Stop();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // timer.Tick += (s, args) => MarkerAngle += RotationSpeed;
            //timer.Interval = TimeSpan.FromMilliseconds(100); // Adjust this value for desired rotation speed
            timer.Start();
        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            // Приостанавливаем движение вектора
            timer.Stop();
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
            canvas1 = new Canvas();
            canvas1.Width = 800; // Задайте необходимую ширину
            canvas1.Height = 800; // Задайте необходимую высоту
            // Добавьте canvas в нужный вам контейнер, например, в Grid:
            CanvasContainer.Children.Add(canvas1);
            canvas1.Children.Clear();
            //var scaleTransform = new ScaleTransform(2, 2); // Увеличиваем в 1.5 раза
            //canvas.LayoutTransform = scaleTransform;
        }

    }
}