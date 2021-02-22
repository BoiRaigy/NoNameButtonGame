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
    class Level3 : SampleLevel
    {

        AwesomeButton button;
        Cursor cursor;
        DontTouch[] dt;
        HoldButton hold;
        public Level3(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 3 - Tutorial time!";
            button = new AwesomeButton(new Vector2(-64, -0), new Vector2(128, 64), Globals.Content.GetTHBox("failbutton")) {
                DrawColor = Color.White,
            };
            button.Click += BtnEvent;

            hold = new HoldButton(new Vector2(200, -0), new Vector2(128, 64), Globals.Content.GetTHBox("failbutton")) {
                DrawColor = Color.White,
            };
            hold.Click += BtnEvent;
            int clustSize = 32;
            Vector2 clustPos = new Vector2(-250, -150);
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            dt = new DontTouch[25];
            for (int i = 0; i < dt.Length; i++) {
                dt[i] = new DontTouch(new Vector2(clustPos.X + clustSize * (i%5), clustPos.Y + clustSize * (i / 5)), new Vector2(clustSize, clustSize), Globals.Content.GetTHBox("zone"));
                dt[i].Enter += BtnEvent;
            }
           
        }



        private void BtnEvent(object sender, EventArgs e) {
           CallFail();
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            hold.Draw(sp);
            for (int i = 0; i < dt.Length; i++) {
                dt[i].Draw(sp);
            }
            
            cursor.Draw(sp);
            
        }

        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            for (int i = 0; i < dt.Length; i++) {
                dt[i].Update(gt, cursor.Hitbox[0]);
            }
            
            cursor.Position = MouseIngame - cursor.Size / 2;
            button.Update(gt, cursor.Hitbox[0]);
            hold.Update(gt, cursor.Hitbox[0]);
        }
    }
}