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
    public class SelectionState : State
    {
        private List<Component> _components;

        SpriteFont buttonFont;
        Texture2D smallbuttonTexture;
        Button leftButton, rigthButton, playButton, backButton;

        string consolestring = "";
        bool drawn = false;

        // For toogle char
        bool toggle = false;
        public double interval = 500;
        public double elapsedTime;

        public SelectionState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, float scalingFactor) : base(game, graphicsDevice, content, scalingFactor)
        {
            consolephrase = "FadN/Selection> ";
            Texture2D buttonTexture = base.content.Load<Texture2D>("Controls/Button");
            smallbuttonTexture = base.content.Load<Texture2D>("Controls/Button_60x60");
            buttonFont = base.content.Load<SpriteFont>("Fonts/Console");
            float disdtance = buttonFont.MeasureString("FadN/Selection> ").X;
            leftButton = new Button(smallbuttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(400, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - (buttonTexture.Height / 2)),
                Text = ""
            };
            rigthButton = new Button(smallbuttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(1000, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - (buttonTexture.Height / 2)),
                Text = "",
            };
            playButton = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(620, (game.GraphicsDevice.Viewport.Bounds.Height / 2) + buttonTexture.Height * 1.5f - 8),
                Text = ""
            };
            backButton = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10, (game.GraphicsDevice.Viewport.Bounds.Height) - buttonTexture.Height * 2.75f),
                Text = ""
            };

            _components = new List<Component>()
            {
                leftButton,
                rigthButton,
                playButton,
                backButton
            };
        }
        protected void PlayButtonOnClick(object sender, EventArgs eventArgs)
        {
        }
        protected void LeftButtonOnClick(object sender, EventArgs eventArgs)
        {
        }
        protected void RigthButtonOnClick(object sender, EventArgs eventArgs)
        {
        }
        protected void BackButtonOnClick(object sender, EventArgs eventArgs)
        {
            game.ChangeState(new LobyState(game, graphicsDevice, content, game.ScalingFactor));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(buttonFont, consolestring, new Vector2(0, 0), Color.White, 0, Vector2.Zero, scalingFactor, SpriteEffects.None, 0);
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
                if (count == 14 && leftButton.getClick() == null)
                {
                    playButton.click += PlayButtonOnClick;
                    leftButton.click += LeftButtonOnClick;
                    rigthButton.click += RigthButtonOnClick;
                    backButton.click += BackButtonOnClick;
                }

                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (count <= 13 && elapsedTime > 62.5)
                {
                    consolestring += consoleOutput(new List<string>() { "", "<", "PLAY" }, new List<Button>() { new Button(smallbuttonTexture, buttonFont,scalingFactor), leftButton, playButton });
                    if(count == 7)
                    {
                        rigthButton.Text = ">";
                    }
                    if(count == 12)
                    {
                        backButton.Text = "BACK";
                    }

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