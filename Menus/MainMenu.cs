using InfiniteBoxEngine;
using InfiniteBoxEngine.GUI.Controls;
using InfiniteBoxEngine.GUI.Menu;
using Isic.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Menus
{
    class MainMenu : Menu
    {
        Button btnContinue, btnNewGame, btnOptions, btnEditor;

        public MainMenu()
        {
            btnContinue = new Button("btnContinue", "Continue", new Vector2(100, 100), 200, 30, null, EngineContentManager.GetTexture("btnContinue.png"), EngineContentManager.GetTexture("btnContinue.png"), EngineContentManager.GetTexture("btnContinue.png"));
            RegisterOrderedControl(btnContinue, null, false);
            this.Active = true;

            btnContinue.RegisterListener(Continue, InfiniteBoxEngine.GUI.MouseButton.Left, InfiniteBoxEngine.GUI.ButtonAction.OnClick);
        }

        public void Continue(Vector2 position)
        {
            Engine.Gameworld.CurrentScene = new TestScene("test");
        }
    }
}
