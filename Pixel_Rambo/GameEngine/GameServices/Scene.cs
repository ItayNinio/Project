using GameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;




namespace GameEngine.GameServices
{
    public abstract class Scene : Canvas   /*
     מחלקה מופשטת המייצגת סצנה במשחק (Canvas).
     מנהלת את כל האובייקטים במשחק, אחראית לרנדר אותם בכל ריצה, לבדוק התנגשויות,
     להוסיף ולהסיר אובייקטים מהמסך ומרשימת המשחק.
     משמשת כבסיס לכל סצנה שנבנה במשחק.
    */
    {
        private List<GameObject> _gameObjects = new List<GameObject>();
        // רשימה של כל האובייקטים שבסצנה
        private List<GameObject> _gameObjectsSnapshot => _gameObjects.ToList();
        // קיצור לרשימת אובייקטים (עותק) כדי לאפשר לולאות בטוחות בזמן שינוי הרשימה

        public double Ground { get; set; } //רצפה

        public Scene()
        {
            Manager.GameEvent.OnRun += Run;
            Manager.GameEvent.OnRun += CheckCollisional;
            //Manager.GameEvent.OnRun += CheckVerticalAlignment;
        }
        //הבנאי של הסצנה, מחבר את האירועים של לולאת המשחק לאירועים המתאימים
        public void Run()
        {


            foreach (var gameObject in _gameObjects)
            {
                if (gameObject is GameMovingObject obj)
                {
                    obj.Render();
                }

            }

        }
        // מריץ את פעולת Render על כל האובייקטים שמסוג GameMovingObject

        public void CheckCollisional()
        {
            foreach (var gameObject in _gameObjectsSnapshot)
            {


                if (gameObject.Collisional)
                {

                    // Collect all objects that collide with the current game object
                    var collidingObjects = _gameObjectsSnapshot
                        .Where(g =>
                            !ReferenceEquals(g, gameObject) && // Exclude itself
                            g.Collisional &&                  // Ensure the object is collisional
                            !RectHelper.Intersect(g.Rect, gameObject.Rect).IsEmpty // Check intersection
                        ).ToList();


                    if (collidingObjects.Any()) // If there are any colliding objects
                    {
                        gameObject.Collide(collidingObjects);
                    }
                }

            }
        }
        // בודקת התנגשויות בין האובייקטים ברשימה

        public List<GameObject> getobjlist()
        {
            return _gameObjects;
        }
        // מחזירה את רשימת האובייקטים בסצנה
        public void AddObject(GameObject gameObject)//הפעולה מסויפה אובייקט אל המאגר ולמסך
        {
            _gameObjects.Add(gameObject); //האובייקט מתווסף לרשימה 

            Children.Add(gameObject.Image);   //תמונת האובייקט מתווספת למסך                                               
        }


        public void RemoveObject(GameObject gameObject)//הפעולה מוחקת אובייקט
        {
            if (_gameObjects.Contains(gameObject)) //הא םהאובייקט המבוקש נמצא ברשימה
            {
                _gameObjects.Remove(gameObject); //מחיקתו מהמאגר
                Children.Remove(gameObject.Image); //מחיקת המראה של האובייקט מהמסך
            }
        }

        public void RemoveAllObject()//הפעולה מוחקת את כל האובייקטים
        {
            foreach (GameObject gameObject in _gameObjects)
            {
                RemoveObject(gameObject);
            }
        }
        public void Init() //פעולה מחזירה את כל האובייקטים למיקום התחלתי
        {
            foreach (GameObject obj in _gameObjects)
            {
                obj.Init();
            }
        }

    }
}