using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Info.xaml
    /// </summary>
    public partial class Info : Window
    {

        public class Header
        {
            public string Name { get; set; }
            public string Date { get; set; }

        }
        public ObservableCollection<Header> list2 = new ObservableCollection<Header>();

        public Info()
        {
            InitializeComponent();
            BuildHeader();
        }
        void OnClick1(object sender, RoutedEventArgs e)
        {

        }
        void OnClick2(object sender, RoutedEventArgs e)
        {
            using (ProjektDbContext db = new ProjektDbContext())
            {
                db.Repairs.Add(new Repair() { TruckId = MainPage.index, Name = "test", Date = Convert.ToDateTime("20-12-2022") });
                db.SaveChanges();
                BuildHeader();
            }
        }
        private void BuildHeader()
        {
            
            using (ProjektDbContext db = new ProjektDbContext())
            {
                var rr = from r in db.Repairs
                         where r.Id == MainPage.index+1
                         select r;

                foreach (var item in rr)
                {

                    list2.Add(new Header() { Name = item.Name, Date = item.Date.ToShortDateString() });
                }
                    dataGrid2.ItemsSource = list2;
                
            }
        }
    }
}
