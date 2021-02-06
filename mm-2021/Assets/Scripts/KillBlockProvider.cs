using System;
using UnityEngine;

public class KillBlockProvider : MonoBehaviour
{
    public RoomProvider _roomProvider;

    void Awake()
    {
        _roomProvider.RoomChanged += Room_Changed;
        _roomProvider.AddKillBlock += AddKillBlock;
    }
    private void AddKillBlock(object sender, PositionalEventArgs e)
    {
        var go = new GameObject("KillBlock");
        go.tag = "Kill";
        go.transform.SetParent(gameObject.transform);
        go.transform.localPosition = new Vector3(e.X, e.Y, 0);
        var boxCollider = go.AddComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(8, 8);
        boxCollider.offset = new Vector2(4, -4);
    }

    private void Room_Changed(object sender, EventArgs e)
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
