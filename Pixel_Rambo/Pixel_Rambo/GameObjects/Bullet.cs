using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;

namespace Pixel_Rambo.GameObjects
{
    public class Bullet : GameMovingObject  // מחלקה שמייצגת קליע שנורה ע"י רמבו (GameMovingObject = אובייקט שיכול לזוז).
    {
        private bool IsFacingRight { get; set; } // האם הקליע פונה ימינה או שמאלה
        private Rectangle bullet_shape { get; set; }         // צורת הקליע (אם תשתמש בה מאוחר יותר)
        private DispatcherTimer removeDelayTimer = new DispatcherTimer();  // טיימר שאחראי למחוק את הקליע באיחור קל (כדי לא לגרום לבאגים כשמוחקים בזמן לולאת התנגשויות)



        public Bullet(Scene scene, string filename, double placeX, double placeY, bool direction) : 
            base(scene, filename, placeX, placeY)
        {
           Image.Width = 40;
            Image.Height = 64;

            IsFacingRight = direction;
            if (IsFacingRight)
            {
               
                _dX = 12;
            }
            else
            {
               
                _dX = -12;
            }
            removeDelayTimer.Interval = TimeSpan.FromMilliseconds(10); // Short delay to avoid collection modification issues.
            removeDelayTimer.Tick += RemoveDelayTimer_Tick;

        }// בנאי – יוצר קליע עם תמונה, מיקום וכיוון תנועה (ימינה או שמאלה)


        //public Bullet(bool flag, Rectangle bullet_shape)
        //{

        //    IsFacingRight = flag;
        //    this.bullet_shape = bullet_shape;

        //}
        public void setbool(bool flag2)
        {
            this.IsFacingRight = flag2;
        }// מאפשר לשנות את כיוון הקליע
        public bool getbool()
        {
            return IsFacingRight;
        }// מאפשר לשנות את כיוון הקליע
        private void RemoveDelayTimer_Tick(object sender, object e)
        {
            removeDelayTimer.Stop();
            _scene.RemoveObject(this);
        }  // פעולה שנקראת כשהטיימר נגמר – מוחקת את הקליע מהמשחק
        public Rectangle GetRectangle()
        {
            return bullet_shape;
        } //מחזיר את הצורה של הקליע (לא בשימוש כרגע בקוד שלי)
        public override void Render()
        {
            base.Render();
            // Check for boundary crossing
            if (Rect.Left < 0 - Image.Width || Rect.Right > _scene?.ActualWidth + Image.Width)
            {
                // Start the timer for delayed removal instead of immediate removal
                removeDelayTimer.Start();
            }
        }        // כל פעם שהקליע מוצג על המסך – בודק אם יצא מגבולות המסך


    }
}
