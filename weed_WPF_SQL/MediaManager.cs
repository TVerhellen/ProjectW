﻿using Microsoft.Win32;
using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Timers;
using System.Windows.Controls;

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
        public bool _audioMuted;
        private double defaultMusicPlayerVolume = 0.5;
        private int defaultFadeRatio = 5;
        private MediaPlayer MusicPlayer;

        private Uri _mp3MainTheme;
        private Uri _mp3HomeTheme;
        private Uri _mp3FarmingTheme;
        private Uri _mp3SellingTheme;
        private Uri _mp3HighscoreTheme;
        private Uri _mp3WebstoreTheme;

        private MediaPlayer OneShotPlayer;
        private double defaultOneShotPlayerVolume = 0.7;

        private Uri _mp3SoundClick;
        private Uri _mp3SoundStart;

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
            _audioMuted = false;
            _mp3MainTheme = null;
            _mp3HomeTheme = null;
            _mp3FarmingTheme = null;
            _mp3SellingTheme = null;
            _mp3HighscoreTheme = null;
            _mp3WebstoreTheme = null;

            OneShotPlayer = new MediaPlayer();

            _mp3SoundClick = null;
            _mp3SoundStart = null;

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
        //States
        public bool AudioMuted
        {
            get { return _audioMuted; }
            set { _audioMuted = value; }
        }
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

        //Audio Toggle & Overdraw
        public void DrawAudioToggle(Grid myMainGrid)
        {

        }
        /// <summary>
        /// Multi-Purpose Function that can Toggle The Audio Button when param is false, syncing from Singleton when true
        /// </summary>
        /// <param name="syncing">Only true when switching windows, false when expecting toggle behavior</param>
        public void ToggleAudio(Button clickedThis, Image seeingThis, GameManager.Scenes nowOpen, bool syncing)
        {
            //When Music Has NOT Been Muted In MediaManager Singleton
            if (!AudioMuted)
            {
                if (!syncing) //When we are simply Toggling On/Off
                {
                    MediaManager.Instance().PauseMusic();
                    this.AudioMuted = true;
                    seeingThis.Source = IcoMuted;
                    clickedThis.Background = Brushes.DarkRed;
                }
                else //When we are syncronizing Audio toggle representation with other Windows through Singleton
                {
                    //Carry over Icon State => Unmuted
                    seeingThis.Source = MediaManager.Instance().IcoUnmuted;
                    clickedThis.Background = Brushes.LawnGreen;
                    //Actually Play The Correct Track
                    PlaySceneTheme(nowOpen);
                }
            }
            else//When Music Has Been Muted In MediaManager Singleton
            {
                if (!syncing) //When we are simply Toggling On/Off
                {
                    this.PlayMusic();
                    this.AudioMuted = false;
                    seeingThis.Source = MediaManager.Instance().IcoUnmuted;
                    clickedThis.Background = Brushes.LawnGreen;
                }
                else //When we are syncronizing Audio toggle representation with other Windows through Singleton
                {
                    seeingThis.Source = MediaManager.Instance().IcoMuted;
                    clickedThis.Background = Brushes.DarkRed;
                }
            }
        }
        private void PlaySceneTheme(GameManager.Scenes nowOpen)
        {
            switch (nowOpen)
            {
                case GameManager.Scenes.Title:
                    if (!this.CheckCurrentAudioUri(Mp3MainTheme))
                    {
                        this.PlayMainTheme();
                    }
                    break;
                case GameManager.Scenes.Login:
                    if (!this.CheckCurrentAudioUri(Mp3MainTheme))
                    {
                        this.PlayMainTheme();
                    }
                    break;
                case GameManager.Scenes.Main:
                    if (!this.CheckCurrentAudioUri(Mp3HomeTheme))
                    {
                        this.PlayHomeTheme();
                    }
                    break;
                case GameManager.Scenes.Farm:
                    if (!this.CheckCurrentAudioUri(Mp3FarmingTheme))
                    {
                        this.PlayFarmingTheme();
                    }
                    break;
                case GameManager.Scenes.Selling:
                    if (!this.CheckCurrentAudioUri(Mp3SellingTheme))
                    {
                        this.PlaySellingTheme();
                    }
                    break;
                case GameManager.Scenes.Highscore:
                    if (!this.CheckCurrentAudioUri(Mp3HighscoreTheme))
                    {
                        this.PlayHighscoreTheme();
                    }
                    break;
                case GameManager.Scenes.Webstore:
                    if (!this.CheckCurrentAudioUri(Mp3WebstoreTheme))
                    {
                        this.PlayWebstoreTheme();
                    }
                    break;
                default:
                    break;
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
                MessageBox.Show("Opening & Playing Media Failed!!\nContact Your Administrator Urgently.","Error Playing Media - Theme",MessageBoxButton.OK,MessageBoxImage.Error);
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

        //Controls
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
        //Looping Permanent
        private void Music_Ended(object sender, EventArgs e)
        {
            MusicPlayer.Position = TimeSpan.Zero;
            MusicPlayer.Play();
        }
        //Fading
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
        public Task PlayOneShotFile(Uri file, bool onLoop)
        {
            var tcs = new TaskCompletionSource<bool>();
            OneShotPlayer.Open(file);
            OneShotPlayer.Volume = defaultOneShotPlayerVolume;
            OneShotPlayer.Play();
            OneShotPlayer.MediaFailed += (o, args) =>
            {
                MessageBox.Show("Opening & Playing Media Failed!!\nContact Your Administrator Urgently.", "Error Playing Media - Sound", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            if (onLoop)
            {
                OneShotPlayer.MediaEnded += new EventHandler(Music_Ended);
            }
            else
            {
                OneShotPlayer.MediaEnded -= new EventHandler(Music_Ended);
            }

            //Give a Result to The Task if opened succesfully try True
            OneShotPlayer.MediaOpened += (sender, e) =>
            {
                //MusicPlayer.Close(); //MediaPlayer is intended for looping background music should not be closed
                tcs.TrySetResult(true);
            };

            return tcs.Task;
        }

        //Audio Files & Functions
        public Uri GetCurrentAudioUri()
        {
            return MusicPlayer.Source;
        }
        public bool CheckCurrentAudioUri(Uri check)
        {
            bool hasMatched = false;

            if(check == MusicPlayer.Source)
            {
                hasMatched = true;
            }

            return hasMatched;
        }
        //Themes
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

        public Uri Mp3HomeTheme
        {
            get
            {
                if (_mp3HomeTheme == null)
                {//Set Audio Source by browsing to containing folder and pinpointing Audio File
                    _mp3HomeTheme = new Uri("../../Assets/audio/HomeTheme.mp3", UriKind.Relative); //Relative Application Resource
                }

                return _mp3HomeTheme;
            }
        }
        public void PlayHomeTheme()
        {
            //As Syncronous Task (Can be called to run on main thread)
            this.PlayMusicFile(Mp3HomeTheme, true);
        }

        public Uri Mp3FarmingTheme
        {
            get
            {
                if (_mp3FarmingTheme == null)
                {//Set Audio Source by browsing to containing folder and pinpointing Audio File
                    _mp3FarmingTheme = new Uri("../../Assets/audio/FarmingTheme.mp3", UriKind.Relative); //Relative Application Resource
                }

                return _mp3FarmingTheme;
            }
        }
        public void PlayFarmingTheme()
        {
            //As Syncronous Task (Can be called to run on main thread)
            this.PlayMusicFile(Mp3FarmingTheme, true);
        }

        public Uri Mp3SellingTheme
        {
            get
            {
                if (_mp3SellingTheme == null)
                {//Set Audio Source by browsing to containing folder and pinpointing Audio File
                    _mp3SellingTheme = new Uri("../../Assets/audio/SellingTheme.mp3", UriKind.Relative); //Relative Application Resource
                }

                return _mp3SellingTheme;
            }
        }
        public void PlaySellingTheme()
        {
            //As Syncronous Task (Can be called to run on main thread)
            this.PlayMusicFile(Mp3SellingTheme, true);
        }

        public Uri Mp3HighscoreTheme
        {
            get
            {
                if (_mp3HighscoreTheme == null)
                {//Set Audio Source by browsing to containing folder and pinpointing Audio File
                    _mp3HighscoreTheme = new Uri("../../Assets/audio/HighscoreTheme.mp3", UriKind.Relative); //Relative Application Resource
                }

                return _mp3HighscoreTheme;
            }
        }
        public void PlayHighscoreTheme()
        {
            //As Syncronous Task (Can be called to run on main thread)
            this.PlayMusicFile(Mp3HighscoreTheme, true);
        }

        public Uri Mp3WebstoreTheme
        {
            get
            {
                if (_mp3WebstoreTheme == null)
                {//Set Audio Source by browsing to containing folder and pinpointing Audio File
                    _mp3WebstoreTheme = new Uri("../../Assets/audio/WebstoreTheme.mp3", UriKind.Relative); //Relative Application Resource
                }

                return _mp3WebstoreTheme;
            }
        }
        public void PlayWebstoreTheme()
        {
            //As Syncronous Task (Can be called to run on main thread)
            this.PlayMusicFile(Mp3WebstoreTheme, true);
        }

        //Sounds
        public Uri Mp3SoundClick
        {
            get 
            {
                if (_mp3SoundClick == null)
                {//Set Audio Source by browsing to containing folder and pinpointing Audio File
                    _mp3SoundClick = new Uri("../../Assets/audio/ClickSound.mp3", UriKind.Relative); //Relative Application Resource
                }
                return _mp3SoundClick; 
            }
        }
        public void PlaySoundClick()
        {
            this.PlayOneShotFile(Mp3SoundClick, false);
        }
        public Uri Mp3SoundStart
        {
            get
            {
                if (_mp3SoundStart == null)
                {//Set Audio Source by browsing to containing folder and pinpointing Audio File
                    _mp3SoundStart = new Uri("../../Assets/audio/StartSound.mp3", UriKind.Relative); //Relative Application Resource
                }
                return _mp3SoundStart;
            }
        }
        public void PlaySoundStart()
        {
            this.PlayOneShotFile(Mp3SoundStart, false);
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
