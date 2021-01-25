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
namespace NoNameButtonGame.LevelSystem.LevelContainer
{
    class Level2 : SampleLevel
    {

        AwesomeButton[] button;


        public Level2(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 2 - WHAAT?!? There is more to this Game?!";
            button = new AwesomeButton[16];
            int randI64 = rand.Next(0, 16);
            for (int i = 0; i < button.Length; i++) {
                if (i == randI64) {
                    button[i] = new AwesomeButton(new Vector2(130 * (i % 4) - 256, (i / 4) * 68 - 128), new Vector2(128, 64), staticContent.Content.GetTHBox("awesomebutton")) {
                        DrawColor = Color.White,
                    };
                    button[i].Click += BtnWinEvent;
                } else {
                    button[i] = new AwesomeButton(new Vector2(130 * (i % 4) - 256, (i / 4) * 68 - 128), new Vector2(128, 64), staticContent.Content.GetTHBox("failbutton")) {
                        DrawColor = Color.White,
                    };
                    button[i].Click += BtnFailEvent;
                }
            }
        }



        private void BtnFailEvent(object sender, EventArgs e) {
            CallFail();
        }

        private void BtnWinEvent(object sender, EventArgs e) {
            CallFinish();
        }
        public override void Draw(SpriteBatch sp) {
            for (int i = 0; i < button.Length; i++) {
                button[i].Draw(sp);
            }

        }

        public override void Update(GameTime gt) {
            base.Update(gt);
            for (int i = 0; i < button.Length; i++) {
                button[i].Update(gt, MouseIngame);
            }
        }
    }
}
