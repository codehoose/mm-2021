using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    class FontColor
    {
        public static char Black = 'a';
        public static char Blue = 'b';
        public static char Red = 'c';
        public static char Magenta = 'd';
        public static char Green = 'e';
        public static char Cyan = 'f';
        public static char Yellow = 'g';
        public static char White = 'h';
    }

    class SpectrumFont : TileImage
    {
        public string Text { get; set; }

        public SpectrumFont(Texture2D texture)
            : base(texture, 8)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, int scale)
        {
            int colourOffset = 0;
            int charIndex = 0;
            int x = 0;

            while (charIndex < Text.Length)
            {
                var xpos = (x * 8) + Position.X;
                var ypos = Position.Y;

                var dest = new Rectangle((int)(xpos * scale),
                             (int)(ypos * scale),
                             (int)(CellSize * Scale * scale),
                             (int)(CellSize * Scale * scale));

                if (Text[charIndex] == '`')
                {
                    charIndex++;
                    colourOffset = (Text[charIndex] - FontColor.Black) * 96;
                }
                else
                {
                    var ord = Text[charIndex] - ' ';
                    SetFrame(ord + colourOffset);
                    Draw(spriteBatch, Texture, dest, Source);
                    x++;
                }

                charIndex++;
            }
        }
    }
}
