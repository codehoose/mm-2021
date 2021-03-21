using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    class RoomBlocks : TileImage
    {
        private readonly Texture2D _background;
        private readonly Texture2D _sun;
        private int _conveyorFrame;
        private MMRoom _room;
        private int _roomId;
        private float _time;

        public int KeyAnimFrame { get; set; }        

        public RoomBlocks(Texture2D texture, Texture2D background, Texture2D sun)
            : base(texture, 8)
        {
            _background = background;
            _sun = sun;
        }

        public void SetRoom(MMRoom room, int roomID)
        {
            _room = room;
            _roomId = roomID;
        }

        public override void Draw(SpriteBatch spriteBatch, int scale)
        {
            var blockOffset = _roomId * 16;
            DrawRoom(spriteBatch, scale, blockOffset, _room);
            DrawKeys(spriteBatch, scale, blockOffset, _room);
        }

        private void DrawKeys(SpriteBatch spriteBatch, float scale, int blockOffset, MMRoom room)
        {
            foreach (var key in room.keys)
            {
                // TODO: Figure out which ones have been collected...
                var keyAnimFrame = blockOffset + 11 + KeyAnimFrame;
                DrawBlock(spriteBatch, (int)(key.x * scale), (int)(key.y * scale), keyAnimFrame, (int)scale);
            }
        }

        private void DrawRoom(SpriteBatch spriteBatch, int scale, int blockOffset, MMRoom room)
        {
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    var blockId = room.blocks[(y * 32) + x];
                    if (blockId == 4)
                    {
                        DrawCrumbling(spriteBatch, x, y, blockOffset, (int)scale);
                    }
                    else if (blockId != 7)
                    {
                        DrawBlock(spriteBatch, (int)(x * 8 * scale), (int)(y * 8 * scale), blockOffset + blockId, (int)scale);
                    }
                }
            }

            DrawConveyor(spriteBatch, blockOffset, (int)scale);

            if (_roomId == 20)
            {
                Draw(spriteBatch, _background, _background.Bounds, _background.Bounds, DrawColor);
                Draw(spriteBatch, _sun, new Rectangle(60, 32, _sun.Width, _sun.Height), _sun.Bounds, DrawColor);
            }
        }

        private void DrawCrumbling(SpriteBatch spriteBatch, int x, int y, int blockOffset, int scale)
        {
            var height = _room.crumbs[y * 32 + x];
            var yoff = (8 - height);
            var dest = new Rectangle(x * 8 * scale, ((y * 8) + yoff) * scale, CellSize * scale, height * scale);
            SetFrame(blockOffset + 4);
            var source = new Rectangle(Source.X, Source.Y, Source.Width, height);

            spriteBatch.Draw(Texture,
                             dest,
                             source,
                             DrawColor,
                             0,
                             Vector2.Zero,
                             SpriteEffects.None,
                             0);
        }

        private void DrawConveyor(SpriteBatch spriteBatch, int blockOffset, int scale)
        {
            var travelator = _room.travelator;

            for (var x = 0; x < travelator.len; x++)
            {
                DrawBlock(spriteBatch, (int)((travelator.pos.x + (x * 8)) * scale), (int)(travelator.pos.y * scale), (blockOffset + 7) + _conveyorFrame, (int)scale);
            }
        }

        private void DrawBlock(SpriteBatch spriteBatch, int x, int y, int blockId, int scale)
        {
            var dest = new Rectangle(x, y, CellSize * scale, CellSize * scale);
            SetFrame(blockId);

            spriteBatch.Draw(Texture,
                             dest,
                             Source,
                             DrawColor,
                             0,
                             Vector2.Zero,
                             SpriteEffects.None,
                             0);
        }

        protected override void OnUpdate(float deltaTime)
        {
            _time += deltaTime;
            if (_time < 0.1f)
            {
                return;
            }

            _time -= 0.1f;

            var travelator = _room.travelator;
            if (travelator.len > 0)
            {
                var tick = travelator.dir > 0 ? 1 : -1;
                _conveyorFrame = (_conveyorFrame + tick) & 3;
            }
        }
    }
}
