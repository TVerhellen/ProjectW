using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginScreen loginScreen;
        public MainWindow()
        {
            InitializeComponent();
            
            loginScreen = new LoginScreen();
        }

        public void ShowLogin()
        {
            loginScreen.Show();
            this.Close();
        }

        public void ShowSellingGame()
        {
            SellGame sellGame = new SellGame();
            sellGame.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //ShowLogin();
            ShowSellingGame();
        }
    }
}
