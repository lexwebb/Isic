using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Isic.Engine.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Isic.Engine.Object
{
    class GameObject : Nameable
    {
        Texture2D texture;
        Vector2 position, position2;
        float width, height;
        Body body;
        bool drawable = true;

        [Obsolete("Use Parameter Method Only")]
        public GameObject()
        {

        }

        /// <summary>
        /// Creates a rectangular textured game object 
        /// </summary>
        /// <param name="world">World the object belongs to</param>
        /// <param name="name">Name of the object</param>
        /// <param name="texture">Objects texture</param>
        /// <param name="position">Initial position in the world (pixels)</param>
        /// <param name="width">Width of the objects collision box</param>
        /// <param name="height">Hieght of the objects collision box</param>
        public GameObject(World world, String name, Texture2D texture, Vector2 position, float width, float height, BodyType type)
        {
            this.SetName(name);
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
            body = BodyFactory.CreateRectangle(world, width, height, 1f, position, name);
            this.body.BodyType = type;
            body.CollisionCategories = Category.All;
        }

        /// <summary>
        /// Creates a collidable edge with no texture
        /// </summary>
        /// <param name="world">World the object belongs to</param>
        /// <param name="name">Name of the object</param>
        /// <param name="start">Start point of the edge</param>
        /// <param name="end">End point of the edge</param>
        public GameObject(World world, String name, Vector2 start, Vector2 end)
        {
            this.SetName(name);
            this.position = start;
            this.position2 = end;
            body = BodyFactory.CreateEdge(world, start, end, name);
        }

        /// <summary>
        /// Draws the game object using stored texture
        /// </summary>
        /// <param name="sb">Spritebatch used for drawing</param>
        /// <param name="gt">Gametime used for animation</param>
        public void Draw(SpriteBatch sb, GameTime gt)
        {
            if(drawable)
                sb.Draw(texture, body.Position, null, Color.White, body.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }

        public bool Drawable
        {
            get { return drawable; }
            set { drawable = value; }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }


        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Body Body
        {
            get { return body; }
            set { body = value; }
        }
    }
}
