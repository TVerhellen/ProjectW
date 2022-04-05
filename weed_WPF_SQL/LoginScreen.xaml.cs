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

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        Character character;
        MainMenu mainMenu;
        
        //Constructors
        public LoginScreen()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Initialize Members
            character = new Character();
            mainMenu = new MainMenu();
        }

        //Form Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Setup Images
            imgBackgroundLogin.Source = DataManager.Instance().ImgLoginScreenBg;

            //Stylize Titles
            lblLoginTitle.FontFamily = DataManager.Instance().FntTitleFont;
            //Stylize Username
            lblUsername.FontFamily = DataManager.Instance().FntSubFont;
            txtUsername.FontFamily = DataManager.Instance().FntMainFont;
            //Stylize Password
            lblPassword.FontFamily = DataManager.Instance().FntSubFont;
            txtPassword.FontFamily = DataManager.Instance().FntMainFont;
            //Stylize Button
            btnLogin.FontFamily = DataManager.Instance().FntTitleFont;

        }

        public void ShowMainMenu()
        {
            mainMenu.Show(character);
            this.Close();
        }
    }
}
