using FarseerPhysics.Dynamics;
using InfiniteBoxEngine;
using InfiniteBoxEngine.Animation.Skeletal;
using InfiniteBoxEngine.GUI.Controls;
using InfiniteBoxEngine.Object;
using InfiniteBoxEngine.Skeletal.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Scenes
{
    class TestScene : Scene
    {
        GameObject crate;
        FixedPlane plane;
        Skeleton skeleton;
        SkeletonRenderer skRenderer;

        public TestScene(string name) :
            base(name)
        {

        }

        public override void HandleInput(ControlManager controlManager)
        {
            if (controlManager.IsLeftClicked)
            {
                Engine.Gameworld.CurrentScene.GameObjects.Add(new Crate(Engine.Gameworld.World, "crate", Engine.Gameworld.Camera.GetRelativeWorldMousePos(controlManager.MousePosition), BodyType.Dynamic));
                Console.Out.WriteLine("Mouse Position: " + controlManager.MousePosition);
                Console.Out.WriteLine("Camera Relative Mouse Position: " + Engine.Gameworld.Camera.GetRelativeWorldMousePos(controlManager.MousePosition));
                Console.Out.WriteLine(Engine.Gameworld.World.BodyList.Count());
            }

            if (controlManager.ScrollAmount != 0)
            {
                Console.Out.WriteLine("Scrolled by: " + controlManager.ScrollAmount + " : " + (float)Math.Round((double)controlManager.ScrollAmount / 1200d, 1));
                //gameworld.Camera.Scale += (float)controlManager.ScrollAmount / 1200f;
                //skeleton.PrimaryNode.Rotation = skeleton.PrimaryNode.Rotation + (float)controlManager.ScrollAmount / 12000f;
                skeleton.Bones["test"].RootNode.Rotation = skeleton.Bones["test"].RootNode.Rotation + (float)controlManager.ScrollAmount / 1200f;
            }

            if (controlManager.IsKeyDown(Keys.Right))
                Engine.Gameworld.Camera.Position += new Vector2(5, 0);
            if (controlManager.IsKeyDown(Keys.Left))
                Engine.Gameworld.Camera.Position += new Vector2(-5, 0);
            if (controlManager.IsKeyDown(Keys.Up))
                Engine.Gameworld.Camera.Position += new Vector2(0, -5);
            if (controlManager.IsKeyDown(Keys.Down))
                Engine.Gameworld.Camera.Position += new Vector2(0, 5);

            if (controlManager.IsKeyDown(Keys.D))
                crate.Body.ApplyForce(new Vector2(100000, 0));
            if (controlManager.IsKeyDown(Keys.A))
                crate.Body.ApplyForce(new Vector2(-100000, 0));
            if (controlManager.IsKeyDown(Keys.W))
                crate.Body.ApplyForce(new Vector2(0, 100000));
        }

        public override void Update(GameTime gameTime)
        {
            Engine.Gameworld.Camera.Focus = crate.Position;
        }

        public override void LoadContent()
        {
            Engine.Gameworld.CurrentScene.RegisterNewTexture("tex_Box.png");
            Engine.Gameworld.CurrentScene.RegisterNewTexture("CheckboxChecked.bmp");
            Engine.Gameworld.CurrentScene.RegisterNewTexture("CheckboxUnChecked.bmp");
            Engine.Gameworld.CurrentScene.RegisterNewTexture("tex_Crate.png");

            Panel panel = new Panel(new Vector2(100, 100), 200, 200, null, null, Color.Yellow);
            panel.Transparency = 0.5f;

            Checkbox checkbox = new Checkbox("test", new Vector2(20f, 20f), 25, 25, panel,
                EngineContentManager.GetTexture("CheckboxChecked.bmp"), EngineContentManager.GetTexture("tex_Box.png"));

            Checkbox checkbox2 = new Checkbox("test", new Vector2(0f, 0f), 25, 25, checkbox,
               EngineContentManager.GetTexture("CheckboxChecked.bmp"), EngineContentManager.GetTexture("CheckboxUnChecked.bmp"));

            Checkbox checkbox3 = new Checkbox("test", new Vector2(0f, 0f), 25, 25, checkbox2,
               EngineContentManager.GetTexture("CheckboxChecked.bmp"), EngineContentManager.GetTexture("CheckboxUnChecked.bmp"));

            Engine.GuiManager.RegisterControl("panel", panel);

            Engine.GuiManager.RegisterControl("test", checkbox);
            checkbox2.YOffset = 10;
            checkbox2.XOffset = 10;
            Engine.GuiManager.RegisterControl("test2", checkbox2);
            checkbox3.YOffset = 10;
            checkbox3.XOffset = 10;
            Engine.GuiManager.RegisterControl("test3", checkbox3);

            skeleton = new Skeleton(new Vector2(0, 100));
            skRenderer = new SkeletonRenderer(skeleton);
            skeleton.PrimaryNode.Rotation = MathHelper.ToRadians(0);
            skeleton.AddBone(skeleton.PrimaryNode, "test", MathHelper.ToRadians(45), 100);
            skeleton.AddBone(skeleton.Bones["test"].EndNode, "test2", MathHelper.ToRadians(45), 100);
            skeleton.AddBone(skeleton.Bones["test2"].EndNode, "test3", MathHelper.ToRadians(45), 100);

            crate = new GameObject(Engine.Gameworld.World, "player", "tex_Box.png", new Vector2(0, 100), 10, 10, BodyType.Dynamic, true);
            plane = new FixedPlane(Engine.Gameworld.World, "plane", new Vector2(-1000, 0), new Vector2(1000, 0));
            //crate.Body.IsStatic = true;

            Engine.Gameworld.CurrentScene.GameObjects.Add(crate);

            //button.RegisterListener(Test, MouseButton.Left, ButtonAction.OnClick);  
        }

        public override void Draw()
        {
            
        }

        public override void DrawForeground()
        {
            
        }
    }
}
