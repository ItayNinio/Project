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
    public class Mushroom: GameMovingObject

    {
        public bool HasCollided { get; private set; } = false;

        private DispatcherTimer gifdelay = new DispatcherTimer();
        private DispatcherTimer removedelay = new DispatcherTimer();
        public enum StateType
        {
            idelLeft, idelRight, movingLeft, movingRight
        }
        public StateType State { get; set; }
        public Mushroom(Scene scene, string filename, double placeX, double placeY) :
           base(scene, filename, placeX, placeY)
        {
            Image.Height = 60;
            _dX = -4;
            removedelay.Interval = TimeSpan.FromMilliseconds(50); // Adjust this to control the delay
            removedelay.Tick += RemovalDelayTimer_Tick;

            gifdelay.Interval = TimeSpan.FromMilliseconds(1500);
            gifdelay.Tick += Change_gif_Tick;
            State = StateType.movingLeft;
        }
        public override void Collide(List<GameObject> collidingObjects)
        {
            foreach (var otherObject in collidingObjects)
            {
                if (otherObject is Bullet bullet)
                {
                    _dX = 0;
                    if (State == StateType.idelLeft || State == StateType.movingLeft)
                    {
                        SetImage("Mushroom/Mushroom_hit_left.gif");
                    }
                    else
                    {
                        SetImage("Mushroom/Mushroom_hit.gif");
                    }
                    _scene.RemoveObject(bullet);
                    _dY = -4;
                    _ddY = 1;
                    Collisional = false;

                    // Additional logic if needed for each bullet
                }
                if (otherObject is Block block && (State == StateType.movingRight || State == StateType.movingLeft))

                {

                    if (State == StateType.movingLeft)
                    {
                        _X = block.Rect.Right;
                        SetImage("Mushroom/Mushroom_idleLeft.gif");// Reset to moving image
                        State = StateType.idelLeft;
                    }
                    else if (State == StateType.movingRight)
                    {
                        _X = block.Rect.Left - Width -1;
                        SetImage("Mushroom/Mushroom_idle.gif");// Reset to moving image
                        State = StateType.idelRight;
                    }
                    _dX = 0;

                    gifdelay.Start();
                }
                if (otherObject is Rambo rambo)
                {
                    HasCollided = true;



                }

            }
        }
        public override void Render()
        {
            base.Render();
            if (_Y > _scene.ActualHeight)
            {
                removedelay.Start();
            }
            if (Rect.Right >= _scene?.ActualWidth && State == StateType.movingRight)
            {
                _dX = 0;
                SetImage("Mushroom/Mushroom_idle.gif");
                State = StateType.idelRight;
                _X = _scene.ActualWidth - (Rect.Width);
                gifdelay.Start();
            }
            if (Rect.Left <= 0 && State == StateType.movingLeft)
            {
                _dX = 0;
                SetImage("Mushroom/Mushroom_idleLeft.gif");// Reset to moving image
                State = StateType.idelLeft;
                _X = 0;
                gifdelay.Start();
            }



        }
        private void RemovalDelayTimer_Tick(object sender, object e)
        {
            // Stop the timer and remove the Heart object after the delay
            removedelay.Stop();
            _scene.RemoveObject(this);
        }
        private void Change_gif_Tick(object sender, object e)
        {
            if (State == StateType.idelRight)
            {
                State = StateType.movingLeft;
                SetImage("Mushroom/Mushroom_run_left.gif"); 
                _dX = -4;
            }
            else if (State == StateType.idelLeft)
            {
                State = StateType.movingRight;
                SetImage("Mushroom/Mushroom_run.gif");
                _dX = 4;
            }


        }
        public void OnHitFromAbove()
        {
            _dX = 0;
            SetImage("Mushroom/Mushroom_hit_left.gif");
            _dY = -4;
            _ddY = 1;
            HasCollided = true;
            Collisional = false;
        }
    }
}
