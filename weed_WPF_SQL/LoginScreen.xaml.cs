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
        //Member Variables
        private bool loggedIn;
        private List<Login> logins;
        private Character character;
        
        //Constructors
        public LoginScreen()
        {
            InitializeComponent();

            //Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //Initialize Members
            loggedIn = false;
            logins = new List<Login>();
            logins = DataManager.GetLogins();
            character = new Character();
        }

        //Methods
        /// <summary>
        /// Multi-Purpose Function that can Toggle The Audio Button when param is false, syncing from Singleton when true
        /// </summary>
        /// <param name="syncing">Only true when switching windows, false when expecting toggle behavior</param>
        private void ToggleAudio(bool syncing)
        {
            //When Music Has NOT Been Muted In MediaManager Singleton
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
            else//When Music Has Been Muted In MediaManager Singleton
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
        private void ToggleLoginBtn(bool enabled)
        {
            if(enabled)
            {
                btnLogin.IsEnabled = true;
                btnLogin.Foreground = Brushes.YellowGreen;
            }
            else
            {
                btnLogin.IsEnabled = false;
                if(loggedIn)
                {
                    btnLogin.Foreground = Brushes.IndianRed;
                }
                else
                {
                    btnLogin.Foreground = Brushes.DarkGreen;
                }
                
            }
        }
        private void ToggleStartBtn(bool enabled)
        {
            if(enabled)
            {
                btnStartGame.IsEnabled = true;
                btnStartGame.Foreground = Brushes.YellowGreen;
            }
            else
            {
                btnStartGame.IsEnabled= false;
                btnStartGame.Foreground = Brushes.IndianRed;
            }

        }
        private void ToggleWarningPnl(bool enabled)
        {
            if(enabled)
            {
                pnlWarning.Opacity = 1;
                //Play a Sound
            }
            else
            {
                pnlWarning.Opacity = 0;
            }
        }
        private void ToggleSaveDataPnl(bool enabled)
        {
            if (enabled)
            {
                //Display
                pnlSaveData.Visibility = Visibility.Visible;
                //Enable
                pnlSaveData.IsEnabled = true;

                //Populate Combobox
                cbCharacterData.Items.Clear();
                cbCharacterData.Items.Add("Start Een Nieuwe Spel"); //Default Value 0
                //When an existing Character is detected select its index
                if (GameManager.Instance().MyCharacter != null && !String.IsNullOrWhiteSpace(GameManager.Instance().MyCharacter.Name))
                {
                    cbCharacterData.Items.Add(GameManager.Instance().MyCharacter.Name);
                    cbCharacterData.SelectedIndex = 1;
                }
                else
                {
                    //Player is forced to start new game because no exiting Character was found on Login Relation
                    cbCharacterData.SelectedIndex = 0;
                }

                //Enable Combobox
                cbCharacterData.IsEnabled = true;
                //Enable StartGame Button
                btnStartGame.IsEnabled = true;
                btnStartGame.Foreground = Brushes.LawnGreen;
            }
            else
            {
                //Display
                pnlSaveData.Visibility = Visibility.Hidden;
                //Disable
                pnlSaveData.IsEnabled = false;

                //Populate Combobox
                cbCharacterData.Items.Clear();

                //Disable Combobox
                cbCharacterData.IsEnabled = false;

                //Disable StartGame Button
                btnStartGame.IsEnabled = false;
                btnStartGame.Foreground = Brushes.DarkGreen;
            }

        }

        private void LogoutUser()
        {
            //Clear Login Details
            GameManager.Instance().MyUser = new Login();
            //Detach Character Savedata from Login
            GameManager.Instance().MyCharacter = new Character();

            //Change logged in state
            loggedIn = false;

            //Change Context of Login Btn
            btnLogin.Content = "Aanmelden";
            btnLogin.Foreground = Brushes.DarkGreen;

            //Clear Password field which auto toggles off Login
            txtPassword.Password = "";

            //Re-enable textboxes untill logout
            txtUsername.IsEnabled = true;
            txtPassword.IsEnabled = true;

            //Hide Warning Panel
            ToggleWarningPnl(false);

            //Hide Start Game Panel
            pnlSaveData.Visibility = Visibility.Hidden;

        }

        //Form Events
        private void Window_Closed(object sender, EventArgs e)
        {
            GameManager.Instance().Shutdown();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Setup Images
            imgBackgroundLogin.Source = MediaManager.Instance().ImgLoginScreenBg;

            //Stylize Titles
            lblLoginTitle.FontFamily = MediaManager.Instance().FntTitleFont;
            //Stylize Username
            lblUsername.FontFamily = MediaManager.Instance().FntSubFont;
            txtUsername.FontFamily = MediaManager.Instance().FntMainFont;
            //Stylize Password
            lblPassword.FontFamily = MediaManager.Instance().FntSubFont;
            txtPassword.FontFamily = MediaManager.Instance().FntTitleFont;
            //Stylize Button
            btnLogin.FontFamily = MediaManager.Instance().FntTitleFont;

            //Disable Password Input
            txtPassword.IsEnabled = false;
            //Clear Warning
            pnlWarning.Opacity = 0;

            //Hide Start Game Panel
            ToggleSaveDataPnl(false);

        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.IsVisible == true)
            {
                ToggleAudio(true);
                ToggleWarningPnl(false);
            }
            
        }

        //Form Element Events
        private void btnMuteMainTheme_Click(object sender, RoutedEventArgs e)
        {
            ToggleAudio(false);
        }
        private void btnBackToSplashScreen_Click(object sender, RoutedEventArgs e)
        {
            GameManager.Instance().ShowTitleScreen();
            this.Hide();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (!loggedIn)
            {
                foreach (Login login in logins)
                {
                    if (login.Authenticate(txtUsername.Text, txtPassword.Password))
                    {
                        //Attach Login Details To GameManager
                        GameManager.Instance().MyUser = login;

                        //Attach Character Data from Login Details
                        GameManager.Instance().MyCharacter = DataManager.GetCharacter(login.LoginID);

                        //Change loggedin state
                        loggedIn = true;

                        //Adjust Login Button Displays
                        btnLogin.Content = "Afmelden";
                        btnLogin.Foreground = Brushes.IndianRed;

                        //Disable textboxes untill logout
                        txtUsername.IsEnabled = false;
                        txtPassword.IsEnabled = false;
                        //Clear Warning Panel
                        ToggleWarningPnl(false);

                        //Hide Start Game Panel
                        ToggleSaveDataPnl(true);

                        break;
                    }
                }

                if (!loggedIn)
                {
                    lblWarning.Content = "Ongekende Aanmeld Gegevens";
                    ToggleWarningPnl(true);
                }
            }
            else
            {
                LogoutUser();
            }
        }
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            bool canStartGame = false;
            //New Game Selected
            if (cbCharacterData.SelectedIndex == 0 && GameManager.Instance().MyCharacter.LoginID > 0)
            {
                MessageBoxResult reply = MessageBox.Show("Opgelet!\n\"Nieuwe Spel Starten\" Werd Geselecteerd!\n\nWenst u de bestaande opslag gegevens te overschrijven?",
                                                        "Waarschuwing: Overschrijven Opslag Gegevens.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (reply == MessageBoxResult.Yes)
                {
                    //New Game Will overwrite old savedata
                    GameManager.Instance().MyCharacter = GameManager.Instance().DefaultCharacter(GameManager.Instance().MyUser);
                    //TODO DataManager.UpdateCharacter();
                }
                else
                {
                    //User will be asked to choose the existing savefile
                    MessageBox.Show("Gelieve een bestaande opslag te selecteren van de uitklapbare lijst.", "Nieuwe Spel Opstarten Werd Geannuleerd", MessageBoxButton.OK,MessageBoxImage.Information);
                    return;
                }
            }
            else if(cbCharacterData.SelectedIndex == 0)
            {
                //Starting New Game Because No Prior Savefile exists
                GameManager.Instance().MyCharacter = GameManager.Instance().DefaultCharacter(GameManager.Instance().MyUser);
                canStartGame = true;
            }
            else
            {
                //Continue existing savefile
                canStartGame = true;
            }

            //When We Are Allowed To Start Game
            if(canStartGame)
            {
                //Hide The Login Screen and Show The MainMenu / Home Screen
                this.Hide();
                GameManager.Instance().ShowMainMenuScreen();
            }


        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtPassword.IsEnabled = true;
                ToggleWarningPnl(false);
            }
        }
        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //When all input fields are filled in 
            if(!String.IsNullOrWhiteSpace(txtUsername.Text) && !String.IsNullOrWhiteSpace(txtPassword.Password))
            {
                //Enable the Login Button
                if(btnLogin.IsEnabled == false)
                {
                    ToggleLoginBtn(true);
                }
            }
            else
            {
                //Disable Login Button
                ToggleLoginBtn(false);
            }

            //When the Existing input is being edited clear the warning
            if(!String.IsNullOrWhiteSpace(txtPassword.Password))
            {
                ToggleWarningPnl(false);
            }
        }
    }
}
