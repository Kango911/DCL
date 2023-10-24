using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;



namespace DCL.Pages
{
    public class Ring
    {
        public static int Index { get; set; }
        private int _index = 0;
        public int Position;

        public Rectangle Rect { get; set; }

        public Ring(Rectangle rect, int position)
        {
            Rect = rect;
            Index = _index++;
            Position = position;
        }
    }

    public partial class Towers : Page
    {
        int[] counts = new[] { 3, 4, 5, 6, 7, 8, 9, 10 };
        int heightRing = 20, widthRing = 50, distance = 150, stickThickness = 10, stickHeight = 180;
        List<Ring> Rings;
        Solver solver = new Solver();
        int iteration = 0;
        (double, double)[] positions;
        (double x, double y) middle;
        int i; bool playActive;

        string text = "Шаги: ";
        private readonly MainWindow _mainWindow;

        SolidColorBrush towerBrush = Brushes.DarkSlateGray;
        SolidColorBrush[] brushes = new[] { Brushes.Violet, Brushes.LightPink, Brushes.DarkBlue, Brushes.RoyalBlue,
            Brushes.ForestGreen, Brushes.DarkOliveGreen, Brushes.Orange, Brushes.IndianRed, Brushes.Gold, Brushes.SpringGreen };


        public Towers(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;

            Rings = new List<Ring>();

            i = 0; playActive = false;
            
            (double x, double y) middle = (mainWindow.Width / 2, mainWindow.Height / 2);

            cbCount.ItemsSource = counts;
            cbCount.SelectedIndex = 0;
            CreateTower();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (iteration > 0)
            {
                iteration--;
                lblCountSteps.Content = text + iteration;

                var ring = Rings.FindAll(x => x.Position == solver.Moves[iteration].poleTo)[0];
                var rect = ring.Rect;
                ring.Position = solver.Moves[iteration].poleFrom;

                Canvas.SetLeft(rect, positions[ring.Position - 1].Item1 - rect.Width / 2);
                Canvas.SetTop(rect, positions[ring.Position - 1].Item2 - rect.Height / 2);

                positions[solver.Moves[iteration].poleTo - 1].Item2 += heightRing;
                positions[solver.Moves[iteration].poleFrom - 1].Item2 -= heightRing;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (iteration < solver.Moves.Count)
            {
                var ring = Rings.FindAll(x => x.Position == solver.Moves[iteration].poleFrom)[0];
                var rect = ring.Rect;
                ring.Position = solver.Moves[iteration].poleTo;

                Canvas.SetLeft(rect, positions[ring.Position - 1].Item1 - rect.Width / 2);
                Canvas.SetTop(rect, positions[ring.Position - 1].Item2 - rect.Height / 2);

                positions[solver.Moves[iteration].poleFrom - 1].Item2 += heightRing;
                positions[solver.Moves[iteration].poleTo - 1].Item2 -= heightRing;

                iteration++;
                lblCountSteps.Content = text + iteration;
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            playActive = !playActive;

            if (playActive)
                CompositionTarget.Rendering += StartAnimation;
            else
                CompositionTarget.Rendering -= StartAnimation;
        }

        private void StartAnimation(object sender, EventArgs e)
        {
            i++;
            if (i % 30 == 0)
                btnNext.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        void CreateTower()
        {
            middle = (_mainWindow.Width / 2, _mainWindow.Height / 2);

            MainCanvas.Children.Add(MakeLine(middle.x, middle.x, middle.y, middle.y - stickHeight, stickThickness));

            MainCanvas.Children.Add(MakeLine(middle.x - distance, middle.x - distance,
                middle.y, middle.y - stickHeight, stickThickness));

            MainCanvas.Children.Add(MakeLine(middle.x + distance, middle.x + distance,
                middle.y, middle.y - stickHeight, stickThickness));

            MainCanvas.Children.Add(MakeLine(middle.x + distance + distance / 2, middle.x - distance - distance / 2,
                middle.y, middle.y, heightRing));

            positions = new[] {
                 (middle.x - distance, middle.y - (counts[cbCount.SelectedIndex] + 1) * heightRing),
                 (middle.x, middle.y - heightRing),
                 (middle.x + distance, middle.y - heightRing),
            };
        }

        private void btnBegin_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
            Rings.Clear();
            CreateTower();

            iteration = 0;
            Ring.Index = 0;
            i = 0;
            lblCountSteps.Content = text + iteration;

            int length = counts[cbCount.SelectedIndex];

            for (int i = 0; i < length; i++)
            {
                var rectangle = MakeRing(middle.x - distance, middle.y - heightRing * length + heightRing * i,
                    heightRing, widthRing + 10 * i, brushes[i]);

                MainCanvas.Children.Add(rectangle);

                Rings.Add(new Ring(rectangle, 1));
            }

            solver.SolveHanoi(length);
        }

        Line MakeLine(double X1, double X2, double Y1, double Y2, double thickness)
        {
            Line line = new Line()
            {
                X1 = X1,
                Y1 = Y1,
                X2 = X2,
                Y2 = Y2,
                Stroke = towerBrush,
                StrokeThickness = thickness,
                StrokeEndLineCap = PenLineCap.Round,
                StrokeStartLineCap = PenLineCap.Round,
                SnapsToDevicePixels = true
            };

            return line;
        }

        Rectangle MakeRing(double X, double Y, int height, int width, SolidColorBrush brush)
        {
            Rectangle rectangle = new Rectangle()
            {
                Height = height,
                Width = width,
                StrokeThickness = 1.2,
                Stroke = Brushes.Black,
                Fill = brush
            };

            Canvas.SetTop(rectangle, Y - rectangle.Height / 2);
            Canvas.SetLeft(rectangle, X - rectangle.Width / 2);

            return rectangle;
        }
    }
}