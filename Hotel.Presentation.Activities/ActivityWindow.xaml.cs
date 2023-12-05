using Hotel.Domain.Exceptions;
using Hotel.Domain.Managers;
using Hotel.Domain.Model;
using Hotel.Presentation.Activities.Model;
using Hotel.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ActivityUI? _activityUI { get; set; }
        private ObservableCollection<DescriptionUI> descriptionUIs = new ObservableCollection<DescriptionUI>();
        public  ObservableCollection<PriceInfoUI> priceInfoUIs = new ObservableCollection<PriceInfoUI>();
        private ActivityManager activityManager;
        public ActivityWindow(ActivityUI activityUI)
        {
            InitializeComponent();
            this._activityUI = activityUI;
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);

            if (_activityUI == null)
            {

                SubmitBtn.Content = "Submit activity";

            }

            else
            {

              
                SubmitBtn.Content = "Update activity";

                IdTextBox.Text = activityUI.Id.ToString();
                FixtureTextBox.Text = activityUI.Fixture.ToString();
                NrOfPlacesTextBox.Text = activityUI.NrOfPlaces.ToString();
                DurationTextBox.Text = activityUI.Description.Duration.ToString();
                LocationTextBox.Text = activityUI.Description.Location;
                ExplanationTextBox.Text = activityUI.Description.Explanation;
                NameTextBox.Text = activityUI.Description.Name;
           



            }
        }


      

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void FreezeInputsSelectedDescription()
        {
            DurationTextBox.IsEnabled = false;
            LocationTextBox.IsEnabled = false;
            ExplanationTextBox.IsEnabled = false;
            NameTextBox.IsEnabled = false;

        }

        private void FreezeInputsSelectedPriceInfo()
        {
            AdultPriceTextBox.IsEnabled = false;
            ChildPriceTextBox.IsEnabled = false;
            DiscountTextBox.IsEnabled = false;
        }

    }
}
