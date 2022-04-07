using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
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

        public BitmapImage img = new BitmapImage();



        public FarmGame()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbSelectStrain.Items.Add("--Select strain--");
            allStrains = DataManager.GetStrainNames();

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
                NameID = 1
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
                CyclesPassed = 1,
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
                CyclesPassed = 1,
                NameID = 1
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
                NameID = 1
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
                NameID = 1
            };

            alleCultivators.Add(newCultivator);
            alleCultivators.Add(newCultivator5);
            alleCultivators.Add(newCultivator2);
            alleCultivators.Add(newCultivator3);
            alleCultivators.Add(newCultivator4);

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

        private void btnAddWeedPlant_Click(object sender, RoutedEventArgs e)
        {
            Cultivator newPlant = new Cultivator()
            {

            };
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
                if (alleCultivators[i - 1].CyclesPassed == 1)
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
                if (alleCultivators[i - 1].CyclesPassed == 1)
                {
                    if (alleCultivators[i - 2].CyclesPassed == 2)
                    {
                        imageWeedLeft += 240;
                    }
                    else
                    {
                        imageWeedLeft += 200;
                    }
                    imageWeedTop = 245;
                }
                else
                {
                    if (alleCultivators[i - 2].CyclesPassed == 2)
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
                    DrawImage(200, 400, imageLeft, imageTop, "Images/WeedPhase2.png");
                    break;
                case 3:
                    DrawImage(60, 148, imageLeft, imageTop, "Images/WeedPhase2.png");
                    break;
                default:
                    break;
            }
        }
    }
}
