using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using Raigy.Camera;
using NoNameButtonGame.LevelSystem.LevelContainer;
using Raigy.Input;

namespace NoNameButtonGame.LevelSystem
{
    class LevelManager : MonoObject
    {
        SampleLevel CurrentLevel;
        public delegate void Changewindowname(string name);
        public Changewindowname ChangeWindowName;

        public CameraClass GetCurrentCamera() {
            return CurrentLevel.Camera;
        }
        int DHeight;
        int DWidth;
        Random rand;
        Vector2 Screen;
        bool BetweenLevels = true;
        bool CanSelect = false;
        bool CanOverallSelect = true;
        int LastLevel = 2;
        public void ChangeScreen(Vector2 Screen) {
            this.Screen = Screen;
            if (CurrentLevel != null)
            CurrentLevel.SetScreen(Screen);
        }
        public LevelManager(int Height, int Width, Vector2 Screen) {
            DHeight = Height;
            DWidth = Width;
            this.Screen = Screen;
            rand = new Random();
            if (CanOverallSelect)
                CanSelect = true;
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
                    InputReaderMouse.CheckKey(InputReaderMouse.MouseKeys.Left, true); //To stop hold button instant reset shenanigans
                    SelectLevel(LastLevel);
                } else {
                    CurrentLevel = new LevelSelect(DWidth, DHeight, Screen, rand);
                    CurrentLevel.Finish += LevelSelected;
                    BetweenLevels = false;
                }
                ChangeWindowName(CurrentLevel.Name);
            }
        }
        private void SelectLevel(int LL) {
            switch (LL) {
                case 0:
                    CurrentLevel = new Level1(DWidth, DHeight, Screen, rand);
                    break;
                case 1:
                    CurrentLevel = new Level2(DWidth, DHeight, Screen, rand);
                    break;
                case 2:
                    CurrentLevel = new Level3(DWidth, DHeight, Screen, rand);
                    break;
                case 3:
                    CurrentLevel = new Level4(DWidth, DHeight, Screen, rand);
                    break;
            }
            CurrentLevel.Finish += LevelFinish;
            CurrentLevel.Fail += LevelFail;
            CurrentLevel.Reset += LevelReset;
            BetweenLevels = false;
        }
        private void LevelSelected(object sender, EventArgs e) {
            LastLevel = int.Parse((sender as GameObjects.TextButton).Name) - 1;
            BetweenLevels = true;
            CanSelect = false;
            SelectLevel(LastLevel);
        }
        private void LevelFinish(object sender, EventArgs e) {
            BetweenLevels = true;
            if (CanOverallSelect)
                CanSelect = true;
            LastLevel++;
        }
        private void LevelFail(object sender, EventArgs e) {
            BetweenLevels = true;
            if (!CanOverallSelect)
                LastLevel--;

        }
        private void LevelReset(object sender, EventArgs e) {
            BetweenLevels = true;
        }
    }
}
