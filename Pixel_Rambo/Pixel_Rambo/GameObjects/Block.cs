using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel_Rambo.GameObjects
{
    class Block : GameObject
    {
             

        public Block(Scene scene, string filename, double placeX, double placeY, double width, double height ,bool colisional) :
          base(scene, filename, placeX, placeY)
        {
            Collisional = colisional;
            Image.Width = width;
            Image.Height = height;


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

                    _scene.RemoveObject(otherObject);
                }

              
            }
        }

    }

}

