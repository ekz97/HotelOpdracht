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
        private ObservableCollection<OrganiserUI> organiserUIs = new ObservableCollection<OrganiserUI>();
        private int _organiserId = -1;

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
        private void RefreshDataGrid()
        {
            activityUIs.Clear();
            foreach (var a in activityManager.GetActivitiesByOrganiserId(_organiserId))
            {
                activityUIs.Add(new ActivityUI(a.Id, a.Fixture.ToString("dd-MM-yyyy"), a.NrOfPlaces, new DescriptionUI(a.Description.Duration, a.Description.Explanation, a.Description.Location, a.Description.Name), new PriceInfoUI(a.PriceInfo.AdultPrice, a.PriceInfo.ChildPrice, a.PriceInfo.Discount)));
            }
            ActivityDataGrid.ItemsSource = activityUIs;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            if(OrganiserComboBox.SelectedItem != null)
            {
                OrganiserUI SelectedOrganiser = (OrganiserUI)OrganiserComboBox.SelectedItem;
                _organiserId = SelectedOrganiser.Id;
                RefreshDataGrid();
            }
        }

        private void MenuItemAddActivity_Click(object sender, RoutedEventArgs e)
        {

           if(_organiserId < 0)
            {
                MessageBox.Show("Select an organiser before adding Activities.", "Add", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                ActivityWindow window = new ActivityWindow(null, _organiserId);
                if (window.ShowDialog() == true)
                {
                    RefreshDataGrid();
                }
            }
        }

        private void MenuItemDeleteActivity_Click(object sender, RoutedEventArgs e)
        {
            if (_organiserId < 0)
            {
                MessageBox.Show("Select an organiser before removing activities.", "Delete", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (ActivityDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Select the activity you want to delete!", "Delete", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    ActivityWindow window = new ActivityWindow((ActivityUI)ActivityDataGrid.SelectedItem, _organiserId);
                    window.DeleteActivity();
                    RefreshDataGrid();
                }
            }
        }

        private void MenuItemUpdateActivity_Click(object sender, RoutedEventArgs e)
        {
            if(_organiserId < 0)
            {
                MessageBox.Show("Select an organiser before updating activities.", "Update", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (ActivityDataGrid.SelectedItem == null)
                {
                    MessageBox.Show("Select the activity you want to update!", "Update", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    ActivityWindow window = new ActivityWindow((ActivityUI)ActivityDataGrid.SelectedItem, _organiserId);
                    if (window.ShowDialog() == true)
                    {
                        RefreshDataGrid();
                    }
                }
            }
        }
    }
}
