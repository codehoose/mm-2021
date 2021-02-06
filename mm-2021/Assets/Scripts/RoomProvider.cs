using ManicMiner.Converter.Lib.Models;
using System;
using System.Collections;
using UnityEngine;

public class RoomProvider : MonoBehaviour
{
    public static int CRUMBLE_BLOCK = 4;
    public static int TRAVELATOR_BLOCK = 7;
    public static int STALAGTITE = 6;
    public static int STALAGMITE = 5;

    public static int[] IGNORE_BLOCKS = new int[] { CRUMBLE_BLOCK, TRAVELATOR_BLOCK };
    public static int[] KILLY_BLOCKS = new int[] { STALAGMITE, STALAGTITE };

    private RoomController _controller;

    [HideInInspector]
    public MMRoom _currentRoom;

    [HideInInspector]
    public int _roomID;

    public Texture2D _blocks;

    public Texture2D _sprites;

    public ExitBlock _exitBlock;

    public event EventHandler<PositionalEventArgs> AddKillBlock;

    public event EventHandler<PositionalWithSourceEventArgs> AddCrumbleBlock;

    public event EventHandler AddTravelator;

    public event EventHandler RoomChanged;

    IEnumerator Start()
    {
        _controller = GetComponent<RoomController>();
        while (!_controller._isReady)
        {
            yield return null;
        }

        RoomChanged?.Invoke(this, EventArgs.Empty);
        SetupRoom();
        _controller.RoomChanged += Room_Changed;
    }

    private void OnDestroy()
    {
        _controller.RoomChanged -= Room_Changed;
    }

    private void SetupRoom()
    {
        _currentRoom = _controller.GetCurrentRoom();
        _roomID = _controller._roomID;
        RenderBackground(_currentRoom);
        AddTravelator?.Invoke(this, EventArgs.Empty);
        RenderExit(_currentRoom);
    }
    
    private void RenderExit(MMRoom room)
    {
        var exitBlocks = _sprites.GetExitFrames(21, _roomID);
        _exitBlock.SetSprites(exitBlocks);
        _exitBlock.transform.localPosition = new Vector3(room.exitPosition.x, -room.exitPosition.y);
    }

    private void RenderBackground(MMRoom room)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var texture = SpriteUtil.CreateTexture(256, 128);
        texture.filterMode = FilterMode.Point;

        var roomBlockOffsetY = (19 - _controller._roomID) * 8;

        for (int y = 0; y < 16; y++)
        {
            for (int x = 0; x < 32; x++)
            {
                var block = room.blocks[(y * 32) + x];
                if (Array.IndexOf(IGNORE_BLOCKS, block) > -1)
                {
                    // If it's a travelator or a crumble block
                    // we don't want to show it. Those are handled separately.
                    if (block == CRUMBLE_BLOCK)
                    {
                        AddCrumbleBlock?.Invoke(this, new PositionalWithSourceEventArgs(x * 8, (-y) * 8, 8 * block, roomBlockOffsetY));
                    }
                    block = 0;
                }
                var roomBlockOffsetX = 8 * block;

                var srcRect = new Rect(roomBlockOffsetX, roomBlockOffsetY, 8, 8);
                var pos = new Vector2(x * 8, (15 - y) * 8);

                if (Array.IndexOf(KILLY_BLOCKS, block) > -1)
                {
                    AddKillBlock?.Invoke(this, new PositionalEventArgs(x * 8, (-y) * 8));
                }

                TexUtil.Blit(_blocks, texture, srcRect, pos);
            }
        }
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, 256, 128), new Vector2(0, 1), 1);
    }

    private void Room_Changed(object sender, EventArgs e)
    {
        RoomChanged?.Invoke(this, EventArgs.Empty);
        SetupRoom();
    }
}
