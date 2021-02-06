using System;
using UnityEngine;

public class TextData
{
    private string _text;

    public int X { get; }

    public int Y { get; }

    public Texture2D Texture { get; set; }

    public SpriteRenderer Renderer { get; set; }

    public int FontColour { get; }

    public string Text { get { return _text; }
        set
        {
            _text = value;
            TextChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler TextChanged;

    public TextData(int x, int y, string text, int fontColour = 7)
    {
        X = x;
        Y = y;
        _text = text;
        FontColour = fontColour;
    }
}