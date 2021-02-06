using System;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumFont : MonoBehaviour
{
    private Dictionary<string, TextData> _text;

    public Texture2D _font;

    public void Add(string key, TextData text)
    {
        var go = new GameObject("Spectrum Text");
        go.transform.SetParent(gameObject.transform);
        go.transform.position = new Vector3(text.X, -text.Y);
        var tex = SpriteUtil.CreateTexture(256, 8);
        tex.Clear(Color.clear);

        var sr = go.AddComponent<SpriteRenderer>();
        text.Texture = tex;
        text.Renderer = sr;
        sr.sprite = Sprite.Create(tex, new Rect(0, 0, 256, 8), new Vector2(0, 1), 1);
        sr.sortingOrder = text.SortingOrder;
        text.TextChanged += Text_Changed;

        _text.Add(key, text);

        ApplyText(text);
    }

    private void ApplyText(TextData text)
    {
        text.Texture.Clear(Color.clear);

        var topY = 184; // Font image is 192px tall, -8 for the actual row

        for (int i = 0; i < text.Text.Length || i == 31; i++)
        {
            var ord = (text.Text[i] - ' ');
            var x = 8 * (ord % 32);
            var y = topY - (text.FontColour * 24) - (8 * (ord / 32));
            TexUtil.Blit(_font, text.Texture, new Rect(x, y, 8, 8), new Vector2(i * 8, 0));
        }
    }

    private void Text_Changed(object sender, EventArgs e)
    {
        var text = sender as TextData;
        ApplyText(text);
    }

    private void Awake()
    {
        _text = new Dictionary<string, TextData>();
    }
}
