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
using NoNameButtonGame.color;
namespace NoNameButtonGame.LevelSystem.LevelContainer
{
    class Level3 : SampleLevel
    {

        AwesomeButton button;
        Cursor cursor;
        HoldButton hold;
        TextBuilder Info;
        Rainbow raincolor;
        Laserwall laserwall;
        StateButton statebutton;
        LockButton lockbutton;
        float GT;
        public Level3(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 3 - Tutorial time!";
            button = new AwesomeButton(new Vector2(-64, -0), new Vector2(128, 64), Globals.Content.GetTHBox("awesomebutton")) {
                DrawColor = Color.White,
            };
            button.Click += CallFinish;

            hold = new HoldButton(new Vector2(200, -0), new Vector2(128, 64), Globals.Content.GetTHBox("failbutton")) {
                DrawColor = Color.White,
            };
            hold.Click += BtnEvent;

            statebutton = new StateButton(new Vector2(200, 75), new Vector2(128, 64), Globals.Content.GetTHBox("failbutton"), 100) {
                DrawColor = Color.White,
            };
            statebutton.Click += BtnEvent;


            lockbutton = new LockButton(new Vector2(64, 75), new Vector2(128, 64), Globals.Content.GetTHBox("failbutton"), true) {
                DrawColor = Color.White,
            };
            lockbutton.Click += BtnEvent;
            Vector2 clustPos = new Vector2(-250, -150);
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            
            Info = new TextBuilder("memebig? => bigindeed!", new Vector2(-0, 150), new Vector2(16, 16), null, 0);
            raincolor = new Rainbow {
                Increment = 32,
                Speed = 32,
                Offset = 256
            };
            laserwall = new Laserwall(new Vector2(-128, -128), new Vector2(128, 96), Globals.Content.GetTHBox("zonenew"));
            laserwall.Enter += BtnEvent;
        }


        
        private void BtnEvent(object sender, EventArgs e) {
            CallFail();
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            hold.Draw(sp);
            Info.Draw(sp);
            laserwall.Draw(sp);
            lockbutton.Draw(sp);
            statebutton.Draw(sp);
            cursor.Draw(sp);
        }

        public override void Update(GameTime gt) {
            GT += gt.ElapsedGameTime.Milliseconds;
            while(GT > 125) {
                GT -= 125;
                //laserwall.Move(new Vector2(1, 0));
            }
            cursor.Update(gt);
            raincolor.Update(gt);
            Info.ChangeColor(raincolor.GetColor(Info.Text.Length));
            Info.Update(gt);
            base.Update(gt);
            
            laserwall.Update(gt, cursor.Hitbox[0]);
            lockbutton.Update(gt, cursor.Hitbox[0]);
            cursor.Position = MousePos - cursor.Size / 2;
            button.Update(gt, cursor.Hitbox[0]);
            hold.Update(gt, cursor.Hitbox[0]);
            statebutton.Update(gt, cursor.Hitbox[0]);
        }
    }
}
