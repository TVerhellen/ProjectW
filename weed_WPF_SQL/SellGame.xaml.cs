using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for SellGame.xaml
    /// </summary>
    public partial class SellGame : Window
    {
        PlayerCharacter player = new PlayerCharacter();
        List<GameCharacter> npcs = new List<GameCharacter>();
        List<Building> buildings = new List<Building>();
        bool playing = true;
        DispatcherTimer timer = new DispatcherTimer();
        Character seller = new Character();
        Random rng = new Random();
        public SellGame()
        {
            InitializeComponent();
            
            // Character
            seller.Weed = 50;
            seller.Money = 10;
            lblWeed.Content = seller.Weed;
            lblMoney.Content = seller.Money;


            // Player Character
            player.Location = new int[] { 50, 700 };
            player.Speed = 10;
            player.direction = 0;

            // Buildings lefttop width height
            Building b1 = new Building(new int[] { 0, 0 }, 50, 700, true);
            Building b2 = new Building(new int[] { 800, 350 }, 50, 400, true);
            Building b3 = new Building(new int[] { 0, 0 }, 750, 50, true);
            Building b4 = new Building(new int[] { 0, 800 }, 850, 50, true);
            Building b5 = new Building(new int[] { 100, 100 }, 200, 100, true);
            Building b6 = new Building(new int[] { 100, 250 }, 150, 350, true);
            Building b7 = new Building(new int[] { 100, 650 }, 400, 100, true);
            Building b8 = new Building(new int[] { 300, 300 }, 200, 100, true);
            Building b9 = new Building(new int[] { 450, 450 }, 300, 100, true);
            Building b10 = new Building(new int[] { 350, 100 }, 150, 150, true);
            Building b11 = new Building(new int[] { 550, 600 }, 200, 150, true);
            Building b12 = new Building(new int[] { 800, 0 }, 50, 300, true);
            Building b13 = new Building(new int[] { 550, 100 }, 200, 300, false);
            b13.Figure.Fill = Brushes.Green;
            Building b14 = new Building(new int[] { 0, 700 }, 50, 100, true);
            b14.Figure.Fill = Brushes.Red;
            buildings.Add(b1);
            buildings.Add(b2);
            buildings.Add(b3);
            buildings.Add(b4);
            buildings.Add(b5);
            buildings.Add(b6);
            buildings.Add(b7);
            buildings.Add(b8);
            buildings.Add(b9);
            buildings.Add(b10);
            buildings.Add(b11);
            buildings.Add(b12);
            buildings.Add(b13);
            buildings.Add(b14);

            //Pedestrians
            List<int[]> ped1Route = new List<int[]>();
            int[] point2 = { 100, 50 };
            int[] point1 = { 300, 50 };
            int[] ped1Loc = { 150, 50 };
            ped1Route.Add(point1);
            ped1Route.Add(point2);
            Pedestrian ped1 = new Pedestrian(ped1Loc, ped1Route, 1, 2);
            npcs.Add(ped1);

            List<int[]> ped2Route = new List<int[]>();
            int[] point4 = { 750, 100 };
            int[] point3 = { 750, 700 };
            ped2Route.Add(point3);
            ped2Route.Add(point4);
            int[] ped2Loc = { 750, 500 };
            Pedestrian ped2 = new Pedestrian(ped2Loc, ped2Route, 2, 1);
            npcs.Add(ped2);

            // Cops
            int[] cop1Loc = { 650, 200 };
            Cop cop1 = new Cop(cop1Loc, player.Location, 1, 1);
            npcs.Add(cop1);

            // Buyers
            int[] buy1Loc = { 350, 500 };
            Buyer buy1 = new Buyer(buy1Loc, 100, 100);
            npcs.Add(buy1);

            // Timer
            timer.Interval = TimeSpan.FromSeconds((double)1/144);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Tick += new EventHandler(cop_UpdateTargetEvent);
            //timer.Start();
            CopGame();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // change player direction
            if (playing)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        player.direction = 1;
                        break;
                    case Key.Right:
                        player.direction = 2;
                        break;
                    case Key.Down:
                        player.direction = 3;
                        break;
                    case Key.Left:
                        player.direction = 4;
                        break;
                }

            }



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Display all Figures
            Canvas.SetLeft(player.Figure, player.Location[0]);
            Canvas.SetTop(player.Figure, player.Location[1]);

            foreach (Building building in buildings)
            {
                Canvas.SetLeft(building.Figure, building.LeftTop[0]);
                Canvas.SetTop(building.Figure, building.LeftTop[1]);
                cvStreets.Children.Add(building.Figure);
            }
            for (int i = 0; i < npcs.Count; i++)
            {
                Canvas.SetLeft(npcs[i].Figure, npcs[i].Location[0]);
                Canvas.SetTop(npcs[i].Figure, npcs[i].Location[1]);
                cvStreets.Children.Add(npcs[i].Figure);
            }
            cvStreets.Children.Add(player.Figure);


        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            playing = true;
        }

        public bool CollisionCheck(Rectangle toCheck, int[] newLoc)
        {
            bool collision = false;

            foreach (Building building in buildings)
            {
                if (building.HasCollision)
                {
                    if (
                        newLoc[0] < Canvas.GetLeft(building.Figure)+building.Figure.Width &&
                        newLoc[0] + toCheck.Width > Canvas.GetLeft(building.Figure) &&
                        newLoc[1] < Canvas.GetTop(building.Figure)+building.Figure.Height &&
                        newLoc[1] + toCheck.Height > Canvas.GetTop(building.Figure)
                        )
                    {
                        if (building.Figure.Fill == Brushes.Red)
                        {
                            player.direction = 0;
                            timer.Stop();
                            if (MessageBox.Show(" u wanna go back home? ", "Your House", MessageBoxButton.YesNo, MessageBoxImage.None) == MessageBoxResult.Yes)
                            {
                                EndOfGame();
                            }
                            else
                            {
                                timer.Start();
                                collision = true;
                            }
                        }
                        else
                        {
                            collision = true;
                        }

                    }
                }

            }
            if (toCheck.Fill == Brushes.Yellow)
            {
                for (int i = 0; i < npcs.Count; i++)
                {
                    if (
                        newLoc[0] < Canvas.GetLeft(npcs[i].Figure)+npcs[i].Figure.Width &&
                        newLoc[0] + toCheck.Width > Canvas.GetLeft(npcs[i].Figure) &&
                        newLoc[1] < Canvas.GetTop(npcs[i].Figure)+npcs[i].Figure.Height &&
                        newLoc[1] + toCheck.Height > Canvas.GetTop(npcs[i].Figure)
                        )
                    {
                        switch (npcs[i].GetType())
                        {
                            case "ped":
                                collision = true;
                                break;
                            case "buyer":
                                player.direction = 0;
                                timer.Stop();
                                Buyer foundBuyer = (Buyer)npcs[i];
                                if (seller.Weed >= foundBuyer.Demand)
                                {
                                    MessageBox.Show($" u sell da weed, +{foundBuyer.Money} dollaz ");
                                    seller.Weed -= foundBuyer.Demand;
                                    seller.Money += foundBuyer.Money;
                                    lblWeed.Content = seller.Weed;
                                    lblMoney.Content = seller.Money;
                                    cvStreets.Children.Remove(npcs[i].Figure);
                                    npcs.Remove(npcs[i]);
                                    i--;
                                }
                                else
                                {
                                    MessageBox.Show(" u dont got enough weed ");
                                }
                                timer.Start();
                                break;
                        }
                    }
                }

            }
            else if (toCheck.Fill == Brushes.Blue)
            {
                if (
                        newLoc[0] < Canvas.GetLeft(player.Figure)+player.Figure.Width &&
                        newLoc[0] + toCheck.Width > Canvas.GetLeft(player.Figure) &&
                        newLoc[1] < Canvas.GetTop(player.Figure)+player.Figure.Height &&
                        newLoc[1] + toCheck.Height > Canvas.GetTop(player.Figure)
                        )
                {
                    timer.Stop();
                    CopGame();
                    //Application.Current.Shutdown();
                }
            }
            return collision;
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            lblCollision.Content = "a";
            if (!CollisionCheck(player.Figure, player.PreviewUpdatedLocation()))
            {
                player.UpdateLocation();
                Canvas.SetLeft(player.Figure, player.Location[0]);
                Canvas.SetTop(player.Figure, player.Location[1]);
            }

            for (int i = 0; i < npcs.Count; i++)
            {
                if (npcs[i].Speed != 0)
                {
                    if (npcs[i].GetType() == "cop")
                    {
                        if (CollisionCheck(npcs[i].Figure, npcs[i].PreviewUpdatedLocation()))
                        {
                            npcs[i].direction = ((Cop)npcs[i]).BackupDirection;
                        }
                        npcs[i].UpdateLocation();
                        Canvas.SetLeft(npcs[i].Figure, npcs[i].Location[0]);
                        Canvas.SetTop(npcs[i].Figure, npcs[i].Location[1]);
                        lblXPos.Content = player.Location[0]-npcs[i].Location[0];
                        lblYPos.Content = player.Location[1]-npcs[i].Location[1];
                    }
                    else if (!CollisionCheck(npcs[i].Figure, npcs[i].PreviewUpdatedLocation()))
                    {
                        npcs[i].UpdateLocation();
                        Canvas.SetLeft(npcs[i].Figure, npcs[i].Location[0]);
                        Canvas.SetTop(npcs[i].Figure, npcs[i].Location[1]);

                    }
                }

            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (playing)
            {
                player.direction = 0;

            }
        }

        public void cop_UpdateTargetEvent(object sender, EventArgs e)
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                if (npcs[i].GetType() == "cop")
                    npcs[i].CurrentTarget = player.Location;
            }
        }

        public void CopGame()
        {
            int locationCaught = Convert.ToInt32(rng.Next(3)+1);
            //if (player.Location[0] > 415)
            //{
            //    if (player.Location[1] > 415)
            //    {
            //        locationCaught = 1; //school
            //    }
            //    else
            //    {
            //        locationCaught = 2; //park
            //    }
            //}
            //else
            //{
            //    if (player.Location[1] > 415)
            //    {
            //        locationCaught = 3; //square
            //    }
            //    else
            //    {
            //        locationCaught = 4; //narrow streets
            //    }

            //}
            CopEscapeGame CopEscape = new CopEscapeGame(2);
            CopEscape.ShowDialog();
            EndOfGame();
        }

        public void EndOfGame()
        {
            Application.Current.Shutdown();
        }
    }
}
