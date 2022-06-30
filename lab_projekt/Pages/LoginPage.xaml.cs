using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {     
        public LoginPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Checking the existence of the user and the correctness of the password. If it is correct, the MainWindow window opens, if not messagebox shows
        /// </summary>
        /// <param name="l">Name given by the user</param>
        /// <param name="p">Password given by the user</param>
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
        /// <summary>
        /// Opens RegisterPage
        /// </summary>
        void OnClick2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/RegisterPage.xaml", UriKind.Relative));
        }       
        /// <summary>
        /// Closing app
        /// </summary>
        void OnClick3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
