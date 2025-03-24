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
    public class Bullet : GameMovingObject
    {
        private bool IsFacingRight { get; set; }
        private Rectangle bullet_shape { get; set; }
        private DispatcherTimer removeDelayTimer = new DispatcherTimer();



        public Bullet(Scene scene, string filename, double placeX, double placeY, bool direction) : 
            base(scene, filename, placeX, placeY)
        {
           Image.Width = 40;
            Image.Height = 64;

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

        //public Bullet(bool flag, Rectangle bullet_shape)
        //{

        //    IsFacingRight = flag;
        //    this.bullet_shape = bullet_shape;

        //}
        public void setbool(bool flag2)
        {
            this.IsFacingRight = flag2;
        }
        public bool getbool()
        {
            return IsFacingRight;
        }
        private void RemoveDelayTimer_Tick(object sender, object e)
        {
            removeDelayTimer.Stop();
            _scene.RemoveObject(this);
        }
        public Rectangle GetRectangle()
        {
            return bullet_shape;
        }
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

    }
}
