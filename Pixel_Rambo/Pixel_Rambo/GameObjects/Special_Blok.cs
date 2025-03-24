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
    class Special_Block : GameObject
    {
       private bool hascollided = false;
        private DispatcherTimer Break_delay = new DispatcherTimer();

        public Special_Block(Scene scene, string filename, double placeX, double placeY, double width, double height) :
          base(scene, filename, placeX, placeY)
        {
            Image.Width = width;
            Image.Height = height;
  

        }
        public void Add_Item()
        {
            if (!hascollided)
            {
                hascollided = true;
                Random rnd = new Random();
                int Rnd = rnd.Next(1, 3);
                if (Rnd == 1)
                {
                    _scene.AddObject(new Coin(_scene, "Coin/coin-shine1.gif", _X + 8, _Y - 30, 30));
                }
                else
                {
                    _scene.AddObject(new Heart(_scene, "Heart/heart.png", _X + 8, _Y - 30, 30));
                }
            }
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
      

    }

}
