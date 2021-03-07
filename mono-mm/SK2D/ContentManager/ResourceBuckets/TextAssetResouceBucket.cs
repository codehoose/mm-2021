using System.IO;

namespace SK2D.ContentManager.ResourceBuckets
{
    class TextAssetResouceBucket : ResourceBucket<string>
    {
        public TextAssetResouceBucket(SK2DGame game, string root)
            : base(game, root)
        {
        }

        public override string Get(string name)
        {
            if (Resources.ContainsKey(name))
            {
                return Resources[name];
            }
            else
            {
                var fullPath = Path.Combine(Root, name);
                var text = File.ReadAllText(fullPath);
                Resources.Add(name, text);
                return text;
            }
        }
    }
}
