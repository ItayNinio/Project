using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
   public class GameUser // מחלקה שמייצגת את פרטי השחקן
    {
        public int UserId { get; set; } =0; //המספר המזהה של השחקן

        public string Username { get; set; } = "Anonymous"; //שם השחקן

        public string UserEmail { get; set; } = string.Empty; //אימייל של השחקן

        public string UserPassword { get; set; } = string.Empty; //הססמא של השחקן

        public int Money { get; set; } = 0; //הכסף שאסף במשחק

        public int MaxLevel { get; set; } = 1;//השלב  המתקדם ביותר שהמשתמש הגיע אליו

        public int CurrentSkinId { get; set; } = 1; //הכוח המיוחד שיש כרגע לשחקן

        public GameLevel CurrentLevel = new GameLevel();//אם המשתמש לא יזדהה הוא ישחק בשלב בררת מחדל
    }
}
