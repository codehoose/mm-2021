using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SK2D.Graphics
{
    public class Renderer
    {
        private readonly Layer[] _layerOrder = new Layer[] { Layer.Background, Layer.Sprite, Layer.Foreground, Layer.UI };

        private Dictionary<Layer, List<Image>> _layers = new Dictionary<Layer, List<Image>>();

        private readonly SpriteBatch _spriteBatch;

        public float Scale { get; set; } = 1f;

        public Renderer(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void AddImage(Image image, Layer layer)
        {
            if (!_layers.TryGetValue(layer, out List<Image> images))
            {
                images = new List<Image>();
                _layers[layer] = images;
            }

            images.Add(image);
        }

        public void AddImage(Image image, Layer layer, int x, int y)
        {
            image.Position = new Vector2(x, y);
            AddImage(image, layer);
        }

        public void Update(float deltaTime)
        {
            foreach (var kvp in _layers)
            {
                foreach (var image in kvp.Value)
                {
                    image.Update(deltaTime);
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            // From https://stackoverflow.com/questions/9215027/nearest-neighbor-zoom
            _spriteBatch.Begin(SpriteSortMode.Deferred,
                               BlendState.AlphaBlend,
                               SamplerState.PointClamp,
                               DepthStencilState.None,
                               RasterizerState.CullCounterClockwise);

            foreach (var layer in _layerOrder)
            {
                if (_layers.ContainsKey(layer))
                {
                    foreach (var image in _layers[layer])
                    {
                        image.Draw(_spriteBatch, Scale);
                    }
                }
            }
            _spriteBatch.End();
        }

        public void Clear()
        {
            foreach (var kvp in _layers)
            {
                kvp.Value.Clear();
            }
        }
    }
}
