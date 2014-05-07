using Isic.Engine;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic
{
    class ContentManager
    {
        static IsicGame game;
        static Dictionary<String, Texture2D> textures = new Dictionary<string, Texture2D>();

        public ContentManager(IsicGame game)
        {
            ContentManager.game = game;
        }

        public static void LoadSceneContent(Scene scene)
        {
            foreach (String contentName in scene.SceneContent)
            {
                textures.Add(contentName, game.Content.Load<Texture2D>(contentName));       
            }
        }

        public static Texture2D GetTexture(String name)
        {
            if(textures.ContainsKey(name))
                return textures[name];
            return null;
        }
    }
}
