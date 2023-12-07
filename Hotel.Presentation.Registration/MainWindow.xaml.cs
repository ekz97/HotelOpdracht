using Hotel.Domain.Managers;
using Hotel.Presentation.Registration.Model;
using Hotel.Util;
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

namespace Hotel.Presentation.Registration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<CustomerUI> _customers = new List<CustomerUI>();
        private CustomerManager _customerManager;
        public MainWindow()
        {
            InitializeComponent();
            _customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            foreach( var customer in _customerManager.GetCustomers(null))
            {
                List<MemberUI> memberUIList = customer.GetMembers()
                    .Select(member => new MemberUI(member.Name, member.Birthday))
                    .ToList();
                _customers.Add(new CustomerUI(customer.Name, customer.Contact.Email, memberUIList));
            }
            CustomerComboBox.ItemsSource = _customers;
            CustomerComboBox.DisplayMemberPath = "DisplayString";
        }

        private void SelectCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CustomerComboBox.SelectedItem == null)
            {
                MessageBox.Show("Select a customer.", "Select", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                RegistrationWindow window = new RegistrationWindow((CustomerUI)CustomerComboBox.SelectedItem);
                if (window.ShowDialog() == true)
                {

                }           
            }
        }
    }
}
