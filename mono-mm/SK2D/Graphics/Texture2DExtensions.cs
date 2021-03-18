using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SK2D.Graphics
{
    public static class Texture2DExtensions
    {
        public static void Fill(this Texture2D texture, Color color)
        {
            var length = texture.Width * texture.Height;

            Color[] colors = new Color[length];
            for (int i = 0; i < length; i++)
            {
                colors[i] = color;
            }
            
            texture.SetData<Color>(colors);
        }
        
    }
}
