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

namespace SimulationInterface
{
    /// <summary>
    /// Logique d'interaction pour InfoMcFix.xaml
    /// </summary>
    public partial class InfoMcFix : Window
    {
        int alg;
        int type;
        NavigationWindow n = new NavigationWindow();
        public InfoMcFix()
        {
            InitializeComponent();
            NavigationWindow n = new NavigationWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e )
        {
           
            int tai;int cnt=0;
            if (taimem.Text != "")
            {
            tai = Convert.ToInt32(taimem.Text);
            List<int> prt_tai = new List<int>();
            foreach (TextBox b in GD.Children)
            {
                prt_tai.Add(Convert.ToInt32(b.Text));
                    cnt += (Convert.ToInt32(b.Text));
            }
            if(tai==cnt) this.Close();SimulationInterface.Page1 mainwin = new SimulationInterface.Page1(prt_tai, tai,type,alg);

           
            
            }
            

           //mainwin.vis();
            
        }

       

        private void nbpart_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(nbpart.Text != "")
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

    }
}
