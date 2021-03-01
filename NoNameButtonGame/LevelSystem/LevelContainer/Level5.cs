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
    class Level5 : SampleLevel
    {

        HoldButton button;
        Cursor cursor;
        TextBuilder[] Infos;
        LockButton lockbutton;
        public Level5(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 5 - MORE BUTTONS!";
            button = new HoldButton(new Vector2(-220, -100), new Vector2(128, 64), Globals.Content.GetTHBox("emptybutton"));
            button.Click += EmptyBtnEvent;
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            Infos = new TextBuilder[2];
            Infos[0] = new TextBuilder("<-- Hold this button till the timer runs out!", new Vector2(-64, -72), new Vector2(8, 8), null, 0);
            Infos[1] = new TextBuilder("<-- This one will be unlocked then", new Vector2(-64, 32), new Vector2(8, 8), null, 0);
            lockbutton = new LockButton(new Vector2(-220, 0), new Vector2(128, 64), Globals.Content.GetTHBox("awesomebutton"), true);
            lockbutton.Click += BtnEvent;
        }


        private void EmptyBtnEvent(object sender, EventArgs e) {
            lockbutton.Locked = !lockbutton.Locked;
        }

        private void BtnEvent(object sender, EventArgs e) {
            CallFinish(sender, e);
        }
        private void WallEvent(object sender, EventArgs e) {
            CallReset(sender, e);
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            for (int i = 0; i < Infos.Length; i++) {
                Infos[i].Draw(sp);
            }
            lockbutton.Draw(sp);
            cursor.Draw(sp);
        }

        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            for (int i = 0; i < Infos.Length; i++) {
                Infos[i].Update(gt);
            }

            cursor.Position = MousePos - cursor.Size / 2;
            button.Update(gt, cursor.Hitbox[0]);
            lockbutton.Update(gt, cursor.Hitbox[0]);
        }
    }
}
