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
namespace ProizPract
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void Oborudovanie_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Oborudov());
        }
        private void Characteristiki_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Characterist());
        }
        private void Vidannoe_oborudovanie_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new VidannoeOborud());
        }
        private void Istoriya_obslugivaniya_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new HistoryObslug());
        }
        private void Polzovateli_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Polzovatel());
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Avtorizaciya());
        }
        private void StoimostOborudovan_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Stoim());
        }
    }
}
