using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
        List<Cultivator> alleCultivators = new List<Cultivator>();


        Point PositionLamp;
        Point PositionFlowerPot;
        Point PositionPlant;

        public double lampLeft;
        public double lampTop;
        public double tableLeft;
        public double tableTop;
        public double flowerPotLeft;
        public double flowerPotTop;
        public double imageWeedLeft;
        public double imageWeedTop;
        private int secondsPassed = 0;

        public BitmapImage img = new BitmapImage();
        Cultivator myCultivator = new Cultivator();


        public FarmGame()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbSelectStrain.Items.Add("--Select strain--");
            allStrains = DataManager.GetStrainNames();

            myCultivator.Tim = new DispatcherTimer();
            myCultivator.Tim.Interval = TimeSpan.FromSeconds(1);
            myCultivator.Tim.Tick += timer_Tick;
            myCultivator.Tim.Start();
            myCultivator.PlantNoWaterEvent += MyCultivator_PlantNoWaterEvent;
            myCultivator.PlantNoFertilizerEvent += MyCultivator_PlantNoFertilizerEvent;
            myCultivator.PlantWaterRequirementEvent += MyCultivator_PlantWaterRequirementEvent;
            myCultivator.PlantFertilizerRequirementEvent += MyCultivator_PlantFertilizerRequirementEvent;
            myCultivator.PlantAlmostDied += MyCultivator_PlantAlmostDied;
            myCultivator.ProgressMonitorEvent += MyCultivator_ProgressMonitorEvent;
            myCultivator.HealthMonitorEvent += MyCultivator_HealthMonitorEvent;

            foreach (var item in allStrains)
            {
                cmbSelectStrain.Items.Add(item);
            }

            //alleCultivators = DataManager.GetCultivators();

            Cultivator newCultivator = new Cultivator()
            {
                CultivatorID = 1,
                FarmID = 1,
                CultID = 1,
                WaterID = 1,
                FertilizerID = 1,
                SoilID = 1,
                LampID = 1,
                CyclesRequired = 1,
                CyclesPassed = 2,
                NameID = 1,
                WaterSupply = 2,
                FertilizerSupply = 3,
                RendementValue = 7
            };
            Cultivator newCultivator2 = new Cultivator()
            {
                CultivatorID = 2,
                FarmID = 1,
                CultID = 1,
                WaterID = 1,
                FertilizerID = 1,
                SoilID = 1,
                LampID = 1,
                CyclesRequired = 1,
                CyclesPassed = 5,
                NameID = 1

            };
            Cultivator newCultivator3 = new Cultivator()
            {
                CultivatorID = 2,
                FarmID = 1,
                CultID = 1,
                WaterID = 1,
                FertilizerID = 1,
                SoilID = 1,
                LampID = 1,
                CyclesRequired = 2,
                CyclesPassed = 5,
                NameID = 1,
                WaterSupply = 2,
                FertilizerSupply = 3,
                RendementValue = 3
            };
            Cultivator newCultivator4 = new Cultivator()
            {
                CultivatorID = 3,
                FarmID = 1,
                CultID = 1,
                WaterID = 1,
                FertilizerID = 1,
                SoilID = 1,
                LampID = 1,
                CyclesRequired = 2,
                CyclesPassed = 2,
                NameID = 1,
                WaterSupply = 2,
                FertilizerSupply = 3,
                RendementValue = 9
            };
            Cultivator newCultivator5 = new Cultivator()
            {
                CultivatorID = 1,
                FarmID = 1,
                CultID = 1,
                WaterID = 1,
                FertilizerID = 1,
                SoilID = 1,
                LampID = 1,
                CyclesRequired = 1,
                CyclesPassed = 2,
                NameID = 1,
                WaterSupply = 2,
                FertilizerSupply = 3,
                RendementValue = 3
            };

            alleCultivators.Add(newCultivator);
            alleCultivators.Add(newCultivator5);
            alleCultivators.Add(newCultivator2);
            alleCultivators.Add(newCultivator3);
            alleCultivators.Add(newCultivator4);


            UpdateCanvas();
            MyCultivator_HealthMonitorEvent();
            MyCultivator_ProgressMonitorEvent();
        }



        private void btnReturnToHome_Click(object sender, RoutedEventArgs e)
        {
            myCultivator.Tim.Stop();
        }

        private void btnAddWeedPlant_Click(object sender, RoutedEventArgs e)
        {
            Cultivator newPlant = new Cultivator()
            {

            };
        }


        // Methods

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
                if (alleCultivators[i - 1].CyclesPassed == 1 || alleCultivators[i - 1].CyclesPassed < 5)
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
                if (alleCultivators[i - 1].CyclesPassed == 1 || alleCultivators[i - 1].CyclesPassed < 5)
                {
                    if (alleCultivators[i - 2].CyclesPassed >= 5)
                    {
                        imageWeedLeft += 240;
                    }
                    else
                    {
                        if (alleCultivators[i - 2].CyclesPassed > 6)
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
                    if (alleCultivators[i - 2].CyclesPassed >= 5)
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
                    DrawImage(100, 280, imageLeft, imageTop, "Images/WeedBaby.png");
                    break;
                case 5:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/WeedPhase2.png");
                    break;
                case 6:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/WeedPhase2.png");
                    break;
                case 7:
                    DrawImage(200, 400, imageLeft, imageTop, "Images/Bag.png");
                    break;
                case 8:
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
            cnvFarmProjection.Children.Add(ShapeWeed.DrawTable(1000, 180, tableLeft, tableTop));

            if (alleCultivators.Count > 0)
            {

                for (int i = 1; i < alleCultivators.Count + 1; i++)
                {
                    PositionLampAndPot(i);
                    PositionWeedImage(i);
                    cnvFarmProjection.Children.Add(ShapeWeed.DrawLamp(lampLeft, lampTop, alleCultivators[i - 1]));
                    cnvFarmProjection.Children.Add(ShapeWeed.DrawFlowerPot(100, 50, alleCultivators[i - 1], flowerPotLeft, flowerPotTop));
                    GetImageWeed(imageWeedLeft, imageWeedTop, alleCultivators[i - 1]);
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            secondsPassed++;

            if (secondsPassed == 5)
            {
                //foreach (var item in alleCultivators)
                //{
                //    item.CyclesPassedPlus++;
                //    //DataManager.UpdateCultivator(item);

                //}
                MyCultivator_HealthMonitorEvent();
                MyCultivator_ProgressMonitorEvent();
                alleCultivators[0].CyclesPassed += 1;
                alleCultivators[1].CyclesPassed += 1;
                alleCultivators[2].CyclesPassed += 1;
                alleCultivators[3].CyclesPassed += 1;
                alleCultivators[4].CyclesPassed += 1;
                UpdateCanvas();
                secondsPassed = 0;
            }
        }

        // Events

        private void MyCultivator_HealthMonitorEvent()
        {
            //for (int i = 0; i < 5; i++)
            //{
            //    if (alleCultivators[0].RendementValuePlus != null)
            //    {
            //        pgrHealthPlant1.Value = (doublalleCultivators[0].RendementValuePlus * 10;
            //    }
            //}

            pgrHealthPlant1.Value = alleCultivators[0].RendementValuePlus == null ? 0 : (double)alleCultivators[0].RendementValuePlus * 10;
            pgrHealthPlant2.Value = alleCultivators[1].RendementValuePlus == null ? 0 : (double)alleCultivators[1].RendementValuePlus * 10;
            pgrHealthPlant3.Value = alleCultivators[2].RendementValuePlus == null ? 0 : (double)alleCultivators[2].RendementValuePlus * 10;
            pgrHealthPlant4.Value = alleCultivators[3].RendementValuePlus == null ? 0 : (double)alleCultivators[3].RendementValuePlus * 10;
            pgrHealthPlant5.Value = alleCultivators[4].RendementValuePlus == null ? 0 : (double)alleCultivators[4].RendementValuePlus * 10;
        }

        private void MyCultivator_ProgressMonitorEvent()
        {
            pgrProgressPlant1.Value = alleCultivators[0].CyclesPassedPlus * 12.5;
            pgrProgressPlant2.Value = alleCultivators[1].CyclesPassedPlus * 12.5;
            pgrProgressPlant3.Value = alleCultivators[2].CyclesPassedPlus * 12.5;
            pgrProgressPlant4.Value = alleCultivators[3].CyclesPassedPlus * 12.5;
            pgrProgressPlant5.Value = alleCultivators[4].CyclesPassedPlus * 12.5;
        }

        private void MyCultivator_PlantAlmostDied()
        {
            MessageBox.Show($"You are a shitty drugsaddict! {Name} is almost died!");
        }

        private void MyCultivator_PlantFertilizerRequirementEvent()
        {
            MessageBox.Show($"Bro, give {Name} some shit!");
        }

        private void MyCultivator_PlantWaterRequirementEvent()
        {
            MessageBox.Show($"Bro, give {Name} some water!");
        }

        private void MyCultivator_PlantNoFertilizerEvent()
        {
            MessageBox.Show($"You are a shitty drugsaddict! {Name} has no shit in it!");
        }

        private void MyCultivator_PlantNoWaterEvent()
        {
            MessageBox.Show($"You are a shitty drugsaddict! {Name} has no water!");
        }

    }
}
