using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using NoNameButtonGame.Text;
using NoNameButtonGame.GameObjects;
using NoNameButtonGame.BeforeMaths;
namespace NoNameButtonGame.LevelSystem.LevelContainer
{
    class SettingsScreen : SampleLevel
    {
        TextBuilder Resolution;
        TextBuilder FixedStep;
        TextBuilder Fullscreen;
        Cursor cursor;
        TextButton[] ResolutionBn;
        TextButton FixedStepBn;
        TextButton FullscreenBn;
        Vector2 VecResolution;
        public delegate void ApplySettings(Vector2 Rec, bool Step, bool Full);
        ApplySettings apply;
        public SettingsScreen(int defaultWidth, int defaultHeight, Vector2 window, Random rand, ApplySettings AS) : base(defaultWidth, defaultHeight, window, rand) {
            apply = AS;
            string s1 ="❌", s2 = "❌";
            if (Globals.IsFix)
                s1 = "✔";
            if (Globals.FullScreen)
                s2 = "✔";
            Name = "Start Menu";
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            FixedStep = new TextBuilder("FixedStep", new Vector2(-64, -0), new Vector2(16, 16), null, 0);
            Resolution = new TextBuilder("1280x720", new Vector2(-64, -64), new Vector2(16, 16), null, 0);
            Fullscreen = new TextBuilder("Fullscreen", new Vector2(-64, 64), new Vector2(16, 16), null, 0);
            ResolutionBn = new TextButton[2];
            ResolutionBn[0] = new TextButton(new Vector2(64, -72), new Vector2(40, 32), Globals.Content.GetTHBox("minibutton"), ">", ">",new Vector2(16,16));
            ResolutionBn[1] = new TextButton(new Vector2(-108, -72), new Vector2(40, 32), Globals.Content.GetTHBox("minibutton"), "<", "<", new Vector2(16, 16));
            ResolutionBn[0].Click += ChangeRes;
            ResolutionBn[1].Click += ChangeRes;
            FixedStepBn = new TextButton(new Vector2(-108, -8), new Vector2(40, 32), Globals.Content.GetTHBox("minibutton"), "IsFixedStep", s1, new Vector2(16, 16));
            FixedStepBn.Text.ChangeColor(new Color[1] { s1 == "❌" ? Color.Red : Color.Green});
            FixedStepBn.Click += ChangePressState;
            FullscreenBn = new TextButton(new Vector2(-108, 56), new Vector2(40, 32), Globals.Content.GetTHBox("minibutton"), "Fullscreen", s2, new Vector2(16, 16));
            FullscreenBn.Text.ChangeColor(new Color[1] { s2 == "❌" ? Color.Red : Color.Green });
            FullscreenBn.Click += ChangePressState;
            VecResolution = new Vector2(1280,720);

        }
        private void ChangeRes(object sender, EventArgs e) {
            
            if ((sender as TextButton).Name == ">") {
                switch (VecResolution.X + "x" + VecResolution.Y) {
                    case "1280x720":
                        VecResolution = new Vector2(1920, 1080);
                        break;
                    case "1920x1080":
                        VecResolution = new Vector2(2560, 1440);
                        break;
                    case "2560x1440":
                        VecResolution = new Vector2(3840, 2160);
                        break;
                    case "3840x2160":
                        VecResolution = new Vector2(1280, 720);
                        break;
                    
                }
            }

            if ((sender as TextButton).Name == "<") {
                switch (VecResolution.X + "x" + VecResolution.Y) {
                    case "1280x720":
                        VecResolution = new Vector2(3840, 2160);
                        break;
                    case "1920x1080":
                        VecResolution = new Vector2(1280, 720);
                        break;
                    case "2560x1440":
                        VecResolution = new Vector2(1920, 1080);
                        break;
                    case "3840x2160":
                        VecResolution = new Vector2(2560, 1440);
                        
                        break;
                }
            }
            Resolution.ChangeText(VecResolution.X + "x" + VecResolution.Y);
            apply(VecResolution, FixedStepBn.Text.Text == "✔", FullscreenBn.Text.Text == "✔");
        }
        
        private void ChangePressState(object sender, EventArgs e) {
            string s = (sender as TextButton).Text.Text;
            switch (s){
                case "❌":
                    (sender as TextButton).Text.ChangeText("✔", new Color[1] { Color.Green });
                    break;
                case "✔":
                    (sender as TextButton).Text.ChangeText("❌", new Color[1] { Color.Red });
                    break;
            }
            apply(VecResolution, FixedStepBn.Text.Text == "✔", FullscreenBn.Text.Text == "✔");
        }
        public override void Draw(SpriteBatch sp) {
            
            FixedStep.Draw(sp);
            Resolution.Draw(sp);
            Fullscreen.Draw(sp);
            for (int i = 0; i < ResolutionBn.Length; i++) {
                ResolutionBn[i].Draw(sp);
            }
            FixedStepBn.Draw(sp);
            FullscreenBn.Draw(sp);
            cursor.Draw(sp);
        }
        public override void Update(GameTime gt) {
            base.Update(gt);
            cursor.Position = MousePos;
            cursor.Update(gt);
            FixedStep.Update(gt);
            Resolution.Update(gt);
            Fullscreen.Update(gt);
            for (int i = 0; i < ResolutionBn.Length; i++) {
                ResolutionBn[i].Update(gt,cursor.Hitbox[0]);
            }
            FixedStepBn.Update(gt, cursor.Hitbox[0]);
            FullscreenBn.Update(gt, cursor.Hitbox[0]);
        }
    }
}
