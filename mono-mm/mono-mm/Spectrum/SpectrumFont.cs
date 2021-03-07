using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    class SpectrumFont : TileImage
    {
        public string Text { get; set; }

        public SpectrumFont(Texture2D texture, int cellSize)
            : base(texture, cellSize)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, float scale)
        {
            int colourOffset = 0;

            for (int x = 0; x < Text.Length; x++)
            {
                var xpos = (x * 8) + Position.X;
                var ypos = Position.Y;

                var dest = new Rectangle((int)(xpos * scale),
                             (int)(ypos * scale),
                             (int)(CellSize * Scale * scale),
                             (int)(CellSize * Scale * scale));

                if (Text[x] == '`')
                {
                    colourOffset = (Text[x] - 96) * 96;
                    x = x + 1;
                }
                else
                {
                    var ord = Text[x] - ' ';
                    SetFrame(ord + colourOffset);
                    Draw(spriteBatch, Texture, dest, Source);
                }
            }
        }
    }
}
