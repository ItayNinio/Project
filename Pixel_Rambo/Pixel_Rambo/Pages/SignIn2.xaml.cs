
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
    public sealed partial class SignIn2 : Page
    {
        public SignIn2()
        {
            this.InitializeComponent();
           
        }

        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }

        private void backbtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn.png"));
        }

        private void backbtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn2.png"));
        }

      
        private void send_btn(object sender, RoutedEventArgs e)
        {
            int? userId = Server.ValidateUser(Email.Text.Trim(), Password2.Password.Trim());
            if (userId.HasValue)
            {
                GameManager.GameUser = Server.GetUser(userId.Value);
                ShowMessage("welcome back");
                Frame.Navigate(typeof(MenuPage));
            }
            else
            {
                ShowMessage("Wring info");
            }
        }
        private void ShowMessage(string message)
        {
            // Example showing a dialog (could also update a TextBlock or similar)
            var dialog = new Windows.UI.Popups.MessageDialog(message);
            _ = dialog.ShowAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void reset_click(object sender, RoutedEventArgs e)
        {
            // Get the input values from the UI.
            // Replace these with your actual control names.
            string username = ForgotUsername.Text.Trim();
            string email = ForgotEmail.Text.Trim();
            if (Server.DoesUserExist(username, email))
            {
                // User exists – proceed with password reset logic.
                // For example, show a confirmation message or send a reset email.
                ShowMessage("User found. please create a new password.");
                ForgotPasswordGrid.Visibility = Visibility.Collapsed;
                PasswordChangeGrid.Visibility = Visibility.Visible;
                // You could also call a function like ResetPassword(username, email);
            }
            else
            {
                // User does not exist – inform the user.
                ShowMessage("No user found with the provided username and email.");
            }
        }



        private async void PasswordChangeSubmit_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordBox.Password.Trim();
            string confirmPassword = ConfirmPasswordBox.Password.Trim();
            string user = ForgotUsername.Text.Trim();
            string email = ForgotEmail.Text.Trim();
            // Validate input

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ShowMessage("Please fill in both password fields.");
                return;
            }
            if (newPassword != confirmPassword)
            {
               ShowMessage("Passwords do not match.");
                return;
            }
            if (!Server.IsStrongPassword(newPassword))
            {
                ShowMessage("Password is not strong enough.");
                return;
            }

            // Optionally: Add further password strength validations here

            // Assume you have the user ID from the user who is resetting their password.
            // This might come from a previously verified user object (e.g., GameManager.GameUser)
            int userId = GameManager.GameUser.UserId; // or obtain the id based on username/email if not logged in

            // Update the password in the database
            Server.UpdateUserPassword(user, email, newPassword);

             ShowMessage("Password has been changed successfully!");

            // Hide the password change grid
            PasswordChangeGrid.Visibility = Visibility.Collapsed;
        }

        private void popup_grid(object sender, RoutedEventArgs e)
        {
            ForgotPasswordGrid.Visibility = Visibility.Visible;
        }
    }
}
