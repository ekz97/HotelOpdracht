using Hotel.Domain.Interfaces;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Customer.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

namespace Hotel.Presentation.Customer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private ObservableCollection<CustomerUI> customerUIs = new ObservableCollection<CustomerUI>();
        private CustomerManager customerManager;
 
        public MainWindow()
        {
            InitializeComponent();
            CustomerDataGrid.ItemsSource = customerUIs;
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            RefreshCustomerData();
        }

        private void RefreshCustomerData(string filter = null)
        {
            //commentaar
            customerUIs.Clear();
            foreach (var customer in customerManager.GetCustomers(filter))
            {
                List<MemberUI> memberUIList = customer.GetMembers()
             .Select(member => new MemberUI(member.Name, member.Birthday))
             .ToList();
                customerUIs.Add(new CustomerUI(customer.Id,customer.Name, customer.Contact.Email, customer.Contact.Address.ToString(), customer.Contact.Phone, customer.GetMembers().Count,memberUIList));
            }
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshCustomerData(SearchTextBox.Text);
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && SearchTextBox.IsFocused)
            {
                RefreshCustomerData(SearchTextBox.Text);
            }
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow window = new CustomerWindow(null);
            if (window.ShowDialog() == true)
            {
                RefreshCustomerData();
            }
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select the customer you want to update!", "update", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                CustomerWindow window = new CustomerWindow((CustomerUI)CustomerDataGrid.SelectedItem);
                if(window.ShowDialog() == true)
                {
                    RefreshCustomerData();
                }
            }
        }

        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select the customer you want to delete!", "Delete", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                CustomerWindow window = new CustomerWindow((CustomerUI)CustomerDataGrid.SelectedItem);
                window.DeleteCustomer();
                RefreshCustomerData();
            }
        } 
    }
}
