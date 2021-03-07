using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace SK2D.ContentManager.ResourceBuckets
{
    internal class TextureResourceBucket : ResourceBucket<Texture2D>
    {
        public TextureResourceBucket(SK2DGame game, string root)
            : base(game, root)
        {
        }

        public override Texture2D Get(string name)
        {
            if (Resources.ContainsKey(name))
            {
                return Resources[name];
            }
            else
            {
                var fullPath = Path.Combine(Root, name);
                var texture2D = Texture2D.FromFile(Game.GraphicsDevice, fullPath);
                Resources.Add(name, texture2D);
                return texture2D;
            }
        }
    }
}
