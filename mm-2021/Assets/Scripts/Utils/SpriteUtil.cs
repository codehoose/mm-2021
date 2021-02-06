using System.Linq;
using UnityEngine;

public static class SpriteUtil
{
    public static Texture2D GetFrame(this Texture2D tex, int frame)
    {
        return GetSpriteData(tex, frame / 20, frame % 20)
                .ToTexture2D(16, 16);
    }

    public static Texture2D[] GetFrames(this Texture2D tex, int startFrame, int length)
    {
        return Range(startFrame, length)
                .Select((n) => GetFrame(tex, n))
                .ToArray();
    }

    public static Sprite[] ToSprites(this Texture2D[] textures)
    {
        return textures.Select(t => t.ToSprite())
                       .ToArray();
    }

    public static Color[] GetSpriteData(this Texture2D tex, int row, int col)
    {
        var y = (24 - row) * 16;
        var x = col * 16;

        return tex.GetPixels(x, y, 16, 16);
    }

    public static int[] Range(int start, int length)
    {
        int[] values = new int[length];
        for(int i = 0; i < length; i ++)
        {
            values[i] = start + i;
        }
        return values;
    }

    public static Sprite[] GetExitFrames(this Texture2D tex, int row, int col)
    {
        var frame0 = GetSpriteData(tex, row, col)
                        .ToTexture2D(16, 16)
                        .ToSprite();
        var frame1 = GetSpriteData(tex, row + 1, col)
                        .ToTexture2D(16, 16)
                        .ToSprite();

        return new Sprite[] { frame0, frame1 };
    }

    public static Texture2D ToTexture2D(this Color[] pixels, int width, int height)
    {
        var tex = CreateTexture(width, height);
        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }

    public static Sprite ToSprite(this Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 1), 1);
    }

    public static Texture2D CreateTexture(int width, int height)
    {
        var tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;
        return tex;
    }

    public static void Clear(this Texture2D tex, Color color)
    {
        Color[] clear = new Color[tex.width * tex.height];
        for (int i = 0; i < clear.Length; i++)
            clear[i] = color;

        tex.SetPixels(clear);
        tex.Apply();
    }
}
