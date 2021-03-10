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
        bool RedoCall = false;
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
            string[] args = Environment.GetCommandLineArgs();
            rand = new Random();
            for (int i = 0; i < args.Length; i++) {
                if (args[i] == "-seed") {
                    if (args.Length > i + 1) {
                        if (int.TryParse(args[i + 1], out int res))
                            rand = new Random(res);
                        
                    }
                }
                    
            }
            state = MState.Startmenu;
            LastLevel = Globals.MaxLevel;
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
                    if (CanOverallSelect && !RedoCall) {
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
                case 4:
                    CurrentLevel = new Level5(DWidth, DHeight, Screen, rand);
                    break;
                case 5:
                    CurrentLevel = new Level6(DWidth, DHeight, Screen, rand);
                    break;
                case 6:
                    CurrentLevel = new Level7(DWidth, DHeight, Screen, rand);
                    break;
                case 7:
                    CurrentLevel = new Level8(DWidth, DHeight, Screen, rand);
                    break;
                case 8:
                    CurrentLevel = new Level9(DWidth, DHeight, Screen, rand);
                    break;
                case 9:
                    CurrentLevel = new Level10(DWidth, DHeight, Screen, rand);
                    break;
                case 10:
                    CurrentLevel = new Level11(DWidth, DHeight, Screen, rand);
                    break;
                case 11:
                    CurrentLevel = new Level12(DWidth, DHeight, Screen, rand);
                    break;
                case 12:
                    CurrentLevel = new Level13(DWidth, DHeight, Screen, rand);
                    break;
                case 13:
                    CurrentLevel = new Level14(DWidth, DHeight, Screen, rand);
                    break;
                case 14:
                    CurrentLevel = new Level15(DWidth, DHeight, Screen, rand);
                    break;
                case 15:
                    CurrentLevel = new Level16(DWidth, DHeight, Screen, rand);
                    break;
                case 16:
                    CurrentLevel = new Level17(DWidth, DHeight, Screen, rand);
                    break;
                case 17:
                    CurrentLevel = new Level18(DWidth, DHeight, Screen, rand);
                    break;
                case 18:
                    CurrentLevel = new Level19(DWidth, DHeight, Screen, rand);
                    break;
                case 19:
                    CurrentLevel = new Level20(DWidth, DHeight, Screen, rand);
                    break;
                case 20:
                    CurrentLevel = new Level21(DWidth, DHeight, Screen, rand);
                    break;
                case 21:
                    CurrentLevel = new Level22(DWidth, DHeight, Screen, rand);
                    break;
                case 22:
                    CurrentLevel = new Level23(DWidth, DHeight, Screen, rand);
                    break;
                case 23:
                    CurrentLevel = new Level24(DWidth, DHeight, Screen, rand);
                    break;
                case 24:
                    CurrentLevel = new Level25(DWidth, DHeight, Screen, rand);
                    break;
                case 25:
                    CurrentLevel = new Level26(DWidth, DHeight, Screen, rand);
                    break;
                case 26:
                    CurrentLevel = new Level27(DWidth, DHeight, Screen, rand);
                    break;
                case 27:
                    CurrentLevel = new Level28(DWidth, DHeight, Screen, rand);
                    break;
                case 28:
                    CurrentLevel = new Level29(DWidth, DHeight, Screen, rand);
                    break;
                case 29:
                    CurrentLevel = new Level30(DWidth, DHeight, Screen, rand);
                    break;
                case 30:
                    CurrentLevel = new Level31(DWidth, DHeight, Screen, rand);
                    break;
                case 31:
                    CurrentLevel = new Level32(DWidth, DHeight, Screen, rand);
                    break;
                case 32:
                    CurrentLevel = new Level33(DWidth, DHeight, Screen, rand);
                    break;
                case 33:
                    CurrentLevel = new Level34(DWidth, DHeight, Screen, rand);
                    break;
                case 34:
                    CurrentLevel = new Level35(DWidth, DHeight, Screen, rand);
                    break;
                case 35:
                    CurrentLevel = new Level36(DWidth, DHeight, Screen, rand);
                    break;
                case 36:
                    CurrentLevel = new Level37(DWidth, DHeight, Screen, rand);
                    break;
                case 37:
                    CurrentLevel = new Level38(DWidth, DHeight, Screen, rand);
                    break;
                case 38:
                    CurrentLevel = new Level39(DWidth, DHeight, Screen, rand);
                    break;
                case 39:
                    CurrentLevel = new Level40(DWidth, DHeight, Screen, rand);
                    break;
                case 40:
                    CurrentLevel = new Level41(DWidth, DHeight, Screen, rand);
                    break;
                case 41:
                    CurrentLevel = new Level42(DWidth, DHeight, Screen, rand);
                    break;
                case 42:
                    CurrentLevel = new Level43(DWidth, DHeight, Screen, rand);
                    break;
                case 43:
                    CurrentLevel = new Level44(DWidth, DHeight, Screen, rand);
                    break;
                case 44:
                    CurrentLevel = new Level45(DWidth, DHeight, Screen, rand);
                    break;
                case 45:
                    CurrentLevel = new Level46(DWidth, DHeight, Screen, rand);
                    break;
                case 46:
                    CurrentLevel = new Level47(DWidth, DHeight, Screen, rand);
                    break;
                case 47:
                    CurrentLevel = new Level48(DWidth, DHeight, Screen, rand);
                    break;
                case 48:
                    CurrentLevel = new Level49(DWidth, DHeight, Screen, rand);
                    break;
                case 49:
                    CurrentLevel = new Level50(DWidth, DHeight, Screen, rand);
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
                    RedoCall = true;
                    LastLevel = Globals.MaxLevel;
                    break;
                case StartScreen.ButtonPressed.LevelSelect:
                    state = MState.BetweenLevel;
                    CanOverallSelect = true;
                    RedoCall = false;
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
            if (!CanOverallSelect) {
                LastLevel++;
                if (Globals.MaxLevel < LastLevel) {
                    Globals.MaxLevel = LastLevel;
                    changesettings.Invoke(Screen, Globals.IsFix, Globals.FullScreen);
                }
            }
            RedoCall = false;

        }
        private void LevelFail(object sender, EventArgs e) {
            state = MState.BetweenLevel;
            RedoCall = true;
        }
        private void LevelReset(object sender, EventArgs e) {
            state = MState.BetweenLevel;
            RedoCall = true;
        }
    }
}
