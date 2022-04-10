using System.Windows;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Constructors
        public MainWindow()
        {
            //Initialize
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        //Methods
        /// <summary>
        /// Multi-Purpose Function that can Toggle The Audio Button when param is false, syncing from Singleton when true
        /// </summary>
        /// <param name="syncing">Only true when switching windows, false when expecting toggle behavior</param>
        private void ToggleAudio(bool syncing)
        {
            if (!MediaManager.Instance().blnMusicMuted)
            {
                if (!syncing) //When we are simply Toggling On/Off
                {
                    MediaManager.Instance().PauseMusic();
                    MediaManager.Instance().blnMusicMuted = true;
                    imgMuteMainTheme.Source = MediaManager.Instance().IcoMuted;
                    btnMuteMainTheme.Background = Brushes.DarkRed;
                }
                else //When we are syncronizing Audio toggle representation with other Windows through Singleton
                {
                    imgMuteMainTheme.Source = MediaManager.Instance().IcoUnmuted;
                    btnMuteMainTheme.Background = Brushes.LawnGreen;
                }

            }
            else
            {
                if (!syncing) //When we are simply Toggling On/Off
                {
                    MediaManager.Instance().PlayMusic();
                    MediaManager.Instance().blnMusicMuted = false;
                    imgMuteMainTheme.Source = MediaManager.Instance().IcoUnmuted;
                    btnMuteMainTheme.Background = Brushes.LawnGreen;
                }
                else //When we are syncronizing Audio toggle representation with other Windows through Singleton
                {
                    imgMuteMainTheme.Source = MediaManager.Instance().IcoMuted;
                    btnMuteMainTheme.Background = Brushes.DarkRed;
                }

            }
        }

        //Form Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Load an instance of GameManager and Display The Title Screen
            GameManager.Instance().ShowSellingGameScreen();
            //GameManager.Instance().ShowTitleScreen();
            //GameManager.Instance().ShowFarmingGameScreen();
            this.Hide(); //Hiding our MainWindow to keep main thread in line
        }

        //Properties


        //Methods


        //Forms Navigation Methods


        //Events


        //Form Events
        private void btnMuteMainTheme_Click(object sender, RoutedEventArgs e)
        {
            ToggleAudio(false);
        }
    }
}
