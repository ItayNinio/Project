using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;



namespace Pixel_Rambo.GameObjects
{
    public class Heart : GameObject /// מחלקת Heart מייצגת את הלב במשחק
    {
        private DispatcherTimer gifdelay = new DispatcherTimer();     
        // טיימר למחיקת האובייקט מהמשחק לאחר אנימציית איסוף
        public bool hasCollided = false;
        // טיימר למחיקת האובייקט מהמשחק לאחר אנימציית איסוף
        public Heart(Scene scene, string filename, double placeX, double placeY, double Height) : 
            base(scene, filename, placeX, placeY)
        {
            Image.Height = Height;
            gifdelay.Interval = TimeSpan.FromMilliseconds(1000); // Adjust this to control the delay
            gifdelay.Tick += RemovalDelayTimer_Tick;
           
        }
        // בנאי: יוצר את הלב, מגדיר את גובה התמונה ומאתחל את הטיימר
        public override void Collide(List<GameObject> collidingObjects)
        {
            foreach (var otherObject in collidingObjects) {
                if (otherObject is Rambo rambo && !hasCollided)
                {

                    if (hasCollided == false)
                    {
                        Manager.GameEvent.OnAddLife();
                        rambo.lifes++;
                        
                       
                    }
                    hasCollided = true;



                    // Set the flag to true to prevent re-triggering
                    Image.Width = 60;
                    Image.Height = 60;
                    _X = _X - 15;
                    _Y = _Y - 15;

                  
                    SetImage("Heart/Shinegif.gif");
                    Render();
                    
                    gifdelay.Start();
                   
                }
                                                          }
        }
        // פעולה שמופעלת בהתנגשות עם אובייקטים אחרים
        private void RemovalDelayTimer_Tick(object sender, object e)
        {
            // Stop the timer and remove the Heart object after the delay
            gifdelay.Stop();
          
            _scene.RemoveObject(this);
        }
        // פעולה שמופעלת כשהטיימר נגמר — מסירה את הלב מהמשחק
    }
}
