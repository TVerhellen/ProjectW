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
        //MainWindow splash;
        Character character;
        MainMenu mainMenu;
        
        //Constructors
        public LoginScreen()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Initialize Members
            //splash = new MainWindow();
            character = new Character();
            mainMenu = new MainMenu();
        }

        //Form Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Setup Images
            imgBackgroundLogin.Source = MediaManager.Instance().ImgLoginScreenBg;

            //Stylize Titles
            lblLoginTitle.FontFamily = MediaManager.Instance().FntTitleFont;
            //Stylize Username
            lblUsername.FontFamily = MediaManager.Instance().FntSubFont;
            txtUsername.FontFamily = MediaManager.Instance().FntMainFont;
            //Stylize Password
            lblPassword.FontFamily = MediaManager.Instance().FntSubFont;
            txtPassword.FontFamily = MediaManager.Instance().FntTitleFont;
            //Stylize Button
            btnLogin.FontFamily = MediaManager.Instance().FntTitleFont;

        }

        public void ShowSplashScreen()
        {
            //splash.Show();
            //this.Close();
        }

        public void ShowMainMenu()
        {
            mainMenu.Show(character);
            this.Close();
        }

        private void btnMuteMainTheme_Click(object sender, RoutedEventArgs e)
        {
            if (!MediaManager.Instance().blnMusicMuted)
            {
                MediaManager.Instance().MusicPlayer.Stop();
                MediaManager.Instance().blnMusicMuted = true;
                imgMuteMainTheme.Source = MediaManager.Instance().IcoMuted;
                btnMuteMainTheme.Background = Brushes.DarkRed;
            }
            else
            {
                MediaManager.Instance().MusicPlayer.Play();
                MediaManager.Instance().blnMusicMuted = false;
                imgMuteMainTheme.Source = MediaManager.Instance().IcoUnmuted;
                btnMuteMainTheme.Background = Brushes.LawnGreen;
            }
        }

        private void btnBackToSplashScreen_Click(object sender, RoutedEventArgs e)
        {
            ShowSplashScreen();
        }
    }
}
