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
        private int _break = 2;
        private DispatcherTimer Break_delay = new DispatcherTimer();

        public Break_Block(Scene scene, string filename, double placeX, double placeY, double width, double height) :
          base(scene, filename, placeX, placeY)
        {
            Image.Width = width;
            Image.Height = height;
            Break_delay.Interval = TimeSpan.FromMilliseconds(500); // Adjust this to control the delay
            Break_delay.Tick += Break_delay_Tick;
        }

        private void Break_delay_Tick(object sender, object e)
        {
            _scene.RemoveObject(this);
        }

        public override void Render()
        {
            base.Render();


        }
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
        }
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
        }

    }

}
