using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class GameLevel
    {
        public int LevelId { get; set; } = 0; //המספר המזהה של שלב
        public int LevelNumber { get; set; } = 1;//מספר השלב
        public int SkeletonHp { get; set; } =1;//כמות החיים של שלד
        public int ReaprHp { get; set; } = 1; //כמות החיים של ריפר
        public int GolemHp { get; set; } = 3; //כמות חיים של גולם
        public int CountSkeleton { get; set; }// מספר של שלדים
        public int Chicken_Speed { get; set; } = 8;//מהירות התנודה של התרנגול
        public int Tree_Bullet_Delay { get; set; }//מספר של ריפרים
       
        public int Tree_Lifes { get; set; }
        public int Mushroom_Speed { get;set; }//מספר הפלטפורמות
        public int Bullet_Speed { get; set; }//מספר המפלצות בחדר
    }
}
