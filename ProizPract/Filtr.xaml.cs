using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace ProizPract
{
    /// <summary>
    /// Логика взаимодействия для Filtr.xaml
    /// </summary>
    public partial class Filtr : Page
    {
        public Filtr()
        {
            InitializeComponent();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
        private void V_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ob = Convert.ToInt32(text1.Text);
                var s = Connect.context.Stoimost.Select(x =>
            new
            {
                Stoimost = x,
                id_stoim = x.id_stoim,
                id_oborud = x.id_oborud,
                cost = x.cost,
                count = x.count,
                Summ = x.cost * x.count,
                Oborudovanie = x.Oborudovanie,
            }).Where(x => x.Stoimost.cost >= ob).ToList();
                Filt.ItemsSource = s;
            }
            catch
            {
                MessageBox.Show("Введены неверные данные", "Ошибка");
            }
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var selectClients = Connect.context.Stoimost.Select(x =>
             new
             {
                 Stoimost = x,
                 id_stoim = x.id_stoim,
                 id_oborud = x.id_oborud,
                 cost = x.cost,
                 count = x.count,
                 Summ = x.cost * x.count,

             }).ToList();
            Filt.ItemsSource = selectClients;
        }
    }
 }
