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
    /// Interaction logic for Webstore.xaml
    /// </summary>
    public partial class Webstore : Window
    {
        //Member Variables
        BitmapImage imgWebstoreBgCult;
        BitmapImage imgWebstoreBgChar;

        //Constructors
        public Webstore()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            SetSources();
        }

        //Methods
        private void SetSources()
        {
            //On Cultivators Upgrading Pages (Default BG)
            imgWebstoreBgCult = new BitmapImage();
            imgWebstoreBgCult.BeginInit();
            imgWebstoreBgCult.UriSource = new Uri("/Assets/img/Webstore_Cult.png", UriKind.Relative);
            imgWebstoreBgCult.EndInit();

            //On Character Upgrades Page
            imgWebstoreBgChar = new BitmapImage();
            imgWebstoreBgChar.BeginInit();
            imgWebstoreBgChar.UriSource = new Uri("/Assets/img/Webstore_Char.png", UriKind.Relative);
            imgWebstoreBgChar.EndInit();
        }
        //Window Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Upon visibility change go to default
            if (this.IsVisible)
            {
                if (imgWebstoreBgCult.UriSource != null)
                {
                    imgWebstoreBg.Source = imgWebstoreBgCult;
                }

                //Start Home Theme via MediaManager
                MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Webstore, true);

                GameManager.Instance().CenterWindowOnScreen(this);
            }
        }

        //Button Events
        private void btnAudioToggle_Click(object sender, RoutedEventArgs e)
        {
            //Sync Audio Symbol's State & Toggle Audio
            MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Webstore, false);
        }
        private void btnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            //Play The Click Sound
            MediaManager.Instance().PlaySoundClick();
            //Close This Window
            this.Hide();
            //Refresh and Display The Loginscreen
            GameManager.Instance().ShowMainMenuScreen();
        }

        //Canvas
        //Webstore Scroll Left Right Events
        private void rectItemLeft_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void rectItemRight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        //Cultivators Upgrade Events
        private void rectUpgradeCultType_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void rectUpgradeLampType_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void rectUpgradeWater_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void rectUpgradeSoil_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void rectUpgradeFertilizer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        //Character Upgrade Events
        private void rectPurchaseBike_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
