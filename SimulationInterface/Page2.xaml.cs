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
using System.Timers;
using System.Windows.Media.Animation;

namespace SimulationInterface
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        
        //private static System.Timers.Timer aTimer;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        enum Algo { First_Fit, Best_Fit, Worst_Fit, Next_Fit };
        int param;
        bool synchron = true;
        Type AlgoR = typeof(Algo);
        RAM_var mem = new RAM_var(1000);
        int P;// les parametre de l algo choisis par l utilisateur
        process_list pro = new process_list();
        int T = 0;
        Color timeclr = new Color();
        List<string> Derl = new List<string>();

        public Page2()
        {

            InitializeComponent();


            name.Text = "P1";
            //text.Foreground =new SolidColorBrush(Colors.Black);
            text.Text = T++.ToString();
            Tai.Text = "La taille de la RAM :      " + (mem.capacite * 1.1).ToString() + " Mo";



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((temp.Text != null) && (taille.Text != null))
            {
                int ind, f = 0;
                try
                {

                    f = Convert.ToInt32(taille.Text);
                    if (f > mem.capacite) throw new Exception();
                    if (f < 0) throw new FormatException();
                }
                catch (FormatException)
                {
                    Error.Text = "Taille Devrait etre un entier positive";
                    
                    ColorAnimation myColorAnimation = new ColorAnimation(Colors.Red, Color.FromRgb(241, 239, 239), TimeSpan.FromMilliseconds(4000));
                    Error.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                    return;
                }
                catch (Exception)
                {
                    Error.Text = "Taille plus grand que la capacité max de la mémoire";
                   
                    ColorAnimation myColorAnimation = new ColorAnimation(Colors.Red, Color.FromRgb(241, 239, 239), TimeSpan.FromMilliseconds(4000));
                    Error.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                    return;
                }
                finally
                {
                    ColorAnimation myColorAnimation = new ColorAnimation(Color.FromRgb(0, 255, 184), TimeSpan.FromMilliseconds(4000));
                    
                }

                if (f < mem.capacite)
                {
                    /*taillebar.Fill = new SolidColorBrush(Color.FromRgb(10, 201, 1));
					tempbar.Fill = new SolidColorBrush(Color.FromRgb(10, 201, 1));*/
                    try
                    {
                        if ((Convert.ToInt32(temp.Text)) < 0) throw new FormatException();
                        pro.ajout_process(name.Text, Convert.ToInt32(taille.Text), Convert.ToInt32(temp.Text) + 1);
                    }
                    catch (ArgumentNullException)
                    {
                        Error.Text = "ArgumentNullException";

                        return;
                    }
                    catch (ArithmeticException)
                    {
                        Error.Text = "Invalide type of taille or temp";
                        return;
                    }
                    catch (FormatException)
                    {
                        Error.Text = "Temp devrait etre un entier positive";
                        ColorAnimation myColorAnimation = new ColorAnimation(Colors.Red, Colors.Gray, TimeSpan.FromMilliseconds(2000));
                        Error.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                        return;
                    }



                    if (T != 0)
                    {
                        Derl.Add("");
                        switch (P)
                        {
                            default:
                                ind = mem.firts_fit(Convert.ToInt32(taille.Text));
                                break;
                            case 0:
                                ind = mem.firts_fit(Convert.ToInt32(taille.Text));
                                break;
                            case 1:
                                ind = mem.best_fit(Convert.ToInt32(taille.Text));
                                break;

                            case 2:
                                ind = mem.worst_fit(Convert.ToInt32(taille.Text));
                                break;
                            case 3:
                                ind = mem.Next_fit(Convert.ToInt32(taille.Text));
                                break;



                        }

                        if (ind >= 0)
                        {
                            Derl[T - 1] += ">>>> le processus <" + name.Text + ">  est ajouter a lemplacement   <" + ind + "> Avec l'algorihtme de:  <<" + Enum.GetValues(AlgoR).GetValue(P) + ">>\n\n";
                            pro.corriger_prt(ind, mem.allocation_process(ind, pro.fifo.Count + pro.en_cours.Count, Convert.ToInt32(taille.Text))); pro.execute_process(pro.fifo.Count + pro.en_cours.Count, ind);
                        }
                    }
                    name.Text = "P" + (pro.fifo.Count + pro.en_cours.Count + 1).ToString();
                }
                else
                {
                    /*taillebar.Fill = new SolidColorBrush(Color.FromRgb(201, 10, 1));
					tempbar.Fill = new SolidColorBrush(Color.FromRgb(201, 10, 1));*/
                }
            }


            System.Timers.Timer timer = new System.Timers.Timer(500);
            timer.Start();
            timer.Elapsed += delegate (object sender1, ElapsedEventArgs ev) { Change_color(sender1, ev); };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            int ind = 0, i = 0;
            while (i < pro.fifo.Count)
            {
                Derl.Add("\n");
                switch (P)
                {

                    case 0:
                        ind = mem.firts_fit(pro.fifo[i].Get_taille());
                        break;
                    case 1:
                        ind = mem.best_fit(pro.fifo[i].Get_taille());
                        break;

                    case 2:
                        ind = mem.worst_fit(pro.fifo[i].Get_taille());
                        break;
                    default:
                        ind = mem.firts_fit(pro.fifo[i].Get_taille());
                        break;


                }

                if (ind >= 0)
                {

                    Derl[0] += ">>>> le processus" + pro.fifo[i].name + "est ajouter a lemplacement " + ind + "\n";

                    pro.corriger_prt(ind, mem.allocation_process(ind, pro.fifo[i].Get_id(), pro.fifo[i].Get_taille())); pro.execute_process(pro.fifo[i].Get_id(), ind);
                }
                else i++;
            }
            DoubleAnimation db = new DoubleAnimation(48, 0, TimeSpan.FromMilliseconds(4000));
            titre.BeginAnimation(TextBlock.HeightProperty, db);
            exp.BeginAnimation(TextBlock.HeightProperty, db);



            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000 + speed.Value * 1000);
            num_text.Text = "0";

            dispatcherTimer.Tick += new EventHandler(OnTimedEvent);

            dispatcherTimer.Start();




        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        public void OnTimedEvent(Object source, EventArgs e)
        {

            int i = -1;
            Application.Current.Dispatcher.BeginInvoke((Action)delegate {
                if (synchron == true) num_text.Text = (T - 1).ToString();
                Derl.Add("");
                derlo.Text = Derl[Convert.ToInt32(num_text.Text)];
                text.Text = T++.ToString();
                ColorAnimation myColorAnimation = new ColorAnimation();
                //myColorAnimation.From = Colors.Cyan;
                Random r = new Random();
                timeclr.R = (byte)r.Next(0, 50);
                timeclr.G = (byte)r.Next(150, 256);
                timeclr.B = (byte)r.Next(120, 200);
                myColorAnimation.To = Color.FromRgb(timeclr.R, timeclr.G, timeclr.B);
                myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(3000));
                //
                DoubleAnimation db2 = new DoubleAnimation(0, 50, TimeSpan.FromMilliseconds(2000));
                timeE.BeginAnimation(Ellipse.StrokeThicknessProperty, db2);
                // Apply the animation to the brush's Color property.
                timeE.Stroke.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);

                //
                ColorAnimation textanime = new ColorAnimation(Colors.Black, Colors.White, TimeSpan.FromMilliseconds(1000));
                DoubleAnimation db = new DoubleAnimation((mem.capacite - mem.calcul_esplibre()) * 100 / mem.capacite / 2, TimeSpan.FromMilliseconds(1000));
                taille_lib.Text = (mem.calcul_esplibre() * 100 / mem.capacite).ToString() + "%";
                esp_libre.BeginAnimation(Ellipse.StrokeThicknessProperty, db);
                //
                DoubleAnimation db1 = new DoubleAnimation(100 - pro.calcul_en_cours(), TimeSpan.FromMilliseconds(1000));
                cours.BeginAnimation(Rectangle.HeightProperty, db1);
                en_courstext.Text = pro.en_cours.Count.ToString();
                Canvas.SetTop(en_courstext, 135 - pro.calcul_en_cours());
                //les state de la fifo
                DoubleAnimation db3 = new DoubleAnimation(100 - pro.calcul_fifo(), TimeSpan.FromMilliseconds(1000));
                hover.BeginAnimation(Rectangle.HeightProperty, db3);
                fifotext.Text = pro.fifo.Count.ToString();
                Canvas.SetTop(fifotext, 135 - pro.calcul_fifo());
                //
                DoubleAnimation db4 = new DoubleAnimation(100 - pro.calcul_finis(), TimeSpan.FromMilliseconds(1000));
                finishov.BeginAnimation(Rectangle.HeightProperty, db4);
                finistext.Text = pro.get_finis().ToString();
                Canvas.SetTop(finistext, 135 - pro.calcul_finis());


            });
            Application.Current.Dispatcher.Invoke((Action)delegate { ram1.Children.Clear(); Sta.Children.Clear(); });
            string d = "";
            while (true)
            {
                i++;
                if (i >= pro.en_cours.Count) break;
                pro.en_cours[i].Set_time(pro.en_cours[i].Get_temps() - 1);
                if (pro.en_cours[i].Get_temps() == 0)
                {
                    Derl[T - 1] += ">>>> le processus <" + pro.en_cours[i].name + "> est libere de lemplacement<" + pro.en_cours[i].Get_part() + ">\n\n";
                    pro.corriger_prt(pro.en_cours[i].Get_part(), mem.Liberation_part(pro.en_cours[i].Get_part()));
                    pro.finish(pro.en_cours[i].Get_id());
                    pro.check_fifo(mem, ref d, P);
                    Derl[T - 1] += d;
                    i--;
                }

            }
            Application.Current.Dispatcher.Invoke((Action)delegate { mem.afficher(ram1, pro); pro.afficher_fifo(ram1); pro.afficher_encours(Sta, param); });

        }
        public void Change_color(Object source, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                /*taillebar.Fill = new SolidColorBrush(Color.FromRgb(188, 188, 191));
				tempbar.Fill = new SolidColorBrush(Color.FromRgb(188, 188, 191));*/
            });
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            synchron = false;
            if (Convert.ToInt32(num_text.Text) < T - 1)
            {
                num_text.Text = (Convert.ToInt32(num_text.Text) + 1).ToString();
                derlo.Text = Derl[Convert.ToInt32(num_text.Text)];
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            synchron = false;
            if (Convert.ToInt32(num_text.Text) > 0)
            {
                num_text.Text = (Convert.ToInt32(num_text.Text) - 1).ToString();
                derlo.Text = Derl[Convert.ToInt32(num_text.Text)];
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       private void Choix_Click(object sender, RoutedEventArgs e)
        {
            P = algo.SelectedIndex;
            switch (P)
            {

                case 0:
                    Algorithme.Text = "First Fit";
                    break;
                case 1:
                    Algorithme.Text = "Best Fit";
                    break;

                case 2:
                    Algorithme.Text = "Worst Fit";
                    break;
                default:
                    Algorithme.Text = "First Fit";
                    break;


            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            param = 1;
            Sta.Children.Clear();
            pro.afficher_encours(Sta, param);
            ALL.Background = new SolidColorBrush(Color.FromRgb(138, 193, 72));
            en_cours.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            en_attente.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            finis.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));

        }

        private void En_cours_Click(object sender, RoutedEventArgs e)
        {
            param = 2;
            Sta.Children.Clear();
            pro.afficher_encours(Sta, param);
            en_cours.Background = new SolidColorBrush(Color.FromRgb(138, 193, 72));
            ALL.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            en_attente.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            finis.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
        }

        private void En_attente_Click(object sender, RoutedEventArgs e)
        {
            param = 3;
            Sta.Children.Clear();
           pro.afficher_encours(Sta, param);
            en_attente.Background = new SolidColorBrush(Color.FromRgb(138, 193, 72));
            en_cours.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            ALL.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            finis.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
        }

        private void Finis_Click(object sender, RoutedEventArgs e)
        {
            param = 4;
            Sta.Children.Clear();
           pro.afficher_encours(Sta, param);
            finis.Background = new SolidColorBrush(Color.FromRgb(138, 193, 72));
            en_cours.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            en_attente.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            ALL.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
        }

        private void Synchro_Click(object sender, RoutedEventArgs e)
        {
            synchron = true;


        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
        }
    }

}

