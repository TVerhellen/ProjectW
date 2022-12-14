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
        BitmapImage imgWebstoreBgBikeShop;
        List<Cultivator> cultivators;
        int indexCultListActiveSHownCult = 0;
        //Constructors
        public Webstore()
        {
            InitializeComponent();

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
            cultivators = DataManager.GetCultivators(GameManager.Instance().MyCharacter) ;
            DrawImgQual();
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

                //Start at tab 1
                indexCultListActiveSHownCult = 0;

                //Check Whether or not cultivators are still that of the logged in character
                cultivators = DataManager.GetCultivators(GameManager.Instance().MyCharacter);
                DrawImgQual();
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
            if (indexCultListActiveSHownCult < 5 && indexCultListActiveSHownCult > 0)
            {
                indexCultListActiveSHownCult--;
                DrawImgQual();
            }
            else if (indexCultListActiveSHownCult == 5)
            {
                indexCultListActiveSHownCult--;
                HideBikePage();
                CultPage();

                DrawImgQual();
            }
            

        }
        private void rectItemRight_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (indexCultListActiveSHownCult < 4 && indexCultListActiveSHownCult > -1)
            {
                indexCultListActiveSHownCult++;
                DrawImgQual();
            }
            else if (indexCultListActiveSHownCult == 4)
            {
                indexCultListActiveSHownCult++;
            }
            
            if (indexCultListActiveSHownCult == 5)
            {
                BikePage();
                HideCultPage();
            }
        }

        //Cultivators Upgrade Events
        private void rectUpgradeCultType_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cultivators[indexCultListActiveSHownCult].CultID < 3)
            {
                cultivators[indexCultListActiveSHownCult].CultID++;
                DataManager.UpdateCultivator(cultivators[indexCultListActiveSHownCult]);

                switch (cultivators[indexCultListActiveSHownCult].CultID)
                {
                    case 1:
                        GameManager.Instance().MyCharacter.Money -= 20;
                        break;

                    case 2:
                        GameManager.Instance().MyCharacter.Money -= 50;
                        break;

                    case 3:
                        GameManager.Instance().MyCharacter.Money -= 80;
                        break;
                    case 4:
                        GameManager.Instance().MyCharacter.Money -= 120;
                        break;

                    default:
                        break;
                }
                DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
                ResetCultivatorList();
                DrawImgQual();
            }
            else
            {
                MessageBox.Show("Maximum behaalt.", "Kan niet hoger :'(.", MessageBoxButton.OK);
            }
        }
        private void rectUpgradeLampType_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cultivators[indexCultListActiveSHownCult].LampID < 5 && cultivators[indexCultListActiveSHownCult].LampID != 2)
            {
                cultivators[indexCultListActiveSHownCult].LampID++;
                DataManager.UpdateCultivator(cultivators[indexCultListActiveSHownCult]);

                switch (cultivators[indexCultListActiveSHownCult].LampID)
                {
                    case 1:
                        GameManager.Instance().MyCharacter.Money -= 20;
                        break;

                    case 2:
                        GameManager.Instance().MyCharacter.Money -= 50;
                        break;

                    case 3:
                        GameManager.Instance().MyCharacter.Money -= 80;
                        break;
                    case 4:
                        GameManager.Instance().MyCharacter.Money -= 120;
                        break;

                    default:
                        break;
                }
                DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
                ResetCultivatorList();
                DrawImgQual();
            }
            else if (cultivators[indexCultListActiveSHownCult].LampID == 2)
            {
                cultivators[indexCultListActiveSHownCult].LampID += 2;
                DataManager.UpdateCultivator(cultivators[indexCultListActiveSHownCult]);

                switch (cultivators[indexCultListActiveSHownCult].LampID)
                {
                    case 1:
                        GameManager.Instance().MyCharacter.Money -= 20;
                        break;

                    case 2:
                        GameManager.Instance().MyCharacter.Money -= 50;
                        break;

                    case 3:
                        GameManager.Instance().MyCharacter.Money -= 80;
                        break;
                    case 4:
                        GameManager.Instance().MyCharacter.Money -= 120;
                        break;

                    default:
                        break;
                }
                DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
                ResetCultivatorList();
                DrawImgQual();
            }
            else
            {
                MessageBox.Show("Maximum behaalt.", "Kan niet hoger :'(.", MessageBoxButton.OK);
            }
        }
        private void rectUpgradeWater_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cultivators[indexCultListActiveSHownCult].WaterID < 5)
            {
                cultivators[indexCultListActiveSHownCult].WaterID++;
                DataManager.UpdateCultivator(cultivators[indexCultListActiveSHownCult]);

                switch (cultivators[indexCultListActiveSHownCult].WaterID)
                {
                    case 1:
                        GameManager.Instance().MyCharacter.Money -= 20;
                        break;

                    case 2:
                        GameManager.Instance().MyCharacter.Money -= 50;
                        break;

                    case 3:
                        GameManager.Instance().MyCharacter.Money -= 80;
                        break;
                    case 4:
                        GameManager.Instance().MyCharacter.Money -= 120;
                        break;

                    default:
                        break;
                }
                DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
                ResetCultivatorList();
                DrawImgQual();
            }
            else
            {
                MessageBox.Show("Maximum behaalt.", "Kan niet hoger :'(.", MessageBoxButton.OK);
            }
        }
        private void rectUpgradeSoil_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cultivators[indexCultListActiveSHownCult].SoilID < 5)
            {
                cultivators[indexCultListActiveSHownCult].SoilID++;
                DataManager.UpdateCultivator(cultivators[indexCultListActiveSHownCult]);

                switch (cultivators[indexCultListActiveSHownCult].SoilID)
                {
                    case 1:
                        GameManager.Instance().MyCharacter.Money -= 20;
                        break;

                    case 2:
                        GameManager.Instance().MyCharacter.Money -= 50;
                        break;

                    case 3:
                        GameManager.Instance().MyCharacter.Money -= 80;
                        break;
                    case 4:
                        GameManager.Instance().MyCharacter.Money -= 120;
                        break;

                    default:
                        break;
                }
                DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
                ResetCultivatorList();
                DrawImgQual();
            }
            else
            {
                MessageBox.Show("Maximum behaalt.", "Kan niet hoger :'(.", MessageBoxButton.OK);
            }
        }
        private void rectUpgradeFertilizer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (cultivators[indexCultListActiveSHownCult].FertilizerID < 5)
            {
                cultivators[indexCultListActiveSHownCult].FertilizerID++;
                DataManager.UpdateCultivator(cultivators[indexCultListActiveSHownCult]);   

                switch (cultivators[indexCultListActiveSHownCult].FertilizerID)
                {
                    case 1:
                        GameManager.Instance().MyCharacter.Money -= 20;
                        break;

                    case 2:
                        GameManager.Instance().MyCharacter.Money -= 50;
                        break;

                    case 3:
                        GameManager.Instance().MyCharacter.Money -= 80;
                        break;
                    case 4:
                        GameManager.Instance().MyCharacter.Money -= 120;
                        break;

                    default:
                        break;
                }
                DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
                ResetCultivatorList();
                DrawImgQual();
            }
            else
            {
                MessageBox.Show("Maximum behaalt.", "Kan niet hoger :'(.", MessageBoxButton.OK);
            }
        }

        //Character Upgrade Events
        private void rectPurchaseBike_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (GameManager.Instance().MyCharacter.HasBike == true)
            {
                MessageBox.Show("Je hebt al wieletjes.", "rol rol.", MessageBoxButton.OK);
            }
            else
            {
                GameManager.Instance().MyCharacter.HasBike = true;
                DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
                MessageBox.Show("Proficiat met de wieletjes.", "Rollend de deur uit.", MessageBoxButton.OK);
            }
            
        }
        private void ResetCultivatorList()
        {
            cultivators.Clear();
            cultivators = DataManager.GetCultivators(GameManager.Instance().MyCharacter);
        }
        private void DrawImgQual()
        {
            //cnvWebstore.Children.Add(ShapeWeed.DrawFlowerPotWeb(100, 50, cultivators[indexCultListActiveSHownCult], 846, 402));
            //cnvWebstore.Children.Add(ShapeWeed.DrawLampWeb(965, 355, cultivators[indexCultListActiveSHownCult]));
            lblFertilizer.Content = cultivators[indexCultListActiveSHownCult].FertilizerID;
            lblSoil.Content = cultivators[indexCultListActiveSHownCult].SoilID;
            lblWater.Content = cultivators[indexCultListActiveSHownCult].WaterID;
            lblSelectedItem.Content = $"Cultivator {indexCultListActiveSHownCult + 1}";
            //Lamp Qual
            switch (cultivators[indexCultListActiveSHownCult].LampID)
            {
                case 1:
                    polLamp.Fill = Brushes.Yellow;
                    break;

                case 2:
                    polLamp.Fill = Brushes.Violet;
                    break;

                case 4:
                    polLamp.Fill = Brushes.AliceBlue;
                    break;

                case 5:
                    polLamp.Fill = Brushes.Cyan;
                    break;

                default:
                    break;
            }
            // Pot Qual
            switch (cultivators[indexCultListActiveSHownCult].CultID)
            {
                case 1:
                    rectPot.Fill = Brushes.GreenYellow;
                    break;

                case 2:
                    rectPot.Fill = Brushes.BlueViolet;
                    break;

                case 3:
                    rectPot.Fill = Brushes.Purple;
                    break;

                default:
                    break;
            }

        }
        private void BikePage()
        {
            imgWebstoreBg.Source = imgWebstoreBgChar;
            lblSelectedItem.Content = "Transport";
        }
        private void CultPage()
        {
            imgWebstoreBg.Source = imgWebstoreBgCult;
        }
        // IsEnabled="False" Focusable="False" Visibility="Hidden"
        private void HideCultPage()
        {
            // Hide Visibility
            rectUpgradeCultType.Visibility = Visibility.Hidden;
            rectUpgradeFertilizer.Visibility = Visibility.Hidden;
            rectUpgradeLampType.Visibility = Visibility.Hidden;
            rectUpgradeSoil.Visibility = Visibility.Hidden;
            rectUpgradeWater.Visibility = Visibility.Hidden;

            // Focus = false
            rectUpgradeCultType.Focusable = false;
            rectUpgradeFertilizer.Focusable = false;
            rectUpgradeLampType.Focusable = false;
            rectUpgradeSoil.Focusable = false;
            rectUpgradeWater.Focusable = false;

            // IsEnabled = false
            rectUpgradeCultType.IsEnabled = false;
            rectUpgradeFertilizer.IsEnabled = false;
            rectUpgradeLampType.IsEnabled = false;
            rectUpgradeSoil.IsEnabled = false;
            rectUpgradeWater.IsEnabled = false;

            // Hide img
            imgWebstoreFert.Visibility = Visibility.Hidden;
            imgWebstoreSoil.Visibility = Visibility.Hidden;
            imgWebstoreWater.Visibility = Visibility.Hidden;
            polLamp.Visibility = Visibility.Hidden;
            rectPot.Visibility = Visibility.Hidden;

            // Hide lbl
            lblFertilizer.Visibility = Visibility.Hidden;
            lblSoil.Visibility = Visibility.Hidden;
            lblWater.Visibility = Visibility.Hidden;

            // Activate Bike
            rectPurchaseBike.IsEnabled = true;
            rectPurchaseBike.Focusable = true;
            rectPurchaseBike.Visibility = Visibility.Visible;

            imgWebstoreBike.Visibility = Visibility.Visible;
            imgWebstoreBike.IsEnabled = true;
            imgWebstoreBike.Focusable = true;

        }
        private void HideBikePage()
        {
            // Show Visibility
            rectUpgradeCultType.Visibility = Visibility.Visible;
            rectUpgradeFertilizer.Visibility = Visibility.Visible;
            rectUpgradeLampType.Visibility = Visibility.Visible;
            rectUpgradeSoil.Visibility = Visibility.Visible;
            rectUpgradeWater.Visibility = Visibility.Visible;

            // Focus = true
            rectUpgradeCultType.Focusable = true;
            rectUpgradeFertilizer.Focusable = true;
            rectUpgradeLampType.Focusable = true;
            rectUpgradeSoil.Focusable = true;
            rectUpgradeWater.Focusable = true;

            // IsEnabled = true
            rectUpgradeCultType.IsEnabled = true;
            rectUpgradeFertilizer.IsEnabled = true;
            rectUpgradeLampType.IsEnabled = true;
            rectUpgradeSoil.IsEnabled = true;
            rectUpgradeWater.IsEnabled = true;

            // Show img
            imgWebstoreFert.Visibility = Visibility.Visible;
            imgWebstoreSoil.Visibility = Visibility.Visible;
            imgWebstoreWater.Visibility = Visibility.Visible;
            polLamp.Visibility = Visibility.Visible;
            rectPot.Visibility = Visibility.Visible;

            // Show lbl
            lblFertilizer.Visibility = Visibility.Visible;
            lblSoil.Visibility = Visibility.Visible;
            lblWater.Visibility = Visibility.Visible;

            // Deactivate Bike
            rectPurchaseBike.IsEnabled = false;
            rectPurchaseBike.Focusable = false;
            rectPurchaseBike.Visibility = Visibility.Hidden;

            imgWebstoreBike.Visibility = Visibility.Hidden;
            imgWebstoreBike.IsEnabled = false;
            imgWebstoreBike.Focusable = false;
        }

    }
}
