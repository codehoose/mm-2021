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
        private float _time;

        public int Room { get; set; }
        public int KeyAnimFrame { get; set; }
        public MMMapFile MapFile { get; set; }

        public RoomBlocks(Texture2D texture, Texture2D background, Texture2D sun, int initialRoom = 0)
            : base(texture, 8)
        {
            Room = initialRoom;
            _background = background;
            _sun = sun;
        }

        public override void Draw(SpriteBatch spriteBatch, float scale)
        {
            var blockOffset = Room * 16;
            var room = MapFile.rooms[Room];

            DrawRoom(spriteBatch, scale, blockOffset, room);
            DrawKeys(spriteBatch, scale, blockOffset, room);
        }

        private void DrawKeys(SpriteBatch spriteBatch, float scale, int blockOffset, MMRoom room)
        {
            foreach (var key in room.keys)
            {
                // TODO: Figure out which ones have been collected...
                var keyAnimFrame = blockOffset + 11 + KeyAnimFrame;
                DrawBlock(spriteBatch, (int)(key.x * scale), (int)(key.y * scale), keyAnimFrame, (int)scale);
            }

    //        count = 0

    //For i = 1 To 5

    //    If cKEYSs(i)= 1

    //        DrawImage(imageBLOCKS, cKEYSx(i), cKEYSy(i), (BLOCKoff + 11) + cKEYSb(i))

    //        count = count + 1

    //        cKEYSbp(i) = cKEYSbp(i) + 1

    //        If cKEYSbp(i)= 2

    //            cKEYSb(i) = (cKEYSb(i) + 1) And 3

    //            cKEYSbp(i) = 0

    //        End If

    //    End If

    //Next
    //If count = 0

    //    cEXITs = 1

    //End If
        }

        private void DrawRoom(SpriteBatch spriteBatch, float scale, int blockOffset, MMRoom room)
        {

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 32; x++)
                {
                    var blockId = room.blocks[(y * 32) + x];
                    if (blockId == 4)
                    {
                        DrawBlock(spriteBatch, (int)(x * 8 * scale), (int)(y * 8 * scale), blockOffset, (int)scale);
                    }
                    else if (blockId != 7)
                    {
                        DrawBlock(spriteBatch, (int)(x * 8 * scale), (int)(y * 8 * scale), blockOffset + blockId, (int)scale);
                    }
                }
            }

            // Draw Conveyor...
            DrawConveyor(spriteBatch, blockOffset, (int)scale);

            if (Room == 20)
            {
                Draw(spriteBatch, _background, _background.Bounds, _background.Bounds);
                Draw(spriteBatch, _sun, new Rectangle(60, 32, _sun.Width, _sun.Height), _sun.Bounds);
            }
        }

        private void DrawConveyor(SpriteBatch spriteBatch, int blockOffset, int scale)
        {
            var travelator = MapFile.rooms[Room].travelator;

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
                             Color.White,
                             0,
                             Vector2.Zero,
                             SpriteEffects.None,
                             0);
        }

        public override void Update(float deltaTime)
        {
            _time += deltaTime;
            if (_time < 0.1f)
            {
                return;
            }

            _time -= 0.1f;

            var travelator = MapFile.rooms[Room].travelator;
            //        If cCONVl> 0
            if (travelator.len > 0)
            {

                //    For x = 0 To(cCONVl - 1)
                //for (var x = 0; x < travelator.len; x++)
                //{

                //    //        DrawBlock(imageBLOCKS, cCONVx + (x * 8), cCONVy, (BLOCKoff + 7) + cCONVanim)

                //    //    Next
                //}

                if (travelator.dir == 1)
                {
                    _conveyorFrame = (_conveyorFrame + 1) & 3;
                }
                else
                {
                    _conveyorFrame = (_conveyorFrame - 1) & 3;
                }

                //    If Not cCONVd

                //        cCONVanim = (cCONVanim + 1) And 3

                //    Else
                //        cCONVanim = (cCONVanim - 1) And 3

                //    End If

                //End If
            }
        }
    }
}
