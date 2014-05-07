using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Engine.Object
{
    class Crate : GameObject
    {
        public Crate(World world, String name, Vector2 position, BodyType bodyType) :      
            base(world, name, ContentManager.GetTexture("tex_Crate"), position, 10, 10, bodyType) 
        {

        }
    }
}
