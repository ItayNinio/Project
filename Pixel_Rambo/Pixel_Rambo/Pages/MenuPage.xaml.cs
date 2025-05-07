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
    //דף תפריט ראשי
    {
        public static double volume2 { get; set; } = 0.5;
        //משתנה שמכיל את עוצמת הווליום של המוזיקה
        public MenuPage()
        {
           
            this.InitializeComponent();
        
            GameEngine.GameServices.Music.Play("muzic.mp3");
        }
        //הפעולה מופעלת כאשר הדף נטען

        bool volume = true;
        //משתנה בוליאני שמכיל את מצב הווליום

        private void sign_in_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            sign_in_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/sign in3.PNG"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void sign_in_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            sign_in_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/sign in2.png")); 
        }
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור

        private void shopb_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopPage));
        }
        //מעבר לדף חנות

        private void shopb_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            shopimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/shop2.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור
        private void shopb_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            shopimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/shop.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור

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
        //כיבוי והפעלה של המוזיקה

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
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור

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
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void sign_in_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignUp));
        }
        //מעבר לדף כניסה

        private void playb_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            playimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/PLAY2.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void playb_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            playimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/PLAY.png"));

        }
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור

        private void optionsb_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsPage));
        }
        //מעבר לדף ההגדרות

        private void playb_Click(object sender, RoutedEventArgs e)
        {
            string dbPath = ApplicationData.Current.LocalFolder.Path;
            Frame.Navigate(typeof(LevelPage));
        }
        //מעבר לדף המשחק

        private void leaderboard_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LeaderboardPage));
        }
        //מעבר לדף הלידרבורד

        private void storage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ChooseFeaturePage));
        }
        //מעבר לדף האחסון

        private void Options_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            optionsimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/options_gold.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void Options_PointerExited(object sender, PointerRoutedEventArgs e)
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור
        {
            optionsimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/options.png"));

        }

        private void Storage_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            storage_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/storage_gold.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void Storage_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            storage_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/storage.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור
        private void exitbtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Exit();
        }
        //יציאה מהאפליקציה

        private void Guide_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GuidePage));
        }

        private void G_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            QuestMark_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/questmarkgold.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void G_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            QuestMark_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/questmark.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור
    }
}
