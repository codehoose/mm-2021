using System;
using UnityEngine;

public class CrumbleBlockProvider : MonoBehaviour
{
    public RoomProvider _roomProvider;

    void Awake()
    {
        _roomProvider.RoomChanged += Room_Changed;
        _roomProvider.AddCrumbleBlock += AddCrumbleBlock;
    }

    private void AddCrumbleBlock(object sender, PositionalWithSourceEventArgs e)
    {
        var tex = SpriteUtil.CreateTexture(8, 8);
        TexUtil.Blit(_roomProvider._blocks, tex, new Rect(e.SrcX, e.SrcY, 8, 8), Vector2.zero);

        var go = new GameObject("CrumbleBlock");
        go.tag = "Crumble";
        go.transform.SetParent(gameObject.transform);
        go.transform.localPosition = new Vector3(e.X, e.Y);
        var boxCollider = go.AddComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(8, 8);
        boxCollider.isTrigger = true;
        boxCollider.offset = new Vector2(4, -4);

        var sr = go.AddComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(tex, new Rect(0, 0, 8, 8), new Vector2(0, 1), 1);
    }

    private void Room_Changed(object sender, EventArgs e)
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
