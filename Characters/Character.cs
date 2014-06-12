using InfiniteBoxEngine;
using InfiniteBoxEngine.Animation.Skeletal;
using InfiniteBoxEngine.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Characters {

    class Character {

        GameObject characterObject; 
        HumanoidSkeleton skeleton;
        SkeletonRenderer skRenderer; 

        public Character(Vector2 position, string name) {
            this.characterObject = new GameObject(Engine.Gameworld.World, name, "", position, 20, 100, FarseerPhysics.Dynamics.BodyType.Dynamic, false);
            this.characterObject.Body.FixedRotation = true;
            this.skeleton = new HumanoidSkeleton(position);
            this.skRenderer = new SkeletonRenderer(skeleton);
        }

        public void Draw(SpriteBatch sb) {
            skRenderer.Draw(sb);
        }

        public SkeletonRenderer SkRenderer {
            get { return skRenderer; }
            set { skRenderer = value; }
        }

        internal HumanoidSkeleton Skeleton {
            get { return skeleton; }
            set { skeleton = value; }
        }

        public GameObject CharacterObject {
            get { return characterObject; }
            set { characterObject = value; }
        }
    }
}
