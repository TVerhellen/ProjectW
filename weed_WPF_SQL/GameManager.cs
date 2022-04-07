using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //Default Values
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

        public Farm DefaultFarm(Character character)
        {
            Farm myNewFarm = new Farm();
            myNewFarm.CharacterID = character.CharacterID;
            myNewFarm.LightingID = 0;
            myNewFarm.HeatingID = 0;
            myNewFarm.HumidityID = 0;

            return myNewFarm;
        }



        //Window Methods
        public void Shutdown()
        {
            System.Windows.Application.Current.Shutdown();
        }
        public void ShowTitleScreen()
        {
            title.Show();
        }
        public void ShowLoginScreen()
        {
            login.Show();
        }
        public void ShowMainMenuScreen()
        {
            main.Show();
        }
        public void ShowSellingGameScreen()
        {
            selling.Show();
        }
        public void ShowFarmingGameScreen()
        {
            farming.Show();
        }

    }
}
