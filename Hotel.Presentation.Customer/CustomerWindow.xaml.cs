using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Hotel.Presentation.Customer.Model;
using System.Linq.Expressions;

namespace Hotel.Presentation.Customer
{
    public partial class CustomerWindow : Window
    {
        private CustomerUI? _customerUI { get; set; }
        private ObservableCollection<MemberUI> _members = new ObservableCollection<MemberUI>();
        private CustomerManager customerManager;

        public CustomerWindow(CustomerUI customerUI)
        {
            InitializeComponent();
            this._customerUI = customerUI;
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);

            if(_customerUI == null)
            {
               
                AddButton.Content = "Add";

            }
            else
            {
                AddButton.Content = "Update";
                IdTextBox.Text = _customerUI.Id?.ToString();
                NameTextBox.Text = _customerUI.Name;
                EmailTextBox.Text = _customerUI.Email;
                PhoneTextBox.Text = _customerUI.Phone;

                string[] addressParts = _customerUI.transformAddress().Split('|');
                CityTextBox.Text = addressParts[0];
                ZipTextBox.Text = addressParts[1];
                StreetTextBox.Text = addressParts[2];
                HouseNumberTextBox.Text = addressParts[3];
                _members = new ObservableCollection<MemberUI>(_customerUI.Members);
            }
            MemberDataGrid.ItemsSource = _members;
        }


        private void AddNewRow_Click(object sender, RoutedEventArgs e)
        {
            _members.Add(new MemberUI("", DateTime.Now));
        }
        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDataGrid.SelectedItem != null)
            {
                MemberUI selectedMember = (MemberUI)MemberDataGrid.SelectedItem;
                if (_members.Contains(selectedMember))
                {
                    _members.Remove(selectedMember);
                }
            }
            else
            {
                MessageBox.Show("Select the member you want to delete!", "Delete", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (_customerUI == null)
            {
                AddCustomer();
            }
            else
            {
                UpdateCustomer();
            }

            DialogResult = true;
            Close();
        }
        private void UpdateCustomer()
        {
            Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
            Customerr customer = customerManager.GetCustomer(_customerUI.Id);
            customer = new Customerr(_customerUI.Id, NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, address));

            bool allMembersValid = _members
                .All(member => !string.IsNullOrWhiteSpace(member.Name));

            if (!allMembersValid)
            {
                MessageBox.Show("Lege inputvelden gedetecteerd , deze worden niet meegenomen in de update");
            }

            List<Member> membersToAdd = _members
                .Where(memberUI => !string.IsNullOrWhiteSpace(memberUI.Name))
                .Select(memberUI => new Member(
                    memberUI.Name,
                    memberUI.Birthday
                )).ToList();
            foreach (Member member in membersToAdd)
            {
                customer.AddMember(member);
            }

            customerManager.UpdateCustomer(customer);
        }
        private void AddCustomer()
        {
            Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);
            _customerUI = new CustomerUI(NameTextBox.Text, EmailTextBox.Text, address.ToAddressLine(), PhoneTextBox.Text, 0);
            
            Customerr customer = new Customerr(_customerUI.Name, new ContactInfo(_customerUI.Email, _customerUI.Phone, address));
            List<Member> membersToAdd = _members
                .Where(memberUI => !string.IsNullOrWhiteSpace(memberUI.Name))
                .Select(memberUI => new Member(
                    memberUI.Name,
                    memberUI.Birthday
                )).ToList();
            
            foreach (Member member in membersToAdd)
            {
                customer.AddMember(member);
            }
            customerManager.AddCustomer(customer);       
        }
        public void DeleteCustomer()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                        customerManager.DeleteCustomer((int)_customerUI.Id);
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
    }
}