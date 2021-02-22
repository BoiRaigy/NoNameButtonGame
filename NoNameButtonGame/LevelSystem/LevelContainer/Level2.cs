﻿using System;
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
        Cursor cursor;

        public Level2(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 2 - WHAAT?!? There is more to this Game?!";
            button = new AwesomeButton[16];
            int randI64 = rand.Next(0, 16);
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            for (int i = 0; i < button.Length; i++) {
                if (i == randI64) {
                    button[i] = new AwesomeButton(new Vector2(130 * (i % 4) - 256, (i / 4) * 68 - 128), new Vector2(128, 64), Globals.Content.GetTHBox("awesomebutton")) {
                        DrawColor = Color.White,
                    };
                    button[i].Click += BtnWinEvent;
                } else {
                    button[i] = new AwesomeButton(new Vector2(130 * (i % 4) - 256, (i / 4) * 68 - 128), new Vector2(128, 64), Globals.Content.GetTHBox("failbutton")) {
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
            cursor.Draw(sp);
        }

        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            cursor.Position = MouseIngame - cursor.Size / 2;
            for (int i = 0; i < button.Length; i++) {
                button[i].Update(gt, cursor.Hitbox[0]);
            }
        }
    }
}
