using ManicMiner.Converter.Lib.Models;
using UnityEngine;

public class MobileObject : MonoBehaviour
{

    private SpriteRenderer _renderer;

    public Sprite[] _sprites;

    private int _direction = 1;
    private int _frame = 0;
    private int _x;
    private int _y;

    private MMMob _mob;

    public void Setup(Texture2D spritesheet, MMMob mob)
    {
        _mob = mob;
        _sprites = SpriteUtil.GetFrames(spritesheet, mob.graphic, mob.fli == 4 ? 8 : 4)
                             .ToSprites();
        _renderer = gameObject.AddComponent<SpriteRenderer>();
        _renderer.sprite = _sprites[mob.dir == 0 ? 0 : mob.ani % _sprites.Length];

        transform.localPosition = new Vector3(mob.pos.x, -mob.pos.y);
        _x = mob.pos.x;
        _y = -mob.pos.y;

        var bc = gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        bc.offset = new Vector2(8, -8);
        bc.size = new Vector2(16, 16);
    }

    public void Tick()
    {
        _frame++;
        _frame %= 4;
        if (_frame == 0)
            _x += 8 * _direction;
        else
            _x += _mob.speed * _direction;

        if (_x >= _mob.maxPos)
        {
            _x = _mob.maxPos - 8;
            _frame = 0;
            _direction = -1;
        } else if (_x < _mob.minPos)
        {
            _x = _mob.minPos;
            _frame = 0;
            _direction = 1;
        }

        var frame = _direction > 0 ? _frame : 3 - _frame;
        frame += _direction < 0 ? 4 : 0;
        transform.localPosition = new Vector3(_x, _y);
        _renderer.sprite = _sprites[frame];
    }

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
}