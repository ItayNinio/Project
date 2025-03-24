using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameServices
{
   public static class Constants
    {
       public enum GameState  //הגדרת מצב המשחק
        {
            Loaded,
            Started,
            Paused,
            GameOver
        } 
    }
}
