﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SK2D.Graphics
{
    public class Image : IPausable
    {
        public bool Paused { get; set; }

        public float TimeScale { get; set; } = 1f;

        public Texture2D Texture { get; }

        public Vector2 Size { get; }

        public Vector2 Position { get; set; }

        public float Scale { get; set; } = 1;

        public Rectangle Source { get; protected set; }

        public bool Hidden { get; set; }

        public Color DrawColor { get; set; } = Color.White;

        public static void Draw(SpriteBatch spriteBatch, Texture2D texture, Rectangle dest, Rectangle source, Color drawColor)
        {
            spriteBatch.Draw(texture,
                             dest,
                             source,
                             drawColor,
                             0,
                             Vector2.Zero,
                             SpriteEffects.None,
                             0);
        }

        public Image(Texture2D texture)
        {
            Texture = texture;
            Size = new Vector2(texture.Width, texture.Height);
            Source = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch, int scale)
        {
            var dest = new Rectangle((int)(Position.X * scale),
                                     (int)(Position.Y * scale),
                                     (int)(Source.Width * Scale * scale),
                                     (int)(Source.Height * Scale * scale));

            Draw(spriteBatch, Texture, dest, Source, DrawColor);
        }

        public void Update(float deltaTime)
        {
            if (Paused)
            {
                return;
            }

            OnUpdate(deltaTime * TimeScale);
        }

        protected virtual void OnUpdate(float deltaTime)
        {

        }
    }
}
