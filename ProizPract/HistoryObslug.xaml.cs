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
    /// Логика взаимодействия для HistoryObslug.xaml
    /// </summary>
    public partial class HistoryObslug : Page
    {
        public HistoryObslug()
        {
            InitializeComponent();
        }
        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            var s = Connect.context.Istoriya_obslugivaniya.ToList();
            IstObslug.ItemsSource = s.OrderBy(x => x.data_obslug).ToList();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddHistoryObslug(null));
        }
        private void Delet_Click(object sender, RoutedEventArgs e)
        {
            var delClients = IstObslug.SelectedItems.Cast<Istoriya_obslugivaniya>().ToList();
            if (MessageBox.Show($"Удалить{delClients.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Connect.context.Istoriya_obslugivaniya.RemoveRange(delClients);
            try
            {
                Connect.context.SaveChanges();
                IstObslug.ItemsSource = Connect.context.Vidannoe_oborudovanie.ToList();
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
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            app.DisplayAlerts = false;
            Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
            sheet.Name = "История обслуживания";
            sheet.Cells[1, 1] = "Код записи";
            sheet.Cells[1, 2] = "Наименование оборудования";
            sheet.Cells[1, 3] = "Дата обслуживания";
            sheet.Cells[1, 4] = "Примечания";
            sheet.Cells.ColumnWidth = 30;
            sheet.Cells[1,1].ColumnWidth = 10;
            var currentRow = 2;
            var s = Connect.context.Istoriya_obslugivaniya.Select(x =>
            new
            {
                Istoriya_obslugivaniya = x,
                Oborudovanie = x.Oborudovanie,

            }).ToList();
            foreach (var item in s)
            {
                sheet.Cells[currentRow, 1] = item.Istoriya_obslugivaniya.id_zapisi;
                sheet.Cells[currentRow, 2] = item.Oborudovanie.naimenovanie;
                sheet.Cells[currentRow, 3] = item.Istoriya_obslugivaniya.data_obslug;
                sheet.Cells[currentRow, 4] = item.Istoriya_obslugivaniya.primechaniya;
                currentRow++;
            }
            try
            {
                sheet.SaveAs("C:\\Users\\Public\\Downloads\\istor_obslug.xlsx");
            }
            catch
            {
                MessageBox.Show("Не удаётся получить путь к файлу", "Ошибка");
            }
        }
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddHistoryObslug((sender as Button).DataContext as Istoriya_obslugivaniya));
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            IstObslug.ItemsSource = Connect.context.Istoriya_obslugivaniya.ToList();
        }
    }
}
