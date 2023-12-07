using Hotel.Domain.Managers;
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
        private bool isFirstSelection = true;


        private ActivityManager _activityManager;

        public RegistrationWindow(CustomerUI customer)
        {
            InitializeComponent();
            _activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            _customer = customer;
            MemberDataGrid.ItemsSource = _customer.Members;
            foreach (var a in _activityManager.GetAllActivities())
            {
                _activityUIs.Add(new ActivityUI(a.Id, a.Fixture.ToString("dd-MM-yyyy"), a.NrOfPlaces, new DescriptionUI(a.Description.Duration, a.Description.Explanation, a.Description.Location, a.Description.Name), new PriceInfoUI(a.PriceInfo.AdultPrice, a.PriceInfo.ChildPrice, a.PriceInfo.Discount)));
            }
            ActivityDataGrid.ItemsSource = _activityUIs;

        }

        private void AddMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ActivityDataGrid.SelectedItem != null)
            {
                if (MemberDataGrid.SelectedItem != null)
                {
                    if (!_registrationUI.Members.Contains(MemberDataGrid.SelectedItem))
                    {
                        _registrationUI.Members.Add((MemberUI)MemberDataGrid.SelectedItem);
                        MessageBox.Show($"{_registrationUI.Members[_registrationUI.Members.Count - 1].Name} is toegevoegd aan {_registrationUI.Activity.Description.Name}");
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
            }
        }

        private void SubmitRegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if(_registrationUI.Activity != null && _registrationUI.Members != null)
            {
                List<Member> members = new List<Member>();
                foreach(MemberUI member in _registrationUI.Members)
                {
                    members.Add(new Member(member.Name, member.Birthday));
                }
                Registrationn registration = new Registrationn(members, new Activity(_registrationUI.Activity.Id, Convert.ToDateTime(_registrationUI.Activity.Fixture), _registrationUI.Activity.NrOfPlaces, new Description(_registrationUI.Activity.Description.Duration, _registrationUI.Activity.Description.Location, _registrationUI.Activity.Description.Explanation, _registrationUI.Activity.Description.Name),new PriceInfo(_registrationUI.Activity.PriceInfo.AdultPrice,_registrationUI.Activity.PriceInfo.ChildPrice,_registrationUI.Activity.PriceInfo.Discount)));
                MessageBox.Show(registration.CalcCost().ToString("€ 0.00"));
            }
        }
    }
}