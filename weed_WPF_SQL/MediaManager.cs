using Microsoft.Win32;
using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Timers;

namespace weed_WPF_SQL
{

    public class MediaManager
    {
        //Fonts
        private FontFamily _fntTitleFont;
        private FontFamily _fntSubFont;
        private FontFamily _fntMainFont;

        //Images
        private BitmapImage _icoUnmuted;
        private BitmapImage _icoMuted;
        private BitmapImage _imgSplashScreenBg;
        private BitmapImage _imgLoginScreenBg;

        //Audio
        private double defaultMusicPlayerVolume = 0.5;
        private int defaultFadeRatio = 5;
        private MediaPlayer MusicPlayer;
        public bool blnMusicMuted = false;

        private MediaPlayer OneShotPlayer;

        private Uri _mp3MainTheme;

        //Timers
        private Timer fadeTimer;
        private bool fadeTimeDone;

        //Singleton
        private static MediaManager instance = null;
        //private static readonly object padlock = new object();

        private MediaManager()
        {
            //Fonts
            _fntTitleFont = new FontFamily();
            _fntSubFont = new FontFamily();
            _fntMainFont = new FontFamily();

            //Icons
            _icoUnmuted = null;
            _icoMuted = null;

            //Images
            _imgSplashScreenBg = new BitmapImage();
            _imgLoginScreenBg = new BitmapImage();

            //Audio
            MusicPlayer = new MediaPlayer();
            _mp3MainTheme = null;
            OneShotPlayer = new MediaPlayer();

            //Other
            //Initialize Ticker
            fadeTimer = new Timer();
            fadeTimer.Interval = 50 / defaultFadeRatio;
            fadeTimer.Elapsed += FadeTimer_Elapsed;
        }
        //Singleton Instance
        public static MediaManager Instance()
        {
            if (instance == null)
            {
                instance = new MediaManager();
            }
            return instance;
        }

        //PROPERTIES
        //Fonts
        public FontFamily FntTitleFont
        {
            get
            {
                if (_fntTitleFont.Source == null)
                {
                    //Set FontFamily
                    _fntTitleFont = Application.Current.Resources["Hollyweed"] as FontFamily;
                }
                return _fntTitleFont;
            }
        }
        public FontFamily FntSubFont
        {
            get
            {
                if (_fntSubFont.Source == null)
                {
                    //Set FontFamily
                    _fntSubFont = Application.Current.Resources["Seaweed"] as FontFamily;
                }
                return _fntSubFont;
            }
        }
        public FontFamily FntMainFont
        {
            get
            {
                if (_fntMainFont.Source == null)
                {
                    //Set FontFamily
                    _fntMainFont = Application.Current.Resources["jsbdoublejointed"] as FontFamily;
                }
                return _fntMainFont;
            }
        }

        //Icons
        public BitmapImage IcoUnmuted
        {
            get
            {
                if(_icoUnmuted == null)
                {
                    //Set Icon Source
                    _icoUnmuted = new BitmapImage();
                    _icoUnmuted.BeginInit();
                    _icoUnmuted.UriSource = new Uri("/Assets/img/musical-note.png", UriKind.Relative);
                    _icoUnmuted.EndInit();
                }
                return _icoUnmuted;
            }
        }
        public BitmapImage IcoMuted
        {
            get
            {
                if(_icoMuted == null)
                {
                    //Set Icon Source
                    _icoMuted = new BitmapImage();
                    _icoMuted.BeginInit();
                    _icoMuted.UriSource = new Uri("/Assets/img/musical-note-muted.png", UriKind.Relative);
                    _icoMuted.EndInit();
                }
                return _icoMuted;
            }
        }

        //Images
        public BitmapImage ImgSplashScreenBg
        {
            get
            {
                if (_imgSplashScreenBg.UriSource == null)
                {
                    //Set Image Source
                    _imgSplashScreenBg = new BitmapImage();
                    _imgSplashScreenBg.BeginInit();
                    _imgSplashScreenBg.UriSource = new Uri("/Assets/img/SplashScreen.jpg", UriKind.Relative);
                    _imgSplashScreenBg.EndInit();
                }
                return _imgSplashScreenBg;

            }
        }
        public BitmapImage ImgLoginScreenBg
        {
            get
            {
                if (_imgLoginScreenBg.UriSource == null)
                {
                    //Set Image Source
                    _imgLoginScreenBg = new BitmapImage();
                    _imgLoginScreenBg.BeginInit();
                    _imgLoginScreenBg.UriSource = new Uri("/Assets/img/LoginScreen.jpg", UriKind.Relative);
                    _imgLoginScreenBg.EndInit();
                }

                return _imgLoginScreenBg;
            }
        }

