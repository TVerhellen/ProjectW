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

        //Methods
        /// <summary>
        /// Multi-Purpose Function that can Toggle The Audio Button when param is false, syncing from Singleton when true
        /// </summary>
        /// <param name="syncing">Only true when switching windows, false when expecting toggle behavior</param>
        private void ToggleAudio(bool syncing)
        {
            //When Music Has NOT Been Muted In MediaManager Singleton
            if (!MediaManager.Instance().blnMusicMuted)
            {
                if (!syncing) //When we are simply Toggling On/Off
                {
                    MediaManager.Instance().PauseMusic();
                    MediaManager.Instance().blnMusicMuted = true;
                    imgMuteMainTheme.Source = MediaManager.Instance().IcoMuted;
                    btnMuteMainTheme.Background = Brushes.DarkRed;
                }
                else //When we are syncronizing Audio toggle representation with other Windows through Singleton
                {
                    imgMuteMainTheme.Source = MediaManager.Instance().IcoUnmuted;
                    btnMuteMainTheme.Background = Brushes.LawnGreen;
                }

            }
            else//When Music Has Been Muted In MediaManager Singleton
            {
                if (!syncing) //When we are simply Toggling On/Off
                {
                    MediaManager.Instance().PlayMusic();
                    MediaManager.Instance().blnMusicMuted = false;
                    imgMuteMainTheme.Source = MediaManager.Instance().IcoUnmuted;
                    btnMuteMainTheme.Background = Brushes.LawnGreen;
                }
                else //When we are syncronizing Audio toggle representation with other Windows through Singleton
                {
                    imgMuteMainTheme.Source = MediaManager.Instance().IcoMuted;
                    btnMuteMainTheme.Background = Brushes.DarkRed;
                }

            }
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
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ToggleAudio(true);
        }

        //Form Element Events
        private void btnMuteMainTheme_Click(object sender, RoutedEventArgs e)
        {
            ToggleAudio(false);
        }

        private void btnBackToSplashScreen_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance().ShowTitleScreen();
            this.Hide();
        }
    }
}
