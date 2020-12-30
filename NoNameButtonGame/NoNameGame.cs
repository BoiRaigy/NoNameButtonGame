using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace NoNameButtonGame
{
    public class NoNameGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D target2D;

        float DefaultWidth = 1280F;
        float DefaultHeight = 720F;
        AwesomeButton button;
        Raigy.Camera.CameraClass camera;
        public NoNameGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            base.Initialize();

            target2D = new RenderTarget2D(GraphicsDevice, (int)DefaultWidth, (int)DefaultHeight);
            _graphics.PreferredBackBufferWidth = (int)DefaultWidth;
            _graphics.PreferredBackBufferHeight = (int)DefaultHeight;
            _graphics.ApplyChanges();
            camera = new Raigy.Camera.CameraClass(new Vector2(DefaultWidth, DefaultHeight));
            button = new AwesomeButton(new Vector2(128,64)) {
                Texture = Content.Load<Texture2D>("awesomebutton"),
                Position = new Vector2(0, 0),
                
             DrawColor = Color.White,
             
            };
            button.Click += BtnEvent;
            CamPos = new Vector2(button.Size.X / 2, button.Size.Y / 2);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            camera.Update(CamPos, new Vector2(2, 2));
            MouseState mouseState = Mouse.GetState();
            Vector2 VMP = mouseState.Position.ToVector2();
            Vector2 V2C = (new Vector2(DefaultWidth  / 2, DefaultHeight  / 2) - new Vector2(VMP.X,VMP.Y) )/ camera.Zoom;
            button.Update(gameTime, CamPos + V2C);
            Console.WriteLine(button.Position);
            Console.WriteLine(CamPos);

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
            
            button.Draw(_spriteBatch);

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
