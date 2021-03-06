using System;
using System.Collections.Generic;
using System.Text;

using Raigy.Obj;
using Raigy.Input;
using Raigy.Camera;

using NoNameButtonGame.Interfaces;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NoNameButtonGame.BeforeMaths;
using NoNameButtonGame.GameObjects;
using NoNameButtonGame.Text;

namespace NoNameButtonGame.LevelSystem.LevelContainer
{
    class Level11 : SampleLevel
    {

        TextBuilder[] text;
        Cursor cursor;
        AwesomeButton button;
        public Level11(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "DEMO END";
            text = new TextBuilder[15];
            button = new AwesomeButton(new Vector2(0, 8), new Vector2(4, 2), Globals.Content.GetTHBox("emptybutton"));
            button.Click += CallFinish;
            text[0] = new TextBuilder("So this is Level 11. I though there would be more. the creator must has", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[1] = new TextBuilder("run out of Ideas other wise he would have put some effort", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[2] = new TextBuilder("into finding an insteresting concept for this level", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[3] = new TextBuilder("but now there only text here. What Am i supposed to do", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[4] = new TextBuilder("with this! unless there is a clue hidden among the", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[5] = new TextBuilder("messages. maybe it was to do with the colors.", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[6] = new TextBuilder("maybe i need to increase the gamma.", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[7] = new TextBuilder("maybe i need to look though the game files", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[8] = new TextBuilder("maybe i need to go on some shady website", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[9] = new TextBuilder("maybe i need to look at the audio with a spectrometer", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[10] = new TextBuilder("or i might just need to wait. what a waste of time", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[11] = new TextBuilder("i usualy like meta games but i though this one was not", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[12] = new TextBuilder("one of them. well i must have guessed wrong.", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[13] = new TextBuilder(" Or this text was just a distraction so you could not", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            text[14] = new TextBuilder("see the button hidden behind the text. Oh wow i feel dumb", new Vector2(0, 0), new Vector2(8, 8), null, 0);
            for (int i = 0; i < text.Length; i++) {
                text[i].ChangePosition(new Vector2(0,-128 + i * 16) - text[i].Size / 2);
                Color[] c = new Color[text[i].Text.Length];
                for (int b = 0; b < c.Length; b++) {
                    if (rand.Next(0, 10) == 0) {
                        switch (rand.Next(0, 5)) {
                            case 0:
                                c[b] = Color.Red;
                                break;
                            case 1:
                                c[b] = Color.Blue;
                                break;
                            case 2:
                                c[b] = Color.Yellow;
                                break;
                            case 3:
                                c[b] = Color.Green;
                                break;
                            case 4:
                                c[b] = Color.Purple;
                                break;
                        }
                    } else
                        c[b] = Color.White;
                }
                text[i].ChangeColor(c);
            }
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
            
        }



        private void BtnEvent(object sender, EventArgs e) {
            CallFinish(sender, e);
        }
        private void WallEvent(object sender, EventArgs e) {
            CallReset(sender, e);
        }
        public override void Draw(SpriteBatch sp) {
            button.Draw(sp);
            for (int i = 0; i < text.Length; i++) {
                text[i].Draw(sp);
            }
            cursor.Draw(sp);
        }

        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            button.Update(gt, cursor.Hitbox[0]);
            for (int i = 0; i < text.Length; i++) {
                text[i].ChangePosition(new Vector2(24, -120 + i * 16) - text[i].rec.Size.ToVector2() / 2);
                text[i].Update(gt);
            }
                cursor.Position = MousePos - cursor.Size / 2;
        }
    }
}
