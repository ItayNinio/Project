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


namespace Pixel_Rambo.Pages
{
 
    public sealed partial class SignUp : Page
    //דף הרשמה
    //המשתמש יכול להירשם עם שם משתמש וסיסמא
    //אם המשתמש לא קיים הוא יכול להירשם
    //אם המשתמש קיים הוא לא יכול להירשם
    //אם המשתמש נרשם הוא יכול להיכנס למשחק
    {
        public SignUp()
        {
            this.InitializeComponent();
        }
        //הפעולה מופעלת כאשר הדף נטען
        private void backbtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
        }
        //חזרה לדף התפריט

        private void backbtn_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור
        private void backbtn_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            backimg.Source = new BitmapImage(new Uri("ms-appx:///Imgs/backbtn2.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            loginbtn.Source = new BitmapImage(new Uri("ms-appx:///Imgs/HaveAccount2.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר נכנס לתחום הכפתור

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            loginbtn.Source = new BitmapImage(new Uri("ms-appx:///Imgs/HaveAccount.png"));
        }
        //החלפת התמונה של הכפתור כאשר העכבר יוצא מתחום הכפתור

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignIn));
        }
        //מעבר לדף התחברות

        private void playb2_Click(object sender, RoutedEventArgs e)
        {
           
            string username = Username.Text.Trim();
            string password = Passwordb.Password.Trim();
            string confirmPassword = Passwordb2.Password.Trim();
            string email = Email.Text.Trim();

           
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email))
            {
                ShowMessage("All fields are required. Please fill in all fields.");
                return;
            }

        
            if (!CheckValidEmail(email))
            {
                ShowMessage("Invalid email format. Please enter a valid email address.");
                return;
            }

          
            if (!CheckValidPassword(password))
            {
                ShowMessage("Password is not strong enough. Make sure it is at least 8 characters long, contains a number, an uppercase letter, and a special character.");
                return;
            }

            if (password != confirmPassword)
            {
                ShowMessage("Passwords do not match. Please confirm your password.");
                return;
            }

           
            int? userId = Server.ValidateNewUser(username);
            if (userId != null)
            {
                ShowMessage("This username is already taken. Please choose another username.");
                return;
            }

           
            GameManager.GameUser = Server.AddNewUser(username, password, email);
            if (GameManager.GameUser != null)
            {
                ShowMessage("Registration successful! You can now log in.");
                Frame.Navigate(typeof(MenuPage)); 
            }
            else
            {
                ShowMessage("Failed to register. Please try again.");
            }
        }
        //לחיצה על כפתור ההרשמה ובדיקת תקינות השדות

        private bool CheckValidEmail(string email)
        {
       
            return System.Text.RegularExpressions.Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
        //בודק אם האימייל תקין

        private bool CheckValidPassword(string password)
        {
            
            return System.Text.RegularExpressions.Regex.IsMatch(
                password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        }
        //בודק אם הסיסמא תקינה

        private void ShowMessage(string message)
        {      
            var dialog = new Windows.UI.Popups.MessageDialog(message);
            _ = dialog.ShowAsync();
        }
        //הפעולה מציגה הודעה למשתמש
    }
}
