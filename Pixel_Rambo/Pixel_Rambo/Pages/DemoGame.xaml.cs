using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixel_Rambo.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DemoGame : Page

    {
        private double objectSpeed = 5.0; // Adjust the speed as needed
        private double jumpSpeed = 10.0; // Adjust the jump speed as needed
        private double gravity = 0.4; // Adjust the gravity as needed
        private double bulletSpeed = 15.0; // Adjust the bullet speed as needed
        private bool movingUp, movingDown, movingLeft, movingRight, isJumping, isFacingLeft = false, isFacingRight, isShootingLeft, isShootingRight;
        private double verticalSpeed = 2.0;
        private BitmapImage defaultImage;
        private BitmapImage leftImage;
        private BitmapImage idleLeft;
        private BitmapImage idle;
        private BitmapImage shoot;
        private BitmapImage shootLeft;
        private Rectangle bullet;
        private List<Bullet> bullets_list = new List<Bullet>();
        private DispatcherTimer timer;
        private DispatcherTimer shootTimer;
        private bool canShoot = true; // Initially, shooting is allowed
        private bool shotfired = false;




        public DemoGame()
        {
          //  GunShot.Source = new Uri("ms-appx:///gunshot.mp3");
            this.InitializeComponent();
            BackgroundMusic.Source = new Uri("ms-appx:///Assets/Backgrounds/game_music.mp3");
            GunShot.Source = new Uri("ms-appx:///gunshot.mp3");
            BackgroundMusic.Play();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;

            defaultImage = new BitmapImage(new System.Uri("ms-appx:///Assets/Rambo/goodgif.gif"));
            leftImage = new BitmapImage(new System.Uri("ms-appx:///Assets/Rambo/goodgifleft.gif"));
            idleLeft = new BitmapImage(new System.Uri("ms-appx:///Assets/Rambo/idelegifleft.gif"));
            idle = new BitmapImage(new System.Uri("ms-appx:///Assets/Rambo/idelegif.gif"));
            shoot = new BitmapImage(new System.Uri("ms-appx:///Assets/Rambo/shootinggif.gif"));
            shootLeft = new BitmapImage(new System.Uri("ms-appx:///Assets/Rambo/shootinggifleft.gif"));

            YourObject.Source = idle;

            timer = new DispatcherTimer();
            timer.Tick += GameLoop;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 16); // Adjust the interval as needed
            timer.Start();


            shootTimer = new DispatcherTimer();
            shootTimer.Interval = TimeSpan.FromMilliseconds(300); // Set the shooting delay to 500 milliseconds (half a second)
            shootTimer.Tick += ShootTimer_Tick;

        }
        private void ShootTimer_Tick(object sender, object e)
        {
            canShoot = true;
            shootTimer.Stop();
        }


        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.W)
                movingUp = true;
            if (args.VirtualKey == Windows.System.VirtualKey.S)
                movingDown = true;
            if (args.VirtualKey == Windows.System.VirtualKey.A)
            {

                if (!movingLeft)
                {
                    // Change the image to the left-facing one
                    YourObject.Source = leftImage;
                    if (!isJumping)
                    {
                        Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) - 9);
                    }
                    movingLeft = true;
                    isFacingLeft = true;
                    isFacingRight = false;
                }
            }
            if (args.VirtualKey == Windows.System.VirtualKey.D)
            {

                // Change the image to the right-facing one
                if (!movingRight)
                {

                    // Change the image to the right-facing one
                    if (YourObject.Source != shoot && YourObject.Source != shootLeft)
                    {
                        movingRight = true;
                        YourObject.Source = defaultImage;
                        if (!isJumping)
                        {
                            Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) - 9);
                        }
                    }
                    else
                    {
                        YourObject.Source = shoot;
                    }



                    isFacingRight = true;
                    isFacingLeft = false;



                }
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Space && !isJumping)
            {
                isJumping = true;
                verticalSpeed = -jumpSpeed;
            }

            if (args.VirtualKey == Windows.System.VirtualKey.Z && canShoot)
            {


                shootTimer.Start();
                canShoot = false;
                shotfired = true;
                GunShot.Play();

                if (isFacingRight)
                {

                    YourObject.Source = shoot;
                    isShootingRight = true;
                    isShootingLeft = false;
                    if (movingRight)
                    {
                        Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) + 9);
                    }

                }
                else
                {

                    YourObject.Source = shootLeft;
                    if (movingLeft)
                    {
                        Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) + 9);
                    }

                    if (!isShootingLeft)
                    {

                        Canvas.SetLeft(YourObject, Canvas.GetLeft(YourObject) - 102);
                        isShootingLeft = true;
                        isShootingRight = false;
                    }


                }
                movingLeft = false;
                movingRight = false;
               
                // Update the existing bullet instance
                Rectangle rectangle = new Rectangle();
                rectangle.Width = 20;
                rectangle.Height = 14;
             //   ImageBrush imageBrush = new ImageBrush();
            //    imageBrush.ImageSource = new BitmapImage(new Uri("ms-appx:///Imgs/special_bullet.png")); // Change path as needed

                // Set the ImageBrush as the fill
            //    rectangle.Fill = imageBrush;
                  rectangle.Fill = new SolidColorBrush(Colors.Yellow);
                bool bullet_diraction = isFacingRight;
                Bullet f = new Bullet(bullet_diraction, rectangle);

                bullets_list.Add(f);
                // Set the position relative to the player
                if (f.getbool() == true)
                {
                    
                    Canvas.SetLeft(rectangle, Canvas.GetLeft(YourObject) + 141);
                    Canvas.SetTop(rectangle, Canvas.GetTop(YourObject) + YourObject.ActualHeight - 60);
                }
                else
                {
                    Canvas.SetLeft(rectangle, Canvas.GetLeft(YourObject) + 78);
                    Canvas.SetTop(rectangle, Canvas.GetTop(YourObject) + YourObject.ActualHeight - 60);
                }


                // Add the bullet to the canvas
                GameCanvas.Children.Add(f.GetRectangle());
                Canvas.SetZIndex(f.GetRectangle(), 0);  // Bullet behind
                Canvas.SetZIndex(YourObject, 1);        // Character in front
            }



        }


        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.W)
                movingUp = false;
            if (args.VirtualKey == Windows.System.VirtualKey.S)
                movingDown = false;
            if (args.VirtualKey == Windows.System.VirtualKey.A && !isShootingLeft && !isShootingRight)
            {
                if (YourObject.Source != idleLeft)
                {
                    Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) + 9);
                }
                movingLeft = false;
                if (!movingRight)
                {
                    YourObject.Source = idleLeft;
                }
                else if (movingRight)
                {
                    YourObject.Source = defaultImage;
                }

            }
            if (args.VirtualKey == Windows.System.VirtualKey.D)
            {
                if (YourObject.Source != idle)
                {
                    Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) + 9);
                }
                movingRight = false;
                if (!movingLeft)
                {
                    YourObject.Source = idle;
                }
                else if (movingLeft)
                {
                    YourObject.Source = leftImage;
                }

            }
            if (args.VirtualKey == Windows.System.VirtualKey.Z)
            {
                if (isFacingRight)
                {
                    YourObject.Source = idle;
                }
                else if (shotfired)
                {
                    YourObject.Source = idleLeft;
                    Canvas.SetLeft(YourObject, Canvas.GetLeft(YourObject) + 102);
                    shotfired = false;
                }
                isShootingLeft = false;
                isShootingRight = false;
            }

        }

        private void GameLoop(object sender, object e)
        {

            if (movingUp)
                Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) - objectSpeed);
            if (movingDown)
                Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) + objectSpeed);
            if (movingLeft)
            {

                Canvas.SetLeft(YourObject, Canvas.GetLeft(YourObject) - objectSpeed);
            }
            if (movingRight)
            {

                Canvas.SetLeft(YourObject, Canvas.GetLeft(YourObject) + objectSpeed);

            }




            if (isJumping)
            {

                Canvas.SetTop(YourObject, Canvas.GetTop(YourObject) + verticalSpeed);
                verticalSpeed += gravity;
                if (Canvas.GetTop(YourObject) + YourObject.ActualHeight >= GameCanvas.ActualHeight)
                {
                    Canvas.SetTop(YourObject, GameCanvas.ActualHeight - YourObject.ActualHeight);
                    isJumping = false;
                }

            }


            MoveBullet();


           


        }


        private void MoveBullet()
        {
            foreach (var bullet in bullets_list)
            {
                if (bullet != null)
                {
                    Rectangle rectangle = bullet.GetRectangle();

                    if (bullet.getbool())
                    {
                        Canvas.SetLeft(rectangle, Canvas.GetLeft(rectangle) + bulletSpeed);
                    }
                    else
                    {
                        Canvas.SetLeft(rectangle, Canvas.GetLeft(rectangle) - bulletSpeed);
                    }

                    if (Canvas.GetLeft(rectangle) < 0 || Canvas.GetLeft(rectangle) > GameCanvas.ActualWidth)
                    {
                        GameCanvas.Children.Remove(rectangle);
                        bullets_list.Remove(bullet);
                        break;
                    }
                }
            }
        }

        


        
        public class Bullet
        {
            private bool IsFacingRight;
            private Rectangle bullet_shape;
            public Bullet(bool flag, Rectangle bullet_shape)
            {

                IsFacingRight = flag;
                this.bullet_shape = bullet_shape;

            }
            public void setbool(bool flag2)
            {
                this.IsFacingRight = flag2;
            }
            public bool getbool()
            {
                return IsFacingRight;
            }
            public Rectangle GetRectangle()
            {
                return bullet_shape;
            }


        }

    }
}
