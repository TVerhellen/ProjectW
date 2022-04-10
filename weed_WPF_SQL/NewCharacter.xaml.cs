using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for NewCharacter.xaml
    /// </summary>
    public partial class NewCharacter : Window
    {
        //Member variables
        private string _characterName;

        //Constructor
        public NewCharacter()
        {
            InitializeComponent();

            //intialize
            _characterName = "John Doe";

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        //Properties
        public string CharacterName
        {
            get { return _characterName; }
            set { _characterName = value; }
        }

        //Window Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(!this.IsVisible)
            {
                //Hide() is not an Event we can tap into
                //Hence upon hiding we call Close() Event, Which Will be called cascading into Closing() Event We Attached Functions To
                this.Close();
            }
            else
            {
                GameManager.Instance().CenterWindowOnScreen(this);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            GameManager.Instance().SetCharacterName(CharacterName);
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                btnConfirm.IsEnabled = true;
                btnConfirm.Foreground = Brushes.LawnGreen;
                CharacterName = txtUsername.Text;
            }
            else
            {
                btnConfirm.IsEnabled = false;
                btnConfirm.Foreground = Brushes.IndianRed;
            }
        }

        private void btnBackToLoginScreen_Click(object sender, RoutedEventArgs e)
        {
            //Play The Click Sound
            MediaManager.Instance().PlaySoundClick();
            //Close This Window
            this.Hide();
            //Refresh and Display The Loginscreen
            GameManager.Instance().ShowLoginScreen();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            CharacterName = txtUsername.Text;

            //Virtually Kiss the..... euhhh.... Virtually *Click* the BackToLoginScreen Button
            btnBackToLoginScreen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }


    }
}
