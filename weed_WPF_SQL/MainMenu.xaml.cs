using System.Windows;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        //Member variables

        //Constructors
        public MainMenu()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        //Overloaded Show to pass our Character Object
        public void Show(Character obj)
        {
            base.Show();
        }

        //member functions

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MediaManager.Instance().PlayHomeTheme();
        }

        //Canvas Hover Events
        //Hover Over Farm
        private void rectToFarm_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
        private void rectToFarm_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        //Hover Over Webstore
        private void rectToStore_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
        private void rectToStore_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        //Hover Over Selling MiniGame
        private void rectToSelling_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
        private void rectToSelling_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        //Hover Over Exit Game
        private void rectExitGame_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
        private void rectExitGame_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        //Canvas On Click Events
        //GO TO Farm
        private void rectToFarm_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        //GO TO Webstore
        private void rectToStore_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        //GO TO Selling MiniGame
        private void rectToSelling_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        //EXIT TO LOGIN
        private void rectExitGame_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

    }
}
