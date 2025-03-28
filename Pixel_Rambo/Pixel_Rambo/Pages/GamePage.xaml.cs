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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Rambo.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private GameManager _gameManager;
        private int currentCoinSound = 0;
        public static double volume { get; set; } = 0.5;


        public GamePage()
        {
            this.InitializeComponent();
            BackgroundMusic.Source = new Uri("ms-appx:///game_music.mp3");
            GunShot.Source = new Uri("ms-appx:///gunshot.mp3");
            coin_sound.Source = new Uri("ms-appx:///coin.mp3");
            coin_sound2.Source = new Uri("ms-appx:///coin.mp3");
            coin_sound3.Source = new Uri("ms-appx:///coin.mp3");
            coin_sound4.Source = new Uri("ms-appx:///coin.mp3");
            Break_sound.Source = new Uri("ms-appx:///rock_destroy.mp3");
            Heart_sound.Source = new Uri("ms-appx:///CollectHeart.mp3");

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Manager.Gamestate = GameState.Started;
            BackgroundMusic.Volume = volume;
            int currentUserId = GameManager.GameUser.UserId;
            List<string> keybinds = Server.GetKeyBinds(currentUserId);
            Keys.UpdateKeysFromDatabase(keybinds);
            scene.RemoveAllObject();
            _gameManager = new GameManager(scene);

            Manager.GameEvent.OnRemoveLife += Update;
            Manager.GameEvent.OnAddLife += Update2;
            Manager.GameEvent.OnUpdateCoin += Update3;
            Manager.GameEvent.OnShoot += ShootPlay;
            Manager.GameEvent.OnBreakBlock += BreakPlay;
            Manager.GameEvent.OnGameOver += GameOver;
            Manager.GameEvent.OnGameWon += GameWon;

            coin_holder.Text = GameManager.GameUser.Money.ToString();
            User_Name.Text = GameManager.GameUser.Username;
            if (Server.GetLives(GameManager.GameUser.UserId) == 3)
            {
                heart4.Visibility = Visibility.Collapsed;
            }



        }

        private void GameWon()
        {
            // Only increment max level if the current level is equal to the max level.
            // This implies the player is completing the highest unlocked level for the first time.
            if (GameManager.GameUser.CurrentLevel.LevelNumber == GameManager.GameUser.MaxLevel)
            {
                GameManager.GameUser.MaxLevel++;
            }

            Server.SaveData(GameManager.GameUser);
            _gameManager.Paused();
            GameWonPopup.Visibility = Visibility.Visible;
        }

        private void GameOver()
        {
          
            Server.SaveData(GameManager.GameUser);
            _gameManager.Paused();
            GameLost.Visibility = Visibility.Visible;
         
           
        }

        private void BreakPlay()
        {
            Break_sound.Play();
        }

        private void ShootPlay()
        {
            GunShot.Play();
        }

        private void Update3()
        {
            coin_holder.Text = GameManager.GameUser.Money.ToString();

            // Determine which MediaElement to use
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

            // Move to the next MediaElement
            currentCoinSound = (currentCoinSound + 1) % 4;


            // Move to the next MediaElement
          
        }

        private void Update()
        {
            if (heart4.Visibility == Visibility.Visible)
            {
                heart4.Visibility = Visibility.Collapsed;
            }
            else if (heart3.Visibility == Visibility.Visible)
            {
                heart3.Visibility = Visibility.Collapsed;
            }
            else if(heart2.Visibility == Visibility.Visible)
            {
                heart2.Visibility = Visibility.Collapsed;
            }
            else if (heart1.Visibility == Visibility.Visible)
            {
                heart1.Visibility = Visibility.Collapsed;
                
            }
        
        }
        private void Update2()
        {
           if (heart1.Visibility == Visibility.Collapsed)
            {
                heart1.Visibility = Visibility.Visible;
            }
           else if (heart2.Visibility == Visibility.Collapsed)
            {
                heart2.Visibility = Visibility.Visible;
            }
           else if (heart3.Visibility == Visibility.Collapsed)
            {
                heart3.Visibility = Visibility.Visible;
            }
            else if (heart4.Visibility == Visibility.Collapsed)
            {
                heart4.Visibility = Visibility.Visible;
            }
            Heart_sound.Play();

        }

        private void stopbtn_Click(object sender, RoutedEventArgs e)
        {
            if (PopupMenu.Visibility != Visibility)
            {
                PopupMenu.Visibility = Visibility.Visible;
                stopbtn.Visibility = Visibility.Collapsed;
            }
            else {
                PopupMenu.Visibility = Visibility.Collapsed;
                stopbtn.Visibility = Visibility.Visible;
            }

            _gameManager.Paused();

            // Call the Paused method from Manager

        }

        private void playb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tryagain_btn_Click(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(GamePage));
        }

   

        private void home_Click(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(MenuPage));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(GamePage));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(MenuPage));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _gameManager.Paused();
            Frame.Navigate(typeof(LevelPage));

        }
    }
}
