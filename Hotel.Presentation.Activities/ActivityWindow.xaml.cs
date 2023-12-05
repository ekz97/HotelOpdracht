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
        //private ObservableCollection<Des> _members = new ObservableCollection<MemberUI>();
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
                DurationTextBox.Text = activityUI.DurationDescription.ToString();
                LocationTextBox.Text = "";
                ExplanationTextBox.Text = "";
                NameTextBox.Text = "";



            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
