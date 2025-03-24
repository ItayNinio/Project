using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameObjects
{
    public abstract class GameMovingObject : GameObject
    {
        protected double _dX;  //מהירות
        protected double _dY;
        protected double _ddX; //תאוצה
        protected double _ddY;
        protected double _toX; //מיקום היעד
        protected double _toY;

        protected GameMovingObject(Scene scene, string fileName, double placeX, double placeY) :
           base(scene, fileName, placeX, placeY)
        {
        }

        public override void Render() //מתבצעת כל הזמן
        {
            _dX += _ddX; //שינוי מהירות
            _dY += _ddY;

            _X += _dX;   //שינוי מיקום
            _Y += _dY;

            if (Math.Abs(_X - _toX) < 4 && Math.Abs(_Y - _toY) < 4) //אם האובייקט הגיע ליעד(גם בערך זה סבבה)
            {
                Stop();  //עצירה
                _X = _toX; //הזזה קטנה לדיוק העצירה
                _Y = _toY;
            }
            base.Render();// קראיה לפעולה מהמחלקה שירשנו ממנה
        }

        public virtual void Stop()
        {
            _dX = _dY = _ddX =_ddY= 0;
        }

        public void MoveTo(double toX, double toY, double speed = 1, double acceleration = 0)
        {
            _toX = toX;
            _toY = toY;

            var len = Math.Sqrt(Math.Pow(_toX - _X, 2) + Math.Pow(_toY - _Y, 2));
            var cos = (_toX - _X) / len;
            var sin = (_toY - _Y) / len;

            //speed *= Constants.SpeedUnit;
            _dX = speed * cos;
            _dY = speed * sin;

            _ddX = acceleration * cos;
            _ddY = acceleration * sin;
        }
    }
}
