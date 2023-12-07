using Hotel.Presentation.Registration.Model;
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
using System.Windows.Shapes;

namespace Hotel.Presentation.Registration
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private CustomerUI _customer;
        public RegistrationWindow(CustomerUI customer)
        {
            InitializeComponent();
             _customer = customer;
            MemberDataGrid.ItemsSource = _customer.Members;
        }

        private void AddMemberBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SubmitRegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
