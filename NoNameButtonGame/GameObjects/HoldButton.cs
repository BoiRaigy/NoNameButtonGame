using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using Microsoft.Xna.Framework.Input;
using NoNameButtonGame.Interfaces;
using NoNameButtonGame.BeforeMaths;
using Raigy.Input;
using NoNameButtonGame.Text;

namespace NoNameButtonGame.GameObjects
{
    class HoldButton : GameObject, IMouseActions, IHitbox
    {
        public HoldButton(Vector2 Pos, Vector2 Size, THBox box) {
            base.Size = Size;
            Position = Pos;
            ImageLocation = new Rectangle((int)box.Imagesize.X, 0, (int)box.Imagesize.X, (int)box.Imagesize.Y);
            FrameSize = box.Imagesize;
            hitbox = new Rectangle[box.Hitbox.Length];
            Texture = box.Texture;
            Scale = new Vector2(Size.X / FrameSize.X, Size.Y / FrameSize.Y);
            hitbox = box.Hitbox;
            text = new TextBuilder("test", new Vector2(float.MinValue,float.MinValue), new Vector2(16, 16), null, 0);
            
            
            IGhitbox = new Rectangle[hitbox.Length];
            for (int i = 0; i < box.Hitbox.Length; i++) {
                IGhitbox[i] = new Rectangle((int)(Position.X + (box.Hitbox[i].X * Scale.X)), (int)(Position.Y + (box.Hitbox[i].Y * Scale.Y)), (int)(box.Hitbox[i].Width * Scale.X), (int)(box.Hitbox[i].Height * Scale.Y));
            }
            
        }
        
        public event EventHandler Leave;
        public event EventHandler Enter;
        public event EventHandler Click;
        bool Hover;
        float HoldTime = 0F;
        public float EndHoldTime = 10000F;
        Rectangle[] hitbox;
        Rectangle[] IGhitbox;
        Vector2 Scale;
        
        TextBuilder text;
        public Rectangle[] Hitbox {
            get => IGhitbox;
        }

        public bool HitboxCheck(Rectangle rec) {
            for (int i = 0; i < Hitbox.Length; i++) {
                if (Hitbox[i].Intersects(rec)) {
                    return true;
                }
            }
            return false;
        }
        private void UpdateHitbox() {
            Scale = new Vector2(Size.X / FrameSize.X, Size.Y / FrameSize.Y);
            for (int i = 0; i < hitbox.Length; i++) {
                IGhitbox[i] = new Rectangle((int)(Position.X + (hitbox[i].X * Scale.X)), (int)(Position.Y + (hitbox[i].Y * Scale.Y)), (int)(hitbox[i].Width * Scale.X), (int)(hitbox[i].Height * Scale.Y));
            }
        }
        public void Update(GameTime gt, Rectangle MousePos) {
            MouseState mouseState = Mouse.GetState();
            if (HitboxCheck(MousePos)) {
                if (!Hover) {
                    Hover = true;
                    if (Enter != null)
                        Enter(this, new EventArgs());
                }
                if (InputReaderMouse.CheckKey(InputReaderMouse.MouseKeys.Left, false)) {
                    HoldTime += gt.ElapsedGameTime.Milliseconds;
                    if (HoldTime > EndHoldTime)
                        Click(this, new EventArgs());
                } else {
                    HoldTime -= gt.ElapsedGameTime.Milliseconds / 2;
                }
            } else {
                if (Hover)
                    if (Leave != null)
                        Leave(this, new EventArgs());
                Hover = false;
                HoldTime -= gt.ElapsedGameTime.Milliseconds / 2;
                
            }
            if (HoldTime < 0) {
                HoldTime = 0;
            }
                
            if (Hover) {
                ImageLocation = new Rectangle((int)FrameSize.X, 0, (int)FrameSize.X, (int)FrameSize.Y);
            } else {
                ImageLocation = new Rectangle(0, 0, (int)FrameSize.X, (int)FrameSize.Y);
            }
            UpdateHitbox();
            text.ChangeText((((EndHoldTime - HoldTime) / 1000).ToString("0.0") +"s").Replace(',','.'));
            
            text.Position = rec.Center.ToVector2() - text.rec.Size.ToVector2() / 2;
            text.Position.Y -= 32;
            text.Update(gt);
            Update(gt);
        }
        
        public override void Draw(SpriteBatch sp) {
            base.Draw(sp);
            text.Draw(sp);
            
        }
    }
}
