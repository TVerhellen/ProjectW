using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Image = System.Windows.Controls.Image;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for FarmGame.xaml
    /// </summary>
    public partial class FarmGame : Window
    {

        List<Name> allStrains = new List<Name>();
        List<Cultivator> alleCultivators;
        List<ProgressBar> allProgressbars = new List<ProgressBar>();
        List<ProgressBar> allHealthBars = new List<ProgressBar>();
        List<Image> allWater = new List<Image>();
        List<Image> allFert = new List<Image>();

        public double lampLeft;
        public double lampTop;
        public double tableLeft;
        public double tableTop;
        public double flowerPotLeft;
        public double flowerPotTop;
        public double imageWeedLeft;
        public double imageWeedTop;
        private int secondsPassed = 0;
        private int duurCycle = 20;

        public BitmapImage img = new BitmapImage();
        Cultivator myDefaultCultivator = new Cultivator() { Titelbar = 1, CyclesPassed = 9 };
        DispatcherTimer Tim;

        //Constructors
        public FarmGame()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            allStrains = DataManager.GetStrainNames();

            Tim = new DispatcherTimer();
            Tim.Interval = TimeSpan.FromSeconds(1);
            Tim.Tick += timer_Tick;
        }

        //Window Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Call Media Singleton for Themesong
            MediaManager.Instance().PlayFarmingTheme();

            //All Progressbar associated by Cultivator
            allProgressbars.Add(pgrProgressPlant1);
            allProgressbars.Add(pgrProgressPlant2);
            allProgressbars.Add(pgrProgressPlant3);
            allProgressbars.Add(pgrProgressPlant4);
            allProgressbars.Add(pgrProgressPlant5);

            allHealthBars.Add(pgrHealthPlant1);
            allHealthBars.Add(pgrHealthPlant2);
            allHealthBars.Add(pgrHealthPlant3);
            allHealthBars.Add(pgrHealthPlant4);
            allHealthBars.Add(pgrHealthPlant5);

            //Start Cycles Timer
            Tim.Start();

            //Populate Combobox
            cmbSelectStrain.Items.Add("--Select strain--");

            foreach (var item in allStrains)
            {
                cmbSelectStrain.Items.Add(item);
            }

            //Access Database for Cultivator Data
            alleCultivators = DataManager.GetCultivatorsByFarmID(DataManager.GetFarmByCharacterID(GameManager.Instance().MyCharacter));
            alleCultivators.Insert(0, myDefaultCultivator);

            // Populate Listbox overview
            UpdateOverviewListBox();

            //Attach Events to Cultivators
            for (int i = 0; i < alleCultivators.Count; i++)
            {
                if (i == 0)
                {
                    // Default
                }
                else
                {
                    alleCultivators[i].PlantNoWaterEvent += MyCultivator_PlantNoWaterEvent;
                    alleCultivators[i].PlantNoFertilizerEvent += MyCultivator_PlantNoFertilizerEvent;
                    alleCultivators[i].PlantWaterRequirementEvent += MyCultivator_PlantWaterRequirementEvent;
                    alleCultivators[i].PlantFertilizerRequirementEvent += MyCultivator_PlantFertilizerRequirementEvent;
                    alleCultivators[i].PlantAlmostDied += MyCultivator_PlantAlmostDied;
                    alleCultivators[i].ProgressMonitorEvent += MyCultivator_ProgressMonitorEvent;
                    alleCultivators[i].HealthMonitorEvent += MyCultivator_HealthMonitorEvent;
                    alleCultivators[i].NoHarvestEvent += MyCultivator_NoHarvestEvent;
                }
            }

            //initial Value Display progress and Health bars
            UpdateProgressbars(alleCultivators);
            UpdateHealthbars(alleCultivators);

            //Refresh Canvas
            UpdateCanvas();

            //Assign Images to list
            SetImageSources();

            //Check Cultivators For Status
            if (alleCultivators.Count > 0)
            {
                CheckCultivators();
            }

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            GameManager.Instance().Shutdown();
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.IsVisible)
            {
                //Reposition Window
                GameManager.Instance().CenterWindowOnScreen(this);

                //Access Database for Cultivator Data
                alleCultivators = DataManager.GetCultivatorsByFarmID(DataManager.GetFarmByCharacterID(GameManager.Instance().MyCharacter));
                alleCultivators.Insert(0, myDefaultCultivator);

                //Check Cultivators For Status
                if (alleCultivators.Count > 0)
                {
                    if(allWater.Count <= 0 || allFert.Count <= 0)
                    {
                        SetImageSources();
                    }
                    CheckCultivators();
                    Tim.Start();
                }

            }

        }

        //Button Events
        private void btnReturnToHome_Click(object sender, RoutedEventArgs e)
        {
            Tim.Stop();
            GameManager.Instance().ShowMainMenuScreen();
            this.Hide();
        }
        private void btnAudioToggle_Click(object sender, RoutedEventArgs e)
        {
            //Sync Audio Symbol's State & Toggle Audio
            MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Farm, false);
        }

        private void btnAddWeedPlant_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lbOverviewCultivators.SelectedIndex != 0 && lbOverviewCultivators.SelectedIndex > 0)
                {
                    // Cast Cultivator from listbox to new object
                    Cultivator newCultivator = (Cultivator)lbOverviewCultivators.SelectedItem;


                    if (newCultivator.CyclesPassed == 11)
                    {
                        // Select Strain and put CyclesPassed to 0
                        if (cmbSelectStrain.SelectedIndex != 0 && cmbSelectStrain.SelectedIndex > 0)
                        {
                            newCultivator.NameID = cmbSelectStrain.SelectedIndex;
                            newCultivator.CyclesPassed = 0;

                            // Reset cultivator to default values
                            newCultivator.RendementValue = 3;
                            newCultivator.ProgresBarColor = 1;
                            newCultivator.WaterSupply = 3;
                            newCultivator.FertilizerSupply = 5;
                            UpdateOverviewListBox();
                            UpdateCanvas();
                            UpdateProgressbars(alleCultivators);
                            UpdateHealthbars(alleCultivators);
                        }
                        else
                        {
                            throw new Exception("Bro, Je hebt geen wietsoort geselecteerd.");
                        }
                    }
                    else
                    {
                        throw new Exception("Bro, er is nog geen bloempot geplaatst!");
                    }

                }
                else
                {
                    throw new Exception("Bro, je hebt nog geen positie aangeklikt waar je een nieuwe plant wil planten.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnGiveWater_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (lbOverviewCultivators.SelectedIndex != 0 && lbOverviewCultivators.SelectedIndex > -1)
                {
                    // Cast selected plant to a new object from listbox
                    Cultivator giveWaterToCultivator = (Cultivator)lbOverviewCultivators.SelectedItem;

                    if(giveWaterToCultivator.CyclesPassed < 11 && giveWaterToCultivator.CyclesPassed > 0)
                    {
                        // Reset watersupply
                        giveWaterToCultivator.WaterSupplyPlus = 3;

                        // Give message
                        MessageBox.Show($"Yeah bro, Cultivator {giveWaterToCultivator.CultivatorID} {DataManager.GetStrainNameofCultivator(giveWaterToCultivator)} heeft water bijgekregen!");

                        //Hide Potential Status Images
                        if(allWater[lbOverviewCultivators.SelectedIndex-1].Visibility == Visibility.Visible)
                        {
                            allWater[lbOverviewCultivators.SelectedIndex-1].Visibility = Visibility.Hidden;
                        }

                        // Update cultivator
                        DataManager.UpdateCultivator(giveWaterToCultivator);
                    }
                    else
                    {
                        throw new Exception("Bro, er is nog geen bloempot geplaatst!");
                    }
                }
                else
                {
                    throw new Exception("Bro, aan welke bloempot wil je water geven?");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnAddFertilizer_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (lbOverviewCultivators.SelectedIndex != 0 && lbOverviewCultivators.SelectedIndex > 0)
                {
                    // Cast selected plant to a new object from listbox
                    Cultivator giveFertilizerToCultivator = (Cultivator)lbOverviewCultivators.SelectedItem;

                    if (giveFertilizerToCultivator.CyclesPassed < 11 && giveFertilizerToCultivator.CyclesPassed > 0)
                    {
                        // Reset watersupply
                        giveFertilizerToCultivator.FertilizerSupplyPlus = 5;

                        // Give message
                        MessageBox.Show($"Yeah bro, Cultivator {giveFertilizerToCultivator.CultivatorID} {DataManager.GetStrainNameofCultivator(giveFertilizerToCultivator)} heeft meststof bijgekregen!");

                        //Hide Potential Status Images
                        if (allFert[lbOverviewCultivators.SelectedIndex-1].Visibility == Visibility.Visible)
                        {
                            allFert[lbOverviewCultivators.SelectedIndex-1].Visibility = Visibility.Hidden;
                        }

                        // Update cultivator
                        DataManager.UpdateCultivator(giveFertilizerToCultivator);
                    }
                    else
                    {
                        throw new Exception("Bro, er is nog geen bloempot geplaatst!");
                    }
                }
                else
                {
                    throw new Exception("Bro, aan welke bloempot wil je voeding geven?");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnHarvestCrops_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (lbOverviewCultivators.SelectedIndex != 0 && lbOverviewCultivators.SelectedIndex > 0)
                {
                    // Cast selected plant to a new object from listbox
                    Cultivator harvestCultivator = (Cultivator)lbOverviewCultivators.SelectedItem;

                    if (harvestCultivator.CyclesPassed < 11 && harvestCultivator.CyclesPassed > 0)
                    {
                        if (harvestCultivator.CyclesPassed >= 8)
                        {
                            // Call harvested weed
                            double harvestRendement = (double)harvestCultivator.RendementValue * 10;

                            // Give the harvested weed to our character
                            GameManager.Instance().MyCharacter.Weed += Convert.ToInt32(harvestRendement);
                            DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);

                            // Give message
                            MessageBox.Show($"Yeah bro, Plant {DataManager.GetStrainNameofCultivator(harvestCultivator)} heeft vette toppen!");

                            //Fertilizers
                            if (allFert[lbOverviewCultivators.SelectedIndex - 1].Visibility == Visibility.Visible)
                            {
                                allFert[lbOverviewCultivators.SelectedIndex - 1].Visibility = Visibility.Hidden;
                            }
                            //Waters
                            if (allWater[lbOverviewCultivators.SelectedIndex - 1].Visibility == Visibility.Visible)
                            {
                                allWater[lbOverviewCultivators.SelectedIndex - 1].Visibility = Visibility.Hidden;
                            }

                            // Update cultivator
                            UpdatePlantWhenReady(harvestCultivator);
                        }
                        else
                        {
                            throw new Exception("Bro, chill, uw plantje is nog aan het groeien!");
                        }

                    }
                    else
                    {
                        throw new Exception("Bro, er is nog geen bloempot geplaatst!");
                    }
                }
                else
                {
                    throw new Exception("Bro, welke big crops wil je oogsten?");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        // Methods
        private void SetImageSources()
        {
            //Water Images
            allWater.Add(imgWater1);
            allWater.Add(imgWater2);
            allWater.Add(imgWater3);
            allWater.Add(imgWater4);
            allWater.Add(imgWater5);

            //Fertilizer Images
            allFert.Add(imgFert1);
            allFert.Add(imgFert2);
            allFert.Add(imgFert3);
            allFert.Add(imgFert4);
            allFert.Add(imgFert5);

            foreach (Image image in allWater)
            {
                image.Width = 50;
                image.Height = 50;
            }

            foreach (Image image in allFert)
            {
                image.Width = 50;
                image.Height = 50;
            }
        }
        private void CheckCultivators()
        {
            int pos = 0;

            foreach (Cultivator cultivator in alleCultivators)
            {
                if(pos > 0)
                {
                    //Check Water
                    if (cultivator.WaterSupply <= 1)
                    {
                        allWater[pos-1].Visibility = Visibility.Visible;
                    }

                    //Check Fertilizer
                    if (cultivator.FertilizerSupply <= 2)
                    {
                        allFert[pos-1].Visibility = Visibility.Visible;
                    }
                }

                pos++;
            }
        }
        private void PositionLampAndPot(int index)
        {
            if (index == 1)
            {
                lampLeft = 50;
                flowerPotLeft = 100;
            }
            else
            {
                lampLeft += 200;
                flowerPotLeft += 200;
            }
            lampTop = 20;
            flowerPotTop = 450;
        }
        private void PositionWeedImage(int i)
        {
            if (i == 1)
            {
                if (alleCultivators[i].CyclesPassed == 1 || alleCultivators[i].CyclesPassed < 4)
                {
                    imageWeedLeft = 100;
                    imageWeedTop = 245;
                }
                else
                {
                    imageWeedLeft = 50;
                    imageWeedTop = 149;
                }
            }
            else
            {
                if (alleCultivators[i].CyclesPassed == 1 || alleCultivators[i].CyclesPassed < 4)
                {
                    if (alleCultivators[i - 1].CyclesPassed >= 4)
                    {
                        imageWeedLeft += 240;
                    }
                    else
                    {
                        if (alleCultivators[i - 1].CyclesPassed > 5)
                        {
                            imageWeedLeft += 180;
                        }
                        else
                        {
                            imageWeedLeft += 200;
                        }

                    }
                    imageWeedTop = 245;
                }
                else
                {
                    if (alleCultivators[i - 1].CyclesPassed >= 4)
                    {
                        imageWeedLeft += 200;
                    }
                    else
                    {
                        imageWeedLeft += 160;
                    }
                    imageWeedTop = 149;
                }

            }
        }

        private void DrawImage(double width, double height, double left, double top, string path)
        {
            // Create Image and set its width and height  
            Image dynamicImage = new Image();
            dynamicImage.Width = width;
            dynamicImage.Height = height;

            Canvas.SetLeft(dynamicImage, left);
            Canvas.SetTop(dynamicImage, top);

            // Create a BitmapSource  
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Relative);
            bitmap.EndInit();

            // Set Image.Source  
            dynamicImage.Source = bitmap;

            // Add Image to Window  
            cnvFarmProjection.Children.Add(dynamicImage);
        }

        private void GetImageWeed(double imageLeft, double imageTop, Cultivator obj)
        {
            switch (obj.CyclesPassed)
            {
                case 1:
                    DrawImage(100, 280, imageLeft, imageTop, "Images/WeedBaby.png");
                    break;
                case 2:
                    DrawImage(100, 280, imageLeft, imageTop, "Images/WeedBaby.png");
                    break;
                case 3:
                    DrawImage(100, 280, imageLeft, imageTop, "Images/WeedBaby.png");
                    break;
                case 4:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/WeedPhase2.png");
                    break;
                case 5:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/WeedPhase2.png");
                    break;
                case 6:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/WeedPhase2.png");
                    break;
                case 7:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/WeedPhase2.png");
                    break;
                case 8:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/Bag.png");
                    break;
                case 9:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/Bag.png");
                    break;
                case 10:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/Bag.png");
                    break;
                default:
                    break;
            }
        }
        private void UpdateCanvas()
        {
            cnvFarmProjection.Children.Clear();

            tableLeft = 50;
            tableTop = 500;
            cnvFarmProjection.Children.Add(ShapeWeed.DrawTable(1000, 210, tableLeft, tableTop));

            if (alleCultivators.Count > 2)
            {
                for (int i = 0; i < alleCultivators.Count; i++)
                {
                    if (i == 0)
                    {
                        // Default
                    }
                    else
                    {
                        PositionLampAndPot(i);
                        PositionWeedImage(i);
                        cnvFarmProjection.Children.Add(ShapeWeed.DrawLamp(lampLeft, lampTop, alleCultivators[i]));
                        cnvFarmProjection.Children.Add(ShapeWeed.DrawFlowerPot(100, 50, alleCultivators[i], flowerPotLeft, flowerPotTop));
                        GetImageWeed(imageWeedLeft, imageWeedTop, alleCultivators[i]);
                    }
                }
            }
        }

        private void UpdateOverviewListBox()
        {
            lbOverviewCultivators.ItemsSource = null;
            lbOverviewCultivators.ItemsSource = alleCultivators;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            secondsPassed++;

            if (secondsPassed == duurCycle)
            {
                foreach (var item in alleCultivators)
                {
                    if (item.CyclesPassed <= 10)
                    {
                        item.CyclesPassedPlus++;
                        item.WaterSupplyPlus--;
                        item.FertilizerSupplyPlus--;
                        DataManager.UpdateCultivator(item);
                    }
                }

                UpdateCanvas();
                secondsPassed = 0;
            }
        }

        private void UpdateProgressbars(List<Cultivator> allcultivators)
        {
            foreach (var item in alleCultivators)
            {
                if (loop == 0)
                {
                    // Default
                }
                else
                {
                    if (item.CyclesPassed <= 10)
                    {
                        allProgressbars[loop - 1].Visibility = Visibility.Visible;
                        allProgressbars[loop - 1].Value = alleCultivators[loop].CyclesPassedPlus * 12.5;
                        DataManager.UpdateCultivator(item);
                    }
                    else
                    {
                        allProgressbars[loop - 1].Visibility = Visibility.Hidden;
                    }
                }
                loop++;
            }

            loop = 0;
        }
        private void UpdateHealthbars(List<Cultivator> allcultivators)
        {
            foreach (var item in alleCultivators)
            {
                if (loop == 0)
                {
                    // Default
                }
                else
                {
                    if (item.CyclesPassed <= 10)
                    {
                        allHealthBars[loop - 1].Visibility = Visibility.Visible;
                        allHealthBars[loop - 1].Value = alleCultivators[loop].RendementValuePlus == null ? 0 : (double)alleCultivators[loop].RendementValuePlus * 10;

                        if (alleCultivators[loop].RendementValuePlus < 3)
                        {
                            allHealthBars[loop - 1].Background = Brushes.Red;
                        }
                        else if (alleCultivators[loop].RendementValuePlus > 3 || alleCultivators[loop].RendementValuePlus < 7)
                        {
                            allHealthBars[loop - 1].Background = Brushes.Yellow;
                        }
                        else
                        {
                            allHealthBars[loop - 1].Background = Brushes.Red;
                        }
                        DataManager.UpdateCultivator(item);
                    }
                    else
                    {
                        allHealthBars[loop - 1].Visibility = Visibility.Hidden;
                    }
                }
                loop++;
            }

            loop = 0;
        }

        private void UpdatePlantWhenReady(Cultivator harvestCultivator)
        {
            // Reset cultivator to default values
            harvestCultivator.NameID = 1;
            harvestCultivator.RendementValue = null;
            harvestCultivator.ProgresBarColor = null;
            harvestCultivator.WaterSupply = null;
            harvestCultivator.FertilizerSupply = null;
            harvestCultivator.CyclesPassed = 11;

            // Update Cultivator from character in database
            DataManager.UpdateCultivator(harvestCultivator);

            // Update listbox overview plants
            UpdateOverviewListBox();

            // Update Canvas
            UpdateCanvas();

            // Update HealthBar and ProgressBar
            UpdateHealthbars(alleCultivators);
            UpdateProgressbars(alleCultivators);

            //Hide Notifications panel
            pnlNotification.Visibility = Visibility.Hidden;
        }

        // Events
        int loop = 0;
        private void MyCultivator_HealthMonitorEvent(object r)
        {
            UpdateHealthbars(alleCultivators);
        }

        private void MyCultivator_ProgressMonitorEvent(Cultivator obj)
        {
            UpdateProgressbars(alleCultivators);
        }

        private void MyCultivator_PlantAlmostDied(Cultivator obj)
        {
            //MessageBox.Show($"You are a shitty drugsaddict! Cultivator {obj.CultivatorID} {DataManager.GetStrainNameofCultivator(obj)} is almost died!");
            lblNotification.Content = $"Let Op! Bloempot {obj.CultivatorID} met {DataManager.GetStrainNameofCultivator(obj)} plant is bijna dood!";
            pnlNotification.Visibility = Visibility.Visible;
        }

        private void MyCultivator_PlantFertilizerRequirementEvent(Cultivator obj)
        {
            //MessageBox.Show($"Bro, give Cultivator {obj.CultivatorID} {DataManager.GetStrainNameofCultivator(obj)} some shit!");

            //IMAGE INTERACTION
            int pos = -1;
            foreach (Cultivator cult in alleCultivators)
            {
                if (cult.CultivatorID == obj.CultivatorID)
                {
                    //Found a Match To Light Up
                    break;
                }
                pos++;
            }

            allFert[pos].Visibility = Visibility.Visible;
        }

        private void MyCultivator_PlantWaterRequirementEvent(Cultivator obj)
        {
            //Cultivator x = sender as Cultivator;
            //MessageBox.Show($"Bro, give Cultivator {obj.CultivatorID} {DataManager.GetStrainNameofCultivator(obj)} some water!");

            //IMAGE INTERACTION
            int pos = -1;
            foreach (Cultivator cult in alleCultivators)
            {
                if(cult.CultivatorID == obj.CultivatorID)
                {
                    //Found a Match To Light Up
                    break;
                }
                pos++;
            }

            allWater[pos].Visibility = Visibility.Visible;
        }

        private void MyCultivator_PlantNoFertilizerEvent(Cultivator obj)
        {
            //MessageBox.Show($"You are a shitty drugsaddict! {this.Name} has no shit in it!");
            //obj.RendementValuePlus--;
            //DataManager.UpdateCultivator(obj);

            lblNotification.Content = $"Let Op! Bloempot met {DataManager.GetStrainNameofCultivator(obj)} plant heeft verse voeding nodig!";
            pnlNotification.Visibility = Visibility.Visible;
        }

        private void MyCultivator_PlantNoWaterEvent(Cultivator obj)
        {
            //MessageBox.Show($"You are a shitty drugsaddict! Cultivator {obj.CultivatorID} {DataManager.GetStrainNameofCultivator(obj)} has no water!");
            //obj.RendementValuePlus--;
            //DataManager.UpdateCultivator(obj);

            lblNotification.Content = $"Let Op! Bloempot met {DataManager.GetStrainNameofCultivator(obj)} plant staat volledig droog!";
            pnlNotification.Visibility = Visibility.Visible;
        }

        private void MyCultivator_NoHarvestEvent(Cultivator obj)
        {
            MessageBox.Show($"Bro, open je ogen! Big Crops van bloempot met {DataManager.GetStrainNameofCultivator(obj)} plant zijn beschimmeld geraakt!\nNu is het voor de vuilbak jonge!");

            //lblNotification.Content = $"Jammer! Bloempot met Rijpe Crops van de {DataManager.GetStrainNameofCultivator(obj)} plant is vernietigd!";
            //pnlNotification.Visibility = Visibility.Visible;

            UpdatePlantWhenReady(obj);
        }

        //Image interaction Cultivator 1
        private void imgWater1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[1].WaterSupplyPlus = 3;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[1]);
            allWater[0].Visibility = Visibility.Hidden;
        }
        private void imgFert1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[1].FertilizerSupplyPlus = 5;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[1]);
            allFert[0].Visibility = Visibility.Hidden;
        }

        //Image interaction Cultivator 2
        private void imgWater2_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[2].WaterSupplyPlus = 3;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[2]);
            allWater[1].Visibility = Visibility.Hidden;
        }
        private void imgFert2_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[2].FertilizerSupplyPlus = 5;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[2]);
            allFert[1].Visibility = Visibility.Hidden;
        }

        //Image interaction Cultivator 3
        private void imgWater3_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[3].WaterSupplyPlus = 3;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[3]);
            allWater[2].Visibility = Visibility.Hidden;
        }
        private void imgFert3_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[3].FertilizerSupplyPlus = 5;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[3]);
            allFert[2].Visibility = Visibility.Hidden;
        }

        //Image interaction Cultivator 4
        private void imgWater4_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[4].WaterSupplyPlus = 3;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[4]);
            allWater[3].Visibility = Visibility.Hidden;
        }
        private void imgFert4_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[4].FertilizerSupplyPlus = 5;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[4]);
            allFert[3].Visibility = Visibility.Hidden;
        }

        //Image interaction Cultivator 5
        private void imgWater5_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[5].WaterSupplyPlus = 3;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[5]);
            allWater[4].Visibility = Visibility.Hidden;
        }
        private void imgFert5_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            alleCultivators[5].FertilizerSupplyPlus = 5;
            // Update cultivator
            DataManager.UpdateCultivator(alleCultivators[5]);
            allFert[4].Visibility = Visibility.Hidden;
        }
    }
}
