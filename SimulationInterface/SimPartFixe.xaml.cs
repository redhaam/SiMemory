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
    /// Logique d'interaction pour Page3.xaml
    /// </summary>
    public partial class SimPartFixe : Page
    {
        public SimPartFixe()
        {
            InitializeComponent();
            InfoMcFix windowInfo = new InfoMcFix();
            windowInfo.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListAlgoFix windowChoix = new ListAlgoFix();
            windowChoix.Show();
        }
    }
}
