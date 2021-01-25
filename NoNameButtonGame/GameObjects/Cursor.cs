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
    class Cursor : GameObject
    {
        public Cursor(Vector2 Pos, Vector2 Size, THBox box) {
            base.Size = Size;
            Position = Pos;
            ImageLocation = new Rectangle(0,0,0,0);
            FrameSize = box.Imagesize;
            Texture = box.Texture;
            DrawColor = Color.White;
        }
        public override void Draw(SpriteBatch sp) {
            base.Draw(sp);
        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }
    }
}
