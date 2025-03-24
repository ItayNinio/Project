using DataBaseProject.Models;
using GameEngine.GameServices;
using Pixel_Rambo.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel_Rambo.GameServices
{
    public class GameManager : Manager
    {
        public static GameUser GameUser { get; set; } = new GameUser();
        public GameManager(Scene scene) : base(scene)
        {
            Init();
        }
        //הפעולה תיצור את כל הדמויות
        private void Init()
        {
            if (GameUser.CurrentLevel.LevelNumber == 1)
            {
                //    Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 800, Scene.ActualHeight - 100));
                //       Scene.AddObject( new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 1200, Scene.ActualHeight - 124));
                Scene.AddObject(new Block(Scene, "Block/tree.png", 900, Scene.ActualHeight - 184, 60, 140, false));
                Scene.AddObject(new Chicken(Scene, "Chicken/chicken_run_left.gif", 800, Scene.ActualHeight - 104));
                //    Scene.AddObject(new Heart(Scene, "Heart/heart.png", 200, 3, 60));
                //   Scene.AddObject(new Heart(Scene, "Heart/heart.png", 265, 3, 60));
                Scene.AddObject(new Heart(Scene, "Heart/heart.png", 800, Scene.ActualHeight - 74, 30));

                //  Scene.AddObject(new Block(Scene, "Block/big_block.png", 600, Scene.ActualHeight - 154, 110));
                //      Scene.AddObject(new Block(Scene, "Block/big_block.png", 900, Scene.ActualHeight - 184, 150));
                //        Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 399, Scene.ActualHeight - 124));
                //      Scene.AddObject(new Spikes(Scene, "Spikes/spikes.png", 600, 0));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 0, Scene.ActualHeight - 44));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 595, Scene.ActualHeight - 44));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 1190, Scene.ActualHeight - 44));
                Scene.AddObject(new Tree(Scene, "Tree/idle_tree.gif", 1000, Scene.ActualHeight - 134, false, GameUser.CurrentLevel.Tree_Bullet_Delay));
                //       Scene.AddObject(new Block(Scene, "Block/block.png", 700, 300, 70));
              
                Scene.AddObject(new Block(Scene, "Block/gg.png", 200, Scene.ActualHeight - 88, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/4blocks.png", 244, Scene.ActualHeight - 132, 88, 88, true));
                Scene.AddObject(new Block(Scene, "Block/2blocks.png", 288, Scene.ActualHeight - 176, 88, 44, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 332, Scene.ActualHeight - 220, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/bridge.png", 360, Scene.ActualHeight - 176, 176, 22, true));
                Scene.AddObject(new Block(Scene, "Block/bridge.png", 505, Scene.ActualHeight - 176, 176, 22, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 664, Scene.ActualHeight - 176, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 664, Scene.ActualHeight - 220, 44, 44, true));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 400, Scene.ActualHeight - 206, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 450, Scene.ActualHeight - 206, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 500, Scene.ActualHeight - 206, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 550, Scene.ActualHeight - 206, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 600, Scene.ActualHeight - 206, 30));
                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 530, Scene.ActualHeight - 237));

                Scene.AddObject(new Checkpoint(Scene, "Checkpoint/Checkpoint.png", 1200, Scene.ActualHeight - 244, 200, 200));
                Scene.AddObject(new Rambo(Scene, "Rambo/idelegif.gif", 10, Scene.ActualHeight - 137));



            }
            else if (GameUser.CurrentLevel.LevelNumber == 2)
            {
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 0, Scene.ActualHeight - 44));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 595, Scene.ActualHeight - 44));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 1190, Scene.ActualHeight - 44));
                Scene.AddObject(new Rambo(Scene, "Rambo/idelegif.gif", 10, Scene.ActualHeight - 137));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 200, Scene.ActualHeight - 88, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 200, Scene.ActualHeight - 132, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 420, Scene.ActualHeight - 88, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 420, Scene.ActualHeight - 132, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 420, Scene.ActualHeight - 176, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 420, Scene.ActualHeight - 220, 44, 44, true));
                Scene.AddObject(new Tree(Scene, "Tree/idle_tree.gif", 1230, Scene.ActualHeight - 134, false, GameUser.CurrentLevel.Tree_Bullet_Delay));
                Scene.AddObject(new Block(Scene, "Block/2blocks.png", 630, Scene.ActualHeight - 264, 88, 44, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 630, Scene.ActualHeight - 308, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/2blocks.png", 718, Scene.ActualHeight - 264, 88, 44, true));
                Scene.AddObject(new Break_Block(Scene, "Block/4break_block.png", 462, Scene.ActualHeight - 220, 176, 44));
      
                Scene.AddObject(new Special_Block(Scene, "Block/special_block.png", 630, Scene.ActualHeight - 517, 44, 44));
                Scene.AddObject(new Heart(Scene, "Heart/heart.png", 800, Scene.ActualHeight - 74, 30));

                Scene.AddObject(new Block(Scene, "Block/bridge.png", 790, Scene.ActualHeight - 264, 176, 22, true));
                Scene.AddObject(new Block(Scene, "Block/bridge.png", 937, Scene.ActualHeight - 264, 176, 22, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 1072, Scene.ActualHeight - 264, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/4blocks.png", 1116, Scene.ActualHeight - 308, 88, 88, true));
                Scene.AddObject(new Block(Scene, "Block/gg.png", 1160, Scene.ActualHeight - 352, 44, 44, true));
                Scene.AddObject(new Block(Scene, "Block/2blocks_vertical.png", 1204, Scene.ActualHeight - 396, 44, 88, true));
                Scene.AddObject(new Block(Scene, "Block/2blocks.png", 1248, Scene.ActualHeight - 396, 88, 44, true));
                Scene.AddObject(new Block(Scene, "Block/2blocks_vertical.png", 464, Scene.ActualHeight - 132, 44, 88, true));
                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 310, Scene.ActualHeight - 104));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 310, Scene.ActualHeight - 405, 30));
                //Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 550, Scene.ActualHeight - 74, 30));
                //Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 610, Scene.ActualHeight - 74, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 670, Scene.ActualHeight - 74, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 730, Scene.ActualHeight - 74, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 860, Scene.ActualHeight - 74, 30));
                //Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 920, Scene.ActualHeight - 74, 30));
                //Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 980, Scene.ActualHeight - 74, 30));
                //Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 1040, Scene.ActualHeight - 74, 30));
                Scene.AddObject(new Block(Scene, "Block/2blocks.png", 1336, Scene.ActualHeight - 396, 88, 44, true));
                Scene.AddObject(new Checkpoint(Scene, "Checkpoint/Checkpoint.png", 1200, Scene.ActualHeight - 595, 200, 200));
              //  Scene.AddObject(new Tree(Scene, "Tree/idle_tree.gif", 1230, Scene.ActualHeight - 487, false, GameUser.CurrentLevel.Tree_Bullet_Delay));


                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 600, Scene.ActualHeight - 325));


                Scene.AddObject(new Chicken(Scene, "Chicken/chicken_run_left.gif", 800, Scene.ActualHeight - 104));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 937, Scene.ActualHeight - 294, 30));







            }
            else if (GameUser.CurrentLevel.LevelNumber == 3)
            {
                // Ground segments
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 0, Scene.ActualHeight - 44));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 595, Scene.ActualHeight - 44));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 1190, Scene.ActualHeight - 44));
                // Extra ground to extend the bonus level
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 1780, Scene.ActualHeight - 44));

                // The player character
                Scene.AddObject(new Rambo(Scene, "Rambo/idelegif.gif", 10, Scene.ActualHeight - 137));

                // Mushrooms as jump platforms (placed at varying heights)
                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 300, Scene.ActualHeight - 150));
                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 600, Scene.ActualHeight - 200));
                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 900, Scene.ActualHeight - 250));
                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 1200, Scene.ActualHeight - 300));

                // Abundant coins for bonus points:
                // Coins along the ground
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 100, Scene.ActualHeight - 90, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 150, Scene.ActualHeight - 90, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 200, Scene.ActualHeight - 90, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 250, Scene.ActualHeight - 90, 30));

                // Coins near mushrooms (to encourage jumping)
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 300, Scene.ActualHeight - 190, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 600, Scene.ActualHeight - 240, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 900, Scene.ActualHeight - 290, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 1200, Scene.ActualHeight - 340, 30));

                // Scattered coins for extra bonus points
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 400, Scene.ActualHeight - 120, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 700, Scene.ActualHeight - 120, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 1000, Scene.ActualHeight - 120, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 1300, Scene.ActualHeight - 120, 30));

                // A special bonus block that might give extra points if hit
                Scene.AddObject(new Special_Block(Scene, "Block/special_block.png", 800, Scene.ActualHeight - 350, 44, 44));

                // A bonus checkpoint for level completion reward
                Scene.AddObject(new Checkpoint(Scene, "Checkpoint/Checkpoint.png", 1400, Scene.ActualHeight - 150, 200, 200));
            }
        }

    }
}
