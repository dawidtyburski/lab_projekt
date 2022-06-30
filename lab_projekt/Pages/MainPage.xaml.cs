using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        /// <summary>
        /// Class that stores truck data needed to display the datagrid
        /// </summary>
        public class Header
        {
            public string State { get; set; }
            public string Plate { get; set; }
            public string BrandModel { get; set; }
            public string VIN { get; set; }
            public string Driver { get; set; }
            public string DriverPhone { get; set; }

        }      
        /// <summary>
        /// List of trucks uses data from class Header
        /// </summary>
        public ObservableCollection<Header> list = new ObservableCollection<Header>();
        
        public MainPage()
        {
            InitializeComponent();
            BuildHeader();
        }
        /// <summary>
        /// Downloading data from the database and creating new Header objects and adding them to the list. Loading a data source into a datagrid
        /// </summary>
        public void BuildHeader()
        { 
            using (ProjektDbContext db = new ProjektDbContext())
            {
                var drivers = db.Trucks
                        .Include(b => b.Driver)
                        .ToList();
                var trucks = from t in db.Trucks
                             select t;
                list.Clear();              
                foreach (var item in trucks)
                {
                    string source = null;
                    TimeSpan diff1 = DateTime.Now - Convert.ToDateTime(item.Insurance);
                    int days1 = (int)Math.Abs(Math.Round(diff1.TotalDays));

                    TimeSpan diff2 = DateTime.Now - Convert.ToDateTime(item.TechReview);
                    int days2 = (int)Math.Abs(Math.Round(diff2.TotalDays));

                    TimeSpan diff3 = DateTime.Now - Convert.ToDateTime(item.TachoLeg);
                    int days3 = (int)Math.Abs(Math.Round(diff3.TotalDays));

                    if (days1 < 7 || days2 < 7 || days3 < 7)
                    {
                        source = "/images/warning.png";
                    }
                    else
                        source = "/images/check.png";

                    list.Add(new Header() {State = source, Plate = item.Plate, BrandModel = $"{item.Brand} {item.Model}", VIN = item.VIN, Driver = $"{item.Driver.Firstname} {item.Driver.Lastname}", DriverPhone = item.Driver.Phone });
                }
                dataGrid1.ItemsSource = list;
            }
        }       
        /// <summary>
        /// Reloading list of trucks and ItemSource of datagrid by button 'Odśwież'
        /// </summary>
        void OnClick0(object sender, RoutedEventArgs e)
        {
            BuildHeader();
        }
        /// <summary>
        /// Switching to the add trucks view (AddTruck.xaml) and downloading the list of drivers from the database
        /// </summary>
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
        /// <summary>
        /// Switching to the add drivers view (AddDriver.xaml)
        /// </summary>
        void OnClick2(object sender, RoutedEventArgs e)
        {
            AddDriver.Visibility = Visibility.Visible;
            Grid.Visibility = Visibility.Hidden;
            btn1.IsEnabled = false;
            btn2.IsEnabled = false;
        }     
        /// <summary>
        /// Adding to database a new truck with parameters given by user, checking if given Plate numbers are not exist in db, reload datagrid ItemSource
        /// </summary>
        void OnClick3(object sender, RoutedEventArgs e)
        {         
            using (ProjektDbContext db = new ProjektDbContext())
            {
                string[] c = ComboBox.SelectedItem.ToString().Split(' ');

                var id = (from d in db.Drivers where d.Firstname == c[0] && d.Lastname == c[1] select d);

                if (db.Trucks.Any(o => o.Plate == Plate.Text))
                {
                    var result = MessageBox.Show("Istnieje pojazd o takim nr rejestracyjnym", "wrong_plate", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        Plate.Text = null;
                    }
                }
                else
                {
                    foreach (var d in id)
                    {

                        db.Trucks.Add(new Truck() { Plate = Plate.Text, Brand = Brand.Text, Model = Model.Text, VIN = VIN.Text, DriverId = d.Id, Insurance = DateTime.Now, TechReview = DateTime.Now, TachoLeg = DateTime.Now });
                    }
                    db.SaveChanges();
                    AddTruck.Visibility = Visibility.Hidden;
                    Grid.Visibility = Visibility.Visible;
                    btn1.IsEnabled = true;
                    btn2.IsEnabled = true;

                    Plate.Text = null;
                    Brand.Text = null;
                    Model.Text = null;
                    VIN.Text = null;
                }                              
            }
            BuildHeader();
        }      
        /// <summary>
        /// Closing current view and back to list of trucks (dataGrid1)
        /// </summary>
        void OnClick4(object sender, RoutedEventArgs e)
        {
            AddTruck.Visibility = Visibility.Hidden;
            Grid.Visibility = Visibility.Visible;
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
        }       
        /// <summary>
        /// Closing app
        /// </summary>
        void OnClick5(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        /// <summary>
        /// Adding to database a new driver with parameters given by user, checking if given Firstname and Lastname are not exist in db, reload ComboBox items from adding truck view
        /// </summary>
        void OnClick6(object sender, RoutedEventArgs e)
        {        
            using (ProjektDbContext db = new ProjektDbContext())
            {

                if (db.Drivers.Any(o => o.Firstname == Firstname.Text) && db.Drivers.Any(o => o.Lastname == Lastname.Text))
                {
                    var result = MessageBox.Show("Istnieje już taki kierowca", "wrong_plate", MessageBoxButton.OK);
                    if(result == MessageBoxResult.OK)
                    {
                        Firstname.Text = null;
                        Lastname.Text = null;
                        Phone.Text = null;
                    }
                }
                else
                {
                    db.Drivers.Add(new Driver() { Firstname = Firstname.Text, Lastname = Lastname.Text, Phone = Phone.Text });
                    db.SaveChanges();

                    AddDriver.Visibility = Visibility.Hidden;
                    Grid.Visibility = Visibility.Visible;
                    btn1.IsEnabled = true;
                    btn2.IsEnabled = true;

                    Firstname.Text = null;
                    Lastname.Text = null;
                    Phone.Text = null;
                }               
            }
            ComboBox.Items.Clear();
        }
        /// <summary>
        /// Closing current view and back to list of trucks (dataGrid1)
        /// </summary>
        void OnClick7(object sender, RoutedEventArgs e)
        {
            AddDriver.Visibility = Visibility.Hidden;
            Grid.Visibility = Visibility.Visible;
            btn1.IsEnabled = true;
            btn2.IsEnabled = true;
        }      
        /// <summary>
        /// Opens context details window of specific truck from dataGrid1, shows a plate,insurance date, technical review date and tachograph legalization date  of this truck
        /// Build data grid of repairs of this specific trucks taken from database
        /// </summary>
        void Context_Details(object sender, RoutedEventArgs e)
        {
            Info win = new Info();
            win.Show();
            
            if (dataGrid1 != null)
            {
                var index = dataGrid1.SelectedIndex;
                
                using (ProjektDbContext db = new ProjektDbContext())
                {
                    var truck = from t in db.Trucks
                                where t.Plate == list[index].Plate
                                select t;
                                       
                    foreach(var t in truck)
                    {
                        var i = t.Id;
                        win.BuildHeader(i);
                        win.Rej.Text = t.Plate;
                        win.OC.Text = t.Insurance.ToShortDateString();
                        win.PT.Text = t.TechReview.ToShortDateString();
                        win.Tacho.Text = t.TachoLeg.ToShortDateString();
                    }
                }                  
            }
        }    
        /// <summary>
        /// Delete specific truck and reload datagrid item source
        /// </summary>
        void Context_Delete(object sender, RoutedEventArgs e)
        {
            if(dataGrid1 != null)
            {
                var index = dataGrid1.SelectedIndex;
                {
                    using (ProjektDbContext db = new ProjektDbContext())
                    {
                        var truck = from t in db.Trucks
                                    where t.Plate == list[index].Plate
                                    select t;

                        foreach (var t in truck)
                        {
                            db.Remove(t);
                        }
                        db.SaveChanges();
                        BuildHeader();
                    }
                }
            }
        }

    }
}
