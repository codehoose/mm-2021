using UnityEngine;

public static class TexUtil
{
    public static void Blit(Texture2D source, Texture2D dest, Rect srcRect, Vector2 pos)
    {
        var x = (int)srcRect.x;
        var y = (int)srcRect.y;
        var w = (int)srcRect.width;
        var h = (int)srcRect.height;

        var dx = (int)pos.x;
        var dy = (int)pos.y;

        var sourceData = source.GetPixels(x, y, w, h);
        dest.SetPixels(dx, dy, w, h, sourceData);
        dest.Apply();
    }
}
