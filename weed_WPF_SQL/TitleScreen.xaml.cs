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

        //Properties


        //Methods
        /// <summary>
        /// Multi-Purpose Function that can Toggle The Audio Button when param is false, syncing from Singleton when true
        /// </summary>
        /// <param name="syncing">Only true when switching windows, false when expecting toggle behavior</param>
        private void ToggleAudio(bool syncing)
        {
            if (!MediaManager.Instance().blnMusicMuted)
            {
                if(!syncing) //When we are simply Toggling On/Off
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
            else
            {
                if(!syncing) //When we are simply Toggling On/Off
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

        //Form Events
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
        private void frmTitleScreen_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ToggleAudio(true);
        }
        private void frmTitleScreen_KeyDown(object sender, KeyEventArgs e)
        {
            GameManager.Instance().ShowLoginScreen();
            this.Hide();
        }


        //Form Element Events
        private void btnMuteMainTheme_Click(object sender, RoutedEventArgs e)
        {
            ToggleAudio(false);
        }


    }
}
