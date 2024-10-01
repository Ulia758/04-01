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
    /// Логика взаимодействия для Polzovatel.xaml
    /// </summary>
    public partial class Polzovatel : Page
    {
        public Polzovatel()
        {
            InitializeComponent();
            
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddPolzovatel(null));
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delClients = Polz.SelectedItems.Cast<Polzovateli>().ToList();
            foreach (var delClient in delClients)
                if (Connect.context.Vidannoe_oborudovanie.Any(x => x.id_user == delClient.id_user))
                {
                    MessageBox.Show("Данные используются в другой таблице", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBox.Show($"Удалить {delClients.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connect.context.Polzovateli.RemoveRange(delClients);
            }
            try
            {
                Connect.context.SaveChanges();
                Polz.ItemsSource = Connect.context.Polzovateli.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
        private void textChang(object sender, TextChangedEventArgs e)
        {
            Polz.ItemsSource = Connect.context.Polzovateli.Where(x => x.surname.StartsWith(Poisk.Text)).ToList();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddPolzovatel((sender as Button).DataContext as Polzovateli));
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Polz.ItemsSource = Connect.context.Polzovateli.ToList();
        }
    }
}
