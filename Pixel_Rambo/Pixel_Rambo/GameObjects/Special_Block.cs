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
    // מחלקת Special_Block מייצגת בלוק מיוחד במשחק,
    // שמכיל אייטם אקראי (מטבע או לב) כשהשחקן מתנגש בו מלמטה
    {
        private bool hascollided = false;
        // משתנה לבדוק אם הבלוק כבר הופעל כדי למנוע הוספה חוזרת

        private DispatcherTimer Break_delay = new DispatcherTimer();
           // בנאי — מקבל את הסצנה, שם קובץ תמונה, מיקום וגודל
        public Special_Block(Scene scene, string filename, double placeX, double placeY, double width, double height) :
          base(scene, filename, placeX, placeY)
        {
            Image.Width = width;
            Image.Height = height;
  

        }
        // בנאי — מקבל את הסצנה, שם קובץ תמונה, מיקום וגודל
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
        // פונקציה Add_Item — מופעלת כשהשחקן פוגע בבלוק מלמטה
        // מוסיפה מטבע או לב באופן אקראי במקום מוגדר מעל הבלוק

        public override void Render()
        {
            base.Render();

        }
        // פונקציה Render — כרגע מפעילה את הפונקציה הבסיסית של GameObject
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
        // פונקציה Collide — בודקת אם פגעו בבלוק כדור או Tree_Bullet
        // במקרה כזה מסירה את הכדור מהמשחק


    }

}
