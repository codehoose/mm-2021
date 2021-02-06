using ManicMiner.Converter.Lib.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelatorAnimator : MonoBehaviour
{
    private List<SpriteRenderer> _blocks;
    public Sprite[] _frames;

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
