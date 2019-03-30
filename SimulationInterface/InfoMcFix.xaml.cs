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
using System.Windows.Navigation;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;

namespace SimulationInterface
{
    /// <summary>
    /// Logique d'interaction pour InfoMcFix.xaml
    /// </summary>
    public partial class InfoMcFix : Window
    {
        int alg;
        int type = 2;
        NavigationWindow n = new NavigationWindow();
        public InfoMcFix()
        {
            InitializeComponent();
            NavigationWindow n = new NavigationWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int tai; int cnt = 0; Boolean err = false;
            if ((taimem.Text != "") && (TypeFile.Text != "") && (type <= 1) && (nbpart.Text != ""))
            {
                tai = Convert.ToInt32(taimem.Text);
                List<int> prt_tai = new List<int>();
                foreach (TextBox b in GD.Children)
                {
                    if (b.Text == "")
                    {
                        err = true;
                    }

                }
                foreach (TextBox b in GD.Children)
                {
                    if (b.Text != "")
                    {
                        prt_tai.Add(Convert.ToInt32(b.Text));
                        cnt += (Convert.ToInt32(b.Text));
                    }

                }
                if (tai == cnt) this.Close();
                else
                {
                    Error.Text = "La somme des tailles des partitions doit être égale à la taille de la mémoire !";


                }
                if (!err)
                {
                    SimulationInterface.Page1 mainwin = new SimulationInterface.Page1(prt_tai, tai, type, alg);
                }


            }
            else
            {
                Error.Text = "Un champ est vide";

            }



            //mainwin.vis();

        }



        private void nbpart_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nbpart.Text != "")
            {
                int a = 0;
                int pos = 10, pas = 600 / Convert.ToInt32(nbpart.Text);
                GD.Children.Clear();

                while (a < Convert.ToInt32(nbpart.Text))
                {
                    TextBox t = new TextBox();
                    t.Width = 600 / Convert.ToInt32(nbpart.Text) - 30;
                    t.Height = 25;
                    GD.Children.Add(t);
                    Canvas.SetLeft(t, pos);
                    a++;
                    pos += pas;
                }
            }

        }


        private void unef_Selected(object sender, RoutedEventArgs e)
        {
            type = 1;
        }
        private void plusf_Selected(object sender, RoutedEventArgs e)
        {
            type = 0;
        }

        private void first_Selected(object sender, RoutedEventArgs e)
        {
            alg = 1;
        }

        private void next_Selected(object sender, RoutedEventArgs e)
        {
            alg = 4;
        }

        private void best_Selected(object sender, RoutedEventArgs e)
        {
            alg = 2;
        }

        private void worst_Selected(object sender, RoutedEventArgs e)
        {
            alg = 3;
        }

        private void Taimem_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void Nbpart_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }


    }
}
