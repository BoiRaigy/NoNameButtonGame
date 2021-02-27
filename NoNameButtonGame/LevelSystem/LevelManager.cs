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
        StartScreen startScreen;
        SettingsScreen settings;
        public delegate void Changewindowname(string name);
        public Changewindowname ChangeWindowName;

        public CameraClass GetCurrentCamera() {
            return state switch {
                MState.Settings => settings.Camera,
                MState.Startmenu => startScreen.Camera,
                MState.BetweenLevel => new CameraClass(Screen),
                _ => CurrentLevel.Camera,
            };
        }
            
        int DHeight;
        int DWidth;
        Random rand;
        Vector2 Screen;
        bool CanOverallSelect = true;
        MState state;
        SettingsScreen.ApplySettings changesettings;
        int LastLevel = 0;
        public void ChangeScreen(Vector2 Screen) {
            this.Screen = Screen;
            if (CurrentLevel != null)
                CurrentLevel.SetScreen(Screen);
        }
        public LevelManager(int Height, int Width, Vector2 Screen, SettingsScreen.ApplySettings changesettings) {
            this.changesettings = changesettings;
            DHeight = Height;
            DWidth = Width;
            this.Screen = Screen;
            rand = new Random();
            state = MState.Startmenu;
            startScreen = new StartScreen(Width, Height, Screen, rand);
            startScreen.Finish += ExitStartScreen;
            settings = new SettingsScreen(Width, Height, Screen, rand, changesettings);
        }
        enum MState
        {
            Settings,
            Startmenu,
            Level,
            BetweenLevel,
            LevelSelect,
        }
        public override void Draw(SpriteBatch sp) {
            switch (state) {
                case MState.Settings:
                    settings.Draw(sp);
                    break;
                case MState.Startmenu:
                    startScreen.Draw(sp);
                    break;
                case MState.Level:
                    CurrentLevel.Draw(sp);
                    break;
                case MState.BetweenLevel:
                    break;
                case MState.LevelSelect:
                    CurrentLevel.Draw(sp);
                    break;
            }
        }
        public override void Update(GameTime gt) {
            if (InputReaderKeyboard.CheckKey(Microsoft.Xna.Framework.Input.Keys.Escape, true)) {
                MState save = state;
                switch (state) {
                case MState.Settings:
                    case MState.Level:
                    case MState.LevelSelect:
                        state = MState.Startmenu;
                        startScreen = new StartScreen(DWidth, DHeight, Screen, rand);
                        startScreen.Finish += ExitStartScreen;
                        break;
                    
                }
            }
            ChangeWindowName((CurrentLevel ?? new SampleLevel(DWidth, DHeight, Screen, rand) { Name = "NoNameButtonGame" }).Name);
            switch (state) {
                case MState.Settings:
                    settings.Update(gt);
                    ChangeWindowName(settings.Name);
                    break;
                case MState.Startmenu:
                    startScreen.Update(gt);
                    ChangeWindowName(startScreen.Name);
                    break;
                case MState.Level:
                    CurrentLevel.Update(gt);
                    break;
                case MState.LevelSelect:
                    CurrentLevel.Update(gt);
                    break;
                case MState.BetweenLevel:
                    InputReaderMouse.CheckKey(InputReaderMouse.MouseKeys.Left, true);
                    if (CanOverallSelect) {
                        CurrentLevel = new LevelSelect(DWidth, DHeight, Screen, rand);
                        CurrentLevel.Finish += LevelSelected;
                        state = MState.LevelSelect;
                    } else {
                        SelectLevel(LastLevel);
                        state = MState.Level;
                    }
                    break;
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
        }
        private void ExitStartScreen(object sender, EventArgs e) {
            StartScreen.ButtonPressed action = (sender as StartScreen).Action;
            switch (action) {
                case StartScreen.ButtonPressed.Start:
                    CanOverallSelect = false;
                    state = MState.BetweenLevel;
                    break;
                case StartScreen.ButtonPressed.LevelSelect:
                    state = MState.BetweenLevel;
                    CanOverallSelect = true;
                    break;
                case StartScreen.ButtonPressed.Settings:
                    state = MState.Settings;
                    settings = new SettingsScreen(DWidth, DHeight, Screen, rand, changesettings);
                    break;
                case StartScreen.ButtonPressed.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
       
        private void LevelSelected(object sender, EventArgs e) {
            LastLevel = int.Parse((sender as GameObjects.TextButton).Name) - 1;
            SelectLevel(LastLevel);
            state = MState.Level;
        }
        private void LevelFinish(object sender, EventArgs e) {

            state = MState.BetweenLevel;
            if (!CanOverallSelect)
                LastLevel++;

        }
        private void LevelFail(object sender, EventArgs e) {
            state = MState.BetweenLevel;
        }
        private void LevelReset(object sender, EventArgs e) {
            state = MState.BetweenLevel;
        }
    }
}
