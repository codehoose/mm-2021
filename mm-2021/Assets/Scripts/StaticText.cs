using System.Collections;
using UnityEngine;

public class StaticText : MonoBehaviour
{
    private TextData _data;

    public string key = "game-over";
    public string text = "GAME OVER";
    public int fontColour = 7   ;

    public int x;

    public int y;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var font = GetComponent<SpectrumFont>();
        _data = new TextData(x, y, text, fontColour);
        font.Add(key, _data);

        var t = text;

        while (true)
        {
            if (t != text)
            {
                t = text;
                _data.Text = text;
            }

            yield return null;
        }
    }
}
