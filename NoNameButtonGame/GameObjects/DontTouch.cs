using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using NoNameButtonGame.BeforeMaths;
using NoNameButtonGame.Interfaces;
using Microsoft.Xna.Framework.Input;
using Raigy.Input;

namespace NoNameButtonGame.GameObjects
{
    class DontTouch: GameObject, IHitbox, IMouseActions
    {
        public DontTouch(Vector2 Pos, Vector2 Size, THBox box) {
            base.Size = Size;
           
            Position = Pos;
            FrameSize = box.Imagesize;
            hitbox = box.Hitbox;
            if (Size.X % 32 != 0 || Size.Y % 32 != 0) {
                FrameSize = FrameSize / 32 * Size;
                for (int i = 0; i < hitbox.Length; i++) {
                    hitbox[i].Size = FrameSize.ToPoint();
                }
            }
            Texture = box.Texture;
            FrameMax = box.Aniframes;
            
            ImageLocation = new Rectangle(0, 0, (int)box.Imagesize.X, (int)box.Imagesize.Y);
            IGhitbox = new Rectangle[hitbox.Length];
            DrawColor = Color.White;
            for (int i = 0; i < box.Hitbox.Length; i++) {
                IGhitbox[i] = new Rectangle((int)(Position.X + (box.Hitbox[i].X * Scale.X)), (int)(Position.Y + (box.Hitbox[i].Y * Scale.Y)), (int)(box.Hitbox[i].Width * Scale.X), (int)(box.Hitbox[i].Height * Scale.Y));
            }
        }
        Rectangle[] hitbox;
        Rectangle[] IGhitbox;
        Vector2 Scale;
        int FramePos = 0;
        int FrameMax = 0;
        int FrameSpeed = 200;
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

        public event EventHandler Leave;
        public event EventHandler Enter;
        public event EventHandler Click;
       
        public override void Draw(SpriteBatch sp) {
            base.Draw(sp);
        }
        float GT;
        public void Update(GameTime gt, Rectangle MousePos) {
            MouseState mouseState = Mouse.GetState();
            GT += gt.ElapsedGameTime.Milliseconds;
            while(GT > FrameSpeed) {
                GT -= FrameSpeed;
                FramePos++;
                if (FramePos == FrameMax) FramePos = 0;
                ImageLocation = new Rectangle(0, FramePos * (int)FrameSize.X, (int)FrameSize.X, (int)FrameSize.Y);
            }
            if (HitboxCheck(MousePos)) {
                Enter(this, new EventArgs());
            }

            UpdateHitbox();
            base.Update(gt);
        }
    }
}
