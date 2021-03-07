using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using SK2D.ContentManager.ResourceBuckets;
using SK2D.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace SK2D.ContentManager
{
    public class LocalContentManager
    {
        private readonly string _root;

        private SK2DGame _game;
        private Dictionary<Type, IResourceBucket> _buckets = new Dictionary<Type, IResourceBucket>();

        public LocalContentManager(SK2DGame game)
        {
            _game = game;
            _root = _game.Content.RootDirectory;
            _buckets.Add(typeof(Texture2D), new TextureResourceBucket(game, Path.Combine(_root, "images")));
            _buckets.Add(typeof(string), new TextAssetResouceBucket(game, Path.Combine(_root, "json")));
        }

        public Texture2D LoadTexture(string name)
        {
            return _buckets[typeof(Texture2D)].Get(name) as Texture2D;
        }

        public Image LoadImage(string name)
        {
            var texture = _buckets[typeof(Texture2D)].Get(name) as Texture2D;
            return new Image(texture);
        }

        public SpriteSheet LoadSpriteSheet(string name, int cellSize)
        {
            var texture = _buckets[typeof(Texture2D)].Get(name) as Texture2D;
            return new SpriteSheet(texture, cellSize);
        }

        public TileImage LoadTileImage(string name, int cellSize)
        {
            var texture = _buckets[typeof(Texture2D)].Get(name) as Texture2D;
            return new TileImage(texture, cellSize);
        }

        public T LoadJson<T>(string name)
        {
            var json = _buckets[typeof(string)].Get(name) as string;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
