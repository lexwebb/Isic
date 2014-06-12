using InfiniteBoxEngine;
using InfiniteBoxEngine.Graphics;
using InfiniteBoxEngine.GUI;
using InfiniteBoxEngine.GUI.Controls;
using InfiniteBoxEngine.Object;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Scenes {
    class LevelEditor : Scene {
        static int MOUSE_MOVE_MIN = 20;
        Vector2 mouseHoldOffset;
        GameObject objectBeingHeld;
        bool isObjectHeld;
        Vector2 mousePos;
        SpriteFont font;
        GameObject selectedObject;

        #region Controls
        Panel pnlObj;
        Button btnNewObj;
        Label lblObjectName;
        TextBox txtObjectName;
        Button btnSaveName;

        Label lblX, lblY;
        Button btnPosLeft, btnPosRight;
        TextBox txtXPos, txtYPos;
        Button btnPosUp, btnPosDown;
        #endregion

        public LevelEditor(String name) :
            base(name) {
            font = EngineContentManager.GetXNAContent().Load<SpriteFont>("Visitor");
        }

        public override void Draw() {
            Engine.GameSpriteBatch.DrawString(font, mousePos.ToString(), new Vector2(10, 10), Color.White);
            DrawUtilities.DrawLineNegativeY(Engine.GameSpriteBatch, new Vector2(0, 10), new Vector2(0, -10), 1, Color.White);
            DrawUtilities.DrawLineNegativeY(Engine.GameSpriteBatch, new Vector2(10, 0), new Vector2(-10, 0), 1, Color.White);
        }

        public override void DrawForeground() {
            if (selectedObject != null)
                DrawUtilities.DrawGameObjBorder(Engine.GameSpriteBatch, new Rectangle(
                    (int)(selectedObject.Body.Position.X - selectedObject.Width / 2),
                    (int)(selectedObject.Body.Position.Y - selectedObject.Height / 2),
                    (int)selectedObject.Width, (int)selectedObject.Height), Color.LimeGreen);
        }

        public override void HandleInput(ControlManager controlManager) {
            mousePos = controlManager.MousePosition;
            Vector2 worldMousePos = Engine.Gameworld.Camera.GetRelativeWorldMousePos(mousePos);

            #region Camera
            if (controlManager.IsKeyDown(Keys.A) || (controlManager.MousePosition.X < MOUSE_MOVE_MIN
                && controlManager.MousePosition.X > 0))
                Engine.Gameworld.Camera.Focus += new Vector2(-2, 0);
            if (controlManager.IsKeyDown(Keys.D) || (controlManager.MousePosition.X > Engine.GetGraphicsDevice().Viewport.Width - MOUSE_MOVE_MIN
                && controlManager.MousePosition.X < Engine.GetGraphicsDevice().Viewport.Width))
                Engine.Gameworld.Camera.Focus += new Vector2(2, 0);
            if (controlManager.IsKeyDown(Keys.W) || (controlManager.MousePosition.Y < MOUSE_MOVE_MIN
                && controlManager.MousePosition.Y > 0))
                Engine.Gameworld.Camera.Focus += new Vector2(0, -2);
            if (controlManager.IsKeyDown(Keys.S) || (controlManager.MousePosition.Y > Engine.GetGraphicsDevice().Viewport.Height - MOUSE_MOVE_MIN
                && controlManager.MousePosition.Y < Engine.GetGraphicsDevice().Viewport.Height))
                Engine.Gameworld.Camera.Focus += new Vector2(0, 2);

            Engine.Gameworld.Camera.TargetScale += (float)controlManager.ScrollAmount / 500;
            #endregion

            if (controlManager.IsLeftClicked) {
                Console.Out.WriteLine("World Click pos: " + worldMousePos);
                foreach (GameObject obj in this.GameObjects) {
                    if (Vector2.Distance(worldMousePos, obj.Body.Position) < obj.Width + obj.Height) {
                        Rectangle rec = new Rectangle((int)(obj.Body.Position.X - obj.Width / 2), (int)(obj.Body.Position.Y - obj.Height / 2), (int)obj.Width, (int)obj.Height);
                        if (rec.Contains(new Point((int)worldMousePos.X, (int)worldMousePos.Y))) {
                            this.selectedObject = obj;
                            this.txtObjectName.Text = selectedObject.Name;
                            UpdateTextBoxes();
                        }
                    }
                }
            }

            if (controlManager.IsLeftReleased) {
                this.mouseHoldOffset = Vector2.Zero;
                this.isObjectHeld = false;
                this.objectBeingHeld = null;
            }

            if (controlManager.IsLeftHeld) {
                foreach (GameObject obj in this.GameObjects) {
                    if (Vector2.Distance(worldMousePos, obj.Body.Position) < obj.Width + obj.Height || isObjectHeld && obj == objectBeingHeld) {
                        Rectangle rec = new Rectangle((int)(obj.Body.Position.X - obj.Width / 2), (int)(obj.Body.Position.Y - obj.Height / 2), (int)obj.Width, (int)obj.Height);
                        if (rec.Contains(new Point((int)worldMousePos.X, (int)worldMousePos.Y)) || isObjectHeld && obj == objectBeingHeld) {
                            if (this.mouseHoldOffset == Vector2.Zero)
                                this.mouseHoldOffset = worldMousePos - obj.Position;
                            obj.Position = worldMousePos - mouseHoldOffset;
                            this.isObjectHeld = true;
                            this.objectBeingHeld = obj;
                        }
                    }
                }
            }

            if (controlManager.IsKeyDown(Keys.Escape)) {
                this.selectedObject = null;
                this.txtObjectName.Text = ""; this.txtXPos.Text = "";
            }
        }

        public override void LoadContent() {
            #region Controls

            pnlObj = new Panel(Vector2.Zero, 200, 400, null, null, Color.DimGray);
            pnlObj.Alignment = Alignment.AboveRight;
            pnlObj.Transparency = 0.5f;
            Engine.GuiManager.RegisterControl("pnlObj", pnlObj);

            btnNewObj = new Button("btnNewObj", "New Object", Vector2.Zero, 180, 30, pnlObj, Color.Purple, Color.MediumPurple, Color.Purple);
            btnNewObj.TextColor = Color.White;
            btnNewObj.XOffset = 10; btnNewObj.YOffset = 10;
            btnNewObj.ShowBorder = true;
            Engine.GuiManager.RegisterControl("btnNewObj", btnNewObj);

            lblObjectName = new Label("Object Name", font, Vector2.Zero, 100, 20, btnNewObj);
            lblObjectName.Alignment = Alignment.Below;
            lblObjectName.YOffset = 10;
            Engine.GuiManager.RegisterControl("lblObjectName", lblObjectName);

            txtObjectName = new TextBox(Vector2.Zero, 110, 20, "", font, lblObjectName, Color.DarkGray);
            txtObjectName.Alignment = Alignment.Below;
            Engine.GuiManager.RegisterControl("txtObjectName", txtObjectName);

            btnSaveName = new Button("btnSaveName", "Save", Vector2.Zero, 60, 20, txtObjectName, Color.Purple, Color.MediumPurple, Color.Purple);
            btnSaveName.TextColor = Color.White;
            btnSaveName.XOffset = 10;
            btnSaveName.ShowBorder = true;
            btnSaveName.Alignment = Alignment.Right;
            btnSaveName.RegisterListener(OnBtnSaveNameClick, MouseButton.Left, ButtonAction.OnClick);
            Engine.GuiManager.RegisterControl("btnSaveName", btnSaveName);

            btnPosUp = new Button("btnPosUp", "U", Vector2.Zero, 50, 20, txtObjectName, Color.Gray, Color.LightBlue, Color.Gray);
            btnPosUp.ShowBorder = true;
            btnPosUp.Alignment = Alignment.Below;
            btnPosUp.TextColor = Color.White;
            btnPosUp.YOffset = 10;
            btnPosUp.XOffset = 30;
            btnPosUp.RegisterListener(OnBtnPosUpClick, MouseButton.Left, ButtonAction.OnClick);
            btnPosUp.RegisterListener(OnBtnPosUpHold, MouseButton.Left, ButtonAction.OnHold);
            Engine.GuiManager.RegisterControl("btnPosUp", btnPosUp);

            txtXPos = new TextBox(Vector2.Zero, 50, 20, "", font, btnPosUp, Color.DarkGray);
            txtXPos.Alignment = Alignment.Below;
            txtXPos.YOffset = 10;
            Engine.GuiManager.RegisterControl("txtXPos", txtXPos);

            txtYPos = new TextBox(Vector2.Zero, 50, 20, "", font, txtXPos, Color.DarkGray);
            txtYPos.Alignment = Alignment.Below;
            txtYPos.YOffset = 10;
            Engine.GuiManager.RegisterControl("txtYPos", txtYPos);

            btnPosLeft = new Button("btnPosLeft", "L", Vector2.Zero, 20, 50, txtXPos, Color.Gray, Color.LightBlue, Color.Gray);
            btnPosLeft.ShowBorder = true;
            btnPosLeft.Alignment = Alignment.Left;
            btnPosLeft.TextColor = Color.White;
            btnPosLeft.XOffset = -10;
            btnPosLeft.RegisterListener(OnBtnPosLeftClick, MouseButton.Left, ButtonAction.OnClick);
            btnPosLeft.RegisterListener(OnBtnPosLeftHold, MouseButton.Left, ButtonAction.OnHold);
            Engine.GuiManager.RegisterControl("btnPosLeft", btnPosLeft);         

            btnPosRight = new Button("btnPosRight", "R", Vector2.Zero, 20, 50, txtXPos, Color.Gray, Color.LightBlue, Color.Gray);
            btnPosRight.ShowBorder = true;
            btnPosRight.Alignment = Alignment.Right;
            btnPosRight.TextColor = Color.White;
            btnPosRight.XOffset = 10;
            btnPosRight.RegisterListener(OnBtnPosRightClick, MouseButton.Left, ButtonAction.OnClick);
            btnPosRight.RegisterListener(OnBtnPosRightHold, MouseButton.Left, ButtonAction.OnHold);
            Engine.GuiManager.RegisterControl("btnPosRight", btnPosRight);

            btnPosDown = new Button("btnPosDown", "D", Vector2.Zero, 50, 20, txtYPos, Color.Gray, Color.LightBlue, Color.Gray);
            btnPosDown.ShowBorder = true;
            btnPosDown.Alignment = Alignment.Below;
            btnPosDown.TextColor = Color.White;
            btnPosDown.YOffset = 10;
            btnPosDown.RegisterListener(OnBtnPosDownClick, MouseButton.Left, ButtonAction.OnClick);
            btnPosDown.RegisterListener(OnBtnPosDownHold, MouseButton.Left, ButtonAction.OnHold);
            Engine.GuiManager.RegisterControl("btnPosDown", btnPosDown);
            #endregion


            this.RegisterNewTexture("tex_Crate.png");

            Crate crate = new Crate(Engine.Gameworld.World, "crate", new Vector2(0, 0), FarseerPhysics.Dynamics.BodyType.Static);
            crate.Body.Rotation = 0;
            this.GameObjects.Add(crate);

            Crate crate2 = new Crate(Engine.Gameworld.World, "crate2", new Vector2(-6, 10), FarseerPhysics.Dynamics.BodyType.Static);
            crate.Body.Rotation = 0;
            this.GameObjects.Add(crate2);
        }

        private void UpdateTextBoxes() {
            this.txtXPos.Text = Math.Round(selectedObject.Position.X, 2).ToString();
            this.txtYPos.Text = Math.Round(selectedObject.Position.Y, 2).ToString();
        }

        private void OnBtnSaveNameClick(Vector2 pos) { if (selectedObject != null) { selectedObject.Name = txtObjectName.Text; } }

        private void OnBtnPosLeftClick(Vector2 pos) { if (selectedObject != null) { selectedObject.Position += new Vector2(-1, 0); UpdateTextBoxes(); } }

        private void OnBtnPosLeftHold(Vector2 pos) { if (selectedObject != null) { selectedObject.Position += new Vector2(-1, 0); UpdateTextBoxes(); } }

        private void OnBtnPosRightClick(Vector2 pos) { if (selectedObject != null) { selectedObject.Position += new Vector2(1, 0); UpdateTextBoxes(); } }

        private void OnBtnPosRightHold(Vector2 pos) { if (selectedObject != null) { selectedObject.Position += new Vector2(1, 0); UpdateTextBoxes(); } }

        private void OnBtnPosUpClick(Vector2 pos) { if (selectedObject != null) { selectedObject.Position += new Vector2(0, 1); UpdateTextBoxes(); } }

        private void OnBtnPosUpHold(Vector2 pos) { if (selectedObject != null) { selectedObject.Position += new Vector2(0, 1); UpdateTextBoxes(); } }

        private void OnBtnPosDownClick(Vector2 pos) { if (selectedObject != null) { selectedObject.Position += new Vector2(0, -1); UpdateTextBoxes(); } }

        private void OnBtnPosDownHold(Vector2 pos) { if (selectedObject != null) { selectedObject.Position += new Vector2(0, -1); UpdateTextBoxes(); } }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime) {
            //throw new NotImplementedException();
        }
    }
}
