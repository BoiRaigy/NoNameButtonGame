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
    class Level19 : SampleLevel
    {

        HoldButton button;
        Cursor cursor;
        LockButton lockbutton;
        Random rand;
        TextBuilder GUN;
        List<Tuple<Laserwall, Vector2>> shots;
        public Level19(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 19 - Hold Click Repeat!";
            this.rand = rand;
            GUN = new TextBuilder("AGUN", new Vector2(-256, 0), new Vector2(16, 16), null, 0);
            button = new HoldButton(new Vector2(-64, -32), new Vector2(128, 64), Globals.Content.GetTHBox("emptybutton"));
            button.EndHoldTime = 25000;
            button.Click += EmptyBtnEvent;
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            
            lockbutton = new LockButton(new Vector2(-192, -32), new Vector2(128, 64), Globals.Content.GetTHBox("awesomebutton"), true);
            lockbutton.Click += BtnEvent;
            shots = new List<Tuple<Laserwall, Vector2>>();
            OldMPos = new Vector2(0, 0);
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
            
            lockbutton.Draw(sp);
            cursor.Draw(sp);
            GUN.Draw(sp);
            for (int i = 0; i < shots.Count; i++) {
                shots[i].Item1.Draw(sp);
            }
        }
        float GT;
        float MGT;
        float ShotTime = 350;
        float TravelSpeed = 5;
        float UpdateSpeed = 2;
        float MaxUpdateSpeed = 10;
        float MinUpdateSpeed = 10;
        Vector2 OldMPos;
        List<int> removeItem = new List<int>();
        public override void Update(GameTime gt) {
            
            cursor.Update(gt);
            base.Update(gt);
            GUN.Update(gt);
            cursor.Position = MousePos - cursor.Size / 2;
            button.Update(gt, cursor.Hitbox[0]);
            lockbutton.Update(gt, cursor.Hitbox[0]);
            MGT += (float)gt.ElapsedGameTime.TotalMilliseconds;
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
        }
    }
}
