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
using System.Windows.Threading;

namespace weed_WPF_SQL
{
    /// <summary>
    /// Interaction logic for Highscores.xaml
    /// </summary>
    public partial class Highscores : Window
    {
        //member variables
        private BitmapImage imgBlinkOff;
        private BitmapImage imgBlinkOn;

        private DispatcherTimer blinkTime;
        private bool blinkOn = false;

        private List<Character> characters;

        public Highscores()
        {
            InitializeComponent();

            // Reposition
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            characters = new List<Character>();
            characters = DataManager.GetCharacters();

            //Initialize images
            imgBlinkOff = new BitmapImage();
            imgBlinkOff.BeginInit();
            imgBlinkOff.UriSource = new Uri("/Assets/img/Highscore_Base.png", UriKind.Relative);
            imgBlinkOff.EndInit();

            imgBlinkOn = new BitmapImage();
            imgBlinkOn.BeginInit();
            imgBlinkOn.UriSource = new Uri("/Assets/img/Highscore_Blink.png", UriKind.Relative);
            imgBlinkOn.EndInit();

            //Set timers
            blinkTime = new DispatcherTimer();
            blinkTime.Interval = TimeSpan.FromSeconds(0.5);
            blinkTime.Tick += new EventHandler(BlinkHighscore);
        }

        //Methods
        private string GetLeaderboard()
        {
            //Setup Display Values
            string leaderboard = "";
            int pad = 10;
            int maxTop = 0;
            if(characters.Count >= 7)
            {
                maxTop = 7;
            }
            else
            {
                maxTop = characters.Count;
            }

            //Gather Top Characters
            for (int i = 1; i < maxTop+1; i++)
            {
                //Truncate Name
                string name = characters[i - 1].Name.ToString();
                int nameLength = name.Length;
                //Determine Padding
                int padDif = 0;
                if (nameLength > 12)
                {
                    name = name.Substring(0, 12);
                }
                else
                {
                    padDif = 12 - (name.Length+1);
                }


                leaderboard += string.Format($"{i.ToString().PadLeft(pad)} {name.PadLeft(pad + padDif)} {characters[i - 1].Money.ToString().PadLeft(pad)}\n");
            }
            return leaderboard;
        }

        //Window Events
        private void Window_Closed(object sender, EventArgs e)
        {
            GameManager.Instance().Shutdown();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Start Timers 
            blinkTime.Start();

            //Stylize Titles
            lblHighscore.FontFamily = MediaManager.Instance().FntScoreFont;

            //Gather Leaderboard
            if(characters.Count > 0)
            {
                lblHighscore.Content = GetLeaderboard();
            }
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                //ReFetch DB Highscores
                if(characters != null)
                {
                    characters = DataManager.GetCharacters();
                    if (characters.Count > 0)
                    {
                        lblHighscore.Content = GetLeaderboard();
                    }
                }

                //Sync Audio Symbol's State & Start or Continue Theme Music
                MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Highscore, true);

                //Reposition Window To Center
                GameManager.Instance().CenterWindowOnScreen(this);

                //Restart Blinking Timer
                blinkTime.Start();
            }
            else
            {
                //Stop Timers 
                blinkTime.Stop();
            }
        }

        //Button Events
        private void btnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            //Play Click Sound
            MediaManager.Instance().PlaySoundClick();

            //Display Main Menu Scene
            GameManager.Instance().ShowMainMenuScreen();
            this.Hide();
        }
        private void btnAudioToggle_Click(object sender, RoutedEventArgs e)
        {
            //Sync Audio Symbol's State & Toggle Audio
            MediaManager.Instance().ToggleAudio(btnAudioToggle, imgAudioToggle, GameManager.Scenes.Highscore, false);
        }

        //Timer Events
        private async void BlinkHighscore(object sender, EventArgs e)
        {
            if(!blinkOn)
            {
                blinkOn = true;
                imgHighscoreBg.Source = imgBlinkOn;
            }
            else
            {
                blinkOn = false;
                imgHighscoreBg.Source = imgBlinkOff;
            }
        }
    }
}
