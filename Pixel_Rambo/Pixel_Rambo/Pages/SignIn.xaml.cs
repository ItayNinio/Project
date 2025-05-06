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



namespace Pixel_Rambo.Pages
{
 
    public sealed partial class SignIn : Page
    //דף התחברות
    //המשתמש יכול להתחבר עם שם משתמש וסיסמא
    //אם המשתמש לא קיים הוא יכול לשחזר את הסיסמא שלו
    //אם המשתמש לא קיים הוא יכול להירשם
    //אם המשתמש קיים הוא יכול להיכנס למשחק
    {
        public SignIn()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
          
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
                ShowMessage("Email or Password are incorrect");
            }
        }
        //הפעולה בודקת אם המשתמש קיים במערכת
        private void ShowMessage(string message)
        {
         
            var dialog = new Windows.UI.Popups.MessageDialog(message);
            _ = dialog.ShowAsync();
        }
        //הפעולה מציגה הודעה למשתמש

        private void reset_click(object sender, RoutedEventArgs e)
        {
          
            string username = ForgotUsername.Text.Trim();
            string email = ForgotEmail.Text.Trim();
            if (Server.DoesUserExist(username, email))
            {
              
                ShowMessage("User found. please create a new password.");
                ForgotPasswordGrid.Visibility = Visibility.Collapsed;
                PasswordChangeGrid.Visibility = Visibility.Visible;
          
            }
            else
            {
           
                ShowMessage("No user found with the provided username and email.");
            }
        }
        //הפעולה בודקת אם המשתמש קיים במערכת לצורך שחזור סיסמה
        private async void PasswordChangeSubmit_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = NewPasswordBox.Password.Trim();
            string confirmPassword = ConfirmPasswordBox.Password.Trim();
            string user = ForgotUsername.Text.Trim();
            string email = ForgotEmail.Text.Trim();
           

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

     
            int userId = GameManager.GameUser.UserId;

         
            Server.UpdateUserPassword(user, email, newPassword);

            ShowMessage("Password has been changed successfully!");

          
            PasswordChangeGrid.Visibility = Visibility.Collapsed;
        }
        //הפעולה משנה את הסיסמא של המשתמש

        private void popup_grid(object sender, RoutedEventArgs e)
        {
            ForgotPasswordGrid.Visibility = Visibility.Visible;
        }
        //הפעולה פותחת את חלון השחזור סיסמא

        private void Button_back(object sender, RoutedEventArgs e)
        {
            ForgotPasswordGrid.Visibility = Visibility.Collapsed;
        }
        //הפעולה סוגרת את חלון השחזור סיסמא
    }
}
