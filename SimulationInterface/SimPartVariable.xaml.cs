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

namespace SimulationInterface
{
    /// <summary>
    /// Logique d'interaction pour SimPartVariable.xaml
    /// </summary>
    public partial class SimPartVariable : Page
    {
        public SimPartVariable()
        {
            InitializeComponent();
            InfoPartVariable windowInfo = new InfoPartVariable();
            windowInfo.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListAlgoVar windowChoix = new ListAlgoVar();
            windowChoix.Show();
        }
    }
}
