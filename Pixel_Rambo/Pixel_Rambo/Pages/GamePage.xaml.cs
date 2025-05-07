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
using Windows.UI.Xaml.Navigation;
using GameEngine.GameServices;
using DataBaseProject;
using static GameEngine.GameServices.Constants;
using Pixel_Rambo.GameObjects;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Rambo.Pages
{
  
    public sealed partial class GamePage : Page
    /// דף משחק ראשי בו מתנהל המשחק בפועל
    {
        // מנהל המשחק
        private GameManager _gameManager;

        // עוקב אחר איזה צליל מטבע לנגן
        private int currentCoinSound = 0;

        // עוצמת שמע כללית
      

        // בנאי הדף – טוען את מקורות הסאונדים
        public GamePage()
        {
            this.InitializeComponent();
          
            GunShot.Source = new Uri("ms-appx:///gunshot.mp3");
            coin_sound.Source = new Uri("ms-appx:///coin.mp3");
            coin_sound2.Source = new Uri("ms-appx:///coin.mp3");
            coin_sound3.Source = new Uri("ms-appx:///coin.mp3");
            coin_sound4.Source = new Uri("ms-appx:///coin.mp3");
            Break_sound.Source = new Uri("ms-appx:///rock_destroy.mp3");
            Heart_sound.Source = new Uri("ms-appx:///CollectHeart.mp3");
           
        }

        // מופעל כאשר הדף נטען – אתחול המשחק
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GunShot.Volume = (double)GameEngine.GameServices.Music.Efects_Volume / 100;
            coin_sound.Volume = (double)GameEngine.GameServices.Music.Efects_Volume / 100;
            coin_sound2.Volume = (double)GameEngine.GameServices.Music.Efects_Volume / 100;
            coin_sound3.Volume = (double)GameEngine.GameServices.Music.Efects_Volume / 100;
            coin_sound4.Volume = (double)GameEngine.GameServices.Music.Efects_Volume / 100;
            Break_sound.Volume = (double)GameEngine.GameServices.Music.Efects_Volume / 100;
            Heart_sound.Volume = (double)GameEngine.GameServices.Music.Efects_Volume / 100;
            // קביעת עוצמת השמע של הצלילים
            Manager.Gamestate = GameState.Started;
            // טוען את המוזיקה ברקע

            // קביעת עוצמת המוזיקה
            GameEngine.GameServices.Music.Pause();
            GameEngine.GameServices.Music.Play("game_music.mp3");

            // טוען את מקשי השליטה מהמסד
            int currentUserId = GameManager.GameUser.UserId;
            List<string> keybinds = Server.GetKeyBinds(currentUserId);
            // אם לא נמצאו מקשים במסד, קובע את ברירת המחדל
            Keys.UpdateKeysFromDatabase(keybinds);
            // פעולה שמעדכנת את מקשי השחקן לפי נתונים ממסד הנתונים

            // ניקוי הסצנה מאובייקטים קודמים
            scene.RemoveAllObject();

            // אתחול מנהל המשחק
            _gameManager = new GameManager(scene);

            // רישום מאזינים לאירועי משחק
            Manager.GameEvent.OnRemoveLife += Update;
            Manager.GameEvent.OnAddLife += Update2;
            Manager.GameEvent.OnUpdateCoin += Update3;
            Manager.GameEvent.OnShoot += ShootPlay;
            Manager.GameEvent.OnBreakBlock += BreakPlay;
            Manager.GameEvent.OnGameOver += GameOver;
            Manager.GameEvent.OnGameWon += GameWon;

            // עדכון תצוגת שם שחקן וכסף
            coin_holder.Text = GameManager.GameUser.Money.ToString();
            User_Name.Text = GameManager.GameUser.Username;

            // הסתרת לב אם החיים מלאים
            if (Server.GetLives(GameManager.GameUser.UserId) == 3)
            {
                heart4.Visibility = Visibility.Collapsed;
            }

            // התחלת טיימר המשחק
            _gameManager.Start_timer();
        }

        // הפונקציה שמופעלת כשמנצחים
        private void GameWon()
        {
            if (GameManager.GameUser.CurrentLevel.LevelNumber == GameManager.GameUser.MaxLevel)
            {
                GameManager.GameUser.MaxLevel++;
            }

            Server.SaveData(GameManager.GameUser);
            _gameManager.GameOver();
            GameWonPopup.Visibility = Visibility.Visible;
        }

        // הפונקציה שמופעלת כשמפסידים
        private void GameOver()
        {
            Server.SaveData(GameManager.GameUser);
            _gameManager.Paused();
            GameLost.Visibility = Visibility.Visible;
        }

        // ניגון קול של שבירת בלוק
        private void BreakPlay()
        {
            Break_sound.Play();
        }

        // ניגון קול של ירייה
        private void ShootPlay()
        {
            GunShot.Play();
        }

        // עדכון תצוגת מטבעות וניגון צליל מטבע
        private void Update3()
        {
            coin_holder.Text = GameManager.GameUser.Money.ToString();

            switch (currentCoinSound)
            {
                case 0:
                    coin_sound.Play();
                    break;
                case 1:
                    coin_sound2.Play();
                    break;
                case 2:
                    coin_sound3.Play();
                    break;
                case 3:
                    coin_sound4.Play();
                    break;
            }

            currentCoinSound = (currentCoinSound + 1) % 4;
        }

        // עדכון הורדת לב מהמסך
        private void Update()
        {
            if (heart4.Visibility == Visibility.Visible)
                heart4.Visibility = Visibility.Collapsed;
            else if (heart3.Visibility == Visibility.Visible)
                heart3.Visibility = Visibility.Collapsed;
            else if (heart2.Visibility == Visibility.Visible)
                heart2.Visibility = Visibility.Collapsed;
            else if (heart1.Visibility == Visibility.Visible)
                heart1.Visibility = Visibility.Collapsed;
        }

        // עדכון הוספת לב למסך וניגון צליל לב
        private void Update2()
        {
            if (heart1.Visibility == Visibility.Collapsed)
                heart1.Visibility = Visibility.Visible;
            else if (heart2.Visibility == Visibility.Collapsed)
                heart2.Visibility = Visibility.Visible;
            else if (heart3.Visibility == Visibility.Collapsed)
                heart3.Visibility = Visibility.Visible;
            else if (heart4.Visibility == Visibility.Collapsed)
                heart4.Visibility = Visibility.Visible;

            Heart_sound.Play();
        }

        // פתיחה/סגירה של תפריט עצירה
        private void stopbtn_Click(object sender, RoutedEventArgs e)
        {
            if (PopupMenu.Visibility != Visibility)
            {
                PopupMenu.Visibility = Visibility.Visible;
                stopbtn.Visibility = Visibility.Collapsed;
                GameEngine.GameServices.Music.Volume = GameEngine.GameServices.Music.Volume * 50 / 100;
            }
            else
            {
                PopupMenu.Visibility = Visibility.Collapsed;
                stopbtn.Visibility = Visibility.Visible;
                GameEngine.GameServices.Music.Volume = (int)(GameEngine.GameServices.Music.Volume * 2);
            }

            _gameManager.Paused();
          
        }

        // כפתור נסה שוב – טוען מחדש את העמוד
        private void tryagain_btn_Click(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(GamePage));
        }

        // כפתור חזרה לתפריט הראשי
        private void home_Click(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(MenuPage));
        }

        // כפתור נוסף שמתחיל את המשחק מחדש
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(GamePage));
        }

        // כפתור נוסף שחוזר לתפריט הראשי
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(MenuPage));
        }

        // כפתור מעבר לעמוד השלבים
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(LevelPage));
        }
    }
}