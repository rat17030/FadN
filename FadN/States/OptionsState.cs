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
    class OptionsState : State
    {
        private List<Component> _components;

        SpriteFont buttonFont;
        Button soundButton, soundDownButton, soundUpButton, soundSlide, musicButton, musicDownButton, musicUpButton, musicSlide, backButton;

        string consolestring = "";
        bool drawn = false;

        private decimal soundvolume = 0.1m;
        private decimal musicvolume = 0.1m;

        // For toogle char
        bool toggle = false;
        public double interval = 500;
        public double elapsedTime;
        public double elapsedTimespeed;

        public OptionsState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, float scalingFactor) : base(game, graphicsDevice, content, scalingFactor)
        {
            consolephrase = "FadN/Options> ";
            Texture2D buttonTexture = base.content.Load<Texture2D>("Controls/Button");
            Texture2D smallButtonTexture = base.content.Load<Texture2D>("Controls/Button_60x60");
            buttonFont = base.content.Load<SpriteFont>("Fonts/Console");
            float disdtance = buttonFont.MeasureString("FadN/Options> ").X;

            #region soundbuttons
            soundButton = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - buttonTexture.Height * 2.5f + 8),
                Text = ""
            };
            soundDownButton = new Button(smallButtonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10 + buttonTexture.Width + 15, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - buttonTexture.Height * 2.5f + 8),
                Text = ""
            };
            soundSlide = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10 + buttonTexture.Width + smallButtonTexture.Width, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - buttonTexture.Height * 2.5f + 8),
                Text = ""
            };
            soundUpButton = new Button(smallButtonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10 + buttonTexture.Width * 2 + smallButtonTexture.Width + 40, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - buttonTexture.Height * 2.5f + 8),
                Text = ""
            };
            #endregion

            #region musicbuttons
            musicButton = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - (buttonTexture.Height / 2)),
                Text = ""
            };
            musicDownButton = new Button(smallButtonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10 + buttonTexture.Width + 15, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - (buttonTexture.Height / 2)),
                Text = ""
            };
            musicSlide = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10 + buttonTexture.Width + smallButtonTexture.Width, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - (buttonTexture.Height / 2)),
                Text = ""
            };
            musicUpButton = new Button(smallButtonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10 + buttonTexture.Width * 2 + smallButtonTexture.Width + 40, (game.GraphicsDevice.Viewport.Bounds.Height / 2) - (buttonTexture.Height / 2)),
                Text = ""
            };
            #endregion

            backButton = new Button(buttonTexture, buttonFont, scalingFactor)
            {
                Position = new Vector2(disdtance + 10, (game.GraphicsDevice.Viewport.Bounds.Height / 2) + buttonTexture.Height * 1.5f - 8),
                Text = ""
            };

            _components = new List<Component>()
            {
                soundButton,
                soundDownButton,
                soundSlide,
                soundUpButton,
                musicButton,
                musicDownButton,
                musicSlide,
                musicUpButton,
                backButton
            };
        }

        #region OnClick_Events
        protected void SoundDownButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (soundvolume > 0 && elapsedTimespeed > interval)
            {
                soundvolume -= 0.1m;
                elapsedTimespeed = 0;
                soundSlide.Text = soundSlide.Text.Substring(0, soundSlide.Text.Length - 1);
            }
        }
        protected void SoundUpButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (soundvolume < 1 && elapsedTimespeed > interval)
            {
                soundvolume += 0.1m;
                elapsedTimespeed = 0;
                soundSlide.Text += "|";
            }
        }
        protected void MusicDownButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (musicvolume > 0 && elapsedTimespeed > interval)
            {
                musicvolume -= 0.1m;
                elapsedTimespeed = 0;
                musicSlide.Text = musicSlide.Text.Remove(musicSlide.Text.Length - 1);
            }
        }
        protected void MusicUpButtonOnClick(object sender, EventArgs eventArgs)
        {
            if (musicvolume < 1 && elapsedTimespeed > interval)
            {
                musicvolume += 0.1m;
                elapsedTimespeed = 0;
                musicSlide.Text += "|";
            }
        }
        protected void BackButtonOnClick(object sender, EventArgs eventArgs)
        {
            // TODO Save Settings
            game.ChangeState(new StartState(game, graphicsDevice, content, game.ScalingFactor));
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

                if (count == 14 && musicDownButton.getClick() == null)
                {
                    soundDownButton.click += SoundDownButtonOnClick;
                    soundUpButton.click += SoundUpButtonOnClick;
                    musicDownButton.click += MusicDownButtonOnClick;
                    musicUpButton.click += MusicUpButtonOnClick;
                    backButton.click += BackButtonOnClick;
                }

                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                elapsedTimespeed += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (count <= 13 && elapsedTime > 62.5)
                {
                    consolestring += consoleOutput(new List<string>() { "Sound", "Music", "BACK" }, new List<Button>() { soundButton, musicButton, backButton });
                    if (count == 5)
                    {
                        soundDownButton.Text = "<";
                        soundSlide.Text = "|";
                        soundUpButton.Text = ">" + game.GraphicsDevice.Viewport.Bounds.Size;
                    }
                    if (count == 7)
                    {
                        musicDownButton.Text = "<";
                        musicSlide.Text = "|";
                        musicUpButton.Text = ">" + game.GraphicsDevice.Viewport.AspectRatio;
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