using UnityEngine;

public class RoomNineteenTopper : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public RoomProvider _roomProvider;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _roomProvider.RoomChanged += Room_Changed;
    }

    private void Room_Changed(object sender, System.EventArgs e)
    {
        _renderer.enabled = _roomProvider._roomID == 19;
    }
}
