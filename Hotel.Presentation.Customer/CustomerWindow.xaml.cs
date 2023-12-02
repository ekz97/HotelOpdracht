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
            IdTextBox.IsEnabled = false;

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

                if (CustomerUI.Members != null && CustomerUI.Members.Any())
                {
                  

                    MemberDataGrid.ItemsSource = CustomerUI.Members;
                }
                else
                {
                    MemberDataGrid.ItemsSource = null;
                }

                string address = "Gent [9000] - Noordstraat - 222";
                string[] separators = { " - ", " [", "] - " };
                string[] addressParts = address.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                CityTextBox.Text = addressParts[0];
                    ZipTextBox.Text = addressParts[1];
                    StreetTextBox.Text = addressParts[2];
                    HouseNumberTextBox.Text = addressParts[3];
              
            }
        }


        private void AddNewRow_Click(object sender, RoutedEventArgs e)
        {
            MemberDataGrid.CommitEdit(); // Zorg ervoor dat eventuele lopende bewerkingen worden toegepast

            var memberList = (List<MemberUI>)MemberDataGrid.ItemsSource;
            if (memberList == null)
            {
                memberList = new List<MemberUI>();
                MemberDataGrid.ItemsSource = memberList;
            }

            memberList.Add(new MemberUI("", DateTime.Now));

            // Herstel de bron van de DataGrid
            MemberDataGrid.ItemsSource = null;
            MemberDataGrid.ItemsSource = memberList;
        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (MemberDataGrid.SelectedItem != null)
            {
                var memberList = (List<MemberUI>)MemberDataGrid.ItemsSource;
                var selectedMember = (MemberUI)MemberDataGrid.SelectedItem;

            
                // Controleer of de geselecteerde rij een bestaande member is
                if (selectedMember.Name != "" && memberList.Contains(selectedMember))
                {
                    memberList.Remove(selectedMember);


                    // Vernieuw de bron van de DataGrid
                    MemberDataGrid.ItemsSource = null;
                    MemberDataGrid.ItemsSource = memberList;
                }

                
            }
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddOrUpdateCustomer()
        {
            
            if (CustomerUI == null)
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
            Keyboard.ClearFocus();

            //CustomerUI.Email = EmailTextBox.Text;
            //CustomerUI.Phone = PhoneTextBox.Text;
            //CustomerUI.Name = NameTextBox.Text;
            //CustomerUI.Address = address.ToAddressLine();

              bool allMembersValid = MemberDataGrid.Items.Cast<MemberUI>()
    .All(member => !string.IsNullOrWhiteSpace(member.Name) && member.Birthday != new DateTime(2001, 1, 1));

            if (!allMembersValid)
            {
                MessageBox.Show("Lege inputvelden gedetecteerd , deze worden niet meegenomen in de update");
            }

            // Controleer of de member-velden niet leeg zijn voordat je ze toevoegt
            List<Member> itemsToAdd = MemberDataGrid.Items.Cast<MemberUI>()
                .Where(memberUI => !string.IsNullOrWhiteSpace(memberUI.Name) && memberUI.Birthday != new DateTime(2001, 1, 1))
                .Select(memberUI => new Member(
                    memberUI.Name,
                    memberUI.Birthday
                )).ToList();

          
             Customerr cust = customerManager.GetCustomer(CustomerUI.Id);

       

             cust = new Customerr(CustomerUI.Id, CustomerUI.Name, new ContactInfo(CustomerUI.Email, CustomerUI.Phone,address));
      

            foreach (Member member in itemsToAdd)
            {
                cust.AddMember(member);
            }

            customerManager.UpdateCustomer(cust);
        }



        private void AddCustomer()
        {
            Address address = new Address(CityTextBox.Text, StreetTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text);

            CustomerUI = new CustomerUI(NameTextBox.Text, EmailTextBox.Text, address.ToAddressLine(), PhoneTextBox.Text, 0);
            Keyboard.ClearFocus();

            List<Member> itemsToAdd = new List<Member>(MemberDataGrid.Items.Cast<Member>());

          

            Customerr  cust = new Customerr(CustomerUI.Name, new ContactInfo(CustomerUI.Email, CustomerUI.Phone, address));

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