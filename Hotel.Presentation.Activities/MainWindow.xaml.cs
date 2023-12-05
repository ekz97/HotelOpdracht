using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Activities.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Hotel.Presentation.Activities
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<ActivityUI> activityUIs = new ObservableCollection<ActivityUI>();
        public ObservableCollection<OrganiserUI> organiserUIs = new ObservableCollection<OrganiserUI>();

        private OrganiserManager organiserManager;
        private ActivityManager activityManager;
        public MainWindow()
        {
            InitializeComponent();
            organiserManager = new OrganiserManager(RepositoryFactory.OrganiserRepository);
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);

            foreach (var organiser in organiserManager.GetOrganisers())
            {
                organiserUIs.Add(new OrganiserUI(organiser.Id, organiser.Name));
            }
            OrganiserComboBox.ItemsSource = organiserUIs;
            OrganiserComboBox.DisplayMemberPath = "Name";

        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            activityUIs.Clear();
            if(OrganiserComboBox.SelectedItem != null)
            {
                OrganiserUI SelectedOrganiser = (OrganiserUI)OrganiserComboBox.SelectedItem;
                int id = SelectedOrganiser.Id;
                foreach(var a in activityManager.GetActivitiesByOrganiserId(id))
                {
                    activityUIs.Add(new ActivityUI(a.Id, a.Fixture, a.NrOfPlaces, a.Description.Duration, a.Description.Location, a.Description.Explanation, a.Description.Name, a.PriceInfo.AdultPrice, a.PriceInfo.ChildPrice,a.PriceInfo.Discount));
                }
                ActivityDataGrid.ItemsSource = activityUIs;
            }
        }

        private void MenuItemAddActivity_Click(object sender, RoutedEventArgs e)
        {
            ActivityWindow window = new ActivityWindow(null);
            if (window.ShowDialog() == true)
            {
                
            }
        }

        private void MenuItemDeleteActivity_Click(object sender, RoutedEventArgs e)
        {
            ActivityWindow window = new ActivityWindow(null);


            if(ActivityDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select the activity you want to delete!", "Delete", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            else
            {
                ActivityWindow window = new ActivityWindow((CustomerUI)CustomerDataGrid.SelectedItem);
                window.DeleteCustomer();
                RefreshCustomerData();
            }

            //if (window.ShowDialog() == true)
            //{
               
            //}
        }

        private void MenuItemUpdateActivity_Click(object sender, RoutedEventArgs e)
        {
            ActivityWindow window = new ActivityWindow();
            if (window.ShowDialog() == true)
            {

            }
        }

     
    }
}
