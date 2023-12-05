using Hotel.Domain.Managers;
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
        public ObservableCollection<OrganiserUI> organisers = new ObservableCollection<OrganiserUI>();

        private OrganiserManager organiserManager;
        public MainWindow()
        {
            InitializeComponent();
            organiserManager = new OrganiserManager(RepositoryFactory.OrganiserRepository);
            //OrganiserComboBox.ItemsSource = 
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {

        }

     





    }
}
