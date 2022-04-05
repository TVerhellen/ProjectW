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
        List<Rectangle> npcsRect = new List<Rectangle>();
        bool playing = false;
        DispatcherTimer timer = new DispatcherTimer();
        public SellGame()
        {
            InitializeComponent();
            Character seller = new Character();
            seller.Weed = 50;
            seller.Money = 10;

            player.Location = new int[] { 50, 700 };
            player.Speed = 10;
            player.direction = 0;

            Pedestrian ped1 = new Pedestrian();
            List<int[]> ped1Route = new List<int[]>();
            int[] point2 = { 100, 50 };
            int[] point1 = { 300, 50 };
            ped1Route.Add(point1);
            ped1Route.Add(point2);
            ped1.Behaviour = 2;
            ped1.RouteCoordinates = ped1Route;
            ped1.CurrentTarget = ped1Route[0];
            ped1.Speed = 1;
            int[] ped1Loc = { 150, 50 };
            ped1.Location = ped1Loc;
            ped1.direction = 1;
            npcs.Add(ped1);

            Pedestrian ped2 = new Pedestrian();
            List<int[]> ped2Route = new List<int[]>();
            int[] point4 = { 750, 100 };
            int[] point3 = { 750, 700 };
            ped2Route.Add(point3);
            ped2Route.Add(point4);
            ped2.Behaviour = 1;
            ped2.RouteCoordinates = ped2Route;
            ped2.CurrentTarget = ped2Route[0];
            ped2.Speed = 2;
            int[] ped2Loc = { 750, 500 };
            ped2.Location = ped2Loc;
            ped2.direction = 1;
            npcs.Add(ped2);

            //Cop cop1 = new Cop();
            //cop1.Behaviour = 3;
            //cop1.CopType = 1;
            //int[] cop1Loc = { 650, 200 };
            //cop1.Location = cop1Loc;

            Rectangle ped1Rect = new Rectangle();
            Rectangle ped2Rect = new Rectangle();
            npcsRect.Add(ped1Rect);
            npcsRect.Add(ped2Rect);
            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
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
            Canvas.SetLeft(playerRect, player.Location[0]);
            Canvas.SetTop(playerRect, player.Location[1]);
            playerRect.Width = 20;
            playerRect.Height = 20;
            playerRect.Fill = player.Fill;
            playerRect.Stroke = Brushes.Black;
            cvStreets.Children.Add(playerRect);
            int counter = 0;
            foreach (Shape rect in npcsRect)
            {
                Canvas.SetLeft(rect, npcs[counter].Location[0]);
                Canvas.SetTop(rect, npcs[counter].Location[1]);
                rect.Width = 20;
                rect.Height = 20;
                rect.Fill = npcs[counter].Fill;
                rect.Stroke = Brushes.Black;
                cvStreets.Children.Add(rect);
                counter++;
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            playing = true;



        }

        public bool CollisionCheck(Shape toCheck, int[] newLoc)
        {
            bool collision = false;
            foreach (Shape shape in cvStreets.Children)
            {
                if (shape != toCheck)
                {
                    if (
                        newLoc[0] < Canvas.GetLeft(shape)+shape.Width &&
                        newLoc[0] + toCheck.Width > Canvas.GetLeft(shape) &&
                        newLoc[1] < Canvas.GetTop(shape)+shape.Height &&
                        newLoc[1] + toCheck.Height > Canvas.GetTop(shape)
                        )
                    {
                        if (shape.Fill != Brushes.Green)
                        {
                            collision = true;
                        }
                        else if (shape.Fill == Brushes.Red)
                        {
                            playing = false;
                            break;
                        }

                    }
                }
            }
            return collision;
        }

        public void timer_Tick(object sender, EventArgs e)
        {
            lblCollision.Content = "a";
            if (!CollisionCheck(playerRect, player.PreviewUpdatedLocation()))
            {
                player.UpdateLocation();
                lblXPos.Content=player.Location[0].ToString();
                lblYPos.Content=player.Location[1].ToString();
                Canvas.SetLeft(playerRect, player.Location[0]);
                Canvas.SetTop(playerRect, player.Location[1]);
            }

            for (int i = 0; i < npcs.Count; i++)
            {
                if (!CollisionCheck(npcsRect[i], npcs[i].PreviewUpdatedLocation()))
                {
                    npcs[i].UpdateLocation();
                    Canvas.SetLeft(npcsRect[i], npcs[i].Location[0]);
                    Canvas.SetTop(npcsRect[i], npcs[i].Location[1]);
                    lblXPos.Content=npcs[i].Location[0].ToString();
                    lblYPos.Content=npcs[i].Location[1].ToString();
                }
            }
        }
    }
}
