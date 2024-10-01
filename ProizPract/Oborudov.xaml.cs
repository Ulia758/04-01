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
    /// Логика взаимодействия для Oborudov.xaml
    /// </summary>
    public partial class Oborudov : Page
    {
        public Oborudov()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddOborudov(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var delClients = Oborud.SelectedItems.Cast<Oborudovanie>().ToList();
            foreach (var delClient in delClients)
                if (Connect.context.Characteristiki_oborudovania.Any(x => x.id_oborud == delClient.id_oborud)|| Connect.context.Vidannoe_oborudovanie.Any(x => x.id_oborud == delClient.id_oborud)|| Connect.context.Istoriya_obslugivaniya.Any(x => x.id_oborud == delClient.id_oborud))
                {
                    MessageBox.Show("Данные используются в другой таблице", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBox.Show($"Удалить {delClients.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connect.context.Oborudovanie.RemoveRange(delClients);
            }
            try
            {
                Connect.context.SaveChanges();
                Oborud.ItemsSource = Connect.context.Oborudovanie.ToList();
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

        private void textChange(object sender, TextChangedEventArgs e)
        {
            Oborud.ItemsSource = Connect.context.Oborudovanie.Where(x => x.naimenovanie.StartsWith(Poisk.Text)).ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddOborudov((sender as Button).DataContext as Oborudovanie));
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Oborud.ItemsSource = Connect.context.Oborudovanie.ToList();
        }
    }
}
