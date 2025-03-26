using GameEngine.GameObjects;
using GameEngine.GameServices;
using Pixel_Rambo.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;



namespace Pixel_Rambo.GameObjects
{
    public class Coin : GameObject
    {


        private DispatcherTimer gifdelay = new DispatcherTimer();
        public bool hasCollided = false;
        public Coin(Scene scene, string filename, double placeX, double placeY, double Height) :
            base(scene, filename, placeX, placeY)
        {
            Image.Height = Height;
            gifdelay.Interval = TimeSpan.FromMilliseconds(1000); // Adjust this to control the delay
            gifdelay.Tick += RemovalDelayTimer_Tick;

        }
        public override void Collide(List<GameObject> collidingObjects)
        {
            foreach (var otherObject in collidingObjects)
            {
                if (otherObject is Rambo rambo && !((rambo.GetsmallRect().Left < this.Rect.Left && rambo.GetsmallRect().Right < this.Rect.Left) || (rambo.GetsmallRect().Left > this.Rect.Right && rambo.GetsmallRect().Right > this.Rect.Right)))
                {

                    if (hasCollided == false)
                    {
                      
                        GameManager.GameUser.Money += 1;
                        Manager.GameEvent.OnUpdateCoin();
                       

                    }
                    hasCollided = true;



                    // Set the flag to true to prevent re-triggering
                    Image.Width = 48;
                    Image.Height = 48;
                   _Y = +_Y  - 29;
                    
                    


                    SetImage("Coin/coin_collected.gif");
                    Render();

                    gifdelay.Start();

                }
            }
        }
        private void RemovalDelayTimer_Tick(object sender, object e)
        {
            // Stop the timer and remove the Heart object after the delay
            gifdelay.Stop();

            _scene.RemoveObject(this);
        }
    }
}