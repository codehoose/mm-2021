using UnityEngine;

public class WillyAnimations
{
    private Texture2D _tex;

    private Sprite[] _right;

    private Sprite[] _left;

    public WillyAnimations(Texture2D tex)
    {
        _tex = tex;

        _right = SpriteUtil.GetFrames(tex, 0, 8)
                           .ToSprites();

        _left = SpriteUtil.GetFrames(tex, 8, 8)
                          .ToSprites();
    }

    public Sprite GetFrame(int frame, int dir = 0) // 0 = right, 1 = left
    {
        return (dir == 0 ? _right : _left)[frame];
    }
}
