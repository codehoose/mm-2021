using System;
using System.Collections;
using ManicMiner.Converter.Lib.Models;
using UnityEngine;

public class MinerWilly : MonoBehaviour
{
    private WillyAnimations _sprites;
    private SpriteRenderer _renderer;

    public Texture2D _blocks;
    public RoomProvider _roomProvider;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _roomProvider.RoomChanged += Room_Changed;
        _sprites = new WillyAnimations(_blocks);
    }

    private void Room_Changed(object sender, EventArgs e)
    {
        var room = _roomProvider._currentRoom;
        Reset(room.willyStart);
    }

    private void Reset(MMWillyStart start)
    {
        StopAllCoroutines();
        transform.position = new Vector3(start.pos.x, -start.pos.y, 0);
        _renderer.sprite = _sprites.GetFrame(0, start.dir);
        StartCoroutine(MoveWilly(start.dir));
    }

    private IEnumerator MoveWilly(int dir)
    {
        var frame = 0;
        var x = transform.position.x;
        var y = transform.position.y;
        var direction = dir == 0 ? 1 : -1;
        var moving = false;
        var tmpFrame = frame;

        while (true)
        {
            var d = GetDirection();
            moving = d != 0;
            if (moving)
                direction = d;

            //var horiz = Input.GetAxis("Horizontal");

            //if (moving)
            //    direction = horiz > 0 ? 1 : -1;
            //else
            //    Input.ResetInputAxes();

            if (moving)
            {
                frame++;
                frame %= 8;

                tmpFrame = direction == 1 ? frame : 7 - frame;

                if (tmpFrame == 0 || tmpFrame % 4 == 0)
                    x += 8 * direction;
                else
                    x += 2 * direction;
            }

            _renderer.sprite = _sprites.GetFrame(tmpFrame, direction == 1 ? 0 : 1);
            transform.position = new Vector3(x, y);

            yield return new WaitForSeconds(0.125f);
        }
    }

    public int GetDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            return 1;

        return 0;
    }
}
