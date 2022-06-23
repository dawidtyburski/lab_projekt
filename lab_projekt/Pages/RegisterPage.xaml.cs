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
    /// Logika interakcji dla klasy RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        bool isAdmin = false;

        void OnClick1(object sender, RoutedEventArgs e)
        {
            using(ProjektDbContext db = new ProjektDbContext())
            {
                string l = adminLogin.Text;
                string p = adminPassword.Password;
                var users = from u in db.Users
                            where u.Admin
                            select u;
                foreach (var item in users)
                {
                    if (l == item.Username)
                    {
                        if (p == item.Password)
                        {
                            isAdmin = true;
                            btn1.IsEnabled = false;
                            btn1.Content = "OK";
                            btn2.IsEnabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Nieprawidlowe haslo", "wrong_password", MessageBoxButton.OK);
                            isAdmin = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nieprawidlowy login", "wrong_login", MessageBoxButton.OK);
                        isAdmin = false;
                    }
                }
            }
        }
        void OnClick2(object sender, RoutedEventArgs e)
        {
            using (ProjektDbContext db = new ProjektDbContext())
            {
                if (isAdmin)
                {
                    string l = Login.Text;
                    string p = Password.Password;

                    if (db.Users.Any(o => o.Username == l))
                    {
                        MessageBox.Show("Login zajęty", "wrong_login", MessageBoxButton.OK);
                        return;
                    }

                    db.Add(new User() { Username = l, Password = p, Admin = false });
                    db.SaveChanges();
                    
                    var result = MessageBox.Show("Użytkownik dodany", "new_user", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        NavigationService.Navigate(new Uri("/Pages/LoginPage.xaml", UriKind.Relative));
                    }                 
                }
                else
                {
                    MessageBox.Show("Podaj dane administratora", "admin?", MessageBoxButton.OK);
                }
            }               
        }
        void OnClick3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/LoginPage.xaml", UriKind.Relative));
        }
    }
}
