using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using GameEngine.GameServices;  
using System;
using DataBaseProject.Models;
using Pixel_Rambo.GameServices;
using DataBaseProject;
using System.Collections.Generic;


namespace Pixel_Rambo.Pages
{
    public sealed partial class OptionsPage : Page
    //דף הגדרות המשחק
    //המשתמש יכול לשנות את מקשי השליטה של המשחק
    //המשתמש יכול לשנות את עוצמת השמע של המשחק
    {    
        private bool isChangingKey = false;
        // האם המשתמש משנה מקש
        private int currentActionIndex = -1;
        // מהו המקש שהמשתמש משנה
        public OptionsPage()
        {
            this.InitializeComponent();
            // Attach to the CoreWindow keydown event so we capture key presses even if the page isn’t focused
            Window.Current.CoreWindow.KeyDown += Window_Pressed;
            // Attach the Loaded event handler for the page.
            this.Loaded += OptionsPage_Loaded;
        }
        //הפעולה מופעלת כאשר הדף נטען
        private void OptionsPage_Loaded(object sender, RoutedEventArgs e)
        {
         
            int currentUserId = GameManager.GameUser.UserId;
            List<string> keybinds = Server.GetKeyBinds(currentUserId);

            // Check that keybinds were found and that we have at least four entries.
            if (keybinds != null && keybinds.Count >= 4)
            {
                Shoot_holder.Text = keybinds[0];  // Shoot
                Jump_holder.Text = keybinds[1];   // Jump
                Left_holder.Text = keybinds[2];   // Left
                Right_holder.Text = keybinds[3];  // Right
            }
            // If no keybinds are found, the UI will display the default values.
            volumeSlider.Value = GameEngine.GameServices.Music.Volume; // Set the volume slider to the current volume level
            sfxSlider.Value = GameEngine.GameServices.Music.Efects_Volume; // Set the SFX slider to the current SFX volume level
        }
        //הפעולה מציגה את המקש של כל פעולה על המסך

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }
        //הפעולה מחזירה את המשתמש לדף הקודם

        private void backbtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn2.png"));
        }
        //הפעולה משנה את התמונה של הלחצן כאשר העכבר נכנס לתחום הלחצן

        private void backbtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn.png"));
        }
        //הפעולה משנה את התמונה של הלחצן כאשר העכבר יוצא מתחום הלחצן

        private void Change_Keybind(object sender, RoutedEventArgs e)
        {
            var clickButton = (Button)sender;
       
            currentActionIndex = clickButton.TabIndex;
            isChangingKey = true;

        
            switch (currentActionIndex)
            {
                case 1:
                    Shoot_holder.Text = "Press a key";
                    break;
                case 2:
                    Jump_holder.Text = "Press a key";
                    break;
                case 3:
                    Left_holder.Text = "Press a key";
                    break;
                case 4:
                    Right_holder.Text = "Press a key";
                    break;
            }
        }
        //הפעולה משנה את המקש של כל פעולה על המסך

        private void new_key(int tabIndex, string newKey)
        {
            switch (tabIndex)
            {
                case 1:
                    Shoot_holder.Text = newKey;
                    break;
                case 2:
                    Jump_holder.Text = newKey;
                    break;
                case 3:
                    Left_holder.Text = newKey;
                    break;
                case 4:
                    Right_holder.Text = newKey;
                    break;
            }
       
            isChangingKey = false;
            currentActionIndex = -1;
            int currentUserId = GameManager.GameUser.UserId;
             Server.SaveKeyBinds(tabIndex,GameManager.GameUser.UserId, newKey);
        }
        //הפעולה משנה את המקש של כל פעולה על המסך


        private void Window_Pressed(CoreWindow sender, KeyEventArgs e)
        {
            if (!isChangingKey || currentActionIndex == -1)
                return;

          
            string newKey = e.VirtualKey.ToString();

          
            new_key(currentActionIndex, newKey);
        }
        //הפעולה מאזינה ללחיצות על מקשים

        private void VolumeSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            GameEngine.GameServices.Music.Volume = (int)volumeSlider.Value;
          
        }

        private void SfxSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            GameEngine.GameServices.Music.Efects_Volume = (int)sfxSlider.Value;
        }
        //הפעולה משנה את עוצמת השמע של המשחק
    }
}
