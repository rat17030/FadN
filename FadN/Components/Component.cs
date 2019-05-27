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

namespace FadN.Components
{
    public abstract class Component
    {
        internal float _scalingFactor;
        private Vector2 _position;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = new Vector2(value.X * _scalingFactor, value.Y * _scalingFactor); }
        }


        public Component(float scalingFactor)
        {
            _scalingFactor = scalingFactor;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}