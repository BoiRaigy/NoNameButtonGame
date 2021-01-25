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
    class Level4 : SampleLevel
    {

        AwesomeButton button;
        Cursor cursor;

        public Level4(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 2 - WHAAT?!? There is more to this Game?!";
            button = new AwesomeButton(new Vector2(-64, -0), new Vector2(128, 64), staticContent.Content.GetTHBox("failbutton")) {
                DrawColor = Color.White,
            };
            button.Click += BtnEvent;
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), staticContent.Content.GetTHBox("cursor"));
        }



        private void BtnEvent(object sender, EventArgs e) {
            CallFail();
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            cursor.Draw(sp);
        }

        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            cursor.Position = MouseIngame - cursor.Size / 2;
            button.Update(gt, cursor.rec);
        }
    }
}
