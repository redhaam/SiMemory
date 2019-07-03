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
    /// Logique d'interaction pour Page2.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Page1.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DocPartFix docPartFix = new DocPartFix();
            docPartFix.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Page2.xaml", UriKind.Relative));
        }

        private void Slide1_Intro_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("MemoireVirtuelle.xaml", UriKind.Relative));

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DocPartVar docPartVar = new DocPartVar();
            docPartVar.Show();
        

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            DocPagination doc = new DocPagination();
            doc.Show();
        }
    }
}
