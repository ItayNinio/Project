using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel_Rambo.GameObjects
{
    class Ground : GameObject /// מחלקת Ground מייצגת את האדמה במשחק
    {
        public Ground(Scene scene, string filename, double placeX, double placeY) :
          base(scene, filename, placeX, placeY)
        {
            Image.Width = 620;
            Image.Height = 44;


        }
        // בנאי שמאתחל את האדמה עם תמונה, מיקום וגובה

        public override void Render()
        {
            base.Render();


        }
        // פעולה שמציירת את האדמה על המסך
        public override void Collide(List<GameObject> collidingObjects)
        {
            foreach (var otherObject in collidingObjects)
            {
                if (otherObject is Bullet bullet)
                {
                    // Remove the bullet from the scene
                    _scene.RemoveObject(bullet);
                }

                // Add additional handling for other types of objects if needed
            }
        }
        // פעולה שמטפלת בהתנגשויות עם אובייקטים אחרים במשחק
    }

}

