using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace SK2D.ContentManager.ResourceBuckets
{
    class WaveFileResourceBucket : ResourceBucket<SoundEffect>
    {
        public WaveFileResourceBucket(SK2DGame game, string root)
            : base(game, root)
        {
        }

        public override SoundEffect Get(string name)
        {
            if (Resources.ContainsKey(name))
            {
                return Resources[name];
            }
            else
            {
                var path = Path.Combine(Root, name);
                var resource = SoundEffect.FromFile(path);
                Resources.Add(name, resource);
                return resource;
            }
        }
    }
}
