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
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
        }

        //Window Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Reposition this screen to center
            GameManager.Instance().CenterWindowOnScreen(this);
        }

        //Change Events
        private void cbShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            //Checkbox Sound
            MediaManager.Instance().PlaySoundClick();

            txtPassword.Visibility = System.Windows.Visibility.Collapsed;
            txtPasswordReveal.Visibility = System.Windows.Visibility.Visible;

            txtPasswordReveal.Focus();
        }

        private void cbShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            //Checkbox Sound
            MediaManager.Instance().PlaySoundClick();

            txtPassword.Visibility = System.Windows.Visibility.Visible;
            txtPasswordReveal.Visibility = System.Windows.Visibility.Collapsed;

            txtPassword.Focus();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Typing Sound
            MediaManager.Instance().PlaySoundType();

            if (!String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtPassword.IsEnabled = true;
            }
            else
            {
                txtPassword.IsEnabled = false;
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Typing Sound
            MediaManager.Instance().PlaySoundType();

            if (!String.IsNullOrWhiteSpace(txtPassword.Password) && !String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                btnConfirm.IsEnabled = true;
                btnConfirm.Foreground = Brushes.YellowGreen;
                cbShowPassword.IsEnabled = true;
            }
            else
            {
                btnConfirm.IsEnabled = false;
                btnConfirm.Foreground = Brushes.IndianRed;
                cbShowPassword.IsEnabled = false;
            }

            //syncronize to Revealed Password
            txtPasswordReveal.Text = txtPassword.Password;
        }

        private void txtPasswordReveal_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Typing Sound
            MediaManager.Instance().PlaySoundType();

            //syncronize to Password only when Revealed...is Revealed ! (O_O)
            if(cbShowPassword.IsChecked == true)
            {
                txtPassword.Password = txtPasswordReveal.Text;
            }
            
        }

        private void txtPassword_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtPassword.Password) && !String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                btnConfirm.IsEnabled = true;
            }
            else
            {
                btnConfirm.IsEnabled = false;
            }
        }

        //Button Events
        private void btnBackToLoginScreen_Click(object sender, RoutedEventArgs e)
        {
            //Play The Click Sound
            MediaManager.Instance().PlaySoundClick();
            //Refresh and Display The Loginscreen
            GameManager.Instance().ShowLoginScreen();
            //Close This Window
            this.Close();
        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            //Create a New User (Login in DB) through GameManager and Pass it to the DataManager
            DataManager.InsertLogin(GameManager.Instance().DefaultUser(txtUsername.Text,txtPassword.Password));
            
            //Virtually Kiss the..... euhhh.... Virtually *Click* the BackToLoginScreen Button
            btnBackToLoginScreen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }
    }
}
