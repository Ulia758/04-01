using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для Zapros.xaml
    /// </summary>
    public partial class Zapros : Page
    {
        public SqlConnection Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Прошлое\ПП2024 по 11.01\ProizPract\ProizPract\Database1.mdf"";Integrated Security=True");
        public Zapros()
        {
            InitializeComponent();
        }
        private void LoadComboBox()
        {
            string sql = "SELECT o.naimenovanie FROM Vidannoe_oborudovanie vo JOIN Oborudovanie o ON vo.id_oborud = o.id_oborud";
            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {

                cmd.CommandType = System.Data.CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);
                List<string> namesList = new List<string>();
                foreach (DataRow row in table.Rows)
                {
                    namesList.Add(row["naimenovanie"].ToString());
                }
                ZaprBox.ItemsSource = namesList;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComboBox();
        }
        private void Zapr_Click(object sender, RoutedEventArgs e)
        {
            string a = Convert.ToString(ZaprBox.SelectionBoxItem);
            var select = Connect.context.Vidannoe_oborudovanie.Select(x =>
            new
            {
                Vidannoe_oborudovanie = x,
                Oborudovanie = x.Oborudovanie,
                Polzovateli = x.Polzovateli,
            }).Where(x => x.Oborudovanie.naimenovanie == a).ToList();
            VidanObor.ItemsSource = select;
        }
        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
    }
}
