using UnityEngine;

public class RoomName : MonoBehaviour
{
    public RoomProvider _roomProvider;

    void Awake()
    {
        _roomProvider.RoomChanged +=
            (o, e) => GetComponent<StaticText>().text = _roomProvider._currentRoom.name;    
    }
}
