using ManicMiner.Converter.Lib.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalBaddies : MonoBehaviour
{
    private bool _enemiesReady;
    public List<MobileObject> _mobs;
    public RoomProvider _roomProvider;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _mobs = new List<MobileObject>();
        _roomProvider.RoomChanged += RoomChanged;

        while (true)
        {
            foreach (var mob in _mobs)
            {
                mob.Tick();
            }

            yield return new WaitForSeconds(0.125f);
        }
    }

    private void RoomChanged(object sender, EventArgs e)
    {
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
        _mobs.Clear();
        MakeEnemies(_roomProvider._currentRoom.horizEnemies);
    }

    private void MakeEnemies(MMMob[] horizEnemies)
    {
        foreach (var mob in horizEnemies)
        {
            var go = new GameObject("Horizontal Enemy");
            go.tag = "Kill";
            go.transform.SetParent(gameObject.transform);
            var mo = go.AddComponent<MobileObject>();
            mo.Setup(_roomProvider._sprites, mob);

            _mobs.Add(mo);
        }
    }
}
