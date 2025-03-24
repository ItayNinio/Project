using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Rambo.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
           
            this.InitializeComponent();
            BackgroundMusic.Source = new Uri("ms-appx:///muzic.mp3");
           
            BackgroundMusic.Play();
        }

        bool volume = true;

        private void sign_in_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            sign_in_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/sign in3.PNG"));
        }

        private void sign_in_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            sign_in_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/sign in2.png")); 
        }

        private void shopb_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage));
        }

        private void shopb_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            shopimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/shop2.png"));
        }

      

        private void shopb_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            shopimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/shop.png"));
        }

      
       

        private void audibtn_Click(object sender, RoutedEventArgs e)
        {
            if (volume)
            {
                BackgroundMusic.Volume = 0;
                sign_in_gimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/audio.png"));
                volume = false;
            }
            else
            {
                BackgroundMusic.Volume = 0.5;
                sign_in_gimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/audio2.png"));
                volume = true;
            }
        }

        private void audibtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (volume)
            {
                sign_in_gimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/audio2.png"));
            }
            else
            {
                sign_in_gimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/audio.png"));
            }
        }

        private void audibtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (volume)
            {
                sign_in_gimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/audio3.png"));
            }
            else
            {
                sign_in_gimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/audi4.png"));
            }
        }

        private void sign_in_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignIn));
        }

        private void playb_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            playimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/PLAY2.png"));
        }

        private void playb_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            playimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/PLAY.png"));

        }

        private void optionsb_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsPage));
        }

        private void playb_Click(object sender, RoutedEventArgs e)
        {
            string dbPath = ApplicationData.Current.LocalFolder.Path;
            Frame.Navigate(typeof(LevelPage));
        }

        

        private void leaderboard_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LeaderboardPage));
        }

        private void storage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChooseFeaturePage));
        }

        private void Options_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            optionsimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/options_gold.png"));
        }

        private void Options_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            optionsimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/options.png"));

        }

        private void Storage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            storage_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/storage_gold.png"));
        }

        private void Storage_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            storage_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/storage.png"));
        }
    }
}
