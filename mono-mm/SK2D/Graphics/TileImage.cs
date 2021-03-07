using Microsoft.Xna.Framework.Graphics;

namespace SK2D.Graphics
{
    public class TileImage : SpriteSheet
    {
        public TileImage(Texture2D blocks, int cellSize = 16)
            : base (blocks, cellSize)
        {

        }
    }
}
