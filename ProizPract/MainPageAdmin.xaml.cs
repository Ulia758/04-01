﻿using System;
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
namespace ProizPract
{
    /// <summary>
    /// Логика взаимодействия для MainPageAdmin.xaml
    /// </summary>
    public partial class MainPageAdmin : Page
    {
        public MainPageAdmin()
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
            Application.Current.Shutdown();
        }
        private void StoimostOborudovan_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Stoim());
        }
        private void BackupDatabase()
        {
            try {
                string connectionString = "Data Source = ULIA\\SQLSERVER;Initial Catalog = UchetObor; Integrated Security = True; Encrypt=False";
                string backupPath = "C:\\Users\\k2510\\UchetObor.bak";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"BACKUP DATABASE UchetObor TO DISK = '{backupPath}'";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Резервная копия базы данных сделана", "Успешно!");
            } 
            catch {
                MessageBox.Show("Не удалось сделать резервную копию данных", "Ошибка");
            }
        }
        private void Cop_Click(object sender, RoutedEventArgs e)
        {
            BackupDatabase();
        }
    }
}
