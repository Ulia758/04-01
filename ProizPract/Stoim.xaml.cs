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
using Excel = Microsoft.Office.Interop.Excel;
namespace ProizPract
{
    /// <summary>
    /// Логика взаимодействия для Stoim.xaml
    /// </summary>
    public partial class Stoim : Page
    {
        public Stoim()
        {
            InitializeComponent();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddStoim((sender as Button).DataContext as Stoimost));
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddStoim(null));
        }
        private void Delet_Click(object sender, RoutedEventArgs e)
        {
            var delClients = St.SelectedItems.Cast<Stoimost>().ToList();
            if (MessageBox.Show($"Удалить{delClients.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Connect.context.Stoimost.RemoveRange(delClients);
            try
            {
                Connect.context.SaveChanges();
                St.ItemsSource = Connect.context.Stoimost.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Otchet_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application app = new Excel.Application()
            {
                Visible = true,
                SheetsInNewWorkbook = 1
            };
            Excel.Workbook workbook = app.Workbooks.Add();
            app.DisplayAlerts = false;
            Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
            sheet.Name = "Стоимость оборудования";
            sheet.Cells[1, 1] = "Код стоимости";
            sheet.Cells[1, 2] = "Наименование оборудования";
            sheet.Cells[1, 3] = "Количество";
            sheet.Cells[1, 4] = "Стоимость";
            sheet.Cells[1, 5] = "Сумма";
            var currentRow = 2;
            var s = Connect.context.Stoimost.Select(x =>
            new
            {
                Stoimost = x,
                Oborudovanie = x.Oborudovanie,
                Summ = x.cost * x.count,
               
            }).ToList();
            foreach (var item in s)
            {
                sheet.Cells[currentRow, 1] = item.Stoimost.id_stoim;
                sheet.Cells[currentRow, 2] = item.Oborudovanie.naimenovanie;
                sheet.Cells[currentRow, 3] = item.Stoimost.count;
                sheet.Cells[currentRow, 4] = item.Stoimost.cost;
                sheet.Cells[currentRow, 5] = item.Summ;
                currentRow++;
            }
            try
            {
                sheet.Columns[1].ColumnWidth = 10;
                sheet.Columns[2].ColumnWidth = 20;
                sheet.Columns[3].ColumnWidth = 20;
                sheet.Columns[4].ColumnWidth = 20;
                sheet.Columns[5].ColumnWidth = 20;
                sheet.Cells[currentRow + 1, 5] = "Общая сумма оборудования: ";
                sheet.Cells[currentRow + 1, 7] = "=SUM(E2:E" + (currentRow - 1) + ")";
            }
            catch { 
            }
            

        }
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var selectClients = Connect.context.Stoimost.Select(x =>
            new
            {
                Stoimost = x,
                id_stoim=x.id_stoim,
                id_oborud=x.id_oborud,
                cost=x.cost,
                count=x.count,
                Summ = x.cost * x.count,
               
            }).ToList();
            St.ItemsSource = selectClients;
        }
        private void Zapros_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Filtr());
        }
    }
}
