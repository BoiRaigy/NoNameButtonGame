using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NoNameButtonGame
{
    public class NoNameGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D target2D;

        float DefaultWidth = 1280F;
        float DefaultHeight = 720F;


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
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }
        Rectangle BackbufferBounds;
        float backbufferAspectRatio;
        float ScreenAspectRatio;
        float rx,ry,rw,rh;
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


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

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.SetRenderTarget(target2D);
            GraphicsDevice.Clear(new Color(50, 50, 50));
            //Game Update

            //End
            GraphicsDevice.SetRenderTarget(null);
            BackbufferBounds = GraphicsDevice.PresentationParameters.Bounds;

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            Rectangle DesRec = new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
            GraphicsDevice.Clear(Color.HotPink);
            _spriteBatch.Draw(target2D, DesRec, null, Color.White);
            _spriteBatch.End();
            
          
            base.Draw(gameTime);
        }
    }
}
