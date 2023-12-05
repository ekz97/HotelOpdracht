using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Activities.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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

namespace Hotel.Presentation.Activities
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private List<DescriptionUI> descriptions = new List<DescriptionUI>(); 
        private List<PriceInfoUI> priceInfos = new List<PriceInfoUI>();
        private int _organiserId;
        private ActivityUI _activity;
        private ActivityManager _activityManager;

        public ActivityWindow(ActivityUI activityUI,int organiserId)
        {
            InitializeComponent();
            _organiserId = organiserId;
            _activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            _activity = activityUI;
            if (_activity == null)
            {

                SubmitBtn.Content = "Add";


            }
            else
            {
                SubmitBtn.Content = "Update";
                IdTextBox.Text = _activity.Id.ToString();
                FixtureTextBox.Text = _activity.Fixture.ToString();
                NrOfPlacesTextBox.Text = _activity.NrOfPlaces.ToString();
                DurationTextBox.Text = _activity.Description.Duration.ToString();
                LocationTextBox.Text = _activity.Description.Location;
                ExplanationTextBox.Text = _activity.Description.Explanation;
                NameTextBox.Text = _activity.Description.Name;
                AdultPriceTextBox.Text = _activity.PriceInfo.AdultPrice.ToString();
                ChildPriceTextBox.Text = _activity.PriceInfo.ChildPrice.ToString();
                DiscountTextBox.Text = _activity.PriceInfo.Discount.ToString();
                


            }
            foreach (var description in _activityManager.GetDescriptions())
            {
                descriptions.Add(new DescriptionUI(description.Duration, description.Explanation, description.Location, description.Name));
            }
            foreach(var priceInfo in _activityManager.GetPriceInfos()) 
            {
                priceInfos.Add(new PriceInfoUI(priceInfo.AdultPrice, priceInfo.ChildPrice, priceInfo.Discount));
            }
            PriceInfoComboBox.ItemsSource = priceInfos;
            PriceInfoComboBox.DisplayMemberPath = "DisplayString";
            DescriptionComboBox.ItemsSource = descriptions;
            DescriptionComboBox.DisplayMemberPath = "Name";
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DescriptionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DescriptionComboBox.SelectedItem != null)
            {
                DescriptionUI selectedDescription = (DescriptionUI)DescriptionComboBox.SelectedItem;

                DurationTextBox.Text = selectedDescription.Duration.ToString();
                LocationTextBox.Text = selectedDescription.Location.ToString();
                ExplanationTextBox.Text = selectedDescription.Explanation.ToString();
                NameTextBox.Text = selectedDescription.Name.ToString();

                DurationTextBox.IsEnabled = false;
                LocationTextBox.IsEnabled = false;
                ExplanationTextBox.IsEnabled = false;
                NameTextBox.IsEnabled = false;
            }
        }

        private void PriceInfoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PriceInfoComboBox.SelectedItem != null)
            {
                PriceInfoUI selectedPriceInfo = (PriceInfoUI)PriceInfoComboBox.SelectedItem;

                AdultPriceTextBox.Text = selectedPriceInfo.AdultPrice.ToString();
                ChildPriceTextBox.Text = selectedPriceInfo.ChildPrice.ToString();
                DiscountTextBox.Text = selectedPriceInfo.Discount.ToString();

                AdultPriceTextBox.IsEnabled = false;
                ChildPriceTextBox.IsEnabled = false;
                DiscountTextBox.IsEnabled = false;
            }
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {

            if (_activity == null)
            {
                AddActivity();
            }
            else
            {
               
            }

            DialogResult = true;
            Close();
        }


        private void UpdateActivity()
        {
            Activity activity = new Activity(Convert.ToInt32(IdTextBox.Text), Convert.ToDateTime(FixtureTextBox.SelectedDate), Convert.ToInt32(NrOfPlacesTextBox.Text), new Description(Convert.ToInt32(DurationTextBox.Text), LocationTextBox.Text, ExplanationTextBox.Text, NameTextBox.Text), new PriceInfo(Convert.ToInt32(AdultPriceTextBox.Text), Convert.ToInt32(ChildPriceTextBox.Text), Convert.ToInt32(DiscountTextBox.Text)));


           

        }

        private void AddActivity()
        {
            Activity activity = new Activity(0, Convert.ToDateTime(FixtureTextBox.SelectedDate), Convert.ToInt32(NrOfPlacesTextBox.Text), new Description(Convert.ToInt32(DurationTextBox.Text), LocationTextBox.Text, ExplanationTextBox.Text, NameTextBox.Text), new PriceInfo(Convert.ToInt32(AdultPriceTextBox.Text), Convert.ToInt32(ChildPriceTextBox.Text), Convert.ToInt32(DiscountTextBox.Text)));
            _activityManager.AddActivity(activity, _organiserId);
        }

        public void DeleteActivity()
        {

        }
    }


}
