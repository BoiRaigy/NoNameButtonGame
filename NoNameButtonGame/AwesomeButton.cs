using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using Microsoft.Xna.Framework.Input;
using NoNameButtonGame.Interfaces;
namespace NoNameButtonGame
{
    class AwesomeButton : GameObject, IMouseActions
    {

        public AwesomeButton(Vector2 Size) {
            base.Size = Size;
            ImageLocation = new Rectangle(32, 0, 32, 16);
        }

        public event EventHandler Leave;
        public event EventHandler Enter;
        public event EventHandler Click;
        bool Hover;
        
        public void Update(GameTime gt, Vector2 MousePos) {
            MouseState mouseState = Mouse.GetState();
            if (rec.Intersects(new Rectangle(MousePos.ToPoint(), new Point(1, 1)))) {
                if (!Hover) {
                    Hover = true;
                    if (Enter != null)
                    Enter(this, new EventArgs());
                } 
                switch (mouseState.LeftButton) {
                    case ButtonState.Pressed:
                        if (Click != null)
                            Click(this, new EventArgs());
                        break;
                }
            } else {
                if (Hover)
                    if (Leave != null)
                        Leave(this, new EventArgs());
                Hover = false;
            }


            if (Hover) {
                ImageLocation = new Rectangle(32, 0, 32, 16);
            } else {
                ImageLocation = new Rectangle(0, 0, 32, 16);
            }



            Update(gt);
        }
    }
}
