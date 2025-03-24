using DatabaseProject;
using DataBaseProject;
using GameEngine.GameObjects;
using GameEngine.GameServices;
using Pixel_Rambo.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Shapes;

namespace Pixel_Rambo.GameObjects
{
    public class Rambo : GameMovingObject
    {
        private List<Bullet> bullets_list = new List<Bullet>();
        private Rectangle bullet;

        private DateTime _lastShotTime = DateTime.MinValue;
        private TimeSpan _shootCooldown = TimeSpan.FromMilliseconds(200);
        private DispatcherTimer gifdelay = new DispatcherTimer();
        private bool _isOnPlatform = false; // Track if the player is supported
        public int lifes { get; set; } = 3;
       
       

        public enum StateType
        {
            idelLeft, idelRight, movingLeft, movingRight, shootingRight, ShootingLeft, hitRight, hitLeft, Dead, Pause
        }
        public enum StateJump
        {
            Jump, stand
        }
        public StateType State
        { get; set; }
        public StateJump State2 { get; set; }
        public double GetX() => _X;
        public double GetY() => _Y;
        private string Right_bullet_sorce = "";
        private string Left_bullet_sorce = "";

        public Rambo(Scene scene, string filename, double placeX, double placeY) :
            base(scene, filename, placeX, placeY)
        {
            Manager.GameEvent.OnKeyDown += Go;
            Manager.GameEvent.OnKeyUp += Stop;
            Image.Height = 90;

            State = StateType.idelRight;
            State2 = StateJump.stand;
            gifdelay.Interval = TimeSpan.FromMilliseconds(800); // Adjust this to control the delay
            gifdelay.Tick += RemovalDelayTimer_Tick;
            
            if (Server.HasPowerBullet(GameManager.GameUser.UserId))
            {
                Right_bullet_sorce = "Rambo/special_bullet3.png";
                Left_bullet_sorce = "Rambo/special_bullet4.png";

            }
            else
            {
               
                Right_bullet_sorce = "Rambo/special_bullet.png";
                Left_bullet_sorce = "Rambo/special_bullet2.png";

            }
}
       
       
        

        public Rect GetsmallRect()
        {
            if (State == StateType.shootingRight)
            {
                return new Rect(_X, _Y, Width - 70, Height);
            }
            else if (State == StateType.ShootingLeft) {
                return new Rect(_X + 70, _Y, Width - 70, Height);
            }
            return new Rect(_X, _Y, Width, Height);
        }
       
        public override void Render()
        {
            base.Render();
            if (Rect.Left <= 0)
            {
                _X = 0;
            }
            if (Rect.Right >= _scene?.ActualWidth)
            {
                _X = _scene.ActualWidth - Image.Height;
            }
            if (Rect.Bottom >= _scene?.ActualHeight - 44)
            {
                _Y = _scene.ActualHeight - Image.ActualHeight - 44;
                _ddY = 0;
                _dY = 0;
                State2 = StateJump.stand;
            }
            if (State2 == StateJump.stand && _isOnPlatform) // Track if the player is supported
            {


                // Check if the player has walked off the platform
                bool noLongerOnPlatform = !_scene.getobjlist().OfType<Block>().Any(ball =>
                    Rect.Bottom == ball.Rect.Top && // Player's bottom aligns with ball's top
                    Rect.Right > ball.Rect.Left && Rect.Left < ball.Rect.Right); // Player's X overlaps ball's X

                if (noLongerOnPlatform && State2 == StateJump.stand)
                {
                    _isOnPlatform = false;
                    State2 = StateJump.Jump;

                    _ddY = 1; // Re-enable gravity
                }
            }



        }



        
        public override void Collide(List<GameObject> collidingObjects)
        {
            foreach (var otherObject in collidingObjects)
            {
                if (otherObject is Mushroom mushroom || otherObject is Tree_Bullet tree_bullet || otherObject is Tree tree || otherObject is Heart heart || otherObject is Chicken )
                {
                    if (otherObject is Mushroom mushroom1 )
                    {
                        // Calculate if collision is from above
                        if (this.Rect.Bottom <= otherObject.Rect.Top + 7)
                        {
                            mushroom1.OnHitFromAbove();
                            _dY = -20;
                            _ddY = 1;
                        }

                    }
                    if (otherObject is Chicken chicken)
                    {
                        // Calculate if collision is from above
                        if (this.Rect.Bottom <= otherObject.Rect.Top + 7)
                        {
                            chicken.OnHitFromAbove();
                            _dY = -10;
                            _ddY = 1;
                        }

                    }

                    if (otherObject is Tree tree1)
                    {
                        // Calculate if collision is from above
                        if (this.Rect.Bottom <= otherObject.Rect.Top + 7)
                        {
                            tree1.hit();
                            _dY = -10;
                            _ddY = 1;
                        }
                    }
                  



                    if (State != StateType.hitLeft && State != StateType.hitRight && !(this.Rect.Bottom <= otherObject.Rect.Top + 7) && !(otherObject is Heart) && !(( GetsmallRect().Left < otherObject.Rect.Left && GetsmallRect().Right < otherObject.Rect.Left) || (GetsmallRect().Left > otherObject.Rect.Right && GetsmallRect().Right > otherObject.Rect.Right)))
                    {
                        if (Manager.GameEvent.OnRemoveLife != null)
                        {

                            Manager.GameEvent.OnRemoveLife();
                            lifes--;
                        }

                        if (State == StateType.idelRight || State == StateType.movingRight || State == StateType.shootingRight)
                        {
                    
                          
                            if (lifes > 0)
                            {
                                _dY = -8;
                                _ddY = 1;
                                SetImage("Rambo/Rambo_hit.gif");
                                State = StateType.hitRight;
                            }
                            else 
                            {
                                State = StateType.Dead;
                                SetImage("Rambo/Rambo_dead.gif");
                                Collisional = false;
                                GameManager.GameEvent.OnGameOver();
                             

                            }
                          
                            
                            if (!gifdelay.IsEnabled) // Ensure the timer isn't already running
                            {
                                gifdelay.Start();
                            }
                        }
                        else if (State == StateType.idelLeft || State == StateType.movingLeft || State == StateType.ShootingLeft)
                        {
                         
                            if (lifes > 0)
                            {
                                _dY = -7;
                                _ddY = 1;
                                SetImage("Rambo/Rambo_hit_Left.gif");
                                State = StateType.hitLeft;
                            }
                            else
                            {
                                State = StateType.Dead;
                                SetImage("Rambo/Rambo_dead_left.gif");
                                Collisional = false;
                                
                            }
                         
                           
                            if (!gifdelay.IsEnabled) // Ensure the timer isn't already running
                            {
                                gifdelay.Start();
                            }
                        }
                      
                    }

                }
                else if (otherObject is Block block)
                {
                    if (Rect.Bottom <= block.Rect.Top + 20 && !(GetsmallRect().Right <= block.Rect.Left && GetsmallRect().Left <= block.Rect.Left) && !(GetsmallRect().Right >= block.Rect.Right && GetsmallRect().Left >= block.Rect.Right)) // Collision from above
                    {
                        _Y = block.Rect.Top - Image.Height; // Place player on top of the ball
                        _dY = 0; // Stop vertical movement
                        _ddY = 0; // Reset vertical acceleration
                        State2 = StateJump.stand;
                        _isOnPlatform = true;
                    }
                    else if (Rect.Top >= block.Rect.Bottom - 20 && Rect.Bottom > block.Rect.Bottom) // Collision from below
                    {
                        _Y = block.Rect.Bottom; // Prevent player from moving through the ball
                        _dY = 0; // Stop vertical movement
                    }
                    else if (Rect.Right >= block.Rect.Left && Rect.Left < block.Rect.Left && State != StateType.shootingRight && State != StateType.ShootingLeft) // Collision from left
                    {
                        _X = block.Rect.Left - Width; // Prevent movement into the ball
                        _dX = 0; // Stop horizontal movement
                    }
                    else if (Rect.Left <= block.Rect.Right && Rect.Right > block.Rect.Right && State != StateType.shootingRight && State != StateType.ShootingLeft) // Collision from right
                    {
                        _X = block.Rect.Right; // Prevent movement into the ball
                        _dX = 0; // Stop horizontal movement
                    }
                }
                else if (otherObject is Break_Block break_block)
                {
                    if (Rect.Bottom <= break_block.Rect.Top + 20 && !(GetsmallRect().Right <= break_block.Rect.Left && GetsmallRect().Left <= break_block.Rect.Left) && !(GetsmallRect().Right >= break_block.Rect.Right && GetsmallRect().Left >= break_block.Rect.Right)) // Collision from above
                    {
                        if (_dY != 0)
                        {
                            break_block.start_break();
                            Manager.GameEvent.OnBreakBlock();
                        }
                        _Y = break_block.Rect.Top - Image.Height; // Place player on top of the ball
                        _dY = 0; // Stop vertical movement
                        _ddY = 0; // Reset vertical acceleration
                        State2 = StateJump.stand;
                        _isOnPlatform = true;

                    }
                    else if (Rect.Top >= break_block.Rect.Bottom - 20 && Rect.Bottom > break_block.Rect.Bottom) // Collision from below
                    {
                        if (_dY != 0)
                        {
                            break_block.start_break();
                            Manager.GameEvent.OnBreakBlock();
                        }
                        _Y = break_block.Rect.Bottom; // Prevent player from moving through the ball
                        _dY = 0; // Stop vertical movement
                    }
                    else if (Rect.Right >= break_block.Rect.Left && Rect.Left < break_block.Rect.Left && State != StateType.shootingRight && State != StateType.ShootingLeft) // Collision from left
                    {
                        _X = break_block.Rect.Left - Width; // Prevent movement into the ball
                        _dX = 0; // Stop horizontal movement
                    }
                    else if (Rect.Left <= break_block.Rect.Right && Rect.Right > break_block.Rect.Right && State != StateType.shootingRight && State != StateType.ShootingLeft) // Collision from right
                    {
                        _X = break_block.Rect.Right; // Prevent movement into the ball
                        _dX = 0; // Stop horizontal movement
                    }
                }
                else if (otherObject is Special_Block special_block)
                {
                    if (Rect.Bottom <= special_block.Rect.Top + 20 && !(GetsmallRect().Right <= special_block.Rect.Left && GetsmallRect().Left <= special_block.Rect.Left) && !(GetsmallRect().Right >= special_block.Rect.Right && GetsmallRect().Left >= special_block.Rect.Right)) // Collision from above
                    {
                       
                        _Y = special_block.Rect.Top - Image.Height; // Place player on top of the ball
                        _dY = 0; // Stop vertical movement
                        _ddY = 0; // Reset vertical acceleration
                        State2 = StateJump.stand;
                        _isOnPlatform = true;

                    }
                    else if (Rect.Top >= special_block.Rect.Bottom - 20 && Rect.Bottom > special_block.Rect.Bottom) // Collision from below
                    {
                        if (_dY != 0)
                        {
                            special_block.Add_Item();
                        }
                        _Y = special_block.Rect.Bottom; // Prevent player from moving through the ball
                        _dY = 0; // Stop vertical movement
                    }
                    else if (Rect.Right >= special_block.Rect.Left && Rect.Left < special_block.Rect.Left && State != StateType.shootingRight && State != StateType.ShootingLeft) // Collision from left
                    {
                        _X = special_block.Rect.Left - Width; // Prevent movement into the ball
                        _dX = 0; // Stop horizontal movement
                    }
                    else if (Rect.Left <= special_block.Rect.Right && Rect.Right > special_block.Rect.Right && State != StateType.shootingRight && State != StateType.ShootingLeft) // Collision from right
                    {
                        _X = special_block.Rect.Right; // Prevent movement into the ball
                        _dX = 0; // Stop horizontal movement
                    }
                }
            }
        }



        private void RemovalDelayTimer_Tick(object sender, object e)
        {
            if ((State == StateType.idelRight || State == StateType.movingRight || State == StateType.shootingRight || State == StateType.hitRight) && lifes > 0)
            {
                SetImage("Rambo/idelegif.gif");
                State = StateType.idelRight;
                Collisional = true;
            }
            else if ((State == StateType.idelLeft || State == StateType.movingLeft || State == StateType.ShootingLeft || State == StateType.hitLeft) && lifes > 0)
            {
                SetImage("Rambo/idelegifleft.gif");
                State = StateType.idelLeft;
                Collisional = true;
            }
         
            gifdelay.Stop();

        }
        
        private void Go(VirtualKey key)
        {
            if ((State == StateType.shootingRight || State == StateType.ShootingLeft ) &&
         (key == Keys.RightKey || key == Keys.LeftKey || key == Keys.UpKey || key == Keys.DownKey))
            {
                return; // Ignore the movement key if the player is shooting
            }
            if (key == Keys.UpKey && State != StateType.Dead)
            {
                _dY = -8;
               
            }

            if (key == Keys.DownKey && State != StateType.Dead)
            {
                _dY = 8;

            }

            if (key == Keys.RightKey && State != StateType.Dead)
            {

                if (State != StateType.movingRight && State != StateType.hitRight && State != StateType.hitLeft)
                {
                    State = StateType.movingRight;
                    SetImage("Rambo/goodgif.gif");
                }
                _dX = 8;
            }

            if (key == Keys.LeftKey && State != StateType.Dead)
            {
                if (State != StateType.movingLeft && State != StateType.hitRight && State != StateType.hitLeft)
                {
                    State = StateType.movingLeft;

                    SetImage("Rambo/goodgifleft.gif");

                }
                _dX = -8;
            }
            if (key == Keys.JumpKey && State2 != StateJump.Jump && State != StateType.Dead)
            {
                State2 = StateJump.Jump;
                Jump();

            }
            if (key == Keys.ShootKey && State != StateType.Dead)
            {
              
                Manager.GameEvent.OnShoot();
                _dX = 0;
                if (DateTime.Now - _lastShotTime < _shootCooldown)
                {
                    return;
                }
              
                _lastShotTime = DateTime.Now;


                if (State == StateType.movingRight || State == StateType.idelRight || State == StateType.shootingRight)
                {
                    if (State != StateType.shootingRight)
                    {
                        State = StateType.shootingRight;
                        SetImage("Rambo/shootinggif.gif");

                    }

                    _scene.AddObject(new Bullet(_scene, Right_bullet_sorce, _X + 63, _Y + 21 , true));
                }
                if (State == StateType.movingLeft || State == StateType.idelLeft || State == StateType.ShootingLeft)
                {
                    if (State != StateType.ShootingLeft)
                    {
                        State = StateType.ShootingLeft;
                        _X = _X - 58;
                        SetImage("Rambo/shootinggifleft.gif");


                    }
                    _scene.AddObject(new Bullet(_scene, Left_bullet_sorce, (_X + Width) - 63 , _Y + 21, false));
                }
            }

        }
        //public override Rect Rect
        //{
        //    get
        //    {
        //        if (State == StateType.ShootingLeft && !(DateTime.Now - _lastShotTime < _shootCooldown))
        //        {
        //            // Smaller Rect when facing right
        //            return GetCustomRect(160, 160);
        //        }
        //       return GetCustomRect(0, 0); ;
        //    }
        //}
        private void Stop(VirtualKey key)
        {
            if (key != Keys.JumpKey)
            {


                if ((key != Keys.JumpKey && State2 != StateJump.Jump && State != StateType.hitRight && State != StateType.hitLeft) ||  State == StateType.Dead)
                {
                    base.Stop();
                }
                if (State == StateType.movingRight)
                {
                    SetImage("Rambo/idelegif.gif");
                    State = StateType.idelRight;
                    _dX = 0;
                }
                if (State == StateType.movingLeft)
                {
                    SetImage("Rambo/idelegifleft.gif");
                    State = StateType.idelLeft;
                    _dX = 0;
                }
                if (State == StateType.shootingRight && key == Keys.ShootKey)
                {
                    State = StateType.idelRight;
                    SetImage("Rambo/idelegif.gif");

                }
                if (State == StateType.ShootingLeft && key == Keys.ShootKey)
                {
                    State = StateType.idelLeft;
                    _X = _X + 58;
                    SetImage("Rambo/idelegifleft.gif");


                }
                if (State == StateType.hitRight || State == StateType.hitLeft && (key == Keys.LeftKey || key == Keys.RightKey))
                {
                    _dX = 0;
                }

            }
        }
        private void Jump()
        {
            _dY = -16;
            _ddY = 1;

        }


    }
}