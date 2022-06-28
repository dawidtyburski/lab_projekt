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
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        void OnClick1(object sender, RoutedEventArgs e)
        {
            using (ProjektDbContext db = new ProjektDbContext())
            {
                string l = Login.Text;
                string p = Password.Password;

                var users = from u in db.Users
                            select u;

                if (db.Users.Any(o => o.Login == l))
                {
                    foreach (var item in users)
                    {
                        if (l == item.Login)
                        {
                            if (p == item.Password)
                            {
                                MainWindow windowToShow = new MainWindow();
                                windowToShow.Show();
                                Application.Current.MainWindow.Dispatcher.Invoke(() =>
                                {
                                    Application.Current.MainWindow.Close();
                                });
                                break;
                            }
                            else
                            {
                                MessageBox.Show("Nieprawidlowe haslo", "wrong_password", MessageBoxButton.OK);
                            }
                        }
                    }
                }
                else
                {
                    var result = MessageBox.Show("Nieprawidlowy login", "wrong_login", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        Login.Text = null;
                        Password.Password = null;
                    }
                }             
            }         
        }
        void OnClick2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/RegisterPage.xaml", UriKind.Relative));
        }
        void OnClick3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
