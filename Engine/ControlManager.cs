using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Engine
{
    class ControlManager
    {
        MouseState mouseState;
        Vector2 mousePosition;

        bool isLeftClicked, isLeftHeld;
        int leftMouseCounter;
        bool isRightClicked, isRightHeld;
        int rightMouseCounter;
        bool isMiddleClicked, isMiddleHeld;
        int middleMouseCounter;
        

        public void UpdateMouseState(MouseState state)
        {
            this.mouseState = state;
            this.mousePosition = new Vector2(mouseState.Position.X, mouseState.Position.Y);

            //------Left-Button--------
            if (mouseState.LeftButton == ButtonState.Pressed)
                leftMouseCounter++;
            else
                leftMouseCounter = 0;

            if (leftMouseCounter == 1)
                this.isLeftClicked = true;
            else if (leftMouseCounter > 1)
            {
                this.isLeftClicked = false;
                this.isLeftHeld = true;
            }
            else
            {
                this.isLeftClicked = false;
                this.isLeftHeld = false;
            }
            //--------------------------

            //------Right-Button--------
            if (mouseState.RightButton == ButtonState.Pressed)
                rightMouseCounter++;
            else
                rightMouseCounter = 0;

            if (rightMouseCounter == 1)
                this.isRightClicked = true;
            else if (rightMouseCounter > 1)
            {
                this.isRightClicked = false;
                this.isRightHeld = true;
            }
            else
            {
                this.isRightClicked = false;
                this.isRightHeld = false;
            }
            //--------------------------

            if (mouseState.MiddleButton == ButtonState.Pressed)
                middleMouseCounter++;
            else
                middleMouseCounter = 0;
        }

        public Vector2 MousePosition
        {
            get { return mousePosition; }
        }

        public bool IsLeftHeld
        {
            get { return isLeftHeld; }
        }

        public bool IsLeftClicked
        {
            get { return isLeftClicked; }
        }
        public bool IsRightHeld
        {
            get { return isRightHeld; }
        }

        public bool IsRightClicked
        {
            get { return isRightClicked; }
        }

        public bool IsMiddleHeld
        {
            get { return isMiddleHeld; }
        }

        public bool IsMiddleClicked
        {
            get { return isMiddleClicked; }
        }
    }
}
