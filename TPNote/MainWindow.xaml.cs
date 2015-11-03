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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TPNote
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FrmPrinc_Loaded(object sender, RoutedEventArgs e)
        {
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"..\..\Img\route.png", UriKind.Relative));
            routeCanvas.Background = ib;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            AvanceVoiture();
        }

        private void DrawVoiture()
        {
            //Line body = new Line();
            //body.Stroke = Brushes.Black;
            //body.X1 = 250;
            //body.Y1 = 250;
            //body.X2 = 0;
            //body.Y2 = 0;
            int[] tabCoordonnees = rdmApparition();
            Rectangle body = new Rectangle();
            body.Stroke = Brushes.Chocolate;
            body.Fill = Brushes.Coral;
            body.Width = 50;
            body.Height = 40;
            routeCanvas.Children.Add(body);
            //Canvas.SetLeft(body, 260);
            //Canvas.SetTop(body, 500);
            Canvas.SetLeft(body, tabCoordonnees[0]);
            Canvas.SetTop(body, tabCoordonnees[1]);
        }

        private int[] rdmApparition()
        {
            int value;
            Random randomGenerator = new Random();
            value = randomGenerator.Next(4);
            int[] tabCoordonnees = new int[2];

                if(value == 0){
                    tabCoordonnees[0] = -50;
                    tabCoordonnees[1] = 265;
                }else if(value == 1){
                    tabCoordonnees[0] = 500;
                    tabCoordonnees[1] = 200;
                }
                else if (value == 2)
                {
                    tabCoordonnees[0] = 190;
                    tabCoordonnees[1] = -50;
                }
                else if (value == 3)
                {
                    tabCoordonnees[0] = 260;
                    tabCoordonnees[1] = 500;
                }

            return tabCoordonnees;

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DrawVoiture();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();
        }

        private void AvanceVoiture()
        {
            Rectangle body;
            for (int i = 0; i <= routeCanvas.Children.Count - 1; i++)
            {
                if (routeCanvas.Children[i] is Rectangle)
                {
                    
                    if (Canvas.GetLeft(routeCanvas.Children[i]) == 195)
                    {
                        body = (Rectangle)routeCanvas.Children[i];
                        body.Width = 40;
                        body.Height = 50;
                        routeCanvas.Children[i] = body;
                        Canvas.SetTop(routeCanvas.Children[i], Canvas.GetTop(routeCanvas.Children[i]) + 1);
                    }
                    else
                    {
                        Canvas.SetLeft(routeCanvas.Children[i], Canvas.GetLeft(routeCanvas.Children[i]) + 1);
                    }
                }
            }
            
        }
    }
}