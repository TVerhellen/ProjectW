using System.Windows;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        //member variables
        SellGame sellingGame = new SellGame();
        FarmGame farmGame = new FarmGame();
        Character character;

        //Constructors
        public MainMenu()
        {
            InitializeComponent();

        }
        public MainMenu(Character obj)
        {
            character = obj;
            InitializeComponent();
        }
        //Overloaded Show to pass our Character Object
        public void Show(Character obj)
        {
            character = obj;
            lbltest.Content = character.Name;
            base.Show();
        }

        //member functions
        public void CallSellingGame()
        {
            sellingGame.Show();
        }
        public void CallFarmGame()
        {
            farmGame.Show();
        }

        public void GoBackToLogin()
        {

            LoginScreen backLogin = new LoginScreen();
            backLogin.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CallFarmGame();
        }
    }
}
