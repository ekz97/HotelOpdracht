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

            LoadData();
        }

        private void LoadData(string filter = null)
        {
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            RefreshCustomerData(filter);
        }

        private void RefreshCustomerData(string filter = null)
        {
            customerUIs.Clear();
      
            foreach (var customer in customerManager.GetCustomers(filter))
            {

                List<MemberUI> memberUIList = customer.GetMembers()
             .Select(member => new MemberUI(member.Name, member.Birthday))
             .ToList();

                customerUIs.Add(new CustomerUI(customer.Id, customer.Name, customer.Contact.Email, customer.Contact.Address.ToAddressLine(), customer.Contact.Phone, customer.GetMembers().Count,memberUIList));
            }
        }
        


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData(SearchTextBox.Text); // Gebruik LoadData in plaats van RefreshCustomerData
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow w = new CustomerWindow(null);
            if (w.ShowDialog() == true)
                RefreshCustomerData();
        }

        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null) MessageBox.Show("not selected", "update");
            else
            {
                CustomerWindow w = new CustomerWindow((CustomerUI)CustomerDataGrid.SelectedItem);
              
                var result = w.ShowDialog();
                RefreshCustomerData(); // Laad gegevens opnieuw na het bijwerken
            }
        }

        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select a customer to delete", "Delete Customer", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                CustomerUI selectedCustomer = (CustomerUI)CustomerDataGrid.SelectedItem;
                CustomerWindow w = new CustomerWindow(selectedCustomer);
                w.DeleteCustomer(); // Roep de DeleteCustomer methode aan vanuit CustomerWindow

                // Hier kun je eventueel een bericht weergeven als de klant met succes is verwijderd
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to delete customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            // Vernieuw de klantgegevens in de grid na verwijdering
            RefreshCustomerData();
        }

    }

}
