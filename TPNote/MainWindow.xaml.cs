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
using MultiAgentSystemPCL.TP;

namespace TPNote
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<VoitureAgent> voitureList = null;
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
            voitureList = new List<VoitureAgent>();
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"..\..\Img\route.png", UriKind.Relative));
            routeCanvas.Background = ib;
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            //voitureList.Add(new VoitureAgent);
            dispatcherTimer.Start();
            InitialisationDesFeux();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {

            updateVoitures();
            drawVoitures();
        }

        private void DrawVoiture(VoitureAgent voiture)
        {
            Rectangle body = new Rectangle();
            body.Stroke = Brushes.Chocolate;
            body.Fill = Brushes.Coral;
            body.Width = voiture.Width;
            body.Height = voiture.Height;
            routeCanvas.Children.Add(body);
            Canvas.SetLeft(body, voiture.CoordonneesApparition[0]);
            Canvas.SetTop(body, voiture.CoordonneesApparition[1]);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            VoitureAgent uneVoiture = new VoitureAgent();
            Console.WriteLine(uneVoiture.Direction);
            voitureList.Add(uneVoiture);
            DrawVoiture(uneVoiture);
        }

        private void updateVoitures()
        {

            foreach (VoitureAgent voiture in voitureList)
            {

                string apparition = voiture.apparitionToString();
                int left = voiture.CoordonneesApparition[0];
                int top = voiture.CoordonneesApparition[1];

                //si la voiture n'a pas encore tournée
                if (!voiture.Turned)
                {
                    if (apparition == "top")
                        voiture.CoordonneesApparition[1] += voiture.Vitesse;
                    else if (apparition == "bot")
                        voiture.CoordonneesApparition[1] -= voiture.Vitesse;
                    else if (apparition == "left")
                        voiture.CoordonneesApparition[0] += voiture.Vitesse;
                    else if (apparition == "right")
                        voiture.CoordonneesApparition[0] -= voiture.Vitesse;

                    if ((apparition == "left" || apparition == "right") && (voiture.Direction != "right" || voiture.Direction != "left"))
                    {
                        if (voiture.Direction == "bot" && left == 300)
                            voiture.tourner();
                        else if (voiture.Direction == "top" && left == 370)
                            voiture.tourner();
                    }
                    if ((apparition == "top" || apparition == "bot") && (voiture.Direction != "bot" || voiture.Direction != "top"))
                    {
                        if (voiture.Direction == "right" && top == 360)
                            voiture.tourner();
                        else if (voiture.Direction == "left" && top == 296)
                            voiture.tourner();
                    }
                }
                else
                {
                    if (voiture.Direction == "right")
                        voiture.CoordonneesApparition[0] += voiture.Vitesse;
                    if (voiture.Direction == "left")
                        voiture.CoordonneesApparition[0] -= voiture.Vitesse;
                    if (voiture.Direction == "bot")
                        voiture.CoordonneesApparition[1] += voiture.Vitesse;
                    if (voiture.Direction == "top")
                        voiture.CoordonneesApparition[1] -= voiture.Vitesse;
                }

            }
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
        /*
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
            }*/

        
        private void drawVoitures()
        {
            viderCanvas();

            Rectangle body;
            foreach(VoitureAgent voiture in voitureList){

                body = new Rectangle();
                body.Stroke = Brushes.Chocolate;
                body.Fill = Brushes.Coral;
                body.Width = voiture.Width;
                body.Height = voiture.Height;
                routeCanvas.Children.Add(body);
                Canvas.SetLeft(body, voiture.CoordonneesApparition[0]);
                Canvas.SetTop(body, voiture.CoordonneesApparition[1]);
            } 
        }

        private void viderCanvas()
        {

            var rectangles = routeCanvas.Children.OfType<Rectangle>().ToList();
            foreach (var rectangle in rectangles)
            {
                routeCanvas.Children.Remove(rectangle);
            }
        }
    }
}