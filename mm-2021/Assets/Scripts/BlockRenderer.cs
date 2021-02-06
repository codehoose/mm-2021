using ManicMiner.Converter.Lib.Models;
using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlockRenderer : MonoBehaviour
{
    //public static int CRUMBLE_BLOCK = 4;
    //public static int TRAVELATOR_BLOCK = 7;
    //public static int STALAGTITE = 6;
    //public static int STALAGMITE = 5;

    //public static int[] IGNORE_BLOCKS = new int[] { CRUMBLE_BLOCK, TRAVELATOR_BLOCK };
    //public static int[] KILLY_BLOCKS = new int[] { STALAGMITE, STALAGTITE };

    //private MMMapFile _mapFile;

    //public Texture2D _blocks;
    //public Texture2D _sprites;
    //public TextAsset _roomData;
    //public int _roomID;

    //public bool _debug = true;

    //public ExitBlock _exitBlock;

    //public event EventHandler RoomChanged;

    //void Awake()
    //{
    //    _mapFile = JsonUtility.FromJson<MMMapFile>(_roomData.text);

    //    if (_debug)
    //    {
    //        LoadRoom();
    //    }
    //}

    public void LoadRoom()
    {
        //var room = _mapFile.rooms[_roomID];
        //RenderBackground(room);
        //RenderTravelator(room);
        //RenderExit(room);
        //RoomChanged?.Invoke(this, EventArgs.Empty);
    }   

    //private void RenderTravelator(MMRoom room)
    //{
    //    var t = room.travelator;
    //    var pos = t.pos;
    //    var go = new GameObject("Travelator");
    //    go.transform.SetParent(gameObject.transform);
    //    go.transform.localPosition = new Vector3(pos.x, -pos.y);
    //    var travelator = go.AddComponent<TravelatorAnimator>();

    //    // Here's a head twister for you! If the direction of the 
    //    // travelator is 0 then the sequence is 0 -> 3 otherwise it's
    //    // 3 -> 0. THEN it's transformed into a sprite and returned 
    //    // as an array.
    //    travelator._frames = (t.dir == 0 ?
    //                                     new int[] { 0, 1, 2, 3 }
    //                                     :
    //                                     new int[] { 3, 2, 1, 0 })
    //        .Select((i) => CreateTravelatorSprite(_roomID, i))
    //        .ToArray();

    //    travelator.Create(t);
    //}

    //private Sprite CreateTravelatorSprite(int roomID, int index)
    //{
    //    var srcRect = new Rect((7 + index) * 8, (19 - roomID) * 8, 8, 8);
    //    var tex = CreateTexture(8, 8);
    //    TexUtil.Blit(_blocks, tex, srcRect, Vector2.zero);
    //    return Sprite.Create(tex, new Rect(0, 0, 8, 8), new Vector2(0, 1), 1);
    //}

    //private void RenderBackground(MMRoom room)
    //{
    //    var spriteRenderer = GetComponent<SpriteRenderer>();
    //    var texture = CreateTexture(256, 128);
    //    texture.filterMode = FilterMode.Point;

    //    var roomBlockOffsetY = (19 - _roomID) * 8;

    //    for (int y = 0; y < 16; y++)
    //    {
    //        for (int x = 0; x < 32; x++)
    //        {
    //            var block = room.blocks[(y * 32) + x];
    //            if (Array.IndexOf(IGNORE_BLOCKS, block) > -1)
    //            {
    //                // If it's a travelator or a crumble block
    //                // we don't want to show it. Those are handled separately.
    //                if (block == CRUMBLE_BLOCK)
    //                {
    //                    AddCrumbleBlock(x * 8, (-y) * 8, 8 * block, roomBlockOffsetY);
    //                }
    //                block = 0;
    //            }
    //            var roomBlockOffsetX = 8 * block;

    //            var srcRect = new Rect(roomBlockOffsetX, roomBlockOffsetY, 8, 8);
    //            var pos = new Vector2(x * 8, (15 - y) * 8);

    //            if (Array.IndexOf(KILLY_BLOCKS, block) > -1)
    //            {
    //                AddKillBlock(x * 8, (-y) * 8);
    //            }

    //            TexUtil.Blit(_blocks, texture, srcRect, pos);
    //        }
    //    }
    //    spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, 256, 128), new Vector2(0, 1), 1);
    //}

    /// <summary>
    /// Add a box collider around the kill block. We'll handle the player touching that
    /// in the locomotion.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    //private void AddKillBlock(int x, int y)
    //{
    //    var go = new GameObject("KillBlock");
    //    go.tag = "Kill";
    //    go.transform.SetParent(gameObject.transform);
    //    go.transform.localPosition = new Vector3(x, y, 0);
    //    var boxCollider = go.AddComponent<BoxCollider2D>();
    //    boxCollider.size = new Vector2(8, 8);
    //    boxCollider.offset = new Vector2(4, -4);
    //}

    ///// <summary>
    ///// Add a crumble block.
    ///// </summary>
    ///// <param name="x"></param>
    ///// <param name="y"></param>
    ///// <param name="srcX"></param>
    ///// <param name="srcY"></param>
    //private void AddCrumbleBlock(int x, int y, int srcX, int srcY)
    //{
    //    var tex = CreateTexture(8, 8);
    //    TexUtil.Blit(_blocks, tex, new Rect(srcX, srcY, 8, 8), Vector2.zero);

    //    var go = new GameObject("CrumbleBlock");
    //    go.tag = "Crumble";
    //    go.transform.SetParent(gameObject.transform);
    //    go.transform.localPosition = new Vector3(x, y);
    //    var boxCollider = go.AddComponent<BoxCollider2D>();
    //    boxCollider.size = new Vector2(8, 8);
    //    boxCollider.isTrigger = true;
    //    boxCollider.offset = new Vector2(4, -4);

    //    var sr = go.AddComponent<SpriteRenderer>();
    //    sr.sprite = Sprite.Create(tex, new Rect(0, 0, 8, 8), new Vector2(0, 1), 1);
    //}

    //private Texture2D CreateTexture(int width, int height)
    //{
    //    var texture = new Texture2D(width, height, _blocks.format, false);
    //    texture.filterMode = FilterMode.Point;
    //    return texture;
    //}
}
