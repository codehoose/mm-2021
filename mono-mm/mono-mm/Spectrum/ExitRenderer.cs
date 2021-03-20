using ManicMiner.Converter.Lib.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    public class ExitRenderer : SpriteSheet
    {
        private static float REFRESH_SPEED = 0.3f;
        private static int NORMAL_EXIT_OFFSET = 420;
        private static int FLASHING_EXIT_OFFSET = 440;

        private int _roomId;
        private int _anim;
        private float _time;

        public bool Flashing { get; set; }

        public ExitRenderer(Texture2D texture)
            : base(texture, 16)
        {

        }

        public void SetRoom(MMRoom room, int roomId)
        {
            _roomId = roomId;
            SetFrame(420 + roomId);
            Position = new Vector2(room.exitPosition.x, room.exitPosition.y);
        }

        protected override void OnUpdate(float deltaTime)
        {
            _time += deltaTime;
            if (_time < REFRESH_SPEED)
            {
                return;
            }

            _time -= REFRESH_SPEED;

            if (Flashing)
            {
                _anim++;
                _anim %= 2;
                var frameOffset = _anim == 0 ? NORMAL_EXIT_OFFSET : FLASHING_EXIT_OFFSET;
                SetFrame(frameOffset + _roomId);
            }
        }
    }
}
