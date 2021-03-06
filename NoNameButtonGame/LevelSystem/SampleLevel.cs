using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using Raigy.Camera;
using NoNameButtonGame.Interfaces;
using NoNameButtonGame.GameObjects;
using Microsoft.Xna.Framework.Input;
namespace NoNameButtonGame.LevelSystem
{
    class SampleLevel : MonoObject, ILevel
    {
        public event EventHandler Fail;
        public event EventHandler Reset;
        public event EventHandler Finish;

        public int DefaultWidth;
        public int DefaultHeight;
        public Vector2 Window;
        public CameraClass Camera;
        public Vector2 CamPos;
        public string Name;
        public Vector2 MousePos;
        public Rectangle CamRec;
        bool Ani = true;
        bool OutAni = false;
        int OutMathValue = 90;
        int OutMath;
        Random rand;
        bool Started = false;
        Lstate Lthis;
        enum Lstate
        {
            Fail,
            Win,
            Reset,
        }
        public SampleLevel(int defaultWidth, int defaultHeight, Vector2 window, Random rand) {
            DefaultWidth = defaultWidth;
            DefaultHeight = defaultHeight;
            CamPos = new Vector2(0, -2291.5984F);
            OutMath = OutMathValue;
            Window = window;
            this.rand = rand;
            Camera = new CameraClass(new Vector2(defaultWidth, defaultHeight));
            
        }

        public override void Draw(SpriteBatch sp) {
            throw new NotImplementedException();
        }
        float OutGT;
        public override void Update(GameTime gt) {
            if (Ani || OutAni) {
                OutGT += (float)gt.ElapsedGameTime.TotalMilliseconds;

                while (OutGT > 8) {
                    OutGT -= 8;
                    Vector2 SinWaveRoute = new Vector2(0, 40F * (float)Math.Sin((float)OutMath / OutMathValue * (Math.PI * 1 )));
                    OutMath--;
                    if (!OutAni)
                        CamPos += SinWaveRoute;
                    else
                        CamPos -= SinWaveRoute;

                    if (OutMath == 0) {
                        Ani = false;
                        OutMath = OutMathValue;
                        if (OutAni) {
                            OutAni = false;
                            switch (Lthis) {
                                case Lstate.Fail:
                                    Fail(send ?? this, args ?? new EventArgs());
                                    break;
                                case Lstate.Win:
                                    Finish(send ?? this, args ?? new EventArgs());
                                    break;
                                case Lstate.Reset:
                                    Reset(send ?? this, args ?? new EventArgs());
                                    break;
                                default:
                                    break;
                            }
                            
                        } else {
                            CamPos = Vector2.Zero;
                        }
                        if (!Started) {
                            Mouse.SetPosition((int)Window.X / 2, (int)Window.Y / 2);
                            Started = true;
                        }
                            
                    }
                }

            }

            {

                Camera.Update(CamPos, new Vector2(0, 0));
                CamRec = new Rectangle((CamPos - new Vector2(DefaultWidth, DefaultHeight)).ToPoint(), new Point(DefaultWidth * 2, DefaultHeight * 2));
                if (!(Ani || OutAni)) {
                    MouseState mouseState = Mouse.GetState();
                    Vector2 VecMouse = mouseState.Position.ToVector2();
                    float TargetScreenDifX = Window.X / DefaultWidth;
                    float TargetScreenDifY = Window.Y / DefaultHeight;
                    Vector2 VMP = new Vector2(VecMouse.X / TargetScreenDifX, VecMouse.Y / TargetScreenDifY);
                    MousePos = new Vector2(VMP.X / Camera.Zoom + CamPos.X - (DefaultWidth / Camera.Zoom) / 2, VMP.Y / Camera.Zoom + CamPos.Y - (DefaultHeight / Camera.Zoom) / 2);

                }
            }
            //if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Right, true)) {
            //    CamPos.X += 45;
            //}
            //if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Left, true)) {
            //    CamPos.X -= 45;
            //}
            //if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Up, true)) {
            //    CamPos.Y -= 45;
            //}
            //if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Down, true)) {
            //    CamPos.Y += 45;
            //}
        }
        object send;
        EventArgs args;
        public virtual void SetScreen(Vector2 Screen) {
            Window = Screen;
        }
        public virtual void CallFinish() {
            if (Finish != null && Finish.GetInvocationList().Length > 0) {
                OutAni = true;
                Lthis = Lstate.Win;
            }
                
        }
        public virtual void CallFail() {
            if (Fail != null && Fail.GetInvocationList().Length > 0) {
                OutAni = true;
                Lthis = Lstate.Fail;
            }

        }
        public virtual void CallReset() {
            if (Reset != null && Reset.GetInvocationList().Length > 0) {
                OutAni = true;
                Lthis = Lstate.Reset;
            }

        }
        public virtual void CallFinish(object s, EventArgs e) {
            if (Finish != null && Finish.GetInvocationList().Length > 0) {
                send = s;
                args = e;
                OutAni = true;
                Lthis = Lstate.Win;
            }

        }
        public virtual void CallFail(object s, EventArgs e) {
            if (Fail != null && Fail.GetInvocationList().Length > 0) {
                send = s;
                args = e;
                OutAni = true;
                Lthis = Lstate.Fail;
            }

        }
        public virtual void CallReset(object s, EventArgs e) {
            if (Reset != null && Reset.GetInvocationList().Length > 0) {
                send = s;
                args = e;
                OutAni = true;
                Lthis = Lstate.Reset;
            }

        }
    }
}
