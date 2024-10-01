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
    /// Логика взаимодействия для Characterist.xaml
    /// </summary>
    public partial class Characterist : Page
    {
        public Characterist()
        {
            InitializeComponent();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddCharacterist(null));
        }
        private void Delet_Click(object sender, RoutedEventArgs e)
        {
            var delClients = Characteris.SelectedItems.Cast<Characteristiki_oborudovania>().ToList();
            if (MessageBox.Show($"Удалить{delClients.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Connect.context.Characteristiki_oborudovania.RemoveRange(delClients);
            try
            {
                Connect.context.SaveChanges();
                Characteris.ItemsSource = Connect.context.Vidannoe_oborudovanie.ToList();
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
            sheet.Name = "Характеристики оборудования";
            sheet.Cells[1, 1] = "Код характеристики";
            sheet.Cells[1, 2] = "Наименование оборудования";
            sheet.Cells[1, 3] = "Процессор";
            sheet.Cells[1, 4] = "Оперативная память";
            sheet.Cells[1, 5] = "Жёсткий диск";
            sheet.Cells[1, 6] = "Графический процессор";
            sheet.Cells[1, 7] = "Другое";
            sheet.Cells.ColumnWidth = 30;
            sheet.Cells[1, 1].ColumnWidth = 10;
            var currentRow = 2;
            var s = Connect.context.Characteristiki_oborudovania.Select(x =>
            new
            {
                Characteristiki_oborudovania = x,
                Oborudovanie = x.Oborudovanie,

            }).ToList();
            foreach (var item in s)
            {
                sheet.Cells[currentRow, 1] = item.Characteristiki_oborudovania.id_kateg;
                sheet.Cells[currentRow, 2] = item.Oborudovanie.naimenovanie;
                sheet.Cells[currentRow, 3] = item.Characteristiki_oborudovania.processor;
                sheet.Cells[currentRow, 4] = item.Characteristiki_oborudovania.operativn_pam;
                sheet.Cells[currentRow, 5] = item.Characteristiki_oborudovania.hard_disk;
                sheet.Cells[currentRow, 6] = item.Characteristiki_oborudovania.graf_proc;
                sheet.Cells[currentRow, 7] = item.Characteristiki_oborudovania.drugoe;
                currentRow++;
            }
            try
            {
                sheet.SaveAs("C:\\Users\\Public\\Downloads\\characteristika_obor.xlsx");
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
            Nav.MainFrame.Navigate(new AddCharacterist((sender as Button).DataContext as Characteristiki_oborudovania));
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Characteris.ItemsSource = Connect.context.Characteristiki_oborudovania.ToList();
        }
    }
}
