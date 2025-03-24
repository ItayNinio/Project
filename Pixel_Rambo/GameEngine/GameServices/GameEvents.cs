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
        public Action OnRun;                            //האירוע שבזכותו העצמים נעים ינועו
        public Action<VirtualKey> OnKeyDown;            //אירוע שבזכותו העצמים יוכלו להקשיב ללחיצת המקשים
        public Action<VirtualKey> OnKeyUp;              //האירוע שבזכותו העצמים יוכלו להקשיב לעזיבת המקש
        public Action OnRemoveLife ;               //האירוע שבאמצעותו נוכל למחוק לב מדף המשחק
        public Action OnAddLife;
        public Action<int> OnGetLife;                   //אירוע שקורה כאשר נוגעים בלב על הרצפה
        public Action<int> OnGetCoin;                   //אירוע הקורה כאשר נוגעים במטבע
        public Action <int> OnScoreRefresh;                   //באמצעות  האירוע נוכל להציג את ההישג המעודכן
        public Action<int> OnUpdateTime;                //האירוע שמאפשר להציג על המסך את משך הזמן הנותר של המשחק
        public Action OnGameOver;                       //האירוע שתרחש כאשר יסתיים המשחק
        public Action<double, double> OnMousePress;     //האירוע שיתרחש כאשר ילחץ הלחצן של עכבר
        public Action OnUpdateCoin;                    //האירוע שבאמצעותו נוכל לעדכן את כמות המטבעות במשחק
        public Action OnShoot;                    //האירוע שבאמצעותו נוכל לעדכן את כמות הלבבות במשחק
             public Action OnBreakBlock; 
             public Action OnGameWon; 
    }
}
