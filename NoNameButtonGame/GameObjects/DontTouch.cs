using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
using NoNameButtonGame.BeforeMaths;
using NoNameButtonGame.Interfaces;

namespace NoNameButtonGame.GameObjects
{
    class DontTouch: GameObject, IHitbox, IMouseActions
    {
        public DontTouch(Vector2 Pos, Vector2 Size, THBox box) {
            base.Size = Size;
            Position = Pos;
            ImageLocation = new Rectangle((int)box.Imagesize.X, 0, (int)box.Imagesize.X, (int)box.Imagesize.Y);
            FrameSize = box.Imagesize;
            hitbox = new Rectangle[box.Hitbox.Length];
            Texture = box.Texture;
        }
        Rectangle[] hitbox;
        public Rectangle[] Hitbox {
            get => hitbox;
        }
       
        public event EventHandler Leave;
        public event EventHandler Enter;
        public event EventHandler Click;

        public override void Draw(SpriteBatch sp) {
            base.Draw(sp);
        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }
    }
}
