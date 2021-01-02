using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using NoNameButtonGame.BeforeMaths;

namespace NoNameButtonGame
{
    public class NoNameGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D target2D;

        float DefaultWidth = 1280F;
        float DefaultHeight = 720F;
        AwesomeButton[] button;
        Raigy.Camera.CameraClass camera;
        public NoNameGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            base.Initialize();
            Console.CursorVisible = false;
            target2D = new RenderTarget2D(GraphicsDevice, (int)DefaultWidth, (int)DefaultHeight);
            _graphics.PreferredBackBufferWidth = (int)DefaultWidth;
            _graphics.PreferredBackBufferHeight = (int)DefaultHeight;
            _graphics.ApplyChanges();
            camera = new Raigy.Camera.CameraClass(new Vector2(DefaultWidth, DefaultHeight));
            button = new AwesomeButton[3];
            button[0] = new AwesomeButton(new Vector2(0, 0),new Vector2(128,64),Content.GetTHBox("awesomebutton")) {
             DrawColor = Color.White,
            };
            button[0].Click += BtnEvent;
            button[1] = new AwesomeButton(new Vector2(-10, -64), new Vector2(16, 8), Content.GetTHBox("awesomebutton")) {
                DrawColor = Color.White,
            };
            button[1].Click += BtnEvent;
            button[2] = new AwesomeButton(new Vector2(200, 100),new Vector2(512, 256), Content.GetTHBox("awesomebutton")) {
                DrawColor = Color.White,
            };
            button[2].Click += BtnEvent;
            //CamPos = new Vector2(button[0].Size.X / 2, button[0].Size.Y / 2);
            //CamPos = new Vector2(700, 400);
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }
        Rectangle BackbufferBounds;
        float backbufferAspectRatio;
        float ScreenAspectRatio;
        float rx,ry,rw,rh;
        Vector2 CamPos;
        
        protected override void Update(GameTime gameTime) {
            Console.SetCursorPosition(0, 0);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Right, true)) {
                CamPos.X += 45;
            }
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Left, true)) {
                CamPos.X -= 45;
            }
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Up, true)) {
                CamPos.Y -= 45;
            }
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.Down, true)) {
                CamPos.Y += 45;
            }
            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.F11, true)) {
                _graphics.ToggleFullScreen();
                if (_graphics.IsFullScreen) {

                    _graphics.PreferredBackBufferWidth = 1920;
                    _graphics.PreferredBackBufferHeight = 1080;
                } else {
                    _graphics.PreferredBackBufferWidth = (int)DefaultWidth;
                    _graphics.PreferredBackBufferHeight = (int)DefaultHeight;
                }
                _graphics.ApplyChanges();

            }
            Console.WriteLine("Screen (Window)");
            Console.WriteLine(_graphics.PreferredBackBufferWidth + "        ");
            Console.WriteLine(_graphics.PreferredBackBufferHeight + "        ");
            camera.Update(CamPos, new Vector2(0, 0));

            Console.WriteLine("Mouse");
            MouseState mouseState = Mouse.GetState();
            Vector2 VecMouse = mouseState.Position.ToVector2();
            Console.WriteLine("VecMouse:" + VecMouse + "        ");
            float TargetScreenDifX = _graphics.PreferredBackBufferWidth / DefaultWidth;
            float TargetScreenDifY = _graphics.PreferredBackBufferHeight / DefaultHeight;
            
            Vector2 VMP = new Vector2(VecMouse.X / TargetScreenDifX, VecMouse.Y / TargetScreenDifY);
            Console.WriteLine("VMP:" + VMP + "        ");

            Vector2 V2C = new Vector2(VMP.X / camera.Zoom + CamPos.X - (DefaultWidth / camera.Zoom) / 2, VMP.Y / camera.Zoom + CamPos.Y - (DefaultHeight / camera.Zoom) / 2);
            Console.WriteLine("V2C:" + V2C + "        ");
            for (int i = 0; i < button.Length; i++) {
                button[i].Update(gameTime, V2C);
            }
            Console.WriteLine("Camera");
            Console.WriteLine(CamPos + "        ");

            base.Update(gameTime);

           
            //SHOUTOUT: https://youtu.be/yUSB_wAVtE8
            BackbufferBounds = GraphicsDevice.PresentationParameters.Bounds;
            backbufferAspectRatio = (float)BackbufferBounds.Width / BackbufferBounds.Height;
            ScreenAspectRatio = (float)target2D.Width / target2D.Height;

            rx = 0f;
            ry = 0f;
            rw = BackbufferBounds.Width;
            rh = BackbufferBounds.Height;
            if (backbufferAspectRatio > ScreenAspectRatio) {
                rw = rh * ScreenAspectRatio;
                rx = (BackbufferBounds.Width - rw) / 2f;
            } else if (backbufferAspectRatio < ScreenAspectRatio) {
                rh = rw / ScreenAspectRatio;
                ry = (BackbufferBounds.Height - rh) / 2f;
            }
        }
        private void BtnEvent(object sender, EventArgs e) {
            Exit();
        }
        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.SetRenderTarget(target2D);
            GraphicsDevice.Clear(new Color(50, 50, 50));

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: camera.CamMatrix);
            for (int i = 0; i < button.Length; i++) {
                button[i].Draw(_spriteBatch);
            }
           

            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);


            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            Rectangle DesRec = new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
            GraphicsDevice.Clear(Color.HotPink);
            _spriteBatch.Draw(target2D, DesRec, null, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
