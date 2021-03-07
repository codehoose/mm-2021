using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    class RoomBlocks : TileImage
    {
        private readonly MMMapFile _mapFile;
        private readonly Texture2D _background;
        private readonly Texture2D _sun;

        public int Room { get; set; }

        public RoomBlocks(Texture2D texture, Texture2D background, Texture2D sun, MMMapFile mapFile, int initialRoom = 0)
            : base(texture, 8)
        {
            Room = initialRoom;
            _background = background;
            _sun = sun;
            _mapFile = mapFile;
        }

        private void DrawBlock(SpriteBatch spriteBatch, int x, int y, int blockId, int scale)
        {
            var dest = new Rectangle(x, y, CellSize * scale, CellSize * scale);
            SetFrame(blockId);

            spriteBatch.Draw(Texture,
                             dest,
                             Source,
                             Color.White,
                             0,
                             Vector2.Zero,
                             SpriteEffects.None,
                             0);
        }

        public override void Draw(SpriteBatch spriteBatch, float scale)
        {
            var blockOffset = Room * 16;
            var room = _mapFile.rooms[Room];
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    var blockId = room.blocks[(y * 32) + x];
                    if (blockId == 4)
                    {
                        DrawBlock(spriteBatch, (int)(x * 8 * scale), (int)(y * 8 * scale), blockOffset, (int)scale);
                    }
                    else
                    {
                        DrawBlock(spriteBatch, (int)(x * 8 * scale), (int)(y * 8 * scale), blockOffset + blockId, (int)scale);
                    }
                }
            }

            if (Room == 20)
            {
                Draw(spriteBatch, _background, _background.Bounds, _background.Bounds);
                Draw(spriteBatch, _sun, new Rectangle(60, 32, _sun.Width, _sun.Height), _sun.Bounds);
            }
        }
    }
}
