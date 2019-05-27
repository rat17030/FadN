using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FadN.Components.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FadN.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager content;

        protected GraphicsDevice graphicsDevice;

        protected MainGame game;

        internal float scalingFactor;

        public int count;

        // Console Phrase which will be repeated
        public string consolephrase = "FadN> ";

        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        protected State(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, float scalingFactor)
        {
            this.game = game;
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            this.scalingFactor = scalingFactor;
            scalingFactor = game.GraphicsDevice.Viewport.Height/720; // 720w||1280h-1080
        }

        public abstract void Update(GameTime gameTime);

        #endregion

        public string consoleOutput(List<string> values, List<Button> buttons)
        {
            string s = "";
            if (count == 1 || count == 2 || count == 3 || count == 4 || count == 6 || count == 8 || count == 10 || count == 11 || count == 12)
            {
                s = consolephrase + "\n";
            }
            if (count == 13)
            {
                s = consolephrase;
            }
            if (count == 5)
            {
                s = consolephrase + "\n";
                buttons[0].Text = values[0];
            }
            if (count == 7)
            {
                s = consolephrase + "\n";
                buttons[1].Text = values[1];
            }
            if (count == 9)
            {
                s = consolephrase + "\n";
                buttons[2].Text = values[2];
            }
            count++;
            return s;
        }
    }
}