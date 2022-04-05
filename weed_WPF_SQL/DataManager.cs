using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace weed_WPF_SQL
{
    public class DataManager
    {
        //Fonts
        private FontFamily _fntTitleFont;
        private FontFamily _fntSubFont;
        private FontFamily _fntMainFont;

        //Images
        private BitmapImage _imgSplashScreenBg;
        private BitmapImage _imgLoginScreenBg;

        //Singleton
        private static DataManager instance = null;
        private static readonly object padlock = new object();

        //Constructors 
        private DataManager()
        {
            //Fonts
            _fntTitleFont = new FontFamily();
            _fntSubFont = new FontFamily();
            _fntMainFont = new FontFamily();

            //Images
            _imgSplashScreenBg = new BitmapImage();
            _imgLoginScreenBg = new BitmapImage();

            //Other

        }
        //Singleton Instance
        public static DataManager Instance()
        {
            if(instance == null)
            {
                instance = new DataManager();
            }
            return instance;
        }

        //Properties
        public FontFamily FntTitleFont
        {
            get 
            { 
                if( _fntTitleFont.Source == null )
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
                if(_imgLoginScreenBg.UriSource == null)
                {
                    //Set Image Source
                    _imgLoginScreenBg= new BitmapImage();
                    _imgLoginScreenBg.BeginInit();
                    _imgLoginScreenBg.UriSource = new Uri("/Assets/img/LoginScreen.jpg", UriKind.Relative);
                    _imgLoginScreenBg.EndInit();
                }

                return _imgLoginScreenBg;
            }
        }

        //METHODS

        //STATICS

    }
}
