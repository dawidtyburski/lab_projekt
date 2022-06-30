using System;
using System.Windows;

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy AddRepairDialog.xaml
    /// </summary>
    public partial class AddRepairDialog : Window
    {     
        /// <summary>
        /// Set default data of text boxes
        /// </summary>
        public AddRepairDialog()
        {
            InitializeComponent();
            Mileage.Text = "0";
            Date.Text = DateTime.Now.ToShortDateString();
        }
        /// <summary>
        /// Checking if dialog result is true
        /// </summary>
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        /// <summary>
        /// Focus on name box 
        /// </summary>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Name.Focus();
        }
        /// <summary>
        /// Return name of repair given by user
        /// </summary>
        public string nameAnswer
        {
            get { return Name.Text; }
        }      
        /// <summary>
        /// Return date of repair given by user
        /// </summary>
        public string dateAnswer
        {
            get { return Date.Text; }
        }       
        /// <summary>
        /// Return mileage given by user
        /// </summary>
        public string mileageAnswer
        {
            get { return Mileage.Text; }
        }
    }
}
