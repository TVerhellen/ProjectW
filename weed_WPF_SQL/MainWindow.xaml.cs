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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Member Variables
        LoginScreen loginScreen;

        //Constructors
        public MainWindow()
        {
            //Initialize
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Initialize Members
            loginScreen = new LoginScreen();

        }

        //Form Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Set Up Images
            imgBackgroundSplashScreen.Source = DataManager.Instance().ImgSplashScreenBg;

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            ShowLogin();
            this.Close();
        }

        //Properties

        //Methods
        public void ShowLogin()
        {
            loginScreen.Show();
        }

        //Events
    }
}
