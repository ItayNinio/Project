using GameEngine.GameObjects;
using GameEngine.GameServices;
using Pixel_Rambo.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel_Rambo.GameObjects
{
    class Checkpoint : GameObject /// מחלקת Checkpoint מייצגת נקודת סיום של שלב במשחק
    {
        public bool hasCollided = false; // משתנה בוליאני שבודק אם כבר הייתה התנגשות עם הדמות כדי שלא תתרחש שוב

        public Checkpoint(Scene scene, string filename, double placeX, double placeY, double width, double height) :
         base(scene, filename, placeX, placeY)
        {
            Image.Width = width;
            Image.Height = height;


        }
        // בנאי שמאתחל את נקודת הסיום עם תמונה, מיקום וגודל
        public override void Collide(List<GameObject> collidingObjects)
        {
            foreach (var otherObject in collidingObjects)
            {
                if (otherObject is Rambo rambo &&   hasCollided == false && rambo.GetsmallRect().Right > this.Rect.Left)
                {
                    //rambo.State = Rambo.StateType.GameOver;

                    hasCollided = true;
                    // Remove the bullet from the scene
                    SetImage("Checkpoint/checkpoint.gif");
                    //GameManager.GameEvent.OnGameOver();
                    GameManager.GameEvent.OnGameWon();

                }

            }
        }
        // פעולה שמתבצעת כאשר יש התנגשות עם אובייקט אחר (כמו רמבו) – במקרה זה, היא בודקת אם הייתה התנגשות עם רמבו ואם לא, היא משנה את התמונה של נקודת הסיום ומסמנת שהייתה התנגשות
    }
}
