﻿using System;
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
        private int vitesse = 1;
        Random randomGenerator = new Random();
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

            PlaceLesFeux(feuB, 490, 490, "feuB");
            PlaceLesFeux(feuH, 347, 347, "feuH");
            PlaceLesFeux(feuG, 337, 490, "FeuG");
            PlaceLesFeux(feuD, 485, 344, "feuD"); 
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

            int apparition;
            secondeCanvas++;
            if(secondeCanvas % 65 == 0){
                apparition = randomGenerator.Next(4);
                VoitureAgent uneVoiture = new VoitureAgent(apparition);

                //Console.WriteLine(uneVoiture.Direction);
                if (!bouchon(uneVoiture.apparitionToString()))
                {
                    voitureList.Add(uneVoiture);
                    DrawVoiture(uneVoiture);
                }
                else
                    Console.WriteLine("bouchon !!");

            }
            updateVoitures();
            drawVoitures();
        }

        private Boolean bouchon(string apparition)
        {
            int total = 0;
            foreach (VoitureAgent uneVoiture in voitureList)
            {
                if (uneVoiture.apparitionToString() == apparition && !uneVoiture.sortieDuCarrefour())
                    total++;
            }

            return (total >= 13 ? true : false);

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
            /*VoitureAgent uneVoiture = new VoitureAgent();
            //Console.WriteLine(uneVoiture.Direction);
            voitureList.Add(uneVoiture);
            DrawVoiture(uneVoiture);*/
            /*ectangle body = new Rectangle();
            body.Stroke = Brushes.Chocolate;
            body.Fill = Brushes.Coral;
            body.Width = voiture.Width;
            body.Height = voiture.Height;

            Rectangle clignotant = new Rectangle();

            clignotant.*/

            
            
            
            if (dispatcherTimer.IsEnabled)
            {
                dispatcherTimer.Stop();
            }
            else
            {
                dispatcherTimer.Start();
            }
            
        }

        private int adapterVitesse(VoitureAgent voiture)
        {

            double distance = 0;
            string apparition = voiture.apparitionToString();
            int left = voiture.CoordonneesApparition[0];
            int top = voiture.CoordonneesApparition[1];
            //voiture.Freine = 1;

            foreach (VoitureAgent uneVoiture in voitureList)
            {
                if (uneVoiture != voiture)
                {
                    //on va déterminer si une voiture devant est arrêtée

                    //if (uneVoiture.apparitionToString() == voiture.apparitionToString() && voiture.directionActuelle() == uneVoiture.directionActuelle())
                    if (voiture.directionActuelle() == uneVoiture.directionActuelle())
                    {
                        /*
                        if (uneVoiture.apparitionToString() == "left")
                            distance = uneVoiture.CoordonneesApparition[0] - (voiture.CoordonneesApparition[0] + voiture.Width);
                        else if (uneVoiture.apparitionToString() == "right")
                            distance = voiture.CoordonneesApparition[0] - (uneVoiture.CoordonneesApparition[0] + uneVoiture.Width);
                        else if (uneVoiture.apparitionToString() == "bot")
                            distance = voiture.CoordonneesApparition[1] - (uneVoiture.CoordonneesApparition[1] + uneVoiture.Height);
                        else if (uneVoiture.apparitionToString() == "top")
                            distance = uneVoiture.CoordonneesApparition[1] - (voiture.CoordonneesApparition[1] + voiture.Height);
                        */


                        if (uneVoiture.directionActuelle() == "right")
                            distance = uneVoiture.CoordonneesApparition[0] - (voiture.CoordonneesApparition[0] + voiture.Width);
                        else if (uneVoiture.directionActuelle() == "left")
                            distance = voiture.CoordonneesApparition[0] - (uneVoiture.CoordonneesApparition[0] + uneVoiture.Width);
                        else if (uneVoiture.directionActuelle() == "top")
                            distance = voiture.CoordonneesApparition[1] - (uneVoiture.CoordonneesApparition[1] + uneVoiture.Height);
                        else if (uneVoiture.directionActuelle() == "bot")
                            distance = uneVoiture.CoordonneesApparition[1] - (voiture.CoordonneesApparition[1] + voiture.Height);

                        if (distance > 0 && distance < 7)
                        {
                            return 4;
                        }
                        else if (distance >= 7 && distance < 15)
                            return 3;
                        else if (distance >= 15 && distance < 25)
                        {
                            return 2;
                        }
                    }
                }

            }

            return 1;
        }

        private Boolean isStopped(VoitureAgent voiture)
        {
            double distance = 0;
            string apparition = voiture.apparitionToString();
            int left = voiture.CoordonneesApparition[0];
            int top = voiture.CoordonneesApparition[1];
            //voiture.Freine = 1;
            double left2 = voiture.CoordonneesApparition[0] + voiture.Width;
            double top2 =voiture.CoordonneesApparition[1] + voiture.Height;


            int _left;
            int _top;

            double _left2;
            double _top2;

            // on vérifie si la voiture est au feu rouge qui la concerne
            if (apparition == "left" && (feuG.Fill == Brushes.Red || feuG.Fill == Brushes.Orange) && left >= 323 && left <= 340)
                return true;

            if (apparition == "right" && (feuD.Fill == Brushes.Red || feuD.Fill == Brushes.Orange) && left >= 481 && left <= 495)
                return true;

            if (apparition == "bot" && (feuB.Fill == Brushes.Red || feuB.Fill == Brushes.Orange) && top >= 485 && top <= 499)
                return true;

            if (apparition == "top" && (feuH.Fill == Brushes.Red || feuH.Fill == Brushes.Orange) && top >= 326 && top <= 341)
                return true;
            //

            foreach (VoitureAgent uneVoiture in voitureList)
            {
                if (uneVoiture != voiture)
                {
                    //on va déterminer si une voiture devant est arrêtée
                    //if (uneVoiture.Stopped && !uneVoiture.Turned && uneVoiture.apparitionToString() == voiture.apparitionToString())
                    if (uneVoiture.Stopped && uneVoiture.apparitionToString() == voiture.apparitionToString())
                    {

                        if (uneVoiture.apparitionToString() == "left")
                            distance = uneVoiture.CoordonneesApparition[0] - (voiture.CoordonneesApparition[0] + voiture.Width);
                        else if (uneVoiture.apparitionToString() == "right")
                            distance = voiture.CoordonneesApparition[0] - (uneVoiture.CoordonneesApparition[0] + uneVoiture.Width);
                        else if (uneVoiture.apparitionToString() == "bot")
                            distance = voiture.CoordonneesApparition[1] - (uneVoiture.CoordonneesApparition[1] + uneVoiture.Height);
                        else if (uneVoiture.apparitionToString() == "top")
                            distance = uneVoiture.CoordonneesApparition[1] - (voiture.CoordonneesApparition[1] + voiture.Height);              


                        if (distance < 6 && distance > 0)
                            return true;

                    }

                    // on va déterminer s'il faut laisser la priorité aux voitures venant d'en face
                    if (voiture.apparitionToString() == "left")
                        distance = uneVoiture.CoordonneesApparition[0] - (voiture.CoordonneesApparition[0] + voiture.Width);
                    else if (voiture.apparitionToString() == "right")
                        distance = voiture.CoordonneesApparition[0] - (uneVoiture.CoordonneesApparition[0] + uneVoiture.Width);
                    else if (voiture.apparitionToString() == "bot")
                        distance = voiture.CoordonneesApparition[1] - (uneVoiture.CoordonneesApparition[1] + uneVoiture.Height);
                    else if (voiture.apparitionToString() == "top")
                        distance = uneVoiture.CoordonneesApparition[1] - (voiture.CoordonneesApparition[1] + voiture.Height);
                   

                   
                    //fonctionnel !!  if (!uneVoiture.Turned && !uneVoiture.Stopped)

                    
                    if (voiture.apparitionToString() == "left" && voiture.Direction == "top" && uneVoiture.apparitionToString() == "right" && voiture.timeToTurn() && distance < 50 && distance > 0 && feuD.Fill == Brushes.GreenYellow)
                            return true;
                    if (voiture.apparitionToString() == "right" && voiture.Direction == "bot" && uneVoiture.apparitionToString() == "left" && voiture.timeToTurn() && distance < 50 && distance > 0 && feuG.Fill == Brushes.GreenYellow)
                            return true;
                    if (voiture.apparitionToString() == "bot" && voiture.Direction == "left" && uneVoiture.apparitionToString() == "top" && voiture.timeToTurn() && distance < 50 && distance > 0 && feuH.Fill == Brushes.GreenYellow)
                            return true;
                    if (voiture.apparitionToString() == "top" && voiture.Direction == "right" && uneVoiture.apparitionToString() == "bot" && voiture.timeToTurn() && distance < 50 && distance > 0 && feuB.Fill == Brushes.GreenYellow)
                            return true;
                    /*
                    if (voiture.apparitionToString() == "left" && voiture.Direction == "top" && uneVoiture.apparitionToString() == "right" && left == 370 && distance < 60 && distance > -40)
                        return true;
                    if (voiture.apparitionToString() == "right" && voiture.Direction == "bot" && uneVoiture.apparitionToString() == "left" && left == 300 && distance < 60 && distance > -40)
                        return true;
                    if (voiture.apparitionToString() == "bot" && voiture.Direction == "left" && uneVoiture.apparitionToString() == "top" && top == 300 && distance < 60 && distance > -40)
                        return true;
                    if (voiture.apparitionToString() == "top" && voiture.Direction == "right" && uneVoiture.apparitionToString() == "bot" && top == 370 && distance < 60 && distance > -40)
                        return true;*/
                    

                    _left = uneVoiture.CoordonneesApparition[0];
                    _left2 = uneVoiture.CoordonneesApparition[0] + uneVoiture.Width;

                    _top = uneVoiture.CoordonneesApparition[1];
                    _top2 = uneVoiture.CoordonneesApparition[1] + uneVoiture.Height;

                    // on va déterminer si c'est le bordel dans l'intersectione
                        if (voiture.directionActuelle() == "left")
                        {
                            distance = left - _left2;
                        }
                        else if (voiture.directionActuelle() == "right")
                        {
                            distance = _left - left2;
                        }
                        else if (voiture.directionActuelle() == "top")
                        {
                            distance = top - _top2;
                        }
                        else if (voiture.directionActuelle() == "bot")
                        {
                            distance = _top - top2;
                        }


                        if (voiture.Direction == "top" || voiture.Direction == "bot")
                        {
                            if (!(left2 < _left || left > _left2))
                            {
                                if (distance < 10 && distance > 0)
                                    return true;
                            }
                        }else{
                            if (!(top2 < _top || top > _top2))
                            {
                                if (distance < 5 && distance > 0)
                                    return true;
                            }
                        }


                        /*if (uneVoiture.Turned && !voiture.Turned && voiture.Stopped)
                        {
                            if (voiture.directionActuelle() == "left" && voiture.Direction == "top" && voiture.Stopped)
                            {
                                if (top - _top2 < 10)
                                    return false;
                            }
                            else if (voiture.directionActuelle() == "right" && voiture.Direction == "bot" && voiture.Stopped)
                            {
                                if (_top - top2 < 10)
                                    return false;
                            }
                            else if (voiture.directionActuelle() == "top" && voiture.Direction == "left" && voiture.Stopped)
                            {
                                if (left - _left2 < 10)
                                    return false;
                            }
                            else if (voiture.directionActuelle() == "bot" && voiture.Direction == "right" && voiture.Stopped)
                            {
                                if (_left - left2 < 10)
                                    return false;
                            }
                        }*/
                        //if(distance)

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

                    voiture.Freine = adapterVitesse(voiture);
                    if(secondeCanvas % voiture.Freine == 0){
                        string apparition = voiture.apparitionToString();
                        int left = voiture.CoordonneesApparition[0];
                        int top = voiture.CoordonneesApparition[1];
                        //on détermine si la voiture s'arrête ou non

                        if (!voiture.sortieDuCarrefour())
                            voiture.Stopped = isStopped(voiture);
                        else
                        {
                            //Console.WriteLine("je sors du carrefour");
                            voiture.Stopped = false;
                        }
                        //si la voiture n'est pas arrêtée.
                        if (!voiture.Stopped)
                        {
                            //si la voiture n'a pas encore tournée
                            if (!voiture.Turned)
                            {


                                if (apparition == "left" && (feuG.Fill == Brushes.Orange || feuG.Fill == Brushes.Red) && left <= 338 && left >= 238)
                                {

                                    if (left >= 318)
                                        voiture.Freine = 3;
                                    else
                                        voiture.Freine = 2;
                                }
                                else
                                    voiture.Freine = 1;


                                if (apparition == "right" && (feuD.Fill == Brushes.Orange || feuD.Fill == Brushes.Red) && left >= 484 && left <= 584)
                                {
                                    if (left <= 504)
                                        voiture.Freine = 3;
                                    else
                                        voiture.Freine = 2;
                                }
                                else
                                    voiture.Freine = 1;

                                if (apparition == "bot" && (feuB.Fill == Brushes.Orange || feuB.Fill == Brushes.Red) && top >= 490 && top <= 590)
                                {
                                    if (top <= 510)
                                        voiture.Freine = 3;
                                    else
                                        voiture.Freine = 2;
                                }
                                else
                                    voiture.Freine = 1;

                                if (apparition == "top" && (feuH.Fill == Brushes.Orange || feuH.Fill == Brushes.Red) && top <= 348 && top >= 248)
                                {
                                    if (top >= 318)
                                        voiture.Freine = 3;
                                    else
                                        voiture.Freine = 2;
                                }
                                else
                                    voiture.Freine = 1;

                                // on détermine dans quel sens on fait avancer la voiture
                                if (apparition == "top")
                                    voiture.CoordonneesApparition[1] += vitesse;
                                else if (apparition == "bot")
                                    voiture.CoordonneesApparition[1] -= vitesse;
                                else if (apparition == "left")
                                    voiture.CoordonneesApparition[0] += vitesse;
                                else if (apparition == "right")
                                    voiture.CoordonneesApparition[0] -= vitesse;
                                //

                                // on détermine si la voiture est dans le carrefour, et donc si c'est le moment de tourner
                                if ((apparition == "left" || apparition == "right") && (voiture.Direction != "right" || voiture.Direction != "left"))
                                {
                                    if (voiture.Direction == "bot" && left == 370)
                                        voiture.tourner();
                                    else if (voiture.Direction == "top" && left == 440)
                                        voiture.tourner();
                                }
                                if ((apparition == "top" || apparition == "bot") && (voiture.Direction != "bot" || voiture.Direction != "top"))
                                {
                                    if (voiture.Direction == "right" && top == 440)
                                        voiture.tourner();
                                    else if (voiture.Direction == "left" && top == 370)
                                        voiture.tourner();
                                }
                                //
                            }
                            else
                            {
                                // si la voiture a tourné, il n'y aura plus de changement de direction, donc on fait avancer la voiture dans sa direction, sans se poser de question
                                if (voiture.Direction == "right")
                                    voiture.CoordonneesApparition[0] += vitesse;
                                if (voiture.Direction == "left")
                                    voiture.CoordonneesApparition[0] -= vitesse;
                                if (voiture.Direction == "bot")
                                    voiture.CoordonneesApparition[1] += vitesse;
                                if (voiture.Direction == "top")
                                    voiture.CoordonneesApparition[1] -= vitesse;
                            }
                        }

                        //supprime l'objet voiture s'il est sortie de la route
                        supprimerVoiture(voiture);
                    }
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

            if (voiture.CoordonneesApparition[0] > 850 || voiture.CoordonneesApparition[0] < -80 || voiture.CoordonneesApparition[1] < -80 || voiture.CoordonneesApparition[1] > 850)
            {
                voitureList.Remove(voiture);
            }

        }
    }
}