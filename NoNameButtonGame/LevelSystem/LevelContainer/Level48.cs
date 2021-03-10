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
    class Level48 : SampleLevel
    {

        LockButton button;
        Cursor cursor;
        TextBuilder Info;
        Laserwall wall;
        TextButton ButtonStartTimer;
        TextBuilder Timer;
        public Level48(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 48 - THERE IS NO ESCAPE!!";
            button = new LockButton(new Vector2(-256, -128), new Vector2(128, 64), Globals.Content.GetTHBox("awesomebutton"), true);
            button.Click += BtnEvent;
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            Info = new TextBuilder("RUN! IT FOLLOWs you!", new Vector2(-64, -132), new Vector2(16, 16), null, 0);

            wall = new Laserwall(new Vector2(-32, -200), new Vector2(64, 64), Globals.Content.GetTHBox("zonenew"));
            wall.Enter += WallEvent;
            ButtonStartTimer = new TextButton(new Vector2(-64, -32), new Vector2(128, 64), Globals.Content.GetTHBox("emptybutton"), "TimerStart", "Start Timer", new Vector2(8, 8));
            ButtonStartTimer.Click += StartTimer;
            Timer = new TextBuilder("0.0S", new Vector2(-16, 64), new Vector2(16, 16), null, 0);

        }

        bool TimerStarted;
        private void StartTimer(object s, EventArgs e) {
            TimerStarted = true;
        }
        private void BtnEvent(object sender, EventArgs e) {
            CallFinish(sender, e);
        }
        private void WallEvent(object sender, EventArgs e) {
            CallReset(sender, e);
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            if (TimerStarted) {
                Info.Draw(sp);
                wall.Draw(sp);
                Timer.Draw(sp);
            }
            if (!TimerStarted && button.Locked)
                ButtonStartTimer.Draw(sp);
            cursor.Draw(sp);
        }
        float GT;
        float TGT;
        public override void Update(GameTime gt) {
            base.Update(gt);
            cursor.Position = MousePos - cursor.Size / 2;
            cursor.Update(gt);
            if (TimerStarted) {
                Info.Update(gt);
                GT += (float)gt.ElapsedGameTime.TotalMilliseconds;
                TGT += (float)gt.ElapsedGameTime.TotalMilliseconds;
                while (GT > 32) {
                    GT -= 32;
                    Vector2 Dir = cursor.Hitbox[0].Center.ToVector2() - wall.rec.Center.ToVector2();

                    wall.Move(Dir / Dir.Length() * (TGT / 1000));
                }
                wall.Update(gt, cursor.Hitbox[0]);
                float TL = (50000 - TGT) / 1000;
                if (TL <= 0) {
                    TimerStarted = false;
                    button.Locked = false;
                }
                Timer.ChangeText(TL.ToString("0.0").Replace(',', '.') + "S");
                Timer.Update(gt);
            }
            if (!TimerStarted && button.Locked)
                ButtonStartTimer.Update(gt, cursor.Hitbox[0]);
            button.Update(gt, cursor.Hitbox[0]);

        }
    }
}
