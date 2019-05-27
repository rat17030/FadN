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
using Microsoft.Xna.Framework.Input.Touch;

namespace FadN.States
{
    class StartState : State
    {
        private List<Component> _components;

        SpriteFont buttonFont;
        Button playButton, optionButton, endButton;

        string consolestring = "";
        bool drawn = false;

        // For toogle char
        bool toggle = false;
        public double interval = 500;
        public double elapsedTime;

        public StartState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, float scalingFactor) : base(game, graphicsDevice, content, scalingFactor)
        {
            consolephrase = "FadN/MainMenu> ";
            Texture2D buttonTexture = base.content.Load<Texture2D>("Controls/Button");
            buttonFont = base.content.Load<SpriteFont>("Fonts/Console");
            float disdtance = buttonFont.MeasureString("FadN/MainMenu> ").X;
            playButton = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - buttonTexture.Height * 2.5f + 8),
                Text = ""
            };
            optionButton = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - (buttonTexture.Height / 2)),
                Text = ""
            };
            endButton = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10, (game.GraphicsDevice.Viewport.Bounds.Height / 2) + buttonTexture.Height * 1.5f - 8),
                Text = ""
            };


            _components = new List<Component>()
            {
                playButton,
                optionButton,
                endButton
            };
        }


        #region Eventhandler

        protected void PlayButtonOnClick(object sender, EventArgs eventArgs)
        {
            game.ChangeState(new LobyState(game, graphicsDevice, content, game.ScalingFactor));
        }
        protected void OptionButtonOnClick(object sender, EventArgs eventArgs)
        {
            game.ChangeState(new OptionsState(game, graphicsDevice, content, game.ScalingFactor));
        }
        protected void EndButtonOnClick(object sender, EventArgs eventArgs)
        {
            game.Exit();

        }

        #endregion

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(buttonFont, consolestring, new Vector2(0, 0), Color.White, 0, Vector2.Zero, game.ScalingFactor, SpriteEffects.None, 0);
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            drawn = true;
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }

            TouchCollection touchCollection = TouchPanel.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                game.Exit();


            if (drawn)
            {

                if (count == 14 && playButton.getClick() == null)
                {
                    playButton.click += PlayButtonOnClick;
                    optionButton.click += OptionButtonOnClick;
                    endButton.click += EndButtonOnClick;
                }


                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (count <= 13 && elapsedTime > 62.5)
                {
                    consolestring += consoleOutput(new List<string>() { "PLAY", "OPTIONS", "END" }, new List<Button>() { playButton, optionButton, endButton });
                    elapsedTime -= 62.5;
                }
                if (elapsedTime > interval)
                {
                    elapsedTime -= interval;
                    if (count == 14 && !toggle)
                    {
                        consolestring += "_";
                        toggle = true;
                    }
                    else if (count == 14 && toggle)
                    {
                        consolestring = consolestring.Substring(0, consolestring.Length - 1);
                        toggle = false;
                    }
                }
            }
        }
    }
}