using System;
using System.Windows;

namespace lab_projekt
{
    /// <summary>
    /// Logika interakcji dla klasy AddRepairDialog.xaml
    /// </summary>
    public partial class AddRepairDialog : Window
    {
        public AddRepairDialog()
        {
            InitializeComponent();
            Mileage.Text = "0";
            Date.Text = DateTime.Now.ToShortDateString();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Name.Focus();
        }

        public string nameAnswer
        {
            get { return Name.Text; }
        }
        public string dateAnswer
        {
            get { return Date.Text; }
        }
        public string mileageAnswer
        {
            get { return Mileage.Text; }
        }
    }
}
