using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace GameEngine.GameObjects
{
    //המחלקה לא מיועדת ליצירת עצמים, אלא היא תהווה בסיס ליצירת מחלקות היורשות ממנה
    //המחלקה תכיל את התכונות ואת כל הפעולות המשותפות לכל העצמים העתידיים שיהיו בפרוייקט
    public abstract class GameObject 
    {
        protected double _X;                        //מיקום נוכחי
        protected double _Y;                        //מיקום נוכחי

        protected double _placeX;                   //מיקום התחלתי
        protected double _placeY;                   //מיקום התחלתי

        public Image Image { get; set; }            // מראה 

       

        protected string _filename;                 // שם הקובץ של התמונה

        public double Width => Image.ActualWidth;   //קיצור דרך לכתיבה נוחה וקצרה יותר
        public double Height => Image.ActualHeight; //קיצור דרך לכתיבה נוחה וקצרה יותר

        public virtual Rect Rect => new Rect(_X, _Y, Width, Height); //המלבן שמקיף את העצם

        public bool Collisional { get; set; } = true;  //אפשר להתנגש בו, לא שקוף

        protected Scene _scene; //זירת המשחק

        public GameObject(Scene scene, string filename, double placeX,double placeY)
        {
            _scene = scene;
            _filename = filename;
            _X = placeX;
            _Y = placeY;
            _placeX = placeX;
            _placeY = placeY;
            Image = new Image();
            SetImage(filename);
            Render();
        }
        public virtual void Render()
        {
            Canvas.SetLeft(Image, _X);
            Canvas.SetTop(Image, _Y);
        }
        protected void SetImage(string filename)
        {
            Image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{filename}"));
        }

      public  virtual void Init()   //מחזיקה את האובייקט למיקומו ההתחלתי
      {
            _X = _placeX;
            _Y = _placeY;
      }

        //הפעולה תתבצע כאשר העצם הנוכחי התנגש בוודאות בעצם אחר 
        //הפעולה ריקה משום שכל דמות שהתנגשה תוכל להגיב באופן שונה ולכן הפעולה תמומש בהתאם לאופן התגובה
        public virtual void Collide(List <GameObject> gameObject)  
        {
        }
    }
}
