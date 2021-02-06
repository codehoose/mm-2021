using ManicMiner.Converter.Lib.Models;
using System;
using System.Collections;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [HideInInspector]
    public bool _isReady;

    [HideInInspector]
    public MMMapFile _mapFile;

    public int _roomID;

    public TextAsset _jsonMapFile;

    public event EventHandler RoomChanged;

    /// <summary>
    /// Get the current room.
    /// </summary>
    /// <returns>The current room</returns>
    public MMRoom GetCurrentRoom() => _mapFile.rooms[_roomID];

    /// <summary>
    /// Load the map data from resources
    /// </summary>
    void Awake()
    {
        _mapFile = JsonUtility.FromJson<MMMapFile>(_jsonMapFile.text);
        _isReady = true;
    }

    /// <summary>
    /// Keep a weather eye on the room number. If it changes, notify
    /// subscribers.
    /// </summary>
    /// <returns></returns>
    IEnumerator Start()
    {
        var lastRoom = _roomID;
        while (true)
        {
            if (lastRoom != _roomID)
            {
                lastRoom = _roomID;
                RoomChanged?.Invoke(this, EventArgs.Empty);
            }

            yield return null;
        }
    }
}
