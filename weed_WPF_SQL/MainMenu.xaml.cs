using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        //Member variables
        BitmapImage imgHomeBg;
        BitmapImage imgHomeExitBg;
        BitmapImage imgHomeHighscoreBg;
        BitmapImage imgHomeFarmBg;
        BitmapImage imgHomeSellingBg;
        BitmapImage imgHomeWebBg;

        //Constructors
        public MainMenu()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Set Bitmap Images
            SetSources();
        }

        //Methods
        private void SetSources()
        {
            //Default BG
            imgHomeBg = new BitmapImage();
            imgHomeBg.BeginInit();
            imgHomeBg.UriSource = new Uri("/Assets/img/Home.png", UriKind.Relative);
            imgHomeBg.EndInit();

            //Exit Hover
            imgHomeExitBg = new BitmapImage();
            imgHomeExitBg.BeginInit();
            imgHomeExitBg.UriSource = new Uri("/Assets/img/Home_Exit.png", UriKind.Relative);
            imgHomeExitBg.EndInit();

            //Highscores Hover
            imgHomeHighscoreBg = new BitmapImage();
            imgHomeHighscoreBg.BeginInit();
            imgHomeHighscoreBg.UriSource = new Uri("/Assets/img/Home_Highscore.png", UriKind.Relative);
            imgHomeHighscoreBg.EndInit();

            //Farming Hover
            imgHomeFarmBg = new BitmapImage();
            imgHomeFarmBg.BeginInit();
            imgHomeFarmBg.UriSource = new Uri("/Assets/img/Home_Farm.png", UriKind.Relative);
            imgHomeFarmBg.EndInit();

            //Selling Hover
            imgHomeSellingBg = new BitmapImage();
            imgHomeSellingBg.BeginInit();
            imgHomeSellingBg.UriSource = new Uri("/Assets/img/Home_Selling.png", UriKind.Relative);
            imgHomeSellingBg.EndInit();

            //Webstore Hover
            imgHomeWebBg = new BitmapImage();
            imgHomeWebBg.BeginInit();
            imgHomeWebBg.UriSource = new Uri("/Assets/img/Home_Web.png", UriKind.Relative);
            imgHomeWebBg.EndInit();
        }

        //Member functions
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Start Home Theme via MediaManager
            //MediaManager.Instance().PlayHomeTheme();
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Upon visibility change go to default
            if(this.IsVisible )
            {
                if(imgHomeBg.UriSource != null)
                {
                    imgMainMenuBg.Source = imgHomeBg;
                }

                //Start Home Theme via MediaManager
                MediaManager.Instance().ToggleAudio(btnAudioToggle,imgAudioToggle,GameManager.Scenes.Main,true);
            }
            
        }

        //Canvas Hover Events
        //Hover Over Farm
        private void rectToFarm_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Play Spray Sound
            MediaManager.Instance().PlaySoundSpray();
            //Display The OverlayedImage
            imgMainMenuBg.Source = imgHomeFarmBg;
        }
        private void rectToFarm_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgMainMenuBg.Source = imgHomeBg;
        }

        //Hover Over Webstore
        private void rectToStore_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Play Spray Sound
            MediaManager.Instance().PlaySoundSpray();
            //Display The OverlayedImage
            imgMainMenuBg.Source = imgHomeWebBg;
        }
        private void rectToStore_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgMainMenuBg.Source = imgHomeBg;
        }

        //Hover Over Highscore
        private void rectToHighscore_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Play Spray Sound
            MediaManager.Instance().PlaySoundSpray();
            //Display The OverlayedImage
            imgMainMenuBg.Source = imgHomeHighscoreBg;
        }

        private void rectToHighscore_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgMainMenuBg.Source = imgHomeBg;
        }

        //Hover Over Selling MiniGame
        private void rectToSelling_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Play Spray Sound
            MediaManager.Instance().PlaySoundSpray();
            //Display The OverlayedImage
            imgMainMenuBg.Source = imgHomeSellingBg;
        }
        private void rectToSelling_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgMainMenuBg.Source = imgHomeBg;
        }

        //Hover Over Exit Game
        private void rectExitGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Play Spray Sound
            MediaManager.Instance().PlaySoundSpray();
            //Display The OverlayedImage
            imgMainMenuBg.Source = imgHomeExitBg;
        }
        private void rectExitGame_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            imgMainMenuBg.Source = imgHomeBg;
        }

        //Canvas On Click Events
        //GO TO Farm
        private void rectToFarm_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Play Click Sound
            MediaManager.Instance().PlaySoundDoor();
            //Display The Farming Scene
            GameManager.Instance().ShowFarmingGameScreen();
            //Hide this scene
            this.Hide();
        }

        //GO TO Webstore
        private void rectToStore_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Play Click Sound
            MediaManager.Instance().PlaySoundClick();
            //Display The Webstore Scene
            GameManager.Instance().ShowWebstoreScreen();
            //Hide this scene
            this.Hide();
        }

        //GO TO Highscore
        private void rectToHighscore_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Play Click Sound
            MediaManager.Instance().PlaySoundClick();
            //Display The Highscore Scene
            GameManager.Instance().ShowHighscoreScreen();
            //Hide this scene
            this.Hide();
        }

        //GO TO Selling MiniGame
        private void rectToSelling_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Play Click Sound
            MediaManager.Instance().PlaySoundDoor();
            //Display The Selling Game Scene
            GameManager.Instance().ShowSellingGameScreen();
            //Hide this scene
            this.Hide();
        }

        //EXIT TO LOGIN
        private void rectExitGame_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Play Click Sound
            MediaManager.Instance().PlaySoundShutdown();
            //Display The Login Scene
            GameManager.Instance().ShowLoginScreen();
            //Hide this scene
            this.Hide();
        }

        private void btnAudioToggle_Click(object sender, RoutedEventArgs e)
        {
            //Sync Audio Symbol's State & Toggle Audio
            MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Main, false);
        }
    }
}
