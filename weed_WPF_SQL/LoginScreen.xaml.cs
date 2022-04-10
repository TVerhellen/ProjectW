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
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        //Member Variables
        private bool loggedIn;
        private List<Login> logins;
        private Character character;

        //Child Windows
        private NewUser createUser;
        private NewCharacter createCharacter;
        
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
        private void LogOut()
        {
            if (loggedIn)
            {//Raise a Click Event To Log Out User on Back To title Click
                btnLogin.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
        public void RefetchLogins()
        {
            logins = DataManager.GetLogins();
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

        //Window Events
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
            if (this.IsVisible)
            {
                //Sync Audio Symbol's State & Start or Continue Theme Music
                MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Login, true);

                //Hide The Warnings Panel
                ToggleWarningPnl(false);

                //Reposition Window To Center
                GameManager.Instance().CenterWindowOnScreen(this);


            }

            //Refetch Login Details
            RefetchLogins();

            //Was the user logged in already?
            if(loggedIn)
            {
                ToggleSaveDataPnl(true);
            }
        }

        //Form Element Events
        private void btnAudioToggle_Click(object sender, RoutedEventArgs e)
        {
            //Sync Audio Symbol's State & Toggle Audio
            MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Login, false);

            //Play Click Sound ?
            MediaManager.Instance().PlaySoundClick();
        }
        private void btnBackToSplashScreen_Click(object sender, RoutedEventArgs e)
        {
            LogOut();

            //Play The Click Sound
            MediaManager.Instance().PlaySoundClick();

            //Display The Title Screen
            GameManager.Instance().ShowTitleScreen();

            //Hide This Scene
            this.Hide();
        }
        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
            
            createUser = new NewUser();
            MediaManager.Instance().PlaySoundClick();
            createUser.Show();
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

            //Play Click Sound
            MediaManager.Instance().PlaySoundClick();
        }
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            //Variables needed to handle choices
            //Character Renaming Window
            createCharacter = new NewCharacter();
            //Conditional To Start Game
            bool canStartGame = false;
            //Set GameManager Character
            GameManager.Instance().MyCharacter = DataManager.GetCharacter(GameManager.Instance().MyUser.LoginID);

            //New Game Selected & Profile already has a savefile
            if (cbCharacterData.SelectedIndex == 0 && GameManager.Instance().MyCharacter != null)
            {
                MessageBoxResult reply = MessageBox.Show("Opgelet!\n\"Nieuwe Spel Starten\" Werd Geselecteerd!\n\nWenst u de bestaande opslag gegevens te overschrijven?",
                                                        "Waarschuwing: Overschrijven Opslag Gegevens.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (reply == MessageBoxResult.Yes)
                {
                    //Write To DB Event Handler
                    createCharacter.Closing += OverwriteExistingCharacter;


                    //Divert To New Creator Window
                    createCharacter.Show();
                    this.Hide();
                }
                else
                {
                    //User will be asked to choose the existing savefile
                    MessageBox.Show("Gelieve een bestaande opslag te selecteren uit de uitklapbare lijst om in te laden.", "Nieuwe Spel Opstarten Werd Geannuleerd", MessageBoxButton.OK,MessageBoxImage.Information);
                    return;
                }
            }
            else if(cbCharacterData.SelectedIndex == 0)
            {

                //Write To DB EventHandler
                createCharacter.Closing += InsertNewCharacter;

                //Rename New Character
                createCharacter.Show();
                this.Hide();

                //canStartGame = true;
            }
            else
            {
                //Continue existing savefile
                canStartGame = true;
            }

            //When We Are Allowed To Start Game
            if(canStartGame)
            {
                //Hide The Login Screen and
                this.Hide();
                //Show The MainMenu / Home Screen
                GameManager.Instance().ShowMainMenuScreen();
            }

            //Play Click Sound
            MediaManager.Instance().PlaySoundClick();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtPassword.IsEnabled = true;
                ToggleWarningPnl(false);
            }
            else
            {
                txtPassword.IsEnabled = false;
            }

            //Play Sounds of Typing
            MediaManager.Instance().PlaySoundType();
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

            //Play Sounds of Typing
            MediaManager.Instance().PlaySoundType();
        }

        private void InsertNewCharacter(object sender, EventArgs e)
        {
            //Starting New Game Because No Prior Savefile exists
            GameManager.Instance().MyCharacter = GameManager.Instance().DefaultCharacter(GameManager.Instance().MyUser, createCharacter.CharacterName);


            int myCharID = DataManager.InsertCharacter(GameManager.Instance().MyCharacter);
            Farm myNewFarm = new Farm();
            myNewFarm.CharacterID = myCharID;
            myNewFarm.LightingID = null;
            myNewFarm.HeatingID = null;
            myNewFarm.HumidityID = null;

            GameManager.Instance().MyUser.CharacterID = myCharID;
            DataManager.UpdateLogin(GameManager.Instance().MyUser);
            int myFarmID = DataManager.InsertFarm(myNewFarm);
            for (int i = 0; i < 5; i++)
            {
                Cultivator myNewCultivator = new Cultivator();
                myNewCultivator.FarmID = myFarmID;
                myNewCultivator.CultID = 1;
                myNewCultivator.WaterID = 1;
                myNewCultivator.FertilizerID = 1;
                myNewCultivator.SoilID = 1;
                myNewCultivator.LampID = 1;
                myNewCultivator.CyclesRequired = 8;
                myNewCultivator.CyclesPassed = 11;
                myNewCultivator.NameID = null;
                myNewCultivator.RendementValue = null;
                myNewCultivator.ProgresBarColor = null;
                myNewCultivator.WaterSupply = null;
                myNewCultivator.FertilizerSupply = null;
                DataManager.InsertCultivator(myNewCultivator);

            }
            GameManager.Instance().MyCharacter.FarmID = myFarmID;
            DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
        }
        private void OverwriteExistingCharacter(object sender, EventArgs e)
        {
            //Create New Save Data in Game Manager
            GameManager.Instance().MyCharacter = GameManager.Instance().ResetCharacter(GameManager.Instance().MyUser, createCharacter.CharacterName);
            //Create 
            DataManager.UpdateCharacter(GameManager.Instance().MyCharacter);
        }
    }
}
