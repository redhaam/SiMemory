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
using System.Windows.Media.Animation;
using System.Text.RegularExpressions;

namespace SimulationInterface
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 

    public partial class Page1 : Page
    {
        static int un_plus ;
        static process_list proc ;
        int param;
        static RAM_fix m;
        static bool synchron = true;
        List<string> Derl = new List<string>();

        static int choix_process = 2;
        System.Windows.Threading.DispatcherTimer messageTimer = new System.Windows.Threading.DispatcherTimer();
        Color timeclr = new Color();
        public Page1()
        {

            InitializeComponent();
            InfoMcFix windowInfo = new InfoMcFix();
            windowInfo.Show();

        }
        public Page1(List<int> l,int cap,int un_p,int alg)
        {

            InitializeComponent();
            m = new RAM_fix(l,cap);
            proc = new process_list();
            un_plus = un_p;
            proc.Set_alg(alg);
            



        }
        public void vis()
        {
              voir.Visibility=Visibility.Visible;
            
        }


        private void btn_sim_Click(object sender, RoutedEventArgs e)
        {
            SimulationInterface.InfoMcFix win1 = new SimulationInterface.InfoMcFix ();
            win1.Show();
        }
        
        private void sim_Click(object sender, RoutedEventArgs e)
        {
            /*while (t != -1)
            {
                processus pro = proc.fifo[0];
                t = 0;

                switch (n)
                {
                    case 1:
                        t = m.Rech_first(pro);
                        break;
                    case 2:
                        t = m.Rech_best(pro);
                        break;
                    case 3:
                        t = m.Rech_worst(pro);
                        break;
                }
                if (t != -1)
                {
                    if (m.list_rep[t].G_vide() == true)
                    {
                        pro.Set_etat(1);
                        pro.Set_part(t);
                        proc.en_cours.Add(pro);
                        m.occ_part(m.list_rep[t],pro ,pro.Get_taille());
                        proc.fifo.Remove(proc.fifo[0]);
                    }
                }
                else { Sta.Children.Clear(); g.CreateACircle(proc.fifo, cn); g.lilster_pro(Sta, proc, choix_process); }
            }*/

            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = TimeSpan.FromMilliseconds(3000);
            messageTimer.Start();



        }
        int T = 1;
        void messageTimer_Tick(object sender, EventArgs e)
        {

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
                DoubleAnimation db2 = new DoubleAnimation(0, 100, TimeSpan.FromMilliseconds(2000));
                timeE.BeginAnimation(Ellipse.StrokeThicknessProperty, db2);
                // Apply the animation to the brush's Color property.
                timeE.Stroke.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);

                //
                ColorAnimation textanime = new ColorAnimation(Colors.Black, Colors.White, TimeSpan.FromMilliseconds(1000));
                DoubleAnimation db = new DoubleAnimation(100 - m.calcul_esplibre(), TimeSpan.FromMilliseconds(1000));
                taille_lib.Text = m.calcul_esplibre().ToString() + "%";
                frag.Text = "l'espace libre fragmenté  : " + m.frag;
                int vitfe = 0; int ttl = 0;
                while (vitfe < m.list_zone_libre.Count())
                {
                    ttl += m.list_zone_libre[vitfe] / 1000;
                    vitfe++;
                }
                libt.Text = "l'espace utilisable  : " + ttl;
                esp_libre.BeginAnimation(Ellipse.StrokeThicknessProperty, db);
                //
                DoubleAnimation db1 = new DoubleAnimation(100 - proc.calcul_en_cours(), TimeSpan.FromMilliseconds(1000));
                cours.BeginAnimation(Rectangle.HeightProperty, db1);
                en_courstext.Text = proc.en_cours.Count.ToString();
                Canvas.SetTop(en_courstext, 135 - proc.calcul_en_cours());
                //les state de la fifo
                DoubleAnimation db3 = new DoubleAnimation(100 - proc.calcul_fifo(), TimeSpan.FromMilliseconds(1000));
                hover.BeginAnimation(Rectangle.HeightProperty, db3);
                fifotext.Text = proc.fifo.Count.ToString();
                Canvas.SetTop(fifotext, 135 - proc.calcul_fifo());
                //
                DoubleAnimation db4 = new DoubleAnimation(100 - proc.calcul_finis(), TimeSpan.FromMilliseconds(1000));
                finishov.BeginAnimation(Rectangle.HeightProperty, db4);
                finistext.Text = proc.get_finis().ToString();
                Canvas.SetTop(finistext, 135 - proc.calcul_finis());


            });
            Application.Current.Dispatcher.Invoke((Action)delegate { cn.Children.Clear(); Sta.Children.Clear(); });
            Application.Current.Dispatcher.Invoke((Action)delegate {
                if (m != null)
                {
                if (un_plus == 0) proc.CreateACircle_2(m, cn);
                else proc.CreateACircle(proc.fifo, cn);


                m.afficher(cn, proc);
                    /*proc.lilster_pro(Sta, choix_process);*/
                    proc.afficher_encours(Sta, choix_process);
                }  
                
            });
            if (un_plus == 0) proc.plusieurs_fifo(m);
            else proc.une_file_rnd(m,Derl);


        }


        private void Add_Pro_Click(object sender, RoutedEventArgs e)
        {
            if (m != null )
            {
                TextBox a = Taille;
                TextBox b = Delay;
                int T = 1;
                if ((a.Text != "") && (b.Text != "") && (b.Text != "0") &&(a.Text != "0"))
                {
                    T = Convert.ToInt32(a.Text);
                    int D = 1;
                    if (b.Text != "") D = Convert.ToInt32(b.Text);

                    cn.Children.Clear();
                    if (un_plus == 0)
                    {
                        proc.add_process(T, D, m);
                        proc.CreateACircle_2(m, cn);
                    }
                    else
                    {
                        proc.Add_Fifo(T, D); proc.CreateACircle(proc.fifo, cn);
                    }
                    m.afficher(cn, proc);
                    Sta.Children.Clear();
                    proc.afficher_encours(Sta, choix_process);
                }
                else
                {
                    Error.Text= "Un champ est vide ou égal à zéro!"; }
                }
            
            else { Error.Text = "ATTENTION : Initializez la Memoire !"; }
                



        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            param = 1;
            Sta.Children.Clear();
            proc.afficher_encours(Sta, param);
            ALL.Background = new SolidColorBrush(Color.FromRgb(138, 193, 72));
            en_cours.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            en_attente.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            finis.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));

        }

        private void En_cours_Click(object sender, RoutedEventArgs e)
        {
            param = 2;
            Sta.Children.Clear();
            proc.afficher_encours(Sta, param);
            en_cours.Background = new SolidColorBrush(Color.FromRgb(138, 193, 72));
            ALL.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            en_attente.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            finis.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
        }

        private void En_attente_Click(object sender, RoutedEventArgs e)
        {
            param = 3;
            Sta.Children.Clear();
            proc.afficher_encours(Sta, param);
            en_attente.Background = new SolidColorBrush(Color.FromRgb(138, 193, 72));
            en_cours.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            ALL.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            finis.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
        }

        private void Finis_Click(object sender, RoutedEventArgs e)
        {
            param = 4;
            Sta.Children.Clear();
            proc.afficher_encours(Sta, param);
            finis.Background = new SolidColorBrush(Color.FromRgb(138, 193, 72));
            en_cours.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            en_attente.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
            ALL.Background = new SolidColorBrush(Color.FromRgb(183, 183, 183));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            choix_process = 3;
            Sta.Children.Clear();
            //proc.lilster_pro(Sta, 0);*/
            proc.afficher_encours(Sta, 3);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            choix_process = 2;
            Sta.Children.Clear();
            // proc.lilster_pro(Sta, 1);*/
            proc.afficher_encours(Sta, 2);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            choix_process = 1;
            Sta.Children.Clear();
            /*proc.lilster_pro(Sta, 0);
            proc.lilster_pro(Sta, 1);*/
            proc.afficher_encours(Sta, 1);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            choix_process = 4;
            Sta.Children.Clear();
            proc.afficher_encours(Sta, 4);
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            messageTimer.Stop();
        }

        private void voir_Click(object sender, RoutedEventArgs e)
        {
            cn.Children.Clear();
            m.afficher(cn, proc);
            
        }

        private void voir_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void av_Click(object sender, RoutedEventArgs e)
        {
            synchron = false;
            if (Convert.ToInt32(num_text.Text) < T - 1)
            {
                num_text.Text = (Convert.ToInt32(num_text.Text) + 1).ToString();
                derlo.Text = Derl[Convert.ToInt32(num_text.Text)];
            }
        }

        private void rt_Click(object sender, RoutedEventArgs e)
        {
            synchron = false;
            if (Convert.ToInt32(num_text.Text) > 0)
            {
                num_text.Text = (Convert.ToInt32(num_text.Text) - 1).ToString();
                derlo.Text = Derl[Convert.ToInt32(num_text.Text)];
            }
        }

        private void Taille_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Delay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }


}
