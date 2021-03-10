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
    class Level47 : SampleLevel
    {


        Cursor cursor;
        TextBuilder Timer;
        TextBuilder GUN;
        List<Tuple<Laserwall, Vector2>> shots;
        public Level47(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 47 - MEGA GUN!";
            Timer = new TextBuilder("", new Vector2(0 - 128), new Vector2(16, 16), null, 0);

            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            GUN = new TextBuilder("AGUN", new Vector2(-256, 0), new Vector2(16, 16), null, 0);
            shots = new List<Tuple<Laserwall, Vector2>>();
        }



        private void BtnEvent(object sender, EventArgs e) {
            CallFinish(sender, e);
        }
        private void WallEvent(object sender, EventArgs e) {
            CallReset(sender, e);
        }
        public override void Draw(SpriteBatch sp) {

            GUN.Draw(sp);
            for (int i = 0; i < shots.Count; i++) {
                shots[i].Item1.Draw(sp);
            }
            Timer.Draw(sp);
            cursor.Draw(sp);
        }
        float GT;
        float MGT;
        float ShotTime = 100;
        float TravelSpeed = 10;
        float UpdateSpeed = 2;
        float MaxUpdateSpeed = 64;
        float MinUpdateSpeed = 4;
        Vector2 OldMPos;
        List<int> removeItem = new List<int>();


        float TimerMax = 30000;
        float TimerC = 0;
        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            GUN.Update(gt);

            MGT += (float)gt.ElapsedGameTime.TotalMilliseconds;
            TimerC += (float)gt.ElapsedGameTime.TotalMilliseconds;
            while (MGT > UpdateSpeed) {
                MGT -= UpdateSpeed;
                for (int i = 0; i < shots.Count; i++) {
                    shots[i].Item1.Move(shots[i].Item2 * TravelSpeed);
                }
                GT += (float)gt.ElapsedGameTime.TotalMilliseconds;
                while (GT > ShotTime) {
                    GT -= ShotTime;
                    Vector2 Dir = cursor.Hitbox[0].Center.ToVector2() - GUN.rec.Center.ToVector2();
                    shots.Add(new Tuple<Laserwall, Vector2>(new Laserwall(GUN.Position, new Vector2(16, 8), Globals.Content.GetTHBox("zonenew")), Dir / Dir.Length()));
                    shots[shots.Count - 1].Item1.Enter += CallFail;
                }
            }
            removeItem.Clear();
            for (int i = 0; i < shots.Count; i++) {
                shots[i].Item1.Update(gt, cursor.Hitbox[0]);
                if (!shots[i].Item1.rec.Intersects(CamRec)) {
                    removeItem.Add(i);
                }
            }
            for (int i = 0; i < removeItem.Count; i++) {
                try {
                    shots.RemoveAt(removeItem[i]);
                } catch { }
            }
            if (MousePos != OldMPos) {
                UpdateSpeed -= Vector2.Distance(MousePos, OldMPos) * 10;
                if (UpdateSpeed < MinUpdateSpeed)
                    UpdateSpeed = MinUpdateSpeed;
            } else {
                UpdateSpeed = MaxUpdateSpeed;
            }
            if (TimerC >= TimerMax)
                CallFinish(this, new EventArgs());
            Timer.Update(gt);
            Timer.ChangeText(((TimerMax - TimerC) / 1000).ToString("0.0") + "S");
            cursor.Position = MousePos - cursor.Size / 2;
            OldMPos = MousePos;
        }
    }
}
