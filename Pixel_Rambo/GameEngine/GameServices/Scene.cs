using GameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;




namespace GameEngine.GameServices
{
    public abstract class Scene : Canvas
    {
        private List<GameObject> _gameObjects = new List<GameObject>();
        private List<GameObject> _gameObjectsSnapshot => _gameObjects.ToList();
        public double Ground { get; set; } //רצפה

        public Scene()
        {
            Manager.GameEvent.OnRun += Run;
            Manager.GameEvent.OnRun += CheckCollisional;
            //Manager.GameEvent.OnRun += CheckVerticalAlignment;
        }
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
        //public void check_object()
        //{
        //    foreach (var gameObject in _gameObjects)
        //    {
        //        if (gameObject is Rambo obj)
        //        {
        //            return obj;
        //        }

        //    }
        //}
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



        //public void CheckVerticalAlignment()
        //{
        //    foreach (var gameObject in _gameObjectsSnapshot)
        //    {
        //        if (gameObject.Collisional)
        //        {
        //            var otherObject = _gameObjectsSnapshot.FirstOrDefault(g =>
        //                                               !ReferenceEquals(g, gameObject) &&

        //                                                 Math.Abs(gameObject.Rect.Left - g.Rect.Left) < g.Width);
        //            if (otherObject != null)
        //            {
        //                gameObject.Vertical_Collide(gameObject);
        //            }
        //        }
        //    }
        //}


        public List<GameObject> getobjlist()
        {
            return _gameObjects;
        }
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