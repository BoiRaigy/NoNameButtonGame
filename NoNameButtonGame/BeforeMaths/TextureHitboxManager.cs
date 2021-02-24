using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace NoNameButtonGame.BeforeMaths
{
    public struct THBox
    {
        public Texture2D Texture;
        public Vector2 Imagesize;
        public Rectangle[] Hitbox;
        public int Aniframes;
        public bool? AniFromTop;
        
    }
    public static class TextureHitboxManager
    {
        public static THBox GetTHBox(this ContentManager mam, string tname) {
            THBox r = new THBox();
            r.Texture = mam.Load<Texture2D>(tname);
            switch (tname) {
                case "awesomebutton":
                    r.Hitbox = new Rectangle[2] { new Rectangle(2,1, 28,14),
                new Rectangle(1,2,30,12) };
                    r.Imagesize = new Vector2(32, 16);
                    break;
                case "startbutton":
                    r.Hitbox = new Rectangle[2] { new Rectangle(2,1, 44,14),
                new Rectangle(1,2,46,12)  };
                    r.Imagesize = new Vector2(48, 16);
                    break;
                case "failbutton":
                    r.Hitbox = new Rectangle[2] { new Rectangle(2,1, 28,14),
                new Rectangle(1,2,30,12) };
                    r.Imagesize = new Vector2(32, 16);
                    break;
                case "cursor":
                    r.Imagesize = new Vector2(r.Texture.Width, r.Texture.Height);
                    r.Hitbox = new Rectangle[1] { new Rectangle(0, 0, 2, 2) };
                    break;
                case "zone":
                    r.Imagesize = new Vector2(8, 8);
                    r.Hitbox = new Rectangle[1] { new Rectangle(0, 0, 8, 8) };
                    r.Aniframes = 16;
                    r.AniFromTop = true;
                    break;
                case "zonenew":
                    r.Imagesize = new Vector2(8, 8);
                    r.Hitbox = new Rectangle[1] { new Rectangle(0, 0, 8, 8) };
                    r.Aniframes = 32;
                    r.AniFromTop = true;
                    break;
                default:
                    r.Imagesize = new Vector2(r.Texture.Width, r.Texture.Height);
                    r.Hitbox = new Rectangle[1] { new Rectangle(0, 0, r.Texture.Width, r.Texture.Height) };
                    break;
            }
            return r;
        }

    }
}
