using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace GameEngine.GameServices
{
   public class GameEvents
   {
        public Action OnRun;                          // אירוע להפעלת פעולת Run עבור כל האובייקטים
        public Action<VirtualKey> OnKeyDown;           // אירוע ללחיצה על מקש
        public Action<VirtualKey> OnKeyUp;             // אירוע לשחרור מקש
        public Action OnRemoveLife;                    // אירוע להסרת לב מהשחקן
        public Action OnAddLife;                       // אירוע להוספת לב לשחקן
        public Action<int> OnGetLife;                  // אירוע בעת איסוף לב מהמשחק
        public Action<int> OnGetCoin;                  // אירוע בעת איסוף מטבע מהמשחק
        public Action<int> OnScoreRefresh;             // אירוע לעדכון ניקוד השחקן במסך
        public Action<int> OnUpdateTime;               // אירוע לעדכון שעון הזמן של המשחק
        public Action OnGameOver;                      // אירוע לסיום משחק
        public Action<double, double> OnMousePress;    // אירוע של לחיצה על העכבר עם מיקום הלחיצה
        public Action OnUpdateCoin;                    // אירוע לעדכון כמות המטבעות של השחקן
        public Action OnShoot;                         // אירוע שמתרחש כאשר השחקן יורה
        public Action OnBreakBlock;                    // אירוע של שבירת בלוק
        public Action OnGameWon;                       // אירוע של ניצחון במשחק
    }
}
