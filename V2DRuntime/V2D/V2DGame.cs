﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Box2DX.Dynamics;
using DDW.V2D.Serialization;
using Microsoft.Xna.Framework;
using Box2DX.Collision;
using Box2DX.Common;
using Microsoft.Xna.Framework.Graphics;
using DDW.Display;
using Microsoft.Xna.Framework.Input;
using DDW.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Net;
using V2DRuntime.Network;

namespace DDW.V2D 
{
    public abstract class V2DGame : Microsoft.Xna.Framework.Game
    {
        public static Stage stage;
        public static ContentManager contentManager;
        public const string ROOT_NAME = V2DWorld.ROOT_NAME;
		public static string currentRootName = V2DWorld.ROOT_NAME;

        public static V2DGame instance;
        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected bool keyDown = false;
        protected bool isFullScreen = false;
        Microsoft.Xna.Framework.Graphics.Color bkgColor = new Microsoft.Xna.Framework.Graphics.Color(60, 60, 80);

		public List<NetworkGamer> gamers = new List<NetworkGamer>();

        protected V2DGame()
        {
            if (instance != null)
            {
                throw new Exception("There can be only one game class.");
            }
            instance = this;

            graphics = new GraphicsDeviceManager(this);
            contentManager = Content;
            Content.RootDirectory = "Content";
            stage = V2DStage.GetInstance();
            GetCursor();
        }

        public virtual bool HasCursor { get { return false; } }

        private Cursor cursor;
        public Cursor GetCursor()
        {
            if (HasCursor && cursor == null)
            {
                cursor = new Cursor(this);
                Components.Add(cursor);
            }
            return cursor;
        }

        protected virtual void CreateScreens()
        {
            //screenPaths.Add(symbolImports[i]);
        }
        protected override void Initialize()
        {
            base.Initialize();

            stage.Initialize();
            CreateScreens();
            stage.SetScreen(0);
        }

        public void SetSize(int width, int height)
        {
            if (width > 0 && height > 0)
            {
                graphics.PreferredBackBufferWidth = width;
                graphics.PreferredBackBufferHeight = height;
                graphics.IsFullScreen = this.isFullScreen;
                graphics.ApplyChanges();
                stage.SetBounds(0, 0, width, height);
            }
        }

        protected override void LoadContent()
        {
			spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void UnloadContent()
        {
        }

		public virtual void ExitToMainMenu()
        {
			NetworkManager.Instance.LeaveSession();
        }
        public virtual void ExitGame()
        {
			this.Exit();
        }

		public virtual void AddGamer(NetworkGamer gamer, int gamerIndex)
		{
			if (!gamers.Contains(gamer))
			{
				gamers.Add(gamer);
			}
		}
		public virtual void RemoveGamer(NetworkGamer gamer)
		{
			if (gamers.Contains(gamer))
			{
				gamers.Remove(gamer);
			}
		}

        protected override void Update(GameTime gameTime)
        {
			stage.Update(gameTime);
			base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bkgColor);

            stage.Draw(spriteBatch);

            base.Draw(gameTime);
        }

    }
}