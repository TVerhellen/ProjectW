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
