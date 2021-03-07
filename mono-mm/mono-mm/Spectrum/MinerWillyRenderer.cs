using System;
using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    public class MinerWillyRenderer : SpriteSheet
    {
        private int dir;

        public MinerWillyRenderer(Texture2D texture)
            : base(texture, 16)
        {
            
        }

        public void Init(MMWillyStart start)
        {
            Position = new Vector2(start.pos.x, start.pos.y);
            dir = start.dir;
        }

        public override void Draw(SpriteBatch spriteBatch, float scale)
        {
            base.Draw(spriteBatch, scale);
        }

        //       public override void Draw(SpriteBatch spriteBatch, float scale)
        //       {
        //           var x = Position.X;
        //           var y = Position.Y;



        ////           If cWILLYd

        ////       DrawImage(image16, cWILLYx And 248, cWILLYy, 8 + ((cWILLYx And 15)Shr 1))
        ////Else

        ////       DrawImage(image16, cWILLYx And 248, cWILLYy, (cWILLYx And 15)Shr 1)
        ////End If
        //       }

        public override void Update(float deltaTime)
        {
            var blockId = dir == 1 ? (8 + ((int)Position.X & 15) >> 1) : ((int)Position.X & 15) >> 1;
            SetFrame(blockId);
        }
    }
}
