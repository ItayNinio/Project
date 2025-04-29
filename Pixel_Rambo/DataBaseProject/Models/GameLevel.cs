using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class GameLevel // מחלקה שמייצגת את פרטי שלב במשחק
    {
        public int LevelId { get; set; } = 0; // מזהה ייחודי לשלב בבסיס הנתונים
        public int LevelNumber { get; set; } = 1; // מספר השלב במשחק (1, 2, 3...)

        public int Chicken_Speed { get; set; } = 8; // מהירות התנועה של התרנגולים בשלב
        public int Tree_Bullet_Delay { get; set; } // השהייה בין יריות של העצים (במילישניות)

        public int Tree_Lifes { get; set; } // כמות החיים של כל עץ בשלב
        public int Mushroom_Speed { get; set; } // מהירות התנועה של הפטריות בשלב
        public int Bullet_Speed { get; set; } // מהירות של הקליעים בשלב
    }
}
