﻿using Hotel.Domain.Managers;
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
        private ObservableCollection<CustomerUI> customerUIs=new ObservableCollection<CustomerUI>();
        private CustomerManager customerManager;
        //private string conn = "Data Source=NB21-6CDPYD3\\SQLEXPRESS;Initial Catalog=HotelDonderdag;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
            customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            customerUIs =new ObservableCollection<CustomerUI>(customerManager.GetCustomers(null).Select(x => new CustomerUI(x.Id,x.Name,x.Contact.Email,x.Contact.Address.ToString(),x.Contact.Phone,x.GetMembers().Count)).ToList());
            CustomerDataGrid.ItemsSource = customerUIs;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            customerUIs =new ObservableCollection<CustomerUI>(customerManager.GetCustomers(SearchTextBox.Text).Select(x => new CustomerUI(x.Id, x.Name, x.Contact.Email, x.Contact.Address.ToString(), x.Contact.Phone, x.GetMembers().Count)).ToList());
            CustomerDataGrid.ItemsSource = customerUIs;
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerWindow w = new CustomerWindow(null);
            if (w.ShowDialog()==true)
                customerUIs.Add(w.CustomerUI);
        }
        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerDataGrid.SelectedItem==null) MessageBox.Show("not selected", "update");
            else
            {
                CustomerWindow w = new CustomerWindow((CustomerUI)CustomerDataGrid.SelectedItem);
                w.ShowDialog();
            }
        }
    }
}
