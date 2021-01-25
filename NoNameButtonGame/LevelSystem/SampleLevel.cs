using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using Raigy.Camera;
using NoNameButtonGame.Interfaces;

using Microsoft.Xna.Framework.Input;
namespace NoNameButtonGame.LevelSystem
{
    class SampleLevel : MonoObject, ILevel
    {
        public event EventHandler Fail;
        public event EventHandler Reset;
        public event EventHandler Finish;
        public CameraClass camera;
        public Vector2 CamPos;
        public int DefaultWidth;
        public int DefaultHeight;
        public Vector2 Window;
        public Vector2 MouseIngame;
        public string Name;
        Random rand;
        public SampleLevel(int defaultWidth, int defaultHeight, Vector2 window, Random rand) {
            DefaultWidth = defaultWidth;
            DefaultHeight = defaultHeight;
            Window = window;
            this.rand = rand;
            camera = new CameraClass(new Vector2(DefaultWidth, DefaultHeight));
        }

        public override void Draw(SpriteBatch sp) {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gt) {
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Right, true)) {
                CamPos.X += 45;
            }
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Left, true)) {
                CamPos.X -= 45;
            }
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Up, true)) {
                CamPos.Y -= 45;
            }
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Down, true)) {
                CamPos.Y += 45;
            }
            camera.Update(CamPos, new Vector2(0, 0));
            MouseState mouseState = Mouse.GetState();
            Vector2 VecMouse = mouseState.Position.ToVector2();
            float TargetScreenDifX = Window.X / DefaultWidth;
            float TargetScreenDifY = Window.Y / DefaultHeight;
            Vector2 VMP = new Vector2(VecMouse.X / TargetScreenDifX, VecMouse.Y / TargetScreenDifY);
            MouseIngame = new Vector2(VMP.X / camera.Zoom + CamPos.X - (DefaultWidth / camera.Zoom) / 2, VMP.Y / camera.Zoom + CamPos.Y - (DefaultHeight / camera.Zoom) / 2);
        }

        public virtual void SetScreen(Vector2 Screen) {
            Window = Screen;
        }
        public virtual void CallFinish() {
            if (Finish != null && Finish.GetInvocationList().Length > 0)
                Finish(this, new EventArgs());
        }
        public virtual void CallFail() {
            if (Fail != null && Fail.GetInvocationList().Length > 0)
                Fail(this, new EventArgs());
        }
        public virtual void CallReset() {
            if (Reset != null && Reset.GetInvocationList().Length > 0)
                Reset(this, new EventArgs());
        }
    }
}
