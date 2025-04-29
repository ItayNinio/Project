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
    public class Tree_Bullet : GameMovingObject // מחלקת Tree_Bullet מייצגת קליע שנורה על ידי עץ במשחק
    {
        private bool IsFacingRight { get; set; }// משתנה בוליאני שקובע את כיוון התזוזה של הקליע (ימינה או שמאלה)

        private Rectangle bullet_shape { get; set; }
        // צורת מלבן עבור הקליע (לא בשימוש פעיל כרגע)
        private DispatcherTimer removeDelayTimer = new DispatcherTimer();
        // טיימר להסרת הקליע מהסצנה לאחר זמן מסוים

        public Tree_Bullet(Scene scene, string filename, double placeX, double placeY, bool direction) :
            base(scene, filename, placeX, placeY)
        {
            Image.Width = 40;
            Image.Height = 60;

            IsFacingRight = direction;
            if (IsFacingRight)
            {

                _dX = 19;
            }
            else
            {

                _dX = -19;
            }
            removeDelayTimer.Interval = TimeSpan.FromMilliseconds(10); // Short delay to avoid collection modification issues
            removeDelayTimer.Tick += RemoveDelayTimer_Tick;

        }
        // בנאי — יוצר קליע חדש עם מיקום, כיוון, מהירות ותמונה
        public void setbool(bool flag2)
        {
            this.IsFacingRight = flag2;
        }
        // פונקציה לשינוי כיוון תזוזה של הקליע
        public bool getbool()
        {
            return IsFacingRight;
        }
        // פונקציה לקבלת כיוון תזוזה של הקליע
        private void RemoveDelayTimer_Tick(object sender, object e)
        {
            removeDelayTimer.Stop();
            _scene.RemoveObject(this);
        }
        // פונקציה שמסירה את הקליע מהסצנה לאחר זמן מסוים
        public Rectangle GetRectangle()
        {
            return bullet_shape;
        }
        // פונקציה שמחזירה את צורת המלבן של הקליע (לא בשימוש פעיל כרגע)
        public override void Render()
        {
            base.Render();
            // Check for boundary crossing
            if (Rect.Left < 0 - Image.Width || Rect.Right > _scene?.ActualWidth + Image.Width)
            {
                // Start the timer for delayed removal instead of immediate removal
                removeDelayTimer.Start();
            }
        }
        // פונקציה Render — מפעילה את הפונקציה הבסיסית של GameMovingObject
        // אם הקליע יצא מגבולות המסך — הפעלת טיימר להסרה
    }
}