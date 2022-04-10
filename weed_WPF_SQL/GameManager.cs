using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace weed_WPF_SQL
{
    public class GameManager
    {
        //Game Parameters
        Login _myUser;
        Character _myCharacter;


        //Windows
        private TitleScreen title;
        private LoginScreen login;
        private MainMenu main;
        private Highscores highscores;
        private Webstore webstore;
        private SellGame selling;
        private FarmGame farming;

        //Singleton
        private static GameManager instance = null;
        //private static readonly object padlock = new object();

        private GameManager()
        {
            //Game Parameters
            MyUser = new Login();
            MyCharacter = new Character();

            //Windows
            title = new TitleScreen();
            login = new LoginScreen();
            main = new MainMenu();
            highscores = new Highscores();
            webstore = new Webstore();
            selling = new SellGame();
            farming = new FarmGame();
        }

        //Singleton Instance
        public static GameManager Instance()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }

        //Parameters
        public Login MyUser
        {
            get { return _myUser; }
            set 
            { 
                _myUser = value;
            }
        }
        public Character MyCharacter
        {
            get { return _myCharacter; }
            set 
            { 
                _myCharacter = value; 
            }
        }

        //Default Entities
        public Login DefaultUser(string username, string password)
        {
            Login myNewUser = new Login();
            myNewUser.Username = username;
            myNewUser.Password = password;

            return myNewUser;
        }
        public Character DefaultCharacter(Login user)
        {
            Character myNewCharacter = new Character();
            if(MyUser.LoginID > 0)
            {
                //Starting Values
                //myNewCharacter.CharacterID = Autofilled???
                myNewCharacter.LoginID = user.LoginID;
                myNewCharacter.Name = "John Doe";
                myNewCharacter.Money = 1500;
                myNewCharacter.Weed = 10;
                myNewCharacter.Reputation = 1;
                myNewCharacter.Stress = 0;

                //Trackers
                myNewCharacter.TotalCycles = 0;
                myNewCharacter.LongestStreak = 0;
                myNewCharacter.LastTimeCaught = 0;

                //Default Modifiers
                myNewCharacter.HasBike = false;
                myNewCharacter.HasStressCapUp = false;
                myNewCharacter.HasStressRecovUp = false;

                //Relational Tables
                myNewCharacter.FarmID = null;
            }

            return myNewCharacter;
        }
        public Character ResetCharacter(Login user, string charName)
        {
            Character myNewCharacter = new Character();
            if (MyUser.LoginID > 0)
            {
                //Starting Values
                myNewCharacter.CharacterID = MyCharacter.CharacterID;
                myNewCharacter.LoginID = user.LoginID;
                myNewCharacter.Name = charName;
                myNewCharacter.Money = 1500;
                myNewCharacter.Weed = 10;
                myNewCharacter.Reputation = 1;
                myNewCharacter.Stress = 0;

                //Trackers
                myNewCharacter.TotalCycles = 0;
                myNewCharacter.LongestStreak = 0;
                myNewCharacter.LastTimeCaught = 0;

                //Default Modifiers
                myNewCharacter.HasBike = false;
                myNewCharacter.HasStressCapUp = false;
                myNewCharacter.HasStressRecovUp = false;

                //Relational Tables
                myNewCharacter.FarmID = null;
            }

            return myNewCharacter;
        }
        public Character CharacterFromDb(Login user)
        {
            Character fetched = new Character();
            if(DataManager.GetCharacter(user.LoginID) != null)
            {
                fetched = DataManager.GetCharacter(user.LoginID);
            }
            return fetched;
        }

        public Farm DefaultFarm(Character character)
        {
            Farm myNewFarm = new Farm();
            myNewFarm.CharacterID = character.CharacterID;
            //myNewFarm.LightingID = 0;
            //myNewFarm.HeatingID = 0;
            //myNewFarm.HumidityID = 0;

            return myNewFarm;
        }

        public Cultivator DefaultCultivator(Farm farm)
        {
            Cultivator myNewCultivator = new Cultivator();
            myNewCultivator.FarmID = farm.FarmID;

            return myNewCultivator;
        }

        //Created Entities
        public void SetCharacterName(string name)
        {
            if(!String.IsNullOrEmpty(name))
            {
                MyCharacter.Name = name;
            }
        }

        //Window Methods
        public void Shutdown()
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void ShowTitleScreen()
        {
            title.IsEnabled = true;
            title.Show();
        }
        public void ShowLoginScreen()
        {
            login.IsEnabled = true;
            login.Show();
        }
        public void ShowMainMenuScreen()
        {
            main.IsEnabled = true;
            main.Show();
        }
        public void ShowHighscoreScreen()
        {
            highscores.IsEnabled = true;
            highscores.Show();
        }
        public void ShowWebstoreScreen()
        {
            webstore.IsEnabled = true;
            webstore.Show();
        }
        public void ShowSellingGameScreen()
        {
            selling.IsEnabled = true;
            selling.Show();
        }
        public void ShowFarmingGameScreen()
        {
            farming.IsEnabled = true;
            farming.Show();
        }
        public void CenterWindowOnScreen(Window repositionThis)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = repositionThis.Width;
            double windowHeight = repositionThis.Height;
            repositionThis.Left = (screenWidth / 2) - (windowWidth / 2);
            repositionThis.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        //Types
        public enum Scenes
        {
            Exe,
            Title,
            Login,
            Main,
            Farm,
            Selling,
            Highscore,
            Webstore
        }
    }
}
