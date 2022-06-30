using Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Info.xaml
    /// </summary>
    public partial class Info : Window
    {
        /// <summary>
        /// Class that stores repairs data needed to display the datagrid
        /// </summary>
        public class Header
        {
            public string Name { get; set; }
            public string Date { get; set; }
            public int Mileage { get; set; }

        }
        /// <summary>
        /// List of repairs uses data from class Header
        /// </summary>
        public ObservableCollection<Header> list2 = new ObservableCollection<Header>();   
        /// <summary>
        /// Store index of selected specific truck
        /// </summary>
        int i = 0;

        public Info()
        {
            InitializeComponent();           
        }       
        /// <summary>
        /// Allows you to change insurance, technical review and tachograph legalization dates in database
        /// </summary>
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
        /// <summary>
        /// Allows you to change dates and mileages of repair of specific truck in database, reload datagrid item source
        /// </summary>
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
                                DateTime dt = DateTime.TryParse((e.EditingElement as TextBox).Text.ToString(), out dt) ? dt : DateTime.Now;
                                r.Date = dt;
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
        /// <summary>
        /// Open AddRepairDialog.xaml and if it returns true then try parse given date to DateTime and given mileage to int, if it not pass insert fegault values (DateTime.Now and 0)
        /// Insert new repair to specific truck and reload dataGrid item source
        /// </summary>
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
        /// <summary>
        /// Downloading data from the database and creating new Header objects and adding them to the list. Loading a data source into a datagrid
        /// Setting datagrid visibility in relation to list count
        /// </summary>
        /// <param name="index">Index of selected specific truck</param>
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
