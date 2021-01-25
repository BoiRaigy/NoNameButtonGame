﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using NoNameButtonGame.BeforeMaths;
using NoNameButtonGame.LevelSystem;


namespace NoNameButtonGame
{
    public class NoNameGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private RenderTarget2D target2D;

        float DefaultWidth = 1280F;
        float DefaultHeight = 720F;
        LevelManager lvmng;
        Texture2D Mousepoint;
        Vector2 MousepointTopLeft;
        bool ShowActualMousePos = false;
        public NoNameGame() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            staticContent.Content = Content;
            IsMouseVisible = false;
            IsFixedTimeStep = false;
            
        }

        protected override void Initialize() {
            base.Initialize();
            Console.CursorVisible = false;
            target2D = new RenderTarget2D(GraphicsDevice, (int)DefaultWidth, (int)DefaultHeight);
            _graphics.PreferredBackBufferWidth = (int)DefaultWidth;
            _graphics.PreferredBackBufferHeight = (int)DefaultHeight;
            _graphics.ApplyChanges();
            lvmng = new LevelManager((int)DefaultHeight, (int)DefaultWidth, new Vector2(DefaultWidth, DefaultHeight));
            lvmng.ChangeWindowName = ChangeTitle;
            Mousepoint = Content.GetTHBox("mousepoint").Texture;
            
            //CamPos = new Vector2(button[0].Size.X / 2, button[0].Size.Y / 2);
            //CamPos = new Vector2(700, 400);
        }
        private void ChangeTitle(string NewName) {
            Window.Title = NewName;
        }
        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }
        Rectangle BackbufferBounds;
        float backbufferAspectRatio;
        float ScreenAspectRatio;
        float rx,ry,rw,rh;
       
       
        protected override void Update(GameTime gameTime) {
            MouseState mouse = Mouse.GetState();
            MousepointTopLeft = mouse.Position.ToVector2() - new Vector2(3, 3);
            base.Update(gameTime);

            if (Raigy.Input.InputReaderKeyboard.CheckKey(Keys.F11, true)) {
                _graphics.ToggleFullScreen();
                if (_graphics.IsFullScreen) {

                    _graphics.PreferredBackBufferWidth = 1920;
                    _graphics.PreferredBackBufferHeight = 1080;
                } else {
                    _graphics.PreferredBackBufferWidth = (int)DefaultWidth + 100;
                    _graphics.PreferredBackBufferHeight = (int)DefaultHeight;
                }
                _graphics.ApplyChanges();
                lvmng.ChangeScreen(new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
            }

            lvmng.Update(gameTime);

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

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: lvmng.GetCurrentCamera().CamMatrix);

            lvmng.Draw(_spriteBatch);

            _spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);


            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            Rectangle DesRec = new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
            GraphicsDevice.Clear(Color.HotPink);
            _spriteBatch.Draw(target2D, DesRec, null, Color.White);
            if (ShowActualMousePos)
            _spriteBatch.Draw(Mousepoint, new Rectangle(MousepointTopLeft.ToPoint(), new Point(6,6)), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
