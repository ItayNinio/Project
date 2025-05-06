using DataBaseProject;
using GameEngine.GameServices;
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
    public sealed partial class LevelPage : Page
    //דף בחירת רמות קושי
    //המשתמש בוחר את רמת הקושי של המשחק
    //המשתמש יכול לבחור בין 3 רמות קושי
    //רמת קושי 1 נפתחת אוטומטית
    //רמת קושי 2 נפתחת לאחר סיום רמת קושי 1
    //רמת קושי 3 נפתחת לאחר סיום רמת קושי 2
    {
        public LevelPage()
        {
            this.InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            var clickButton = (Button)sender; //מהו הלחצן שגרם לפעולה לקרות
            FillLevel(clickButton.TabIndex);
            Manager.Gamestate = Constants.GameState.Started;
            Frame.Navigate(typeof(GamePage));
          
            

        }
        //הפעולה תעביר את המשתמש לדף המשחק ותמלא את השלב הנבחר

        private void FillLevel(int levelNumber)  //הפעולה תמלא את ערכי השלב שהמשתמש בחר
        {
            Server.SetCurrentLevel(GameManager.GameUser, levelNumber);
            
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
            
        }
        //חזרה לדף התפריט

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OpeningClosingLevels();
        }

        //MaxLevelהפעולה פותחת וחוסמת לחצנים לבחירת רמות הקושי בהתאם ל 
        private void OpeningClosingLevels()
        {
            
            CloseAllLevels();
            if (GameManager.GameUser.MaxLevel > 1)
            {
               
                level_2.IsEnabled = true;
                level2_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/Number02.png"));
            }
            if (GameManager.GameUser.MaxLevel > 2)
            {
                level_3.IsEnabled = true;
                level3_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/Number03.png"));
            }
        }
        //הפעולה סוגרת את כל הלחצנים לבחירת רמות הקושי

        private void CloseAllLevels()
        {
            level_2.IsEnabled = false;
            level2_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/lock.png"));

            level_3.IsEnabled = false;
            level3_image.Source = new BitmapImage(new Uri("ms-appx:///Imgs/lock.png"));


        }
        //הפעולה סוגרת את כל הלחצנים לבחירת רמות הקושי
    }
}
