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
    class Level24 : SampleLevel
    {

        AwesomeButton button;
        Cursor cursor;
        TextBuilder[] Infos;
        Laserwall[] WallLeft;
        Laserwall[] WallRight;
        Laserwall[] Blocks;
        int WallLength = 10;

        TextBuilder GUN;

        List<Tuple<Laserwall, Vector2>> shots;
        public Level24(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 24 - now with a gun";
            button = new AwesomeButton(new Vector2(-256, -0), new Vector2(128, 64), Globals.Content.GetTHBox("awesomebutton"));
            button.Click += BtnEvent;
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            Infos = new TextBuilder[2];
            Infos[0] = new TextBuilder("wow you did it!", new Vector2(120, -132), new Vector2(8, 8), null, 0);
            Infos[1] = new TextBuilder("gg!", new Vector2(120, -100), new Vector2(8, 8), null, 0);
            WallLeft = new Laserwall[WallLength];
            WallRight = new Laserwall[WallLength];
            List<Laserwall> Walls = new List<Laserwall>();
            for (int i = 0; i < WallLength; i++) {
                WallLeft[i] = new Laserwall(new Vector2(96, 512 * i), new Vector2(416, 512), Globals.Content.GetTHBox("zonenew"));
                WallRight[i] = new Laserwall(new Vector2(-512, 512 * i), new Vector2(416, 512), Globals.Content.GetTHBox("zonenew"));
                WallRight[i].Enter += CallFail;
                WallLeft[i].Enter += CallFail;
            }
            for (int i = 0; i < (WallLength - 1) * 8 / 2; i++) {
                int c = rand.Next(0, 3);
                if (c != 0)
                    Walls.Add(new Laserwall(new Vector2(-96, 512 + 128 * i), new Vector2(64, 64), Globals.Content.GetTHBox("zonenew")));
                if (c != 1)
                    Walls.Add(new Laserwall(new Vector2(-32, 512 + 128 * i), new Vector2(64, 64), Globals.Content.GetTHBox("zonenew")));
                if (c != 2)
                    Walls.Add(new Laserwall(new Vector2(32, 512 + 128 * i), new Vector2(64, 64), Globals.Content.GetTHBox("zonenew")));

                // Walls.Add(new Laserwall(new Vector2(-96 + rand.Next(0, 3) * 64, 512 + 128 * i), new Vector2(64, 64), Globals.Content.GetTHBox("zonenew")));

            }
            Blocks = Walls.ToArray();
            for (int i = 0; i < Blocks.Length; i++) {
                Blocks[i].Enter += CallFail;
            }
            shots = new List<Tuple<Laserwall, Vector2>>();
            GUN = new TextBuilder("AGUN", new Vector2(-256, 0), new Vector2(16, 16), null, 0);
        }



        private void BtnEvent(object sender, EventArgs e) {
            CallFinish(sender, e);
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            for (int i = 0; i < Infos.Length; i++) {
                Infos[i].Draw(sp);
            }
            for (int i = 0; i < WallLength; i++) {
                if (WallLeft[i].rec.Intersects(CamRec))
                    WallLeft[i].Draw(sp);
                if (WallRight[i].rec.Intersects(CamRec))
                    WallRight[i].Draw(sp);

            }
            for (int i = 0; i < Blocks.Length; i++) {
                if (Blocks[i].rec.Intersects(CamRec))
                    Blocks[i].Draw(sp);
            }
            GUN.Draw(sp);
            for (int i = 0; i < shots.Count; i++) {
                shots[i].Item1.Draw(sp);
            }
            cursor.Draw(sp);
        }
        float GT;

        float GT2;
        float MGT;
        float ShotTime = 500;
        float TravelSpeed = 2;
        float UpdateSpeed = 2;
        float MaxUpdateSpeed = 128;
        float MinUpdateSpeed = 128;
        Vector2 OldMPos;
        List<int> removeItem = new List<int>();
        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            GUN.Update(gt);
            for (int i = 0; i < Infos.Length; i++) {
                Infos[i].Update(gt);
            }
            GT += (float)gt.ElapsedGameTime.TotalMilliseconds;
            while (GT > 8) {
                GT -= 8;
                for (int i = 0; i < WallLength; i++) {
                    WallLeft[i].Move(new Vector2(0, -2));
                    WallRight[i].Move(new Vector2(0, -2));
                }
                for (int i = 0; i < Blocks.Length; i++) {
                    Blocks[i].Move(new Vector2(0, -2));
                }
            }

            MGT += (float)gt.ElapsedGameTime.TotalMilliseconds;
            while (MGT > UpdateSpeed) {
                MGT -= UpdateSpeed;
                for (int i = 0; i < shots.Count; i++) {
                    shots[i].Item1.Move(shots[i].Item2 * TravelSpeed);
                }
                GT2 += (float)gt.ElapsedGameTime.TotalMilliseconds;
                while (GT2 > ShotTime) {
                    GT2 -= ShotTime;
                    Vector2 Dir = cursor.Hitbox[0].Center.ToVector2() - GUN.rec.Center.ToVector2();
                    Laserwall ls = new Laserwall(GUN.Position, new Vector2(16, 8), Globals.Content.GetTHBox("zonenew"));
                    ls.DrawColor = Color.Green;
                    shots.Add(new Tuple<Laserwall, Vector2>(ls, Dir / Dir.Length()));
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
            cursor.Position = MousePos - cursor.Size / 2;
            button.Update(gt, cursor.Hitbox[0]);
            for (int i = 0; i < WallLength; i++) {
                WallLeft[i].Update(gt, cursor.Hitbox[0]);
                WallRight[i].Update(gt, cursor.Hitbox[0]);
            }
            for (int i = 0; i < Blocks.Length; i++) {
                Blocks[i].Update(gt, cursor.Hitbox[0]);
            }
            OldMPos = MousePos;
        }
    }
}
