using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    public class LivesIndicator : SpriteSheet
    {
        public int Lives { get; set; }

        public int Frame { get; set; }

        public bool MusicPlaying { get; set; }

        public bool CheatOn { get; set; }

        public LivesIndicator(Texture2D texture)
            : base(texture, 16)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, float scale)
        {
            var count = Math.Clamp(Lives, 0, 8);
            var livesFrame = MusicPlaying ? Frame : 0;

            for (var x = 0; x < count; x++)
            {
                SetFrame(livesFrame);
                var dest = new Rectangle((int)(x * 16 * scale), (int)(168 * scale), (int)(CellSize * scale), (int)(CellSize * scale));
                Draw(spriteBatch, Texture, dest, Source);
            }

            if (CheatOn)
            {
                SetFrame(461);
                var dest = new Rectangle((int)(count * 16 * scale), (int)(168 * scale), (int)(CellSize * scale), (int)(CellSize * scale));
                Draw(spriteBatch, Texture, dest, Source);
            }
        }
    }
}
