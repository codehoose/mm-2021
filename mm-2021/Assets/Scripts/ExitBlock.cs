using System.Collections;
using UnityEngine;

public class ExitBlock : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private BoxCollider2D _boxCollider;

    public Sprite[] _sprites;

    public bool _flashing;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    public void SetSprites(Sprite[] sprites)
    {
        _sprites = sprites;
        _renderer.sprite = _sprites[0];
    }

    IEnumerator Start()
    {
        var index = 0;

        while (true)
        {
            if (!_flashing)
            {
                yield return null;
            }
            else
            {
                _renderer.sprite = _sprites[index];
                yield return new WaitForSeconds(0.5f);
                index++;
                index %= _sprites.Length;
            }
        }
    }
}
