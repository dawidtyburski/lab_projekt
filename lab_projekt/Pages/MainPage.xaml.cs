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
        public MainPage()
        {
            InitializeComponent();
            ShowTreeView();

        }
        private void ShowTreeView()
        {
            using (ProjektDbContext db = new ProjektDbContext())
            {
                List<string> heads = new List<string>();
                var trucks = from t in db.Trucks
                             select t;
                foreach (var t in trucks)
                {
                    heads.Add($"{t.Brand} { t.Model} {t.Plate}");
                }
                for (int i = 0; i < heads.Count; i++)
                {
                    
                    TreeViewItem newTree = new TreeViewItem();
                    newTree.Header = heads[i];
                    newTree.Items.Add(new TreeViewItem() { Header = "x" });
                    TreeView1.Items.Add(newTree);
                }
            }


        }
        void OnClick1(object sender, RoutedEventArgs e)
        {

        }
        void OnClick2(object sender, RoutedEventArgs e)
        {

        }

    }
}
