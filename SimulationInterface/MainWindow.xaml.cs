using System;
using MaterialDesignThemes.Wpf.Transitions;
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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            
            InitializeComponent();
            Main.Content = new Accueil();


        }

        private void Button_Click_p1(object sender, RoutedEventArgs e)
        {
            Main.Content = new Accueil();

        }

        private void Button_Click_p2(object sender, RoutedEventArgs e)
        {
            Main.Content = new Menu();
        }

        private void Button_Click_p3(object sender, RoutedEventArgs e)
        {
            Main.Content = new DocumentationFixe();
        }

        private void Button_Click_p4(object sender, RoutedEventArgs e)
        {
            Main.Content = new SimPartFixe();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new SimPartVariable();

        }

        

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Main.Content = new Apropos();

        }

        private void Button_Click_p9(object sender, RoutedEventArgs e)
        {

        }
    }
}
