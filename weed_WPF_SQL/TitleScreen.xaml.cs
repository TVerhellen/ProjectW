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
using System.Timers;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for TitleScreen.xaml
    /// </summary>
    public partial class TitleScreen : Window
    {
        //Managers


        //Member Variables
        bool blnMusicMuted = false;
        bool blnFlashingDown = true;

        //Form Objects
        LoginScreen loginScreen;

        //Timer
        Timer flashTimer;

        public TitleScreen()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Initialize Forms
            loginScreen = new LoginScreen();

            //Initialize ticker
            flashTimer = new Timer();
            flashTimer.Interval = 20;
            flashTimer.Elapsed += FlashTimer_Elapsed;
        }

        private void frmTitleScreen_Loaded(object sender, RoutedEventArgs e)
        {
            //Set Up Images
            imgBackgroundSplashScreen.Source = MediaManager.Instance().ImgSplashScreenBg;

            //Stylize Titles
            lblGameTitle.FontFamily = MediaManager.Instance().FntTitleFont;
            //Stylize Sub-Titles
            lblPressAnyKey.FontFamily = MediaManager.Instance().FntSubFont;

            //Play Main Theme
            //MediaManager.Instance().PlayAudioFile(MediaManager.Instance().Mp3MainTheme);
            //or
            MediaManager.Instance().PlayMainTheme();

            //Start Dynamic Styling Timer
            flashTimer.Start();
        }

        private void frmTitleScreen_KeyDown(object sender, KeyEventArgs e)
        {
            ShowLogin();
            this.Close();
        }


        //Properties


        //Methods
        private void FlashPressAnyKey()
        {
            //Check if Outer Opacity Has Been Reached
            if (lblPressAnyKey.Opacity >= 1)
            {
                blnFlashingDown = true;
            }
            else if (lblPressAnyKey.Opacity <= 0.1)
            {
                blnFlashingDown = false;
            }

            //Start Scaling and Altering Opacity depending on direction
            if (blnFlashingDown)
            {
                lblPressAnyKey.FontSize--;
                lblPressAnyKey.Opacity -= 0.1;
            }
            else
            {
                lblPressAnyKey.FontSize++;
                lblPressAnyKey.Opacity += 0.1;
            }
        }

        //Events
        /// <summary>
        /// An Async Method to call on a Parallel Compute Thread To Call External UI functions
        /// </summary>
        private async void FlashTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Invoke our Method Upon Timer Tick
            Application.Current.Dispatcher.Invoke(new Action(() => { FlashPressAnyKey(); }));
        }

        //Forms Navigation Methods
        public void ShowLogin()
        {
            loginScreen.Show();
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
    }
}
