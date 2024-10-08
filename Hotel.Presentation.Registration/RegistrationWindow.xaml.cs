﻿using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Activities.Model;
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
using System.Windows.Shapes;

namespace Hotel.Presentation.Registration
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private CustomerUI _customer;
        private List<ActivityUI> _activityUIs = new List<ActivityUI>();
        private RegistrationUI _registrationUI = new RegistrationUI();
        private List<MemberUI> _registeredMemberUis = new List<MemberUI>();


        private ActivityManager _activityManager;
        private RegistrationManager _registrationManager;

        public RegistrationWindow(CustomerUI customer)
        {
            InitializeComponent();
            _activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            _registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository);

            MemberDataGrid.LoadingRow += MemberDataGrid_LoadingRow;

            _customer = customer;
            MemberDataGrid.ItemsSource = _customer.Members;
            foreach (var a in _activityManager.GetAllActivities())
            {
                _activityUIs.Add(new ActivityUI(a.Id, a.Fixture.ToString("dd-MM-yyyy"), a.NrOfPlaces, new DescriptionUI(a.Description.Duration, a.Description.Explanation, a.Description.Location, a.Description.Name), new PriceInfoUI(a.PriceInfo.AdultPrice, a.PriceInfo.ChildPrice, a.PriceInfo.Discount)));
            }
            ActivityDataGrid.ItemsSource = _activityUIs;

        }
        private void RefreshRegistredMembers()
        {
            _registeredMemberUis.Clear();

            foreach (var member in _registrationManager.GetRegistratedMembersForActivity(_customer.Id, _registrationUI.Activity.Id))
            {
                _registeredMemberUis.Add(new MemberUI(member.Name, member.Birthday));
            }

            // Refresh de weergave van de DataGrid
            MemberDataGrid.Items.Refresh();
            

        }
        private void Reset()
        {

            _registrationUI = new RegistrationUI();
            _registeredMemberUis.Clear();
            ActivityDataGrid.IsEnabled = true;
            MemberDataGrid.SelectedItem = null;
            ActivityDataGrid.SelectedItem = null;
        }

        private void AddMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityDataGrid.SelectedItem != null)
            {
                if (MemberDataGrid.SelectedItem != null)
                {
                    MemberUI selectedMember = (MemberUI)MemberDataGrid.SelectedItem;

                    if (!_registeredMemberUis.Contains(selectedMember))
                    {
                        _registrationUI.Members.Add(selectedMember);
                        _registeredMemberUis.Add(selectedMember);

                        MessageBox.Show($"{selectedMember.Name} is toegevoegd aan {_registrationUI.Activity.Description.Name}");

                        DataGridRow selectedRow = (DataGridRow)MemberDataGrid.ItemContainerGenerator.ContainerFromItem(selectedMember);
                        if (selectedRow != null)
                        {
                            selectedRow.Background = Brushes.LightBlue;
                        }

                        MemberDataGrid.SelectedItem = null;

                    }
                    else
                    {
                        MessageBox.Show("This member is already registered for this activity!", "Member", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a member you want to register to the activity!", "Member", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please choose an activity before registering a member.", "Activity", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ActivityDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActivityDataGrid.SelectedItem != null)
            {
                _registrationUI.Activity = (ActivityUI)ActivityDataGrid.SelectedItem;
                ActivityDataGrid.IsEnabled = false;
                RefreshRegistredMembers();

            }
        }

        private void SubmitRegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if(_registrationUI.Activity != null && _registrationUI.Members.Count > 0)
            {
                List<Member> members = new List<Member>();
                foreach(MemberUI member in _registrationUI.Members)
                {
                    members.Add(new Member(member.Name, member.Birthday));
                }
                Registrationn registration = new Registrationn(members, new Activity(_registrationUI.Activity.Id, Convert.ToDateTime(_registrationUI.Activity.Fixture), _registrationUI.Activity.NrOfPlaces, new Description(_registrationUI.Activity.Description.Duration, _registrationUI.Activity.Description.Location, _registrationUI.Activity.Description.Explanation, _registrationUI.Activity.Description.Name),new PriceInfo(_registrationUI.Activity.PriceInfo.AdultPrice,_registrationUI.Activity.PriceInfo.ChildPrice,_registrationUI.Activity.PriceInfo.Discount)));
                _registrationManager.AddRegistration(registration, _customer.Id);
                MessageBox.Show(registration.CalcCost().ToString("€ 0.00"));
                RefreshRegistredMembers();
                Reset();
            }
            else
            {
                MessageBox.Show("Add a member before submitting.", "Sumbit", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MemberDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            MemberUI member = e.Row.Item as MemberUI;

            if (member != null && _registeredMemberUis.Contains(member))
            {
                e.Row.Background = Brushes.LightGreen; // Je kunt hier je gewenste achtergrondkleur instellen
            }
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if(_registrationUI.Activity != null) 
            {

                Reset();
                foreach (var item in MemberDataGrid.Items)
                {
                    DataGridRow row = (DataGridRow)MemberDataGrid.ItemContainerGenerator.ContainerFromItem(item);
                    if (row != null)
                    {
                        row.Background = Brushes.White; // Reset de achtergrondkleur naar standaardwaarde
                    }
                }
            }
            else
            {
                Close();
            }
        }
    }
}