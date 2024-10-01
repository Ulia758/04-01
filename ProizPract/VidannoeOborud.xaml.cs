using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для VidannoeOborud.xaml
    /// </summary>
    public partial class VidannoeOborud : System.Windows.Controls.Page
    {
        public VidannoeOborud()
        {
            InitializeComponent();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddVidannoeOborud(null));
        }
        private void Delet_Click(object sender, RoutedEventArgs e)
        {
            var delClients = VidanObor.SelectedItems.Cast<Vidannoe_oborudovanie>().ToList();
            if (MessageBox.Show($"Удалить{delClients.Count} записей", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                Connect.context.Vidannoe_oborudovanie.RemoveRange(delClients);
            try
            {
                Connect.context.SaveChanges();
                VidanObor.ItemsSource = Connect.context.Vidannoe_oborudovanie.ToList();
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
            sheet.Name = "Выданное оборудование";
            sheet.Cells[1, 1] = "Код выдачи";
            sheet.Cells[1, 2] = "Наименование оборудования";
            sheet.Cells[1, 3] = "Пользователь";
            sheet.Cells[1, 4] = "Дата выдачи";
            sheet.Cells[1, 5] = "Дата возврата";
            sheet.Cells.ColumnWidth = 30;
            sheet.Cells[1, 1].ColumnWidth = 10;
            var currentRow = 2;
            var s = Connect.context.Vidannoe_oborudovanie.Select(x =>
            new
            {
                Vidannoe_oborudovanie = x,
                Oborudovanie = x.Oborudovanie,
                Polzovateli = x.Polzovateli,
                
            }).ToList();
            foreach (var item in s)
            {
                sheet.Cells[currentRow, 1] = item.Vidannoe_oborudovanie.id_vidachi;
                sheet.Cells[currentRow, 2] = item.Oborudovanie.naimenovanie;
                sheet.Cells[currentRow, 3] = item.Polzovateli.surname;
                sheet.Cells[currentRow, 4] = item.Vidannoe_oborudovanie.data_vidachi;
                sheet.Cells[currentRow, 5] = item.Vidannoe_oborudovanie.data_vozvrata;
                currentRow++;
            }
            try
            {
                sheet.SaveAs("C:\\Users\\Public\\Downloads\\vidannoe obor.xlsx");
            }
            catch {
                MessageBox.Show("Не удаётся получить путь к файлу", "Ошибка");
            }
        }
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddVidannoeOborud((sender as System.Windows.Controls.Button).DataContext as Vidannoe_oborudovanie));
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            VidanObor.ItemsSource = Connect.context.Vidannoe_oborudovanie.ToList();
        }
        private void Zapros_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Zapros());
        }
        private void Filtr_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Zapros());
        }
    }
}
