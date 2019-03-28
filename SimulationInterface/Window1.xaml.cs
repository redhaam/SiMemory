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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(int k)
        {
            InitializeComponent();
            int a = 0;
            while (a < k)
            {
                TextBox t = new TextBox();
                t.Width = 252;
                t.Height = 25;
                SP.Children.Add(t);
                a++;
            }
            //this.Show();
        }
        private void valider_Click(object sender, RoutedEventArgs e)
        {
           /* List<int> prt_tai = new List<int>();
            StackPanel p = SP;
            int cmp = 0;
            foreach (TextBox b in p.Children)
            {
                prt_tai.Add(Convert.ToInt32(b.Text));
                cmp += Convert.ToInt32(b.Text);
            }

            SimulationInterface.Page1 mainwin = new SimulationInterface.Page1(prt_tai,cmp);
            //mainwin.Show();
            win1.Close();*/
        }
    }
}
