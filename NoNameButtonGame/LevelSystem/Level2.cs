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
namespace NoNameButtonGame.LevelSystem
{
    class Level2 : SampleLevel
    {

        AwesomeButton button;


        public Level2(int defaultWidth, int defaultHeight, Vector2 window) : base(defaultWidth, defaultHeight, window) {
            Name = "Level 2 - WHAAT?!? There is more to this Game?!";
            button = new AwesomeButton(new Vector2(-64, -232), new Vector2(128, 64), staticContent.Content.GetTHBox("awesomebutton")) {
                DrawColor = Color.White,
            };
            button.Click += BtnEvent;
        }



        private void BtnEvent(object sender, EventArgs e) {
            CallFinish();
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
        }

        public override void Update(GameTime gt) {
            base.Update(gt);

            button.Update(gt, MouseIngame);

        }
    }
}
