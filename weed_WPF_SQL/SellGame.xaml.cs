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
        Rectangle playerRect = new Rectangle();
        List<GameCharacter> npcs = new List<GameCharacter>();
        List<Cop> cops = new List<Cop>();
        //List<Rectangle> npcsRect = new List<Rectangle>();
        //List<Rectangle> copsRect = new List<Rectangle>();
        List<Building> buildings = new List<Building>();
        bool playing = true;
        DispatcherTimer timer = new DispatcherTimer();
        List<int> copsTimeout = new List<int>();
        Character seller = new Character();
        public SellGame()
        {
            InitializeComponent();
            // Character
            seller.Weed = 50;
            seller.Money = 10;

            // Player Character
            player.Location = new int[] { 50, 700 };
            player.Speed = 10;
            player.direction = 0;
            cvStreets.Children.Add(player.Figure);

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

            //Pedestrians
            List<int[]> ped1Route = new List<int[]>();
            int[] point2 = { 100, 50 };
            int[] point1 = { 300, 50 };
            int[] ped1Loc = { 150, 50 };
            ped1Route.Add(point1);
            ped1Route.Add(point2);
            Pedestrian ped1 = new Pedestrian(ped1Loc, ped1Route, 1, 2);
            npcs.Add(ped1);
            cvStreets.Children.Add(ped1.Figure);

            List<int[]> ped2Route = new List<int[]>();
            int[] point4 = { 750, 100 };
            int[] point3 = { 750, 700 };
            ped2Route.Add(point3);
            ped2Route.Add(point4);
            int[] ped2Loc = { 750, 500 };
            Pedestrian ped2 = new Pedestrian(ped2Loc, ped2Route, 2, 1);
            npcs.Add(ped2);
            cvStreets.Children.Add(ped2.Figure);

            // Cops
            int[] cop1Loc = { 650, 200 };
            Cop cop1 = new Cop(cop1Loc, player.Location, 1, 1);
            cops.Add(cop1);
            npcs.Add(cop1);
            copsTimeout.Add(0);
            cvStreets.Children.Add(cop1.Figure);

            // Buyers
            int[] buy1Loc = { 350, 500 };
            Buyer buy1 = new Buyer(buy1Loc, 10, 10);
            npcs.Add(buy1);
            cvStreets.Children.Add(buy1.Figure);

            // Timer
            timer.Interval = TimeSpan.FromSeconds((double)1/144);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Tick += new EventHandler(cop_UpdateTargetEvent);
            timer.Start();
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
            MediaManager.Instance().PlaySellingTheme();
            // Display all Figures
            Canvas.SetLeft(player.Figure, player.Location[0]);
            Canvas.SetTop(player.Figure, player.Location[1]);
            for (int i = 0; i < npcs.Count; i++)
            {
                Canvas.SetLeft(npcs[i].Figure, npcs[i].Location[0]);
                Canvas.SetTop(npcs[i].Figure, npcs[i].Location[1]);
            }
            foreach(Building building in buildings)
            {
                Canvas.SetLeft(building.Figure, building.LeftTop[0]);
                Canvas.SetTop(building.Figure, building.LeftTop[1]);
                cvStreets.Children.Add(building.Figure);
            }


        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            playing = true;



        }

        public bool CollisionCheck(Shape toCheck, int[] newLoc)
        {
            bool collision = false;
            
            {

            }
            for (int i = 0; i < cvStreets.Children.Count; i++)
            {
                Shape currentShape = (Shape)cvStreets.Children[i];
                if (currentShape != toCheck)
                {
                    if (
                        newLoc[0] < Canvas.GetLeft(currentShape)+currentShape.Width &&
                        newLoc[0] + toCheck.Width > Canvas.GetLeft(currentShape) &&
                        newLoc[1] < Canvas.GetTop(currentShape)+(currentShape).Height &&
                        newLoc[1] + toCheck.Height > Canvas.GetTop(currentShape)
                        )
                    {
                        
                        if (currentShape.Fill == Brushes.Red)
                        {
                            playing = false;
                            break;
                        }
                        else if (currentShape.Fill == Brushes.Lime && toCheck.Fill == Brushes.Yellow)
                        {
                            if(seller.Weed >= ((Buyer)npcs[i]).Demand)
                            {
                                seller.Weed -= ((Buyer)npcs[i]).Demand;
                                seller.Money += ((Buyer)npcs[i]).Money;
                                cvStreets.Children.RemoveAt(i);
                            }
                        }
                        else if (currentShape.Fill != Brushes.Green)
                        {
                            collision = true;
                        }
                    }
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
                    if (!CollisionCheck(npcs[i].Figure, npcs[i].PreviewUpdatedLocation()))
                    {
                        npcs[i].UpdateLocation();
                        Canvas.SetLeft(npcs[i].Figure, npcs[i].Location[0]);
                        Canvas.SetTop(npcs[i].Figure, npcs[i].Location[1]);

                    }
                }

            }
            for (int i = 0; i < cops.Count; i++)
            {
                if (CollisionCheck(cops[i].Figure, cops[i].PreviewUpdatedLocation()))
                {
                    cops[i].direction = cops[i].BackupDirection;
                }
                cops[i].UpdateLocation();
                Canvas.SetLeft(cops[i].Figure, cops[i].Location[0]);
                Canvas.SetTop(cops[i].Figure, cops[i].Location[1]);
                lblXPos.Content=cops[i].direction.ToString();
                lblYPos.Content=cops[i].BackupDirection.ToString();
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
            for (int i = 0; i < cops.Count; i++)
            {
                cops[i].CurrentTarget = player.Location;
            }
        }
    }
}
