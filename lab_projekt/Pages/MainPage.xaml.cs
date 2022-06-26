﻿using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public class Header
        {
            public string Plate { get; set; }
            public string BrandModel { get; set; }
            public string VIN { get; set; }
            public string Driver { get; set; }
            public string DriverPhone { get; set; }

        }
        public class Repairs
        {
            public string Name { get; set; }
            //public string Date { get; set; }
        }
        public MainPage()
        {
            InitializeComponent();
            BuildHeader();
        }
        private void BuildHeader()
        {
            var list = new ObservableCollection<Header>();

            using (ProjektDbContext db = new ProjektDbContext())
            {
                var drivers = db.Trucks
                        .Include(b => b.Driver)
                        .ToList();
                var trucks = from t in db.Trucks
                             select t;

                foreach (var item in trucks)
                {

                    list.Add(new Header() { Plate = item.Plate, BrandModel = $"{item.Brand} {item.Model}", VIN = item.VIN, Driver = $"{item.Driver.Firstname} {item.Driver.Lastname}", DriverPhone = item.Driver.Phone });
                }

                dataGrid1.ItemsSource = list;

                //var repairs = db.Trucks.Include(x => x.Repairs).ToList();
            }
        }
        public void UpdateHeader()
        {
            BuildHeader();
        }
        void OnClick1(object sender, RoutedEventArgs e)
        {
            AddTruck.Visibility = Visibility.Visible;
            Grid.Visibility = Visibility.Hidden;
            btn1.IsEnabled = false;
            btn2.IsEnabled = false;
            using (ProjektDbContext db = new ProjektDbContext())
            {
                var drivers = from d in db.Drivers
                              select d;
                foreach (var d in drivers)
                {
                    ComboBox.Items.Add($"{d.Firstname} {d.Lastname}");
                }
            }
        }
        void OnClick2(object sender, RoutedEventArgs e)
        {

        }
        void OnClick3(object sender, RoutedEventArgs e)
        {
            AddTruck.Visibility = Visibility.Hidden;
            Grid.Visibility = Visibility.Visible;
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
            using (ProjektDbContext db = new ProjektDbContext())
            {
                string[] c = ComboBox.SelectedItem.ToString().Split(' ');

                var id = (from d in db.Drivers where d.Firstname == c[0] && d.Lastname == c[1] select d);

                foreach (var d in id)
                {
                    db.Trucks.Add(new Truck() { Plate = Plate.Text, Brand = Brand.Text, Model = Model.Text, VIN = VIN.Text, DriverId = d.Id, Insurance = DateTime.Now, TechReview = DateTime.Now, TachoLeg = DateTime.Now });

                }
                db.SaveChanges();
            }
            BuildHeader();
        }
        void OnClick4(object sender, RoutedEventArgs e)
        {
            AddTruck.Visibility = Visibility.Hidden;
            Grid.Visibility = Visibility.Visible;
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
        }
        void OnClick5(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }

        void Context_Details(object sender, RoutedEventArgs e)
        {
        }
        void Context_Delete(object sender, RoutedEventArgs e)
        {
        }

    }
}
