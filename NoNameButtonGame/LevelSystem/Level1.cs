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
namespace NoNameButtonGame.LevelSystem
{
    class Level1 : SampleLevel 
    {

        AwesomeButton[] button;
       

        public Level1(int defaultWidth, int defaultHeight, Vector2 window) : base(defaultWidth, defaultHeight, window) {
            
            
            button = new AwesomeButton[3];
            button[0] = new AwesomeButton(new Vector2(0, 0), new Vector2(128, 64), staticContent.Content.GetTHBox("awesomebutton")) {
                DrawColor = Color.White,
            };
            button[0].Click += BtnEvent;
            button[1] = new AwesomeButton(new Vector2(-10, -64), new Vector2(16, 8), staticContent.Content.GetTHBox("awesomebutton")) {
                DrawColor = Color.White,
            };
            button[1].Click += BtnEvent;
            button[2] = new AwesomeButton(new Vector2(200, 100), new Vector2(512, 256), staticContent.Content.GetTHBox("awesomebutton")) {
                DrawColor = Color.White,
            };
            button[2].Click += BtnEvent;
        }

        

        private void BtnEvent(object sender, EventArgs e) {
            System.Environment.Exit(15);
        }
        public override void Draw(SpriteBatch sp) {
            for (int i = 0; i < button.Length; i++) {
                button[i].Draw(sp);
            }
        }

        public override void Update(GameTime gt) {
            base.Update(gt);
            

            for (int i = 0; i < button.Length; i++) {
                button[i].Update(gt, MouseIngame);
            }

        }
    }
}
