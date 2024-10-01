﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Логика взаимодействия для AddVidannoeOborud.xaml
    /// </summary>
    public partial class AddVidannoeOborud : Page
    {
        Vidannoe_oborudovanie k;
        public AddVidannoeOborud(Vidannoe_oborudovanie c)
        {
            InitializeComponent();
            if (c == null)
                c = new Vidannoe_oborudovanie();
            DataContext = k = c;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (k.id_vidachi == 0)
            {
                Connect.context.Vidannoe_oborudovanie.Add(k);
            }
            try
            {
                 Connect.context.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Nav.MainFrame.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
    }
}
