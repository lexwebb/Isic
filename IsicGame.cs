#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using InfiniteBoxEngine;
using InfiniteBoxEngine.Object;
using InfiniteBoxEngine.Skeletal.Animation;
using InfiniteBoxEngine.Animation.Skeletal;
using InfiniteBoxEngine.GUI.Controls;
using Isic.Menus;
using Isic.Scenes;
#endregion

namespace Isic {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class IsicGame : Game {
        static GraphicsDeviceManager graphics;

        Engine engine;

        int frameRate = 0;
        int frameCounter = 0;
        TimeSpan elapsedTime = TimeSpan.Zero;

        public IsicGame()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            engine = new Engine(this, graphics);
            engine.Initialize();
            Engine.Gameworld.CurrentScene = new LevelEditor("MainMenu");
            //engine.CurrentMenu = new MainMenu();

            this.IsMouseVisible = true;
            this.Window.AllowUserResizing = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            engine.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            engine.Update(gameTime);

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            engine.Draw(gameTime);
        }
    }
}
