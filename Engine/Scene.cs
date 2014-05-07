using Isic.Engine.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Engine
{
    class Scene
    {
        List<String> sceneContent = new List<string>();
        List<GameObject> gameObjects = new List<GameObject>();

        internal List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        public List<String> SceneContent
        {
            get { return sceneContent; }
            set { sceneContent = value; }
        }

        
    }
}
