using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SK2D.Graphics;

namespace MonoManicMiner.Spectrum
{
    public class AirRenderer : Image
    {
        private int _airLeft = 256;

        public int AirLeft
        {
            get { return _airLeft; }
            set
            {
                _airLeft = value;
                Source = new Rectangle(0, 0, _airLeft, 4);
            }
        }

        public AirRenderer(Texture2D texture)
            : base(texture)
        {
        }
    }
}
