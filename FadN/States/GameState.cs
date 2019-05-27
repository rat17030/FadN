using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FadN.Components;
using FadN.Components.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FadN.States
{
    public class GameState : State
    {
        private List<Component> _components;

        SpriteFont buttonFont;
        Button gameButton, continueButton, backButton;

        string consolestring = "";
        bool drawn = false;
        
        // For toogle char
        bool toggle = false;
        public double interval = 500;
        public double elapsedTime;

        public GameState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, float scalingFactor) : base(game, graphicsDevice, content, scalingFactor)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                game.Exit();
        }

    }
}