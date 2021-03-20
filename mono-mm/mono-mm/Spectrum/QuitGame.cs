using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoManicMiner.Spectrum
{
    class QuitGame : SpectrumFont
    {
        private Texture2D _background;
        private Rectangle _dest;

        public QuitGame(Texture2D font, Texture2D background)
            : base(font)
        {
            _background = background;
            Text = $"`{FontColor.Yellow}Quit Game? Y/N";
            Position = new Vector2(9 * 8, 80);
            _dest = new Rectangle((int)(Position.X - 8), (int)(Position.Y - 8), background.Width, background.Height);
        }

        public override void Draw(SpriteBatch spriteBatch, int scale)
        {
            var dest = new Rectangle((int)(_dest.X * scale),
                         (int)(_dest.Y * scale),
                         (int)(_dest.Width * Scale * scale),
                         (int)(_dest.Height * Scale * scale));

            Draw(spriteBatch, _background, dest, _background.Bounds);
            base.Draw(spriteBatch, scale);
        }

    }
}
