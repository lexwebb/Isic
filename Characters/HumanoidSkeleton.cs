using InfiniteBoxEngine.Skeletal.Animation;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Characters {
    class HumanoidSkeleton : Skeleton {

        BoneNode waistNode;
        #region Bones
        Bone lowerSpine, upperSpine;
        Bone leftThigh, leftCalf, leftFoot;
        Bone rightThigh, rightCalf, rightFoot;
        Bone leftArm, leftForearm, leftHand;
        Bone rightArm, rightForearm, rightHand;
        Bone neck, skull;
        #endregion

        public HumanoidSkeleton(Vector2 position)
            : base(position) {

            waistNode = new BoneNode(null, position, 0);
            this.PrimaryNode = waistNode;

            this.AddBone(this.PrimaryNode, "lowerSpine", MathHelper.ToRadians(0), 15);
            this.AddBone(this.Bones["lowerSpine"].EndNode, "upperSpine", MathHelper.ToRadians(0), 15);

            this.AddBone(this.PrimaryNode, "leftThigh", MathHelper.ToRadians(100), 20);
            this.AddBone(this.Bones["leftThigh"].EndNode, "leftCalf", MathHelper.ToRadians(0), 20);
            this.AddBone(this.Bones["leftCalf"].EndNode, "leftFoot", MathHelper.ToRadians(40), 10);

            this.AddBone(this.PrimaryNode, "rightThigh", MathHelper.ToRadians(80), 20);
            this.AddBone(this.Bones["rightThigh"].EndNode, "rightCalf", MathHelper.ToRadians(0), 20);
            this.AddBone(this.Bones["rightCalf"].EndNode, "rightFoot", MathHelper.ToRadians(320), 10);

            //lowerSpine = new Bone(this, waistNode, 15, MathHelper.ToRadians(0));
            //upperSpine = new Bone(this, lowerSpine.EndNode, 15, MathHelper.ToRadians(0));

            //leftThigh = new Bone(this, waistNode, 20, MathHelper.ToRadians(190));
            //leftCalf = new Bone(this, leftThigh.EndNode, 15, MathHelper.ToRadians(0));
            //leftFoot = new Bone(this, leftCalf.EndNode, 10, MathHelper.ToRadians(40));

            //rightThigh = new Bone(this, waistNode, 20, MathHelper.ToRadians(170));
            //rightCalf = new Bone(this, rightThigh.EndNode, 15, MathHelper.ToRadians(0));
            //rightFoot = new Bone(this, rightCalf.EndNode, 10, MathHelper.ToRadians(320));

            //this.Bones.Add("lowerSpine", lowerSpine);
            //this.Bones.Add("upperSpine", upperSpine);
            //this.Bones.Add("leftThigh", leftThigh);
            //this.Bones.Add("leftCalf", leftCalf);
            //this.Bones.Add("leftFoot", leftFoot);
            //this.Bones.Add("rightThigh", rightThigh);
            //this.Bones.Add("rightCalf", rightCalf);
            //this.Bones.Add("rightFoot", rightFoot);
        }
    }
}
