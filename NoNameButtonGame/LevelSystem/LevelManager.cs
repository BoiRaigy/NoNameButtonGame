﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using Raigy.Camera;

namespace NoNameButtonGame.LevelSystem
{
    class LevelManager : MonoObject
    {
        SampleLevel CurrentLevel;
        public delegate void Changewindowname(string name);
        public Changewindowname ChangeWindowName;

        public CameraClass GetCurrentCamera() {
            return CurrentLevel.camera;
        }
        int DHeight;
        int DWidth;
        Vector2 Screen;
        bool BetweenLevels = true;
        bool CanSelect = false;
        int LastLevel = 0;
        public void ChangeScreen(Vector2 Screen) {
            this.Screen = Screen;
        }
        public LevelManager(int Height, int Width, Vector2 Screen) {
            DHeight = Height;
            DWidth = Width;
            this.Screen = Screen;
        }
        public override void Draw(SpriteBatch sp) {
            if (!BetweenLevels)
                CurrentLevel.Draw(sp);
        }

        public override void Update(GameTime gt) {
            if (!BetweenLevels)
                CurrentLevel.Update(gt);
            else {
                if (!CanSelect) {
                    switch (LastLevel) {
                        case 0:
                            CurrentLevel = new Level1(DWidth, DHeight, Screen);
                            break;
                        case 1:
                            CurrentLevel = new Level2(DWidth, DHeight, Screen);
                            
                            break;
                    }
                    CurrentLevel.Finish += LevelFinish;
                    BetweenLevels = false;
                } else {
                    //LevelSelection or something
                }
                ChangeWindowName(CurrentLevel.Name);
            }
        }
        private void LevelFinish(object sender, EventArgs e) {
            BetweenLevels = true;
            LastLevel++;
        }
    }
}
