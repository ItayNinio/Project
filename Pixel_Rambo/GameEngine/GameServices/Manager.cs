using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static GameEngine.GameServices.Constants;

namespace GameEngine.GameServices
{
    /*
     המחלקה המופשטת, שדורשת שיהיה לה יורש   				 
     היא מחזיקה בבמה , שני טיימרים, חבילת אירועים סטטית ומצב המשחק	   
     המחלקה יוצרת שני טיימרים, יוצרת חבילת אירועים סטטית, מכילה פעולות התחלת המשחק, עצירה, המשך וסיום
     OnClock, OnRun שני הטיימרים במידה ופועלים מפעילים ללא הפסקה שני אירועים בהתאמה 
     בנוסף, במחלקה נעשית הרשמה ללחיצה ולעזיבת המקשים. אם מתרחשת עזיבה או לחיצה על המקש, מתבצעים שני אירועים בהתאמה. כל מי שנרשם לאירועים הללו מגיב אחרת
      */
    public abstract class Manager
    {
        public Scene Scene {get; private set; }

        public static GameState Gamestate { get; set; } = GameState.Loaded; //מצב המשחק

        protected static DispatcherTimer _runTimer;    //הטיימר יפעיל ללא הפסקה אירוע OnRun

        public static GameEvents GameEvent { get; } = new GameEvents();  //יצירת אירועים הנמצאים במחלקה GameEvents

        public Manager(Scene scene)
        {
            Scene = scene;
            if (_runTimer == null)    // אם הטיימר עדיין לא נוצר אז אנחנו יוצרים אותו
            {
                _runTimer = new DispatcherTimer();
                _runTimer.Interval = TimeSpan.FromMilliseconds(1);
                _runTimer.Tick += _runTimer_Tick; ;
            }
            _runTimer.Start();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown; ;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp; ;
        }
        //המפעולה מתבצעת כאשר המשתמש עוזב על מקש כלשהו
        private void CoreWindow_KeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if(GameEvent.OnKeyUp != null)            //אם יש מי שנרשם לאירוע OnKeyDown
            {
                GameEvent.OnKeyUp(args.VirtualKey);  //מפעילים את האירוע
            }
        }
        //המפעולה מתבצעת כאשר המשתמש לוחץ על מקש כלשהו
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            if (GameEvent.OnKeyDown != null)            //אם יש מי שנרשם לאירוע OnKeyDown
            {
                GameEvent.OnKeyDown(args.VirtualKey);  //מפעילים את האירוע
            }
        }
        // הפעולה פועלת ללא הפסקה
        private void _runTimer_Tick(object sender, object e)
        {
            if (GameEvent.OnRun != null)
            {
                GameEvent.OnRun();    //האירוע OnRun מופעל ללא הפסקה
            }
        }
        public void Start()
        {
            Scene.Init();
            Gamestate = GameState.Started;
        }
        public void Paused()
        {
            Gamestate = GameState.Paused;
        }
        public void Resume()
        {
            Gamestate = GameState.Started;
        }
        public virtual void GameOver()
        {
            if(Gamestate != GameState.GameOver)
            {
                Gamestate = GameState.GameOver;
                Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
                Window.Current.CoreWindow.KeyUp -= CoreWindow_KeyUp;
                _runTimer.Stop();
            }
        }
    }
}
