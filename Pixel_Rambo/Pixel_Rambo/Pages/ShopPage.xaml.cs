
using DataBaseProject;
using Pixel_Rambo.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ShopPage : Page
    //דף חנות
    {
        public ShopPage()
        {
            this.InitializeComponent();
        }
        //הפעולה מופעלת כאשר הדף נטען

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }
        //חזרה לדף התפריט

        private void backbtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn2.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void backbtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור


        private void Buy_Click1(object sender, RoutedEventArgs e)
        {
            if (GameManager.GameUser.Money < 7)
            {
                ShowMessage("You do not have enough money");
            }
            else
            {
                List<int> ids = Server.GetOwnProductId(GameManager.GameUser);
                if (ids == null)
                {
                    playimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));
                    GameManager.GameUser.Money -= 7;
                    GameManager.GameUser.CurrentSkinId = 1;
                    Server.AddProduct(GameManager.GameUser, 1);
                    ShowMessage("Item has been added to your inventory");
                }
                else if (ids.Contains(1))
                {
                    ShowMessage("You already have this skin");
                }
                else
                {
                    playimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));
                    GameManager.GameUser.Money -= 7;
                    GameManager.GameUser.CurrentSkinId = 1;
                    Server.AddProduct(GameManager.GameUser, 1);
                    ShowMessage("Item has been added to your inventory");
                }
            }
            money_holder.Text = GameManager.GameUser.Money.ToString();

        }
        //כאשר השחקן רוצה לקנות את פריט 1

        private void ShowMessage(string message)
        {
            // Example showing a dialog (could also update a TextBlock or similar)
            var dialog = new Windows.UI.Popups.MessageDialog(message);
            _ = dialog.ShowAsync();
        }
        //הפעולה מציגה הודעה לשחקן

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            money_holder.Text = GameManager.GameUser.Money.ToString();
            List<int> ids = Server.GetOwnProductId(GameManager.GameUser);
            if (ids != null && ids.Contains(1))
            {
                playimage.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));

            }
            if (ids != null && ids.Contains(2))
            {
                playimage2.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));

            }
            if (ids != null && ids.Contains(3))
            {
                playimage3.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));

            }
        }
        //הפריטים שבבעלות השחקן מופיעים כבר עם התמונה שמראה שהפריט שלו 

        private void Buy_Click2(object sender, RoutedEventArgs e)
        {
            if (GameManager.GameUser.Money < 8)
            {
                ShowMessage("You do not have enough money");
            }
            else
            {
                List<int> ids = Server.GetOwnProductId(GameManager.GameUser);
                if (ids == null)
                {
                    playimage2.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));
                    GameManager.GameUser.Money -= 8;
                    GameManager.GameUser.CurrentSkinId = 2;
                    Server.AddProduct(GameManager.GameUser, 2);
                    ShowMessage("Item has been added to your inventory");
                }
                else if (ids.Contains(2))
                {
                    ShowMessage("You already have this skin");
                }
                else
                {
                    playimage2.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));
                    GameManager.GameUser.Money -= 8;
                    GameManager.GameUser.CurrentSkinId = 2;
                    Server.AddProduct(GameManager.GameUser, 2);
                    ShowMessage("Item has been added to your inventory");
                }
                money_holder.Text = GameManager.GameUser.Money.ToString();
            }
        }
        //כאשר השחקן רוצה לקנות את פריט 2

        private void Buy_Click3(object sender, RoutedEventArgs e)
        {
            if (GameManager.GameUser.Money < 5)
            {
                ShowMessage("You do not have enough money");
            }
            else
            {
                List<int> ids = Server.GetOwnProductId(GameManager.GameUser);
                if (ids == null)
                {
                    playimage3.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));
                    GameManager.GameUser.Money -= 5;
                    GameManager.GameUser.CurrentSkinId = 3;
                    Server.AddProduct(GameManager.GameUser, 3);
                    ShowMessage("Item has been added to your inventory");
                }
                else if (ids.Contains(3))
                {
                    ShowMessage("You already have this skin");
                }
                else
                {
                    playimage3.Source = new BitmapImage(new Uri("ms-appx:///Imgs/owned_btn.png"));
                    GameManager.GameUser.Money -= 5;
                    GameManager.GameUser.CurrentSkinId = 3;
                    Server.AddProduct(GameManager.GameUser, 3);
                    ShowMessage("Item has been added to your inventory");
                }
            }
            money_holder.Text = GameManager.GameUser.Money.ToString();
        }
        //כאשר השחקן רוצה לקנות את פריט 3
    }
}
