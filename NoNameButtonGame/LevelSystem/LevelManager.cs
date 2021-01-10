using System;
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
        public CameraClass GetCurrentCamera() {
            return CurrentLevel.camera;
        }
        int DHeight;
        int DWidth;
        Vector2 Screen;
        public void ChangeScreen(Vector2 Screen) {
            this.Screen = Screen;
        }
        public LevelManager(int Height,int Width, Vector2 Screen) {
            DHeight = Height;
            DWidth = Width;
            this.Screen = Screen;
            CurrentLevel = new Level1(DWidth,DHeight,Screen);
        }
        public override void Draw(SpriteBatch sp) {
            CurrentLevel.Draw(sp);
        }

        public override void Update(GameTime gt) {
            CurrentLevel.Update(gt);
        }
    }
}
