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
        private DispatcherTimer feuTimer;
        private Ellipse feuH;
        private Ellipse feuB;
        private Ellipse feuG;
        private Ellipse feuD;
        private int secondeFeu;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitialisationDesFeux()
        {
            secondeFeu = 0;
            feuTimer = new DispatcherTimer();
            feuTimer.Tick += feuTimer_Tick;
            feuTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            feuB = new Ellipse();
            feuD = new Ellipse();
            feuG = new Ellipse();
            feuH = new Ellipse();

            PlaceLesFeux(feuB, 420, 425, "feuB");
            PlaceLesFeux(feuH, 267, 257, "feuH");
            PlaceLesFeux(feuG, 257, 420, "FeuG");
            PlaceLesFeux(feuD, 425, 267, "feuD"); 
        }

        private void PlaceLesFeux(Ellipse unFeu, int left, int top, string nomFeu)
        {
            unFeu.Fill = Brushes.Gray;
            unFeu.Width = 15;
            unFeu.Height = 15;
            unFeu.Tag = nomFeu;
            routeCanvas.Children.Add(unFeu);
            Canvas.SetLeft(unFeu, left);
            Canvas.SetTop(unFeu, top);
            
        }

        private void feuTimer_Tick(object sender, EventArgs e)
        {
            secondeFeu++;

            if (feuB.Fill == Brushes.GreenYellow && secondeFeu == 7)
            {
                feuB.Fill = Brushes.Orange;
                feuH.Fill = Brushes.Orange;
            }
            if (feuB.Fill == Brushes.Orange && secondeFeu == 10)
            {
                feuB.Fill = Brushes.Red;
                feuH.Fill = Brushes.Red;
            }
            if (feuB.Fill == Brushes.Red && secondeFeu == 13)
            {
                secondeFeu = 0;
                feuG.Fill = Brushes.GreenYellow;
                feuD.Fill = Brushes.GreenYellow;
            }

            if (feuG.Fill == Brushes.GreenYellow && secondeFeu == 7)
            {
                feuG.Fill = Brushes.Orange;
                feuD.Fill = Brushes.Orange;
            }
            if (feuG.Fill == Brushes.Orange && secondeFeu == 10)
            {
                feuG.Fill = Brushes.Red;
                feuD.Fill = Brushes.Red;
            }
            if (feuB.Fill == Brushes.Red && secondeFeu == 13)
            {
                secondeFeu = 0;
                feuH.Fill = Brushes.GreenYellow;
                feuB.Fill = Brushes.GreenYellow;
            }         
        }

        private void FrmPrinc_Loaded(object sender, RoutedEventArgs e)
        {
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"..\..\Img\route.png", UriKind.Relative));
            routeCanvas.Background = ib;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            InitialisationDesFeux();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            AvanceVoiture();
        }

        private void DrawVoiture()
        {
            int[] tabCoordonnees = rdmApparition();
            Rectangle body = new Rectangle();
            body.Stroke = Brushes.Chocolate;
            body.Fill = Brushes.Coral;
            body.Width = 50;
            body.Height = 40;
            routeCanvas.Children.Add(body);
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
            feuB.Fill = Brushes.GreenYellow;
            feuH.Fill = Brushes.GreenYellow;
            feuG.Fill = Brushes.Red;
            feuD.Fill = Brushes.Red;

            feuTimer.Start();
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