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
    class Level10 : SampleLevel
    {

        TextButton[] button;
        Cursor cursor;
        TextBuilder Questions;
        int Awnsered;
        int[] RightAwnsers = new int[3] { 0, 2, 1 };
        
        public Level10(int defaultWidth, int defaultHeight, Vector2 window, Random rand) : base(defaultWidth, defaultHeight, window, rand) {
            Name = "Level 10 - QnA Time!";
            button = new TextButton[3];
            Questions = new TextBuilder("3 + 4 = 5 => 5 + 5 =?", new Vector2(-64, -128), new Vector2(8, 8), null, 0);
            button[0] = new TextButton(new Vector2(-64, -96), new Vector2(128, 64), Globals.Content.GetTHBox("emptybutton"), "0", "7", new Vector2(8, 8));
            button[1] = new TextButton(new Vector2(-64, -32), new Vector2(128, 64), Globals.Content.GetTHBox("emptybutton"), "1", "11", new Vector2(8, 8));
            button[2] = new TextButton(new Vector2(-64, 32), new Vector2(128, 64), Globals.Content.GetTHBox("emptybutton"), "2", "5", new Vector2(8, 8));
            for (int i = 0; i < button.Length; i++) {
                button[i].Click += BtnEvent;
            }
            cursor = new Cursor(new Vector2(0, 0), new Vector2(7, 10), Globals.Content.GetTHBox("cursor"));
        }



        private void BtnEvent(object sender, EventArgs e) {

            if (RightAwnsers[Awnsered] != int.Parse((sender as TextButton).Name) && RightAwnsers[Awnsered] != -1) {
                CallFail(this, e);
            } else {
                Awnsered++;
                if (Awnsered == RightAwnsers.Length)
                    CallFinish(this, e);
                else {
                    switch (Awnsered) {
                        case 1:
                            Questions.ChangeText("Level 2 has ? Buttons!");
                            button[0].Text.ChangeText("25");
                            button[1].Text.ChangeText("20");
                            button[2].Text.ChangeText("16");
                            break;
                        case 2:
                            Questions.ChangeText("What previous Level had you hold a button");
                            button[0].Text.ChangeText("3!");
                            button[1].Text.ChangeText("6!");
                            button[2].Text.ChangeText("4!");
                            break;
                    }
                    
                }
            }

        }
        public override void Draw(SpriteBatch sp) {
            for (int i = 0; i < button.Length; i++) {
                button[i].Draw(sp);
            }
            Questions.Draw(sp);
            cursor.Draw(sp);
        }

        public override void Update(GameTime gt) {
            cursor.Update(gt);
            base.Update(gt);
            Questions.ChangePosition(new Vector2(0, -128) - Questions.rec.Size.ToVector2() / 2);
            Questions.Update(gt);
            for (int i = 0; i < button.Length; i++) {
                button[i].Update(gt, cursor.Hitbox[0]);
            }
            cursor.Position = MousePos - cursor.Size / 2;
        }
    }
}
