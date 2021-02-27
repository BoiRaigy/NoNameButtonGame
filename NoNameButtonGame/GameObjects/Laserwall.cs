using System;
using Raigy.Obj;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NoNameButtonGame.Interfaces;
using NoNameButtonGame.BeforeMaths;
using System.Collections.Generic;
namespace NoNameButtonGame.GameObjects
{
    class Laserwall : GameObject, IHitbox, IMouseActions, IMoveable
    {
        DontTouch[] dt;
        public Laserwall(Vector2 Pos, Vector2 Size, THBox box) {
            this.Size = Size;
            this.Position = Pos;
            Vector2 grid = Size / 32;


            Vector2 gridedge = new Vector2(Size.X % 32, Size.Y % 32);
            grid = new Vector2((float)(int)grid.X, (float)(int)grid.Y);

            if (gridedge.X == 0 && gridedge.Y == 0) {
                dt = new DontTouch[((int)grid.X) * ((int)grid.Y)];
                for (int i = 0; i < grid.Y; i++) {
                    for (int a = 0; a < grid.X; a++) {
                        dt[i * ((int)grid.X) + a] = new DontTouch(new Vector2(Pos.X + a * 32, Pos.Y + i * 32), new Vector2(32, 32), box);
                        dt[i * ((int)grid.X) + a].Enter += CallEnter;
                    }
                }
            } else {
                int gx = 0, gy = 0;
                if (gridedge.X != 0)
                    gx = (int)grid.X + 1;
                else
                    gx = (int)grid.X;
                if (gridedge.Y != 0)
                    gy = (int)grid.Y + 1;
                else
                    gy = (int)grid.Y;
                dt = new DontTouch[gx * gy];
                for (int i = 0; i < gy; i++) {
                    for (int a = 0; a < gx; a++) {
                        Vector2 CSize = new Vector2(gridedge.X, gridedge.Y);
                        if (a < grid.X)
                            CSize.X = 32;
                        if (i < grid.Y)
                            CSize.Y = 32;
                        dt[i * gx + a] = new DontTouch(new Vector2(Pos.X + a * 32, Pos.Y + i * 32), CSize, box);

                        dt[i * gx + a].Enter += CallEnter;
                    }
                }
            }
            List<Rectangle> Hitboxes = new List<Rectangle>();
            for (int i = 0; i < dt.Length; i++) {
                for (int a = 0; a < dt[i].Hitbox.Length; a++) {
                    Hitboxes.Add(dt[i].Hitbox[a]);
                }
            }
            hitbox = Hitboxes.ToArray();
        }

        private void CallEnter(object sender, EventArgs e) {
            Enter(sender, e);
        }
        Rectangle[] hitbox;
        public Rectangle[] Hitbox => hitbox;

        public event EventHandler Leave;
        public event EventHandler Enter;
        public event EventHandler Click;
        public void Update(GameTime gt, Rectangle MousePos) {
            for (int i = 0; i < dt.Length; i++) {
                dt[i].Update(gt, MousePos);
            }
            for (int i = 0; i < hitbox.Length; i++) {
                if (hitbox[i].Intersects(MousePos))
                    Enter(this, new EventArgs());
            }
            base.Update(gt);
        }

        public override void Draw(SpriteBatch sp) {
            for (int i = 0; i < dt.Length; i++) {
                dt[i].Draw(sp);
            }
        }

        public bool Move(Vector2 Direction) {
            try { 
                Position += Direction;
                for (int i = 0; i < dt.Length; i++) {
                    dt[i].Position += Direction;
                }
            } catch { return false; }
            return true;
        }
    }
}
