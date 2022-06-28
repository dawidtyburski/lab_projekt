using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
            public int Mileage { get; set; }

        }
        public ObservableCollection<Header> list2 = new ObservableCollection<Header>();
        int i = 0;

        public Info()
        {
            InitializeComponent();           
        }
        void OnClick1(object sender, RoutedEventArgs e)
        {
            using (ProjektDbContext db = new ProjektDbContext())
            {
                var trucks = from t in db.Trucks
                              where t.Id == i
                              select t;
                foreach(var t in trucks)
                {
                    try
                    {
                        if (!t.Insurance.ToShortDateString().Equals(OC.Text))
                        {
                            t.Insurance = Convert.ToDateTime(OC.Text);
                        }
                        if (!t.TechReview.ToShortDateString().Equals(PT.Text))
                        {
                            t.TechReview = Convert.ToDateTime(PT.Text);
                        }
                        if (!t.TachoLeg.ToShortDateString().Equals(Tacho.Text))
                        {
                            t.TachoLeg = Convert.ToDateTime(Tacho.Text);
                        }
                    }
                    catch
                    {
                    }
                }
                db.SaveChanges();
            }
        }
        void dataGrid2_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;
                    if (bindingPath == "Date")
                    {
                        int rowIndex = e.Row.GetIndex();
                        using (ProjektDbContext db = new ProjektDbContext())
                        {
                            var rep = from r in db.Repairs
                                     where r.Name == list2[rowIndex].Name
                                     select r;
                           foreach(var r in rep)
                            {
                                r.Date = Convert.ToDateTime((e.EditingElement as TextBox).Text.ToString());
                            }
                            db.SaveChanges();
                            BuildHeader(i);                           
                        }
                    }
                    else if (bindingPath == "Mileage")
                    {
                        int rowIndex = e.Row.GetIndex();
                        using (ProjektDbContext db = new ProjektDbContext())
                        {
                            var rep = from r in db.Repairs
                                      where r.Name == list2[rowIndex].Name
                                      select r;
                            foreach (var r in rep)
                            {
                                r.Mileage = Int32.Parse((e.EditingElement as TextBox).Text);
                            }
                            db.SaveChanges();
                            BuildHeader(i);
                        }
                    }
                }
            }
        }
        void OnClick2(object sender, RoutedEventArgs e)
        {
            using (ProjektDbContext db = new ProjektDbContext())
            {
                AddRepairDialog inputDialog = new AddRepairDialog();

                
                if (inputDialog.ShowDialog() == true)
                {
                    int n = Int32.TryParse(inputDialog.mileageAnswer, out n) ? n : 0;
                    DateTime dt = DateTime.TryParse(inputDialog.dateAnswer, out dt) ? dt : DateTime.Now;
                    db.Repairs.Add(new Repair() { TruckId = i, Name = inputDialog.nameAnswer, Date = Convert.ToDateTime(dt), Mileage = n });
                    db.SaveChanges();
                    BuildHeader(i);
                }
            }
        }
        public void BuildHeader(int index)
        {
            i = index;
            using (ProjektDbContext db = new ProjektDbContext())
            {
                list2.Clear();
                var rr = from r in db.Repairs
                         where r.TruckId == index
                         select r;

                foreach (var item in rr)
                {
                    list2.Add(new Header() { Name = item.Name, Date = item.Date.ToShortDateString(), Mileage = item.Mileage});
                }
                dataGrid2.ItemsSource = list2;

                if (list2.Count > 0)
                {
                    RepairsList.Visibility = Visibility.Visible;
                }
                else
                    RepairsList.Visibility = Visibility.Hidden;

            }
        }
    }
}
