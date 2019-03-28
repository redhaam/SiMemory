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

namespace SimulationInterface
{
    /// <summary>
    /// Logique d'interaction pour ListAlgoFix.xaml
    /// </summary>
    public partial class ListAlgoFix : Window
    {
        public ListAlgoFix()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // changement d'algorithme
            this.Close();
            
        }
    }
}
