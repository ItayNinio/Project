
using DataBaseProject;
using Pixel_Rambo.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class SignIn : Page
    {
        public SignIn()
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

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            loginbtn.Source = new BitmapImage(new Uri("ms-appx:///Imgs/HaveAccount2.png"));
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            loginbtn.Source = new BitmapImage(new Uri("ms-appx:///Imgs/HaveAccount.png")); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignIn2));
        }

        private void playb2_Click(object sender, RoutedEventArgs e)
        {
            // Trim input for extra spaces
            string username = Username.Text.Trim();
            string password = Passwordb.Password.Trim();
            string confirmPassword = Passwordb2.Password.Trim();
            string email = Email.Text.Trim();

            // Check for empty fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email))
            {
                ShowMessage("All fields are required. Please fill in all fields.");
                return;
            }

            // Validate email
            if (!CheckValidEmail(email))
            {
                ShowMessage("Invalid email format. Please enter a valid email address.");
                return;
            }

            // Validate password
            if (!CheckValidPassword(password))
            {
                ShowMessage("Password is not strong enough. Make sure it is at least 8 characters long, contains a number, an uppercase letter, and a special character.");
                return;
            }

            // Check if passwords match
            if (password != confirmPassword)
            {
                ShowMessage("Passwords do not match. Please confirm your password.");
                return;
            }

            // Check if username already exists
            int? userId = Server.ValidateNewUser(username);
            if (userId != null)
            {
                ShowMessage("This username is already taken. Please choose another username.");
                return;
            }

            // Add user to the databas
            GameManager.GameUser = Server.AddNewUser(username, password, email);
            if (GameManager.GameUser != null)
            {
                ShowMessage("Registration successful! You can now log in.");
                Frame.Navigate(typeof(MenuPage)); // Navigate to the next page after successful registration
            }
            else
            {
                ShowMessage("Failed to register. Please try again.");
            }
        }

        private bool CheckValidEmail(string email)
        {
            // Check for a basic email format with regex
            return System.Text.RegularExpressions.Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        private bool CheckValidPassword(string password)
        {
            // Check for password strength: minimum 8 chars, 1 uppercase, 1 lowercase, 1 digit, 1 special char
            return System.Text.RegularExpressions.Regex.IsMatch(
                password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }

        private void ShowMessage(string message)
        {
            // Example showing a dialog (could also update a TextBlock or similar)
            var dialog = new Windows.UI.Popups.MessageDialog(message);
            _ = dialog.ShowAsync();
        }

    }
}
