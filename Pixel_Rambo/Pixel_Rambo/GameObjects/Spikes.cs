using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel_Rambo.GameObjects
{
    class Spikes : GameMovingObject
    {
        public Spikes(Scene scene, string filename, double placeX, double placeY) :
          base(scene, filename, placeX, placeY)
        {
            Image.Height = 20;
            Image.Width = 40;
            
          
        }
     
    }
}
