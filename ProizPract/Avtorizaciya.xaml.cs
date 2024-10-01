using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для Avtorizaciya.xaml
    /// </summary>
    public partial class Avtorizaciya : Page
    {
        public Avtorizaciya()
        {
            InitializeComponent();
        }

        private void Vhod_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = passwBox.Password.Trim();
            Polzovateli authUser = null;
            using (Database1Entities d = new Database1Entities())
            {
                authUser = d.Polzovateli.Where(b =>
                b.login == login && b.password == pass).FirstOrDefault();
            }
            if (authUser == null)
            {
                MessageBox.Show("Неверный логин и/или пароль","ERROR");
            }
            else 
            {
                if (login == "gendir" && pass == "111")
                {
                    Nav.MainFrame.Navigate(new MainPageAdmin());
                }
                else Nav.MainFrame.Navigate(new MainPage());
            }
        }
    }
}
