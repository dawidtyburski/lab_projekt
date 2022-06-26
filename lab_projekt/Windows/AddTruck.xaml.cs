using Models;
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
using System.Windows.Shapes;

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
            List<string> repairs = new List<string>();

            repairs.Add("Wymina oleju silnikowego");
            repairs.Add("Wymina oleju skrzyni biegów");
            repairs.Add("Wymina oleju dyferencjału");
            repairs.Add("Wymina opon przednich");
            repairs.Add("Wymina opon tylnych");
            repairs.Add("Wymina klocków hamulcowych przednich");
            repairs.Add("Wymina klocków hamulcowych tylnych");
            repairs.Add("Wymina tarcz hamulcowych przednich");
            repairs.Add("Wymina tarcz hamulcowych tylnych");
            repairs.Add("Wymina amortyzatorów przednich");
            repairs.Add("Wymina poduszek tylnego zawieszenia");
            repairs.Add("Wymina oleju silnikowego");

           using (ProjektDbContext db = new ProjektDbContext())
           {
                string[] c = ComboBox.SelectedItem.ToString().Split(' ');

                var id = (from d in db.Drivers where d.Firstname == c[0] && d.Lastname == c[1] select d);
                
                foreach(var d in id)
                {
                    db.Trucks.Add(new Truck() { Plate = Plate.Text, Brand = Brand.Text, Model = Model.Text, VIN = VIN.Text, DriverId = d.Id, Insurance = DateTime.Now, TechReview = DateTime.Now, TachoLeg = DateTime.Now, });

                }
                /*var newTruck = (from t in db.Trucks where t.Plate == Plate.Text select t.Id);
                foreach (var t in newTruck)
                {
                    for (int i = 0; i < repairs.Count; i++)
                    {                       
                        db.Repairs.Add(new Repair() { Name = repairs[i], TruckId = t, Date = DateTime.Now});
                    }
                }*/
                db.SaveChanges();
           }

        }
        void OnClick2(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Close();
        }
    }
    
}
