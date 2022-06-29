using Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy AddTruck.xaml
    /// </summary>

    public partial class AddTruck : Window
    {
        public AddTruck()
        {
            InitializeComponent();

            using (ProjektDbContext db = new ProjektDbContext())
            {
                var drivers = from d in db.Drivers
                              select d;
                foreach(var d in drivers)
                {
                    ComboBox.Items.Add($"{d.Firstname} {d.Lastname}");
                }
            }
        }
        void OnClick1(object sender, RoutedEventArgs e)
        {
           using (ProjektDbContext db = new ProjektDbContext())
           {
                string[] c = ComboBox.SelectedItem.ToString().Split(' ');

                var id = (from d in db.Drivers where d.Firstname == c[0] && d.Lastname == c[1] select d);
                
                foreach(var d in id)
                {
                    db.Trucks.Add(new Truck() { Plate = Plate.Text, Brand = Brand.Text, Model = Model.Text, VIN = VIN.Text, DriverId = d.Id, Insurance = DateTime.Now, TechReview = DateTime.Now, TachoLeg = DateTime.Now, });

                }
                db.SaveChanges();
           }

        }
        void OnClick2(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }
    }
    
}
