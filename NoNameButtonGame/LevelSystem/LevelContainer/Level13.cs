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
using NoNameButtonGame.Text;

namespace NoNameButtonGame.LevelSystem.LevelContainer
{
    class Level13 : SampleLevel
    {

        AwesomeButton button;
        Cursor cursor;
        Laserwall wallup;
        Laserwall walldown;
        public Level13(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 13 - Swap time.";
            button = new AwesomeButton(new Vector2(-256, -0), new Vector2(128, 64), Globals.Content.GetTHBox("awesomebutton"));
            button.Click += CallFinish;
            cursor = new Cursor(new Vector2(0, 32), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
             wallup = new Laserwall(new Vector2(-(defaultWidth / Camera.Zoom), -defaultHeight - 40),new Vector2(DefaultWidth, defaultHeight), Globals.Content.GetTHBox("zonenew"));
             walldown = new Laserwall(new Vector2(-(defaultWidth / Camera.Zoom), 40), new Vector2(DefaultWidth, defaultHeight), Globals.Content.GetTHBox("zonenew"));
            wallup.Enter += CallFail;
            walldown.Enter += CallFail;
        }
        
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            wallup.Draw(sp);
            walldown.Draw(sp);
            cursor.Draw(sp);
        }

        float Multiplier = 100;
        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            double angle = gt.TotalGameTime.Milliseconds / 1000F * Math.PI * 2;
            cursor.Position = new Vector2(Multiplier * (float)Math.Sin(angle), Multiplier * (float)Math.Cos(angle));
            button.Position = MousePos - button.Size / 2;
            wallup.Update(gt, button.rec);
            walldown.Update(gt, button.rec);
            button.Update(gt, cursor.Hitbox[0]);

        }
    }
}
