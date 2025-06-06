﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using static GameEngine.GameServices.Constants;

namespace GameEngine.GameServices
{
    /*
       מחלקה מופשטת שמייצגת את מנהל המשחק.
       היא מחזיקה את הבמה (Scene), טיימר ראשי (_runTimer), חבילת אירועים (GameEvent) ומצב משחק.
       אחראית להפעיל את לולאת המשחק, לאתחל, לעצור, להמשיך ולהאזין ללחיצות מקשים.
      */
    public abstract class Manager
    {
        // מחזיק את האובייקט Scene (במה) של המשחק
        public Scene Scene {get; private set; }

      
        public static GameState Gamestate { get; set; } = GameState.Started; //מצב המשחק

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
        // טיימר סטטי שמריץ את האירוע OnRun באופן קבוע

        public void Start()
        {
            Scene.Init();
            Gamestate = GameState.Started;
        }
        //אתחול המשחק
        public void Start_timer()
        {
            Gamestate = GameState.Started;
            _runTimer.Start();

        }
        //הפעלת הטיימר
        public void Paused()
        {
            if (Gamestate != GameState.Paused) 
            {
                Gamestate = GameState.Paused;
                _runTimer.Stop();
            }
            else 
            {
                Gamestate = GameState.Started;
                _runTimer.Start();
            }

        }
        //עצירת המשחק
        public void Resume()
        {
            Gamestate = GameState.Started;
            _runTimer.Start();
        }
        //המשך המשחק
        public virtual void GameOver()
        {
            if (Gamestate != GameState.GameOver)
            {
                Gamestate = GameState.GameOver;
                Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
                Window.Current.CoreWindow.KeyUp -= CoreWindow_KeyUp;
                _runTimer.Stop();
            }
        }
        //סיום המשחק
    }
}
