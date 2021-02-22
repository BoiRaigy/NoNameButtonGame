using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Raigy.Obj;
namespace NoNameButtonGame.Text
{
    class Letter : GameObject
    {
        Character character;
        public Letter(Vector2 Position, Vector2 Size, Character Cchar, Color CColor) {
            this.Position = Position;
            this.Size = Size;
            this.Texture = Globals.Content.Load<Texture2D>("Font");
            DrawColor = CColor;
            FrameSize = new Vector2(8, 8);
            ChangeCharacter(Cchar);
        }
        public void ChangeColor(Color Ccolor) {
            DrawColor = Ccolor;
        }
        public void ChangeCharacter(Character Cchar) {
            character = Cchar; 
            ImageLocation = new Rectangle(new Point((int)Cchar % 5 * 8, (int)Cchar / 5 * 8), FrameSize.ToPoint());
        }
       
        public override void Draw(SpriteBatch sp) {
            base.Draw(sp);
        }

        public override void Update(GameTime gt) {
            base.Update(gt);
        }
        public enum Character
        {
            c0,
            c1,
            c2,
            c3,
            c4,
            c5,
            c6,
            c7,
            c8,
            c9,
            cA,
            cB,
            cC,
            cD,
            cE,
            cF,
            cG,
            cH,
            cI,
            cJ,
            cK,
            cL,
            cM,
            cN,
            cO,
            cP,
            cQ,
            cR,
            cS,
            cT,
            cU,
            cV,
            cW,
            cX,
            cY,
            cZ,
            cEXCLAMATION,
            cQUESTION,
            cSLASH,
            cMINUS,
            cBIGGERAS,
            cEQUAL,
            cSMALLERAS,
            cSTAR,
            cPLUS,
            cPERCENT,
            cOPENBRACKET,
            cCLOSEBRACKET,
            cSEMICOLON,
            cNone
        }
    }
}
