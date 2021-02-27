using System;
using System.Collections.Generic;
using System.Text;

using Raigy.Obj;
using Raigy.Input;
using Raigy.Camera;

using NoNameButtonGame.Interfaces;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NoNameButtonGame.BeforeMaths;
using NoNameButtonGame.GameObjects;
using NoNameButtonGame.Text;
using NoNameButtonGame.color;
namespace NoNameButtonGame.LevelSystem.LevelContainer
{
    class Level3 : SampleLevel
    {

        AwesomeButton button;
        Cursor cursor;
        TextBuilder Info;
        Rainbow raincolor;
        Laserwall[] laserwall;
        float GT;
        public Level3(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 3 - Tutorial time!";
            

            
            Vector2 clustPos = new Vector2(-250, -150);
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            
            Info = new TextBuilder("this is bad! ->", new Vector2(-32, 132), new Vector2(16, 16), null, 0);
            raincolor = new Rainbow {
                Increment = 32,
                Speed = 32,
                Offset = 256
            };
            laserwall = new Laserwall[6];
            Vector2 Move = new Vector2(-100,0);
            button = new AwesomeButton(Move + new Vector2(-110,-110), new Vector2(128, 64), Globals.Content.GetTHBox("awesomebutton")) {
                DrawColor = Color.White,
            };
            button.Click += CallFinish;
            laserwall[0] = new Laserwall(new Vector2(Move.X - 128, Move.Y-128), new Vector2(8, 96), Globals.Content.GetTHBox("zonenew"));
            laserwall[1] = new Laserwall(new Vector2(Move.X - 128, Move.Y - 128), new Vector2(160, 8), Globals.Content.GetTHBox("zonenew"));
            laserwall[2] = new Laserwall(new Vector2(Move.X + 32, Move.Y - 128), new Vector2(8, 96), Globals.Content.GetTHBox("zonenew"));
            laserwall[3] = new Laserwall(new Vector2(Move.X - 32, Move.Y - 32), new Vector2(72, 8), Globals.Content.GetTHBox("zonenew"));
            laserwall[4] = new Laserwall(new Vector2(Move.X - 128, Move.Y - 32), new Vector2(72, 8), Globals.Content.GetTHBox("zonenew"));
            laserwall[5] = new Laserwall(new Vector2(190, 100), new Vector2(64, 64), Globals.Content.GetTHBox("zonenew"));
            for (int i = 0; i < laserwall.Length; i++) {
                laserwall[i].Enter += LaserEvent;
            }
            
        }

        private void LaserEvent(object sender, EventArgs e) {
            CallFail(sender, e);
        }

        private void BtnEvent(object sender, EventArgs e) {
            CallFinish(sender,e);
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            
            Info.Draw(sp);
            for (int i = 0; i < laserwall.Length; i++) {
                laserwall[i].Draw(sp);
            }
            cursor.Draw(sp);
           
        }

        public override void Update(GameTime gt) {
            GT += gt.ElapsedGameTime.Milliseconds;
            while(GT > 125) {
                GT -= 125;
                //laserwall.Move(new Vector2(1, 0));
            }
            cursor.Update(gt);
            raincolor.Update(gt);
            Info.ChangeColor(raincolor.GetColor(Info.Text.Length));
            Info.Update(gt);
            base.Update(gt);
            for (int i = 0; i < laserwall.Length; i++) {
                laserwall[i].Update(gt, cursor.Hitbox[0]);
            }
            
            cursor.Position = MousePos - cursor.Size / 2;
            button.Update(gt, cursor.Hitbox[0]);
        }
    }
}
