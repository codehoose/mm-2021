using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SK2D.Graphics
{
    public class Image
    {
        public Texture2D Texture { get; }

        public Vector2 Size { get; }

        public Vector2 Position { get; set; }

        public float Scale { get; set; } = 1;

        public Rectangle Source { get; protected set; }

        public Image(Texture2D texture)
        {
            Texture = texture;
            Size = new Vector2(texture.Width, texture.Height);
            Source = new Rectangle(0, 0, texture.Width, texture.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch, float scale)
        {
            var dest = new Rectangle((int)(Position.X * scale),
                                     (int)(Position.Y * scale),
                                     (int)(Size.X * Scale * scale),
                                     (int)(Size.Y * Scale * scale));
            spriteBatch.Draw(Texture,
                             dest,
                             Source,
                             Color.White,
                             0,
                             Vector2.Zero,
                             SpriteEffects.None, 0);
        }
    }
}
