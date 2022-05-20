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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy OrganizerPage.xaml
    /// </summary>
    public partial class OrganizerPage : Page
    {
        public class DataObject
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string E { get; set; }
            public string F { get; set; }
            public string G { get; set; }
            public string H { get; set; }
        }
            

       
        public OrganizerPage()
        {
            InitializeComponent();

            var list = new ObservableCollection<DataObject>();

            ProjektDbContext db = new ProjektDbContext();
            var trucks = from t in db.Trucks
                        select t;

            foreach (var item in trucks)
            {
                list.Add(new DataObject() { A = item.Plate, B = $"{item.Brand} {item.Model}", C = item.Vin, D = $"", E = "", F = item.Insurance.ToShortDateString(), G = item.TechReview.ToShortDateString(), H = item.TachoLeg.ToShortDateString() });
            }

            dataGrid1.ItemsSource = list;
        }
        
    }
}
