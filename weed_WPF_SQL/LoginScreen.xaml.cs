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
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        Character character = new Character();
        MainMenu mainMenu = new MainMenu();
        
        public LoginScreen()
        {
            InitializeComponent();

            character.Name = "John Doe";
        }

        public void CallMainMenu()
        {
            mainMenu.Show(character);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CallMainMenu();
        }
    }
}
