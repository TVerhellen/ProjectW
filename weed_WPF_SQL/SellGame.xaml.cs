using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for SellGame.xaml
    /// </summary>
    public partial class SellGame : Window
    {
        PlayerCharacter player = new PlayerCharacter();
        Rectangle rect = new Rectangle();
        bool playing = false;
        public SellGame()
        {
            InitializeComponent();
            Character seller = new Character();
            seller.Weed = 50;
            seller.Money = 10;

            player.Location = new int[] { 50, 700 };
            player.Speed = 10;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (playing)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        if (!CollisionCheck(player.PreviewUpdatedLocation(1)))
                        {
                            player.UpdateLocation(1);
                            lblXPos.Content=player.Location[0].ToString();
                            lblYPos.Content=player.Location[1].ToString();
                            Canvas.SetLeft(rect, player.Location[0]);
                            Canvas.SetTop(rect, player.Location[1]);
                        }

                        break;
                    case Key.Right:
                        if (!CollisionCheck(player.PreviewUpdatedLocation(2)))
                        {
                            player.UpdateLocation(2);
                            lblXPos.Content=player.Location[0].ToString();
                            lblYPos.Content=player.Location[1].ToString();
                            Canvas.SetLeft(rect, player.Location[0]);
                            Canvas.SetTop(rect, player.Location[1]);
                        }
                        break;
                    case Key.Down:
                        if (!CollisionCheck(player.PreviewUpdatedLocation(3)))
                        {
                            player.UpdateLocation(3);
                            lblXPos.Content=player.Location[0].ToString();
                            lblYPos.Content=player.Location[1].ToString();
                            Canvas.SetLeft(rect, player.Location[0]);
                            Canvas.SetTop(rect, player.Location[1]);
                        }
                        break;
                    case Key.Left:
                        if (!CollisionCheck(player.PreviewUpdatedLocation(4)))
                        {
                            player.UpdateLocation(4);
                            lblXPos.Content=player.Location[0].ToString();
                            lblYPos.Content=player.Location[1].ToString();
                            Canvas.SetLeft(rect, player.Location[0]);
                            Canvas.SetTop(rect, player.Location[1]);
                        }
                        break;
                }
            }
            


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(rect, player.Location[0]);
            Canvas.SetTop(rect, player.Location[1]);
            rect.Width = 20;
            rect.Height = 20;
            rect.Fill = Brushes.Yellow;
            rect.Stroke = Brushes.Black;
            cvStreets.Children.Add(rect);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            playing = true;



        }

        public bool CollisionCheck(int[] newLoc)
        {
            bool collision = false;
            foreach (Shape shape in cvStreets.Children)
            {
                if (shape != rect)
                {
                    if (
                        newLoc[0] < Canvas.GetLeft(shape)+shape.Width &&
                        newLoc[0] + rect.Width > Canvas.GetLeft(shape) &&
                        newLoc[1] < Canvas.GetTop(shape)+shape.Height &&
                        newLoc[1] + rect.Height > Canvas.GetTop(shape)
                        )
                    {
                        if (shape.Fill == Brushes.DimGray)
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
    }
}
