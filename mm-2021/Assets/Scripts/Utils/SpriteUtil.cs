using UnityEngine;

public static class SpriteUtil
{
    public static Color[] GetSpriteData(this Texture2D tex, int row, int col)
    {
        var y = (24 - row) * 16;
        var x = col * 16;

        return tex.GetPixels(x, y, 16, 16);
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
