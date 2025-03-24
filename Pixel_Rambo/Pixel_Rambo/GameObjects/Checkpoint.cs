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
    class Checkpoint : GameObject
    {
        public bool hasCollided = false;
        public Checkpoint(Scene scene, string filename, double placeX, double placeY, double width, double height) :
         base(scene, filename, placeX, placeY)
        {
            Image.Width = width;
            Image.Height = height;


        }
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
    }
}
