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


namespace lab_projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            
        }
        void OnClick1(object sender, RoutedEventArgs e)
        {
            string l = Login.Text;
            string p = Password.Password;
            ProjektDbContext db = new ProjektDbContext();
            var users = from u in db.Users
                        select u;
            foreach (var item in users)
            {
                if (l == item.Username)
                {
                    if (p == item.Password)
                    {
                        Login.Text = "Zalogowano!!";
                    }
                    else break;

                }
                else break;

            }

        }
        void OnClick2(object sender, RoutedEventArgs e)
        {

        }
        void OnClick3(object sender, RoutedEventArgs e)
        {

        }
    }
}
