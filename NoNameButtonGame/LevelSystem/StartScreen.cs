using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using NoNameButtonGame.Text;
using NoNameButtonGame.GameObjects;
using NoNameButtonGame.BeforeMaths;

namespace NoNameButtonGame.LevelSystem.LevelContainer
{
    class StartScreen : SampleLevel
    {
        AwesomeButton Start;
        AwesomeButton Setting;
        AwesomeButton LevelSelect;
        AwesomeButton Exit;
        Cursor cursor;
        public StartScreen(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Start Menu";
            int Startpos = -(64 * 2);
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            Start = new AwesomeButton(new Vector2(-64, Startpos), new Vector2(160, 64), Globals.Content.GetTHBox("startbutton"));
            Start.Click += BnEvStart;
            LevelSelect = new AwesomeButton(new Vector2(-92, Startpos + 64), new Vector2(216, 64), Globals.Content.GetTHBox("selectbutton"));
            LevelSelect.Click += BnEvSelect;
            Setting = new AwesomeButton(new Vector2(-130, Startpos + 64 * 2), new Vector2(292, 64), Globals.Content.GetTHBox("settingsbutton"));
            Setting.Click += BnEvSettings;
            Exit = new AwesomeButton(new Vector2(-52, Startpos + 64 * 3), new Vector2(136, 64), Globals.Content.GetTHBox("exitbutton"));
            Exit.Click += BnEvExit;
        }
        
        public ButtonPressed Action;
        public enum ButtonPressed
        {
            Start,
            LevelSelect,
            Settings,
            Exit
        }
        private void BnEvStart(object sender, EventArgs e) {
            Action = ButtonPressed.Start;
            CallFinish(this, e);
        }
        private void BnEvSelect(object sender, EventArgs e) {
            Action = ButtonPressed.LevelSelect;
            CallFinish(this, e);
        }
        private void BnEvSettings(object sender, EventArgs e) {
            Action = ButtonPressed.Settings;
            CallFinish(this, e);
        }
        private void BnEvExit(object sender, EventArgs e) {
            Action = ButtonPressed.Exit;
            CallFinish(this, e);
        }
        public override void Draw(SpriteBatch sp) {

            Start.Draw(sp);
            Setting.Draw(sp);
            LevelSelect.Draw(sp);
            Exit.Draw(sp);
            cursor.Draw(sp);
        }
        public override void Update(GameTime gt) {
            base.Update(gt);
            Start.Update(gt, cursor.Hitbox[0]);
            Setting.Update(gt, cursor.Hitbox[0]);
            LevelSelect.Update(gt, cursor.Hitbox[0]);
            Exit.Update(gt, cursor.Hitbox[0]);
            cursor.Position = MousePos;
            cursor.Update(gt);
        }
    }
}
