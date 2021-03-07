using System.Collections.Generic;

namespace SK2D.ContentManager
{
    abstract class ResourceBucket<T> : IResourceBucket
    {
        protected Dictionary<string, T> Resources { get; } = new Dictionary<string, T>();

        protected string Root { get; }

        public SK2DGame Game { get; }

        public abstract T Get(string name);

        object IResourceBucket.Get(string name)
        {
            return  Get(name);
        }

        public ResourceBucket(SK2DGame game, string root)
        {
            Game = game;
            Root = root;
        }
    }
}