        //AUDIO
        //Music
        public Task PlayMusicFile(Uri file, bool onLoop)
        {
            var tcs = new TaskCompletionSource<bool>();
            MusicPlayer.Open(file);
            MusicPlayer.Volume = defaultMusicPlayerVolume;
            MusicPlayer.Play();
            MusicPlayer.MediaFailed += (o, args) =>
            {
                MessageBox.Show("Opening & Playing Media Failed!!\nContact Your Administrator Urgently.","Error Playing Media",MessageBoxButton.OK,MessageBoxImage.Error);
            };

            if (onLoop)
            {
                MusicPlayer.MediaEnded += new EventHandler(Music_Ended);
            }
            else
            {
                MusicPlayer.MediaEnded -= new EventHandler(Music_Ended);
            }

            //Give a Result to The Task if opened succesfully try True
            MusicPlayer.MediaOpened += (sender, e) =>
            {
                //MusicPlayer.Close(); //MediaPlayer is intended for looping background music should not be closed
                tcs.TrySetResult(true);
            };

            return tcs.Task; 
        }
        public void PlayMusic()
        {
            MusicPlayer?.Play();
        }
        public void PauseMusic()
        {
            MusicPlayer?.Pause();
        }
        public void StopMusic()
        {
            MusicPlayer?.Stop();
        }
        private void Music_Ended(object sender, EventArgs e)
        {
            MusicPlayer.Position = TimeSpan.Zero;
            MusicPlayer.Play();
        }
        public void StartFadeOutMusic(int miliseconds)
        {
            fadeTimeDone = false;
            fadeTimer.Interval = miliseconds / defaultFadeRatio; //Fadeout Interval is 5 because max volume is 0.5 and decrement is 0.1
            fadeTimer.Start();
        }
        private void FadeOutMusic()
        {
            //Check To See If Muted State Has Been Reached 
            if(MusicPlayer != null && MusicPlayer.Volume > 0 && !fadeTimeDone)
            {

                MusicPlayer.Volume -= 0.1;
            }
            else
            {
                fadeTimeDone = true;
                fadeTimer.Stop();

                //Set Music Volume Back To Max of 50%
                MusicPlayer.Volume = defaultMusicPlayerVolume;
            }
        }
        //OneShots


        //Audio Files & Functions
        public Uri Mp3MainTheme
        {
            get
            {
                if(_mp3MainTheme == null)
                {
                    //Set Audio Source by browsing to containing folder and pinpointing Audio File
                    //_mp3MainTheme = new Uri(String.Format("{0}\\MainTheme.mp3",AppDomain.CurrentDomain.BaseDirectory)); // Direct Application Resource (Copy To Debug, See App Properties->Resources.resX)
                    //Or
                    _mp3MainTheme = new Uri("../../Assets/audio/MainTheme.mp3", UriKind.Relative); //Relative Application Resource
                }

                return _mp3MainTheme;
            }
        }
        public void PlayMainTheme()
        {
            //As Async Task (not voidable, must be called from Async function/class
            //TaskCompletionSource<bool> myTsk = new TaskCompletionSource<bool>();
            //Task myTask = new Task(() => PlayAudioFile(Mp3MainTheme));
            //return myTask;

            //As Syncronous Task (Can be called to run on main thread)
            this.PlayMusicFile(Mp3MainTheme, true);
        }

        //Events
        /// <summary>
        /// An Async Event Method to call on a Parallel Compute Thread To Call External functions by Timer
        /// </summary>
        private async void FadeTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Invoke our Method Upon Timer Tick
            Application.Current.Dispatcher.Invoke(new Action(() => { FadeOutMusic(); }));
        }

        //Other
        /// <summary>
        /// Request the User To Specify an Audio File (MP3)
        /// </summary>
        /// <returns>Uri of Selected Source, empty if null</returns>
        public Uri OpenAudioFileDialog()
        {
            Uri mySource = new Uri("");
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";

            if(openFileDialog.ShowDialog() == true)
            {
                mySource = new Uri(openFileDialog.FileName);
            }
            
            return mySource;
        }

    }
}
