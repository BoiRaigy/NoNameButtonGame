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

namespace NoNameButtonGame.LevelSystem.LevelContainer
{
    class Level6 : SampleLevel
    {

        Cursor cursor;
        float WaitTime = 20000;
        int CurrentShot = 0;
        Laserwall ShotOne;
        AwesomeButton button;
        Random rand;
        public Level6(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            this.rand = rand;
            Name = "Level 6 - Now what?!";

            cursor = new Cursor(new Vector2(100, 100), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            ShotOne = new Laserwall(new Vector2(-1000, -100), new Vector2(8, 8), Globals.Content.GetTHBox("zonenew"));
            button = new AwesomeButton(new Vector2(0, 0), new Vector2(64, 32), Globals.Content.GetTHBox("awesomebutton"));
        }



        private void BtnEvent(object sender, EventArgs e) {
            CallFinish(sender, e);
        }
        private void WallEvent(object sender, EventArgs e) {
            CallReset(sender, e);
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            ShotOne.Draw(sp);
            cursor.Draw(sp);
        }
        float GT;
        float WaitGT;
        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            cursor.Position = MousePos - cursor.Size / 2;
            GT += gt.ElapsedGameTime.Milliseconds;
            WaitGT += gt.ElapsedGameTime.Milliseconds;

            if (WaitGT >= WaitTime) {
                WaitGT -= WaitTime;
                CurrentShot++;
            }
            while (GT > 32) {
                GT -= 32;
                switch (CurrentShot) {
                    case 0:
                        //state 1
                        break;
                }
            }
            button.Update(gt, cursor.Hitbox[0]);
            ShotOne.Update(gt, cursor.Hitbox[0]);
        }
    }
}
