using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Customer.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Hotel.Presentation.Customer
{
    public partial class CustomerWindow : Window
    {
        public CustomerUI CustomerUI { get; set; }
        private CustomerManager customerManager;

        public CustomerWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            this.CustomerUI = customerUI;
            FillCustomerInformation();
        }

        private void FillCustomerInformation()
        {
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            if (CustomerUI != null)
            {
                IdTextBox.Text = CustomerUI.Id?.ToString();
                NameTextBox.Text = CustomerUI.Name;
                EmailTextBox.Text = CustomerUI.Email;
                PhoneTextBox.Text = CustomerUI.Phone;
                MemberDataGrid.ItemsSource = CustomerUI.MemberList;

                string[] addressParts = CustomerUI.Address.ToAddressLine().Split('|');
                if (addressParts.Length >= 4)
                {
                    CityTextBox.Text = addressParts[0];
                    ZipTextBox.Text = addressParts[1];
                    StreetTextBox.Text = addressParts[2];
                    HouseNumberTextBox.Text = addressParts[3];
                }
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddOrUpdateCustomer()
        {
            Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);

            if (CustomerUI == null)
            {
                AddCustomer();
            }
            else
            {
                Keyboard.ClearFocus();

                CustomerUI.Email = EmailTextBox.Text;
                CustomerUI.Phone = PhoneTextBox.Text;
                CustomerUI.Name = NameTextBox.Text;
                CustomerUI.Address = address;

                List<Member> itemsToAdd = new List<Member>(MemberDataGrid.Items.Cast<Member>());

                CustomerUI.MemberList.Clear();
                Customerr cust = customerManager.GetCustomer(CustomerUI.Id);

                cust = new Customerr(CustomerUI.Id, CustomerUI.Name, new ContactInfo(CustomerUI.Email, CustomerUI.Phone, CustomerUI.Address));

                foreach (Member member in itemsToAdd)
                {
                    cust.AddMember(member);
                }

                customerManager.UpdateCustomer(cust);
            }

            DialogResult = true;
            Close();
        }


        private void AddCustomer()
        {
            Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);

            CustomerUI = new CustomerUI(NameTextBox.Text, EmailTextBox.Text, address, PhoneTextBox.Text, 0);
            Keyboard.ClearFocus();

            List<Member> itemsToAdd = new List<Member>(MemberDataGrid.Items.Cast<Member>());

            Customerr cust = new Customerr(CustomerUI.Name, new ContactInfo(CustomerUI.Email, CustomerUI.Phone, CustomerUI.Address));

            foreach (Member member in itemsToAdd)
            {
                cust.AddMember(member);
            }

            customerManager.AddCustomer(cust);

          
        }





        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrUpdateCustomer();
        }

        public void DeleteCustomer()
        {
            if (CustomerUI != null && CustomerUI.Id != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        customerManager.DeleteCustomer((int)CustomerUI.Id);
                        MessageBox.Show("Customer deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        //DialogResult = true;
                        Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete customer: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No customer selected or invalid ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }

       

    }
}