using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace FadN.Components.Controls
{
    public class Button : Component
    {

        #region Fields

        private Point _currentTouch;

        private SpriteFont _font;

        private Point _previousTouch;

        private Texture2D _texture;

        #endregion

        #region Properties

        public event EventHandler click = null;

        public void resetPreviosTuch()
        {
            _previousTouch = new Point(0, 0);
        }

        public EventHandler getClick()
        {
            return click;
        }

        public Color PenColor { get; set; }

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)(Position.X), (int)(Position.Y), (int)(_texture.Width * _scalingFactor), (int)(_texture.Height * _scalingFactor)); }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font, float scalingFactor) : base(scalingFactor)
        {
            _texture = texture;

            _font = font;

            PenColor = Color.White;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(Position.X, Position.Y), null, Color.White, 0, Vector2.Zero, _scalingFactor, SpriteEffects.None, 0);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + 10);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2) + 4;

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            TouchCollection touchLocations = TouchPanel.GetState();
            if (touchLocations.Count > 0)
            {
                _previousTouch = _currentTouch;
                _currentTouch = touchLocations[0].Position.ToPoint();

                var touchRectangle = new Rectangle(_currentTouch.X, _currentTouch.Y, 1, 1);
                if (touchRectangle.Intersects(Rectangle))
                {
                    click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion

    }
}