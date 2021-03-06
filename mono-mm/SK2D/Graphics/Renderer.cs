﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SK2D.Graphics
{
    public class Renderer
    {
        private Color _clearColour = Color.Black;

        private Queue<Action> _actions = new Queue<Action>();

        private readonly Layer[] _layerOrder = new Layer[] { Layer.Background, Layer.Sprite, Layer.Foreground, Layer.UI };

        private Dictionary<Layer, List<Image>> _layers = new Dictionary<Layer, List<Image>>();

        private readonly SpriteBatch _spriteBatch;

        public int Scale { get; set; } = 1;

        public bool ClearBackBuffer { get; set; } = true;

        public Renderer(SpriteBatch spriteBatch)
        {
            _spriteBatch = spriteBatch;
        }

        public void AddImage(Image image, Layer layer)
        {
            _actions.Enqueue(() =>
            {

                if (!_layers.TryGetValue(layer, out List<Image> images))
                {
                    images = new List<Image>();
                    _layers[layer] = images;
                }

                images.Add(image);
            });
        }

        public void AddImage(Image image, Layer layer, int x, int y)
        {
            _actions.Enqueue(() =>
            {
                image.Position = new Vector2(x, y);
                AddImage(image, layer);
            });
        }

        public void Update(float deltaTime)
        {
            while (_actions.Count > 0)
            {
                _actions.Dequeue()();
            }

            foreach (var kvp in _layers)
            {
                foreach (var image in kvp.Value)
                {
                    if (!image.Paused)
                    {
                        image.Update(deltaTime);
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (ClearBackBuffer)
            {
                _spriteBatch.GraphicsDevice.Clear(_clearColour);
            }

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
                        if (!image.Hidden)
                        {
                            image.Draw(_spriteBatch, Scale);
                        }
                    }
                }
            }
            _spriteBatch.End();
        }

        public void Clear()
        {
            _actions.Enqueue(() =>
            {
                foreach (var kvp in _layers)
                {
                    kvp.Value.Clear();
                }
            });
        }
    }
}
