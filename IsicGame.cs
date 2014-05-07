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
using Isic.Engine;
using Isic.Engine.Object;
#endregion

namespace Isic
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class IsicGame : Game
    {
        GraphicsDeviceManager graphics;
        ContentManager contentManager;
        SpriteBatch spriteBatch;
        World world;
        Scene currentScene;
        ControlManager controlManager;

        static float PHYSICS_STEP = 1f / 60f;

        public IsicGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = new ContentManager(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            world = new World(new Vector2(0f, 98.1f));
            //Set display to simulation display ratio
            ConvertUnits.SetDisplayUnitToSimUnitRatio(10f);

            controlManager = new ControlManager();

            currentScene = new Scene();

            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            world.ContactManager.OnBroadphaseCollision += OnBroadphaseCollision;

            currentScene.SceneContent.Add("tex_Crate");

            ContentManager.LoadSceneContent(currentScene);

            
            currentScene.GameObjects.Add(new Crate(world, "crate", new Vector2(100f, 100f), BodyType.Dynamic));
            currentScene.GameObjects.Add(new Crate(world, "crate2", new Vector2(105f, 80f), BodyType.Dynamic));
            currentScene.GameObjects.Add(new FixedPlane(world, "floor", new Vector2(-1000, 300), new Vector2(3000, 300)));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            HandleInput();

            //---------Physics---------
            world.Step(PHYSICS_STEP);
            //-------------------------

            base.Update(gameTime);
        }

        private void HandleInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            controlManager.UpdateMouseState(Mouse.GetState());
 
            if(controlManager.IsLeftClicked)
                currentScene.GameObjects.Add(new Crate(world, "crate", controlManager.MousePosition, BodyType.Dynamic));

        }

        private void OnBroadphaseCollision(ref FixtureProxy fixtureProxyOne, ref FixtureProxy fixtureProxyTwo)
        {

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            spriteBatch.Begin();

            foreach(GameObject gameObject in currentScene.GameObjects){
                gameObject.Draw(spriteBatch, gameTime);
            }

            spriteBatch.End();
        }

        public World getWorld() { return world; }
    }
}
