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
    class Break_Block : GameObject
    {
        private int _break = 2; // מונה לפגיעות - הבלוק נשבר אחרי שתי פגיעות
        private DispatcherTimer Break_delay = new DispatcherTimer(); // טיימר למחיקת הבלוק מהמסך

        public Break_Block(Scene scene, string filename, double placeX, double placeY, double width, double height) :
          base(scene, filename, placeX, placeY)
        {
            Image.Width = width;
            Image.Height = height;
            Break_delay.Interval = TimeSpan.FromMilliseconds(500); // Adjust this to control the delay
            Break_delay.Tick += Break_delay_Tick;
        }// בנאי: יוצר בלוק עם תמונה, מיקום, גובה ורוחב

        private void Break_delay_Tick(object sender, object e)
        {
            _scene.RemoveObject(this);
        } // כאשר הזמן עובר – מסיר את הבלוק מהסצנה

        public override void Render()
        {
            base.Render();


        }//ציור הבלוק על המסך
        public override void Collide(List<GameObject> collidingObjects)
        {
            foreach (var otherObject in collidingObjects)
            {
                if (otherObject is Bullet bullet || otherObject is Tree_Bullet tree_Bullet)
                {
                    // Remove the bullet from the scene
                    _scene.RemoveObject(otherObject);
                }
   
            }
        }// מה קורה כאשר בלוקים מתנגשים עם השחקן 
        public void start_break()
        {
           if (_break == 2)
            {
                _break--;
                SetImage("Block/4break_block(2).png");
            }
           else
            {
                SetImage("Block/4break_block.png");
                Break_delay.Start();
            }
        }  // התחלת תהליך השבירה

    }

}
