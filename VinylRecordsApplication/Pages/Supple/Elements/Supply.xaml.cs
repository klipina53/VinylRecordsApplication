﻿using System;
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

namespace VinylRecordsApplication.Pages.Supple.Elements
{
    /// <summary>
    /// Логика взаимодействия для Supply.xaml
    /// </summary>
    public partial class Supply : UserControl
    {
        IEnumerable<Classes.Manufacturer> AllManufacturers = Classes.Manufacturer.AllManufacturers();
        IEnumerable<Classes.Record> AllRecords = Classes.Record.AllRecords();
        Classes.Supple supply;
        Pages.Supply.Main main;

        public Supply(Classes.Supple supply, Pages.Supply.Main main)
        {
            InitializeComponent();
            this.supply = supply;
            this.main = main;
            tbManufacturer.Text = AllManufacturers.Where(x => x.Id == supply.IdManufacturer).First().Name;
            tbRecord.Text = AllRecords.Where(x => x.Id == supply.IdRecord).First().Name;
            tbDateDelivery.Text = CorrectDate(supply.DateDelivery);
            tbCount.Text = supply.Count.ToString();
        }

        private string CorrectDate(string Value)
        {
            return Value.Split(' ')[0];
        }
        private void EditSupply(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Pages.Supple.Add(this.supply));
        }

        private void DeleteSupply(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Удалить поставку №: {this.supply.Id}?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.supply.Delete();
                main.supplyParent.Children.Remove(this);
                MessageBox.Show($"Поставка №{this.supply.Id} успешно удалена.", "Уведомление");
            }
        }
    }
}
