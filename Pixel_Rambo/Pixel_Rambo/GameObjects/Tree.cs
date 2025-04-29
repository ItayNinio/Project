using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Pixel_Rambo.GameObjects
{
   class Tree : GameMovingObject // מחלקת Tree מייצגת עץ אויב שיורה קליעי עץ לעבר השחקן במרווחי זמן קבועים
    {
        private DispatcherTimer fireTimer = new DispatcherTimer(); // טיימר לירי
        private DispatcherTimer idleTimer = new DispatcherTimer();  // טיימר לחזרה למצב idle אחרי ירי  
        private bool Direction; // האם העץ יורה ימינה או שמאלה

        public Tree(Scene scene, string filename, double placeX, double placeY, bool direction, double delay) :
          base(scene, filename, placeX, placeY)
        {

            Image.Height = 90;
            fireTimer.Interval = TimeSpan.FromSeconds(delay); // Shoots every 2 seconds
            fireTimer.Tick += Shoot;
            idleTimer.Interval = TimeSpan.FromMilliseconds(delay + 110);
            idleTimer.Tick += ReturnToIdle;
            fireTimer.Start();
            Direction = direction;

        }
        // בנאי — יוצר את העץ במשחק עם מיקום, כיוון, וקובע את קצב הירי

        private void Shoot(object sender, object e)
        {
             if (Manager.Gamestate != Constants.GameState.Paused){
                if (!Direction)
                {
                    _X = _X - 8;
                    SetImage("Tree/tree_shootingLeft.gif");
                    Render();
                }
                else
                {
                    SetImage("Tree/tree_shooting.gif");

                }
                _scene.AddObject(new Tree_Bullet(_scene, "Tree/Bullet_Left.png", _X, _Y + 18, Direction));
                idleTimer.Start();
            }
        }      // פונקציית Shoot — מפעילה את פעולת הירי של העץ

        private void ReturnToIdle(object sender, object e)
        {

            idleTimer.Stop();
            if (Direction)
            {

                SetImage("Tree/tree_idleLeft.gif");
                Render();
            }
            else
            {
                SetImage("Tree/idle_tree.gif");
                _X = _X + 8;
            }
        }     // פונקציית ReturnToIdle — מחזירה את העץ למצב idle לאחר ירי

        public void hit()
        {
            if (Direction)
            {
                SetImage("Tree/tree_hit.gif");
            }
            else
            {
                SetImage("Tree/tree_hit_left.gif");
            }


            _dY = -4;
            _ddY = 1;

        }
        // פונקציה שמופעלת כשהעץ חוטף פגיעה

        public override void Collide(List<GameObject> collidingObjects)
        {
            foreach (var otherObject in collidingObjects)
            {
                if (otherObject is Bullet bullet)
                {
                    _scene.RemoveObject(bullet);       
                    hit();
                    fireTimer.Stop();
                }
            }
        }
        // פונקציה Collide — מטפלת בהתנגשות של קליעים בעץ


    }
}