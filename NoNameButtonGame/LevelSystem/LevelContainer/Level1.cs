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
    class Level1 : SampleLevel
    {

        AwesomeButton button;
        Cursor cursor;
        TextBuilder Info;
        public Level1(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {

            button = new AwesomeButton(new Vector2(-64, -32), new Vector2(160, 64), Globals.Content.GetTHBox("startbutton")) {
                DrawColor = Color.White,
            };
            button.Click += BtnEvent;
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            Name = "Click the Button!";
            Info = new TextBuilder("How hard can it be?", new Vector2(-128, -64), new Vector2(16, 16), null, 0);
        }



        private void BtnEvent(object sender, EventArgs e) {
            CallFinish();
        }
        public override void Draw(SpriteBatch sp) {
            Info.Draw(sp);
            button.Draw(sp);
            cursor.Draw(sp);
        }

        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            cursor.Position = MousePos - cursor.Size / 2;
            button.Update(gt, cursor.Hitbox[0]);
            Info.Update(gt);
        }
    }
}
