using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using Microsoft.Xna.Framework.Input;
using NoNameButtonGame.Interfaces;
using NoNameButtonGame.BeforeMaths;
namespace NoNameButtonGame
{
    class AwesomeButton : GameObject, IMouseActions, IHitbox
    {

        public AwesomeButton(Vector2 Pos, Vector2 Size, THBox box) {
            base.Size = Size;
            Position = Pos;
            ImageLocation = new Rectangle((int)box.Imagesize.X, 0, (int)box.Imagesize.X, (int)box.Imagesize.Y);
            FrameSize = box.Imagesize;
            hitbox = new Rectangle[box.Hitbox.Length];
            Texture = box.Texture;
            Vector2 Scale = new Vector2(Size.X / FrameSize.X, Size.Y / FrameSize.Y);
            for (int i = 0; i < box.Hitbox.Length; i++) {
                hitbox[i] = new Rectangle((int)(Position.X + (box.Hitbox[i].X * Scale.X)), (int)(Position.Y + (box.Hitbox[i].Y * Scale.Y)), (int)(box.Hitbox[i].Width * Scale.X), (int)(box.Hitbox[i].Height * Scale.Y));
            }
        }

        public event EventHandler Leave;
        public event EventHandler Enter;
        public event EventHandler Click;
        bool Hover;
        
        Rectangle[] hitbox;
        public Rectangle[] Hitbox {
            get => hitbox;
        }

        public bool HitboxCheck(Rectangle rec) {
            for (int i = 0; i < Hitbox.Length; i++) {
                if (Hitbox[i].Intersects(rec)) {
                    return true;
                }
            }
            return false;
        }
        public void Update(GameTime gt, Vector2 MousePos) {
            MouseState mouseState = Mouse.GetState();
            if (HitboxCheck(new Rectangle(MousePos.ToPoint(), new Point(1, 1)))) {
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
