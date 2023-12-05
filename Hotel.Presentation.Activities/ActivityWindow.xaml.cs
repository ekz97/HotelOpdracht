using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using Hotel.Presentation.Activities.Model;
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

namespace Hotel.Presentation.Activities
{
    /// <summary>
    /// Interaction logic for ActivityWindow.xaml
    /// </summary>
    public partial class ActivityWindow : Window
    {
        private List<DescriptionUI> descriptions = new List<DescriptionUI>(); 
        private List<PriceInfoUI> priceInfos = new List<PriceInfoUI>();

        private ActivityManager _activityManager;
        public ActivityWindow()
        {
            InitializeComponent();
            _activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            foreach(var description in _activityManager.GetDescriptions())
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
    }
}
