using Microsoft.Win32;
using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        public MediaPlayer MusicPlayer;
        public bool blnMusicMuted = false;
        public MediaPlayer OneShotPlayer;
        private Uri _mp3MainTheme;

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

        //Audio
        public Task PlayAudioFile(Uri file, bool onLoop)
        {
            var tcs = new TaskCompletionSource<bool>();
            MusicPlayer.MediaEnded += (sender, e) =>
            {
                MusicPlayer.Close();
                tcs.TrySetResult(true);
            };
            MusicPlayer.Open(file);
            MusicPlayer.Play();
            MusicPlayer.MediaFailed += (o, args) =>
            {
                MessageBox.Show("Opening & Playing Media Failed!!","Error Playing Media",MessageBoxButton.OK,MessageBoxImage.Error);
            };

            if(onLoop)
            {
                MusicPlayer.MediaEnded += new EventHandler(Music_Ended);
            }
            else
            {
                MusicPlayer.MediaEnded -= new EventHandler(Music_Ended);
            }

            return tcs.Task; 
        }
        private void Music_Ended(object sender, EventArgs e)
        {
            MusicPlayer.Position = TimeSpan.Zero;
            MusicPlayer.Play();
        }

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
            this.PlayAudioFile(Mp3MainTheme, true);
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
