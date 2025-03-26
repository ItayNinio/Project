using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using GameEngine.GameServices;  // For Server and GameUser
using System;
using DataBaseProject.Models;
using Pixel_Rambo.GameServices;
using DataBaseProject;
using System.Collections.Generic;


namespace Pixel_Rambo.Pages
{
    public sealed partial class OptionsPage : Page
    {
        // Flag to check if key change is active
        private bool isChangingKey = false;
        // Track the action index for the current keybind change (set via TabIndex)
        private int currentActionIndex = -1;

        public OptionsPage()
        {
            this.InitializeComponent();
            // Attach to the CoreWindow keydown event so we capture key presses even if the page isn’t focused
            Window.Current.CoreWindow.KeyDown += Window_Pressed;
            // Attach the Loaded event handler for the page.
            this.Loaded += OptionsPage_Loaded;
        }

        /// <summary>
        /// Handles the Loaded event of the page. This method loads saved keybinds
        /// for the current user and updates the UI accordingly.
        /// </summary>
        private void OptionsPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Retrieve the current user id (replace with your actual user retrieval logic)
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
        }


        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            // Save the keybinds when leaving the OptionsPage.
       

            Frame.Navigate(typeof(MenuPage));
        }

        private void backbtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn2.png"));
        }

        private void backbtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn.png"));
        }

        /// <summary>
        /// Handles when a Change button is clicked. Sets the placeholder text and enables key listening.
        /// </summary>
        private void Change_Keybind(object sender, RoutedEventArgs e)
        {
            var clickButton = (Button)sender;
            // Use the TabIndex to identify which action’s key is being changed.
            currentActionIndex = clickButton.TabIndex;
            isChangingKey = true;

            // Update the placeholder text based on the action.
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

        /// <summary>
        /// Updates the key display once a new key is pressed.
        /// </summary>
        /// <param name="tabIndex">The action identifier (from TabIndex)</param>
        /// <param name="newKey">The key that was pressed</param>
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
            // Reset the flags so that only one key press is captured.
            isChangingKey = false;
            currentActionIndex = -1;
            int currentUserId = GameManager.GameUser.UserId;
             Server.SaveKeyBinds(tabIndex,GameManager.GameUser.UserId, newKey);
        }

        /// <summary>
        /// Captures key presses. When a key is pressed while in key-change mode,
        /// update the corresponding action’s key.
        /// </summary>
        private void Window_Pressed(CoreWindow sender, KeyEventArgs e)
        {
            if (!isChangingKey || currentActionIndex == -1)
                return;

            // Get the pressed key's name (e.g., "A", "Space", etc.)
            string newKey = e.VirtualKey.ToString();

            // Update the keybind based on the current action index
            new_key(currentActionIndex, newKey);
        }

        private void VolumeSlider_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
         GamePage.volume = volumeSlider.Value / 100;
        }
    }
}
