using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Models;

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
        /// <summary>
        /// isAdmin storing data or the administrator is logged in
        /// </summary>
        bool isAdmin = false;
        /// <summary>
        /// Checking the existence of the administrator and the correctness of the password. If it is correct, isAdmin set to true, if not messagebox shows and isAdmin stay false
        /// </summary>
        /// <param name="l">>Name given by the administrator</param>
        /// <param name="p">Password given by the administrator</param
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
                    if (l == item.Login)
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
        /// <summary>
        /// Creating a new user, checking if given username is not exist in database, if it is correct insert new user to db and move to LoginPage.xaml
        /// </summary>
        /// <param name="l">Name given by the user</param>
        /// <param name="p">Password given by the user</param>
        void OnClick2(object sender, RoutedEventArgs e)
        {
            using (ProjektDbContext db = new ProjektDbContext())
            {
                if (isAdmin)
                {
                    string l = Login.Text;
                    string p = Password.Password;

                    if (db.Users.Any(o => o.Login == l))
                    {
                        MessageBox.Show("Login zajęty", "wrong_login", MessageBoxButton.OK);
                        return;
                    }

                    db.Add(new User() { Login = l, Password = p, Admin = false });
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
        /// <summary>
        /// Navigate to LoginPage by cancel button
        /// </summary>
        void OnClick3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/LoginPage.xaml", UriKind.Relative));
        }
    }
}
