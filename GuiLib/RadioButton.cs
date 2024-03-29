﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GuiLib {

    class RadioButton : Control {
        public event EventHandler onChange;
        private Animation buttonStates;

        public RadioButton() {
            text = "";

            buttonStates = new Animation(2, 1, Sheet.MainSheet);
            setSize(24, 24);
        }

        public override void initialize() {
            buttonStates.loadSheet(new Rectangle(2, 52, 98, 48));
            buttonStates.updateScale(new Vector2(controlSize.Width, controlSize.Height));

            base.initialize();
        }

        public override void update(Vector2 offset) {
            if (InputHandler.leftClickRelease() && hovering) {
                isSelected = true;
                buttonStates.frame = 1;
                select();
                renderer.render(realSize);
                eventTrigger(onChange);
            }
            base.update(offset);
        }

        protected override void sizeChanged(object sender, EventArgs e) {
            realSize = new Size(realSize.Width + textSize.Width, (int)Math.Max(realSize.Height, textSize.Height));
        }

        protected override void setSize(int Width, int Height){
            controlSize = new Size(Width, Height);
            realSize = new Size(Width + textSize.Width, (int)Math.Max(Height, textSize.Height));

            buttonStates.updateScale(new Vector2(controlSize.Width, controlSize.Height));
        }

        public override void deselect() {
            if (!isSelected) return;

            isSelected = false;
            buttonStates.frame = 0;
            eventTrigger(onChange);
        }

        public override void render() {
            buttonStates.draw();
            GUIRoot.spriteBatch.DrawString(FontManager.fonts[Font.Verdana], text,
                new Vector2(controlSize.Width + 2, controlSize.Height / 2 - textSize.Height / 2), Color.Black);
        }
    }
}
