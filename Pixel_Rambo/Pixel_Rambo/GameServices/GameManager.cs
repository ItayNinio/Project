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
        // ניהול ראשי של המשחק – אחראי ליצירת הדמויות, האובייקטים והמפלצות לפי השלב
        public static GameUser GameUser { get; set; } = new GameUser();   // המשתמש הנוכחי שמשחק (שומר מידע כמו שלב נוכחי, כסף, חיים וכו')
        public GameManager(Scene scene) : base(scene)    // בנאי שמקבל את הסצנה של המשחק ומפעיל את אתחול האובייקטים
        {
            Init();
        }
        //הפעולה תיצור את כל הדמויות
        private void Init()     // פעולה שיוצרת את כל הדמויות, האובייקטים, המפלצות ועוד בהתאם לשלב הנוכחי

        {
            if (GameUser.CurrentLevel.LevelNumber == 3)
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

            }      // שלב 1

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

                Scene.AddObject(new Special_Block(Scene, "Block/special_block.png", 630, Scene.ActualHeight - 500, 44, 44));
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

            }  // שלב 2


            else if (GameUser.CurrentLevel.LevelNumber == 1)
            {
                // Player starting position
                Scene.AddObject(new Rambo(Scene, "Rambo/idelegif.gif", 10, 343 - 93));

                // === First Row ===
                for (int i = 0; i < 750; i += 145)
                    Scene.AddObject(new Block(Scene, "Block/bridge.png", i, 340, 176, 22, true));

                Scene.AddObject(new Break_Block(Scene, "Block/4break_block.png", 883, 340, 176, 44));

                for (int i = 1043; i < Scene.ActualWidth; i += 145)
                    Scene.AddObject(new Block(Scene, "Block/bridge.png", i, 340, 176, 22, true));

                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 400, 310, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 500, 310, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 600, 310, 30));
                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 700, 340 - 61));

                // === Second Row ===
                for (int i = 0; i < 450; i += 145)
                    Scene.AddObject(new Block(Scene, "Block/bridge.png", i, 500, 176, 22, true));

                Scene.AddObject(new Break_Block(Scene, "Block/4break_block.png", 588, 500, 176, 44));

                for (int i = 748; i < Scene.ActualWidth; i += 145)
                    Scene.AddObject(new Block(Scene, "Block/bridge.png", i, 500, 176, 22, true));

                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 250, 470, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 350, 470, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 450, 470, 30));
                Scene.AddObject(new Chicken(Scene, "Chicken/chicken_run_left.gif", 800, 500 - 61));
                Scene.AddObject(new Heart(Scene, "Heart/heart.png", 950, 470, 30));

                // === Third Row ===
                for (int i = 0; i < 600; i += 145)
                    Scene.AddObject(new Block(Scene, "Block/bridge.png", i, 660, 176, 22, true));

                Scene.AddObject(new Break_Block(Scene, "Block/4break_block.png", 733, 660, 176, 44));

                for (int i = 893; i < Scene.ActualWidth; i += 145)
                    Scene.AddObject(new Block(Scene, "Block/bridge.png", i, 660, 176, 22, true));

                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 350, 630, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 450, 630, 30));
                Scene.AddObject(new Coin(Scene, "Coin/coin-shine1.gif", 550, 630, 30));
                Scene.AddObject(new Mushroom(Scene, "Mushroom/Mushroom_run_left.gif", 950, 660 - 61));

                // === Ground segments at bottom ===
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 0, Scene.ActualHeight - 44));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 595, Scene.ActualHeight - 44));
                Scene.AddObject(new Ground(Scene, "Block/floor.png", 1190, Scene.ActualHeight - 44));

                // === Checkpoint sitting on top of the ground ===
                Scene.AddObject(new Checkpoint(Scene, "Checkpoint/Checkpoint.png", 1200, Scene.ActualHeight - 44 - 200, 200, 200));
            }

        }

    }
}
