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
        //Member Variables
        bool blnFlashingDown = true;

        //Form Objects
        LoginScreen loginScreen;

        //Timer
        Timer fadeTimer;
        Timer flashTimer;

        public TitleScreen()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Initialize Forms
            loginScreen = new LoginScreen();

            //Initialize tickers
            fadeTimer = new Timer();
            fadeTimer.Interval = 69;
            fadeTimer.Elapsed += FadeTimer_Elapsed;
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
        //private void ToggleAudio(bool syncing)
        //{
        //    if (!MediaManager.Instance().AudioMuted)
        //    {
        //        if(!syncing) //When we are simply Toggling On/Off
        //        {
        //            MediaManager.Instance().PauseMusic();
        //            MediaManager.Instance().AudioMuted = true;
        //            imgMuteMainTheme.Source = MediaManager.Instance().IcoMuted;
        //            btnMuteMainTheme.Background = Brushes.DarkRed;
        //        }
        //        else //When we are syncronizing Audio toggle representation with other Windows through Singleton
        //        {
        //            imgMuteMainTheme.Source = MediaManager.Instance().IcoUnmuted;
        //            btnMuteMainTheme.Background = Brushes.LawnGreen;
        //            if (!MediaManager.Instance().CheckCurrentAudioUri(MediaManager.Instance().Mp3MainTheme))
        //            {
        //                MediaManager.Instance().PlayMainTheme();
        //            }
        //        }

        //    }
        //    else
        //    {
        //        if(!syncing) //When we are simply Toggling On/Off
        //        {
        //            MediaManager.Instance().PlayMusic();
        //            MediaManager.Instance().AudioMuted = false;
        //            imgMuteMainTheme.Source = MediaManager.Instance().IcoUnmuted;
        //            btnMuteMainTheme.Background = Brushes.LawnGreen;
        //        }
        //        else //When we are syncronizing Audio toggle representation with other Windows through Singleton
        //        {
        //            imgMuteMainTheme.Source = MediaManager.Instance().IcoMuted;
        //            btnMuteMainTheme.Background = Brushes.DarkRed;
        //        }

        //    }
        //}
        private void HideForFader()
        {
            lblGameTitle.Visibility = Visibility.Hidden;
            lblGameTitleBg.Visibility = Visibility.Hidden;
            lblPressAnyKey.Visibility = Visibility.Hidden;
            lblPressAnyKeyBg.Visibility = Visibility.Hidden;
            btnAudioToggle.Visibility = Visibility.Hidden;
        }
        private void RevealAfterFader()
        {
            lblGameTitle.Visibility = Visibility.Visible;
            lblGameTitleBg.Visibility = Visibility.Visible;
            lblPressAnyKey.Visibility = Visibility.Visible;
            lblPressAnyKeyBg.Visibility = Visibility.Visible;
            btnAudioToggle.Visibility = Visibility.Visible;
        }
        private void FadeTitle()
        {
            if(lblFader.Opacity > 0)
            {
                lblFader.Opacity -= 0.008;
            }
            else
            {
                RevealAfterFader();
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
        private async void FadeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Invoke our Method Upon Timer Tick
            Application.Current.Dispatcher.Invoke(new Action(() => { FadeTitle(); }));
        }
        /// <summary>
        /// An Async Method to call on a Parallel Compute Thread To Call External UI functions
        /// </summary>
        private async void FlashTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Invoke our Method Upon Timer Tick
            Application.Current.Dispatcher.Invoke(new Action(() => { FlashPressAnyKey(); }));
        }

        //Form Events
        private void frmTitleScreen_Closed(object sender, EventArgs e)
        {
            fadeTimer.Stop();
            flashTimer.Stop();
            GameManager.Instance().Shutdown();
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

            //Start Fading Timer
            fadeTimer.Start();
            //Hide Elements
            HideForFader();

            //Start Dynamic Styling Timer
            flashTimer.Start();
        }
        private void frmTitleScreen_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.IsVisible)
            {
                //Scene Has been made visible for the first time or has been reopened
                MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Title, true);
            }
        }
        private void frmTitleScreen_KeyDown(object sender, KeyEventArgs e)
        {
            //Play The Iconic Sound
            //MediaManager.Instance().PlaySoundStart(); //Easter Egg ?
            //Switch Scenes
            GameManager.Instance().ShowLoginScreen();
            //Hide this Window
            this.Hide();
        }


        //Form Element Events
        private void btnAudioToggle_Click(object sender, RoutedEventArgs e)
        {
            //Simply Request an Audio Toggle to MediaManager
            MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Title, false);
        }
    }
}
