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

        }

        private void SubmitRegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
