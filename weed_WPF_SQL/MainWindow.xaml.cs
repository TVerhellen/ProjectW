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

        //Form Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Load an instance of GameManager and Display The Title Screen
            GameManager.Instance().ShowTitleScreen();
            this.Hide(); //Hiding our MainWindow to keep main thread in line
        }

        //Properties


        //Methods


        //Forms Navigation Methods


        //Events


        //Form Events
        private void btnAudioToggle_Click(object sender, RoutedEventArgs e)
        {
            //Toggle The Audio from the MainWindow if possible
            MediaManager.Instance().ToggleAudio(btnAudioToggle,imgAudioToggle,GameManager.Scenes.Exe,false);
        }
    }
}
