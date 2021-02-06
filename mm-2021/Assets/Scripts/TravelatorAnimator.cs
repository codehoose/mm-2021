using ManicMiner.Converter.Lib.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TravelatorAnimator : MonoBehaviour
{
    private List<SpriteRenderer> _blocks;
    public Sprite[] _frames;

    public RoomProvider _roomProvider;

    void Awake()
    {
        _roomProvider.RoomChanged += Room_Changed;
        _roomProvider.AddTravelator += AddTravelator;
    }

    private void AddTravelator(object sender, System.EventArgs e)
    {
        StopAllCoroutines();
        Destroy(GetComponent<BoxCollider2D>());

        var t = _roomProvider._currentRoom.travelator;
        //var pos = t.pos;
        transform.localPosition = new Vector3(t.pos.x, -t.pos.y, 0);
        // Here's a head twister for you! If the direction of the 
        // travelator is 0 then the sequence is 0 -> 3 otherwise it's
        // 3 -> 0. THEN it's transformed into a sprite and returned 
        // as an array.
        _frames = (t.dir == 0 ? new int[] { 0, 1, 2, 3 }
                              : new int[] { 3, 2, 1, 0 })
            .Select((i) => CreateTravelatorSprite(_roomProvider._roomID, i))
            .ToArray();

        Create(t);
    }

    private Sprite CreateTravelatorSprite(int roomID, int index)
    {
        var srcRect = new Rect((7 + index) * 8, (19 - roomID) * 8, 8, 8);
        var tex = SpriteUtil.CreateTexture(8, 8);
        TexUtil.Blit(_roomProvider._blocks, tex, srcRect, Vector2.zero);
        return Sprite.Create(tex, new Rect(0, 0, 8, 8), new Vector2(0, 1), 1);
    }


    private void Room_Changed(object sender, System.EventArgs e)
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Create(MMTravelator travelator)
    {
        _blocks = new List<SpriteRenderer>();
        
        for (int i = 0; i < travelator.len; i++)
        {
            var go = new GameObject();
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = _frames[0];
            go.transform.SetParent(gameObject.transform);
            go.transform.localPosition = new Vector3(i * 8, 0);
            _blocks.Add(sr);
        }

        // Create a box collider
        var boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        boxCollider.size = new Vector2(travelator.len * 8, 8);
        boxCollider.offset = new Vector2(travelator.len * 4, -4);

        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        var index = 0;
        while (true)
        {
            foreach (var block in _blocks)
            {
                block.GetComponent<SpriteRenderer>().sprite = _frames[index];
            }
            yield return new WaitForSeconds(0.1f);
            index++;
            index %= _frames.Length;
        }
    }
}
