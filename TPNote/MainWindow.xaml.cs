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
        private Boolean feuGActif;
        private int secondeFeu;
        private int secondeCanvas;

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

            if (secondeFeu == 7)
            {
                if (!feuGActif)
                {
                    feuB.Fill = Brushes.Orange;
                    feuH.Fill = Brushes.Orange;
                }
                else
                {
                    feuG.Fill = Brushes.Orange;
                    feuD.Fill = Brushes.Orange;                  
                }
            }
            else if (secondeFeu == 10)
            {
                if (!feuGActif)
                {
                    feuB.Fill = Brushes.Red;
                    feuH.Fill = Brushes.Red;
                }
                else
                {
                    feuG.Fill = Brushes.Red;
                    feuD.Fill = Brushes.Red;
                }
            }
            else if (secondeFeu == 13)
            {
                secondeFeu = 0;
                feuGActif = !feuGActif;
                if (feuGActif)
                {
                    feuG.Fill = Brushes.GreenYellow;
                    feuD.Fill = Brushes.GreenYellow;
                }
                else
                {
                    feuH.Fill = Brushes.GreenYellow;
                    feuB.Fill = Brushes.GreenYellow;
                }
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
            dispatcherTimer.Start();
            InitialisationDesFeux();
            feuH.Fill = Brushes.GreenYellow;
            feuB.Fill = Brushes.GreenYellow;
            feuG.Fill = Brushes.Red;
            feuD.Fill = Brushes.Red;
            feuGActif = false;
            feuTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            secondeCanvas++;
            if(secondeCanvas % 100 == 0){
                VoitureAgent uneVoiture = new VoitureAgent();
                //Console.WriteLine(uneVoiture.Direction);
                voitureList.Add(uneVoiture);
                DrawVoiture(uneVoiture);
            }
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
            


            /*Rectangle body = new Rectangle();
            body.Stroke = Brushes.Chocolate;
            body.Fill = Brushes.Coral;
            body.Width = 250;// voiture.Width;
            body.Height = 40;//voiture.Height;
            routeCanvas.Children.Add(body);
            Canvas.SetLeft(body, 390);
            Canvas.SetTop(body, 300);*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            VoitureAgent uneVoiture = new VoitureAgent();
            //Console.WriteLine(uneVoiture.Direction);
            voitureList.Add(uneVoiture);
            DrawVoiture(uneVoiture);
        }

        private Boolean isStopped(VoitureAgent voiture)
        {
            double distance = 0;
            string apparition = voiture.apparitionToString();
            int left = voiture.CoordonneesApparition[0];
            int top = voiture.CoordonneesApparition[1];

            // on vérifie si la voiture est au feu rouge qui la concerne
            if (apparition == "left" && feuG.Fill == Brushes.Red && left == 244)
                return true;

            if (apparition == "right" && feuD.Fill == Brushes.Red && left == 424)
                return true;

            if (apparition == "bot" && feuB.Fill == Brushes.Red && top == 424)
                return true;

            if (apparition == "top" && feuH.Fill == Brushes.Red && top == 244)
                return true;
            //

            foreach (VoitureAgent uneVoiture in voitureList)
            {
                if (uneVoiture != voiture) 
                {
                    //on va déterminer si une voiture devant est arrêtée
                    if (uneVoiture.Stopped && !uneVoiture.Turned && uneVoiture.apparitionToString() == voiture.apparitionToString()){

                        if(uneVoiture.apparitionToString() == "left")
                            distance = uneVoiture.CoordonneesApparition[0] - (voiture.CoordonneesApparition[0] + voiture.Width);
                        else if(uneVoiture.apparitionToString() == "right")
                            distance = voiture.CoordonneesApparition[0] - (uneVoiture.CoordonneesApparition[0] + uneVoiture.Width);
                        else if (uneVoiture.apparitionToString() == "bot")
                            distance = voiture.CoordonneesApparition[1] - (uneVoiture.CoordonneesApparition[1] + uneVoiture.Height);
                        else if (uneVoiture.apparitionToString() == "top")
                            distance = uneVoiture.CoordonneesApparition[1] - (voiture.CoordonneesApparition[1] + voiture.Height);

                        if (distance < 4 && distance > 0)
                            return true;
                    }

                    if (voiture.apparitionToString() == "left")
                        distance = uneVoiture.CoordonneesApparition[0] - (voiture.CoordonneesApparition[0] + voiture.Width);
                    else if (voiture.apparitionToString() == "right")
                        distance = voiture.CoordonneesApparition[0] - (uneVoiture.CoordonneesApparition[0] + uneVoiture.Width);
                    else if (voiture.apparitionToString() == "bot")
                        distance = voiture.CoordonneesApparition[1] - (uneVoiture.CoordonneesApparition[1] + uneVoiture.Height);
                    else if (voiture.apparitionToString() == "top")
                        distance = uneVoiture.CoordonneesApparition[1] - (voiture.CoordonneesApparition[1] + voiture.Height);


                    if (!voiture.Turned && !uneVoiture.Turned && !uneVoiture.Stopped)
                    {
                        if (voiture.apparitionToString() == "left" && voiture.Direction == "top" && uneVoiture.apparitionToString() == "right" && distance < 80 && distance > -20 && left <= 369 && left >= 365)
                            return true;
                        if (voiture.apparitionToString() == "right" && voiture.Direction == "bot" && uneVoiture.apparitionToString() == "left" && distance < 80 && distance > -20 && left <= 300 && left >= 296)
                            return true;
                        if (voiture.apparitionToString() == "bot" && voiture.Direction == "left" && uneVoiture.apparitionToString() == "top" && distance < 80 && distance > -20 && top <= 296 && top >= 292)
                            return true;
                        if (voiture.apparitionToString() == "top" && voiture.Direction == "right" && uneVoiture.apparitionToString() == "bot" && distance < 80 && distance > -20 && top <= 360 && top >= 356)
                            return true;
                     
                    }





                }

            }


            //si aucune raison n'implique l'arret de la voiture, elle ne s'arrête pas...
            return false;
        }

        private void updateVoitures()
        {


           // for (int i = 0; i < voitureList.Count - 1; i++ )

                for (int i = 0; i <= voitureList.Count - 1; i++)
                {

                    VoitureAgent voiture = voitureList[i];
                    string apparition = voiture.apparitionToString();
                    int left = voiture.CoordonneesApparition[0];
                    int top = voiture.CoordonneesApparition[1];
                    //on détermine si la voiture s'arrête ou non
                    voiture.Stopped = isStopped(voiture);



                    //si la voiture n'est pas arrêtée.
                    if (!voiture.Stopped)
                    {
                        //si la voiture n'a pas encore tournée
                        if (!voiture.Turned)
                        {
                            // on détermine dans quel sens on fait avancer la voiture
                            if (apparition == "top")
                                voiture.CoordonneesApparition[1] += voiture.Vitesse;
                            else if (apparition == "bot")
                                voiture.CoordonneesApparition[1] -= voiture.Vitesse;
                            else if (apparition == "left")
                                voiture.CoordonneesApparition[0] += voiture.Vitesse;
                            else if (apparition == "right")
                                voiture.CoordonneesApparition[0] -= voiture.Vitesse;
                            //

                            // on détermine si la voiture est dans le carrefour, et donc si c'est le moment de tourner
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
                            //
                        }
                        else
                        {
                            // si la voiture a tourné, il n'y aura plus de changement de direction, donc on fait avancer la voiture dans sa direction, sans se poser de question
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

                    //supprime l'objet voiture s'il est sortie de la route
                    supprimerVoiture(voiture);
                }
        }
        
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

        private void supprimerVoiture(VoitureAgent voiture)
        {

            if (voiture.CoordonneesApparition[0] > 760 || voiture.CoordonneesApparition[0] < -60 || voiture.CoordonneesApparition[1] < -60 || voiture.CoordonneesApparition[1] > 760)
            {
                voitureList.Remove(voiture);
            }

        }
    }
}