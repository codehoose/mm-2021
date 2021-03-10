using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SK2D.Graphics
{
    public class SpriteSheet : Image
    {
        private readonly int _maxFrames;
        private readonly int _columns;

        protected int CellSize { get; }

        public SpriteSheet(Texture2D texture, int cellSize = 16)
            : base(texture)
        {
            CellSize = cellSize;
            _columns = texture.Width / cellSize;
            _maxFrames = _columns * (texture.Height / cellSize);
        }

        public override void Draw(SpriteBatch spriteBatch, int scale)
        {
            var dest = new Rectangle((int)(Position.X * scale), (int)(Position.Y * scale), (int)(CellSize * scale), (int)(CellSize * scale));
            Draw(spriteBatch, Texture, dest, Source);
        }

        public void SetFrame(int frame)
        {
            Source = CalculateRectangle(frame);
        }

        private Rectangle CalculateRectangle(int frame)
        {
            var f = frame % _maxFrames;
            var x = (f % _columns) * CellSize;
            var y = (f / _columns) * CellSize;
            return new Rectangle(x, y, CellSize, CellSize);
        }
    }
}
