using System.Collections;
using UnityEngine;

public class ScrollingFont : MonoBehaviour
{
    public string text = "                                  .  .  .  .  .  .  .  .  .  . MANIC MINER . .  BUG-BYTE ltd. 1983 . . By Matthew Smith . . . Q to P = Left & Right . . Bottom row = Jump . . A to G = Pause . . H to L = Tune On/Off . . . Guide Miner Willy through 20 lethal caverns   .  .  .  .  .  .  .  .                                        ";

    public float cooldown = 0.1f;

    public int x = 0;
    public int y = 128;

    IEnumerator Start()
    {
        var font = GetComponent<SpectrumFont>();

        var textData = new TextData(x, y, "                                ", 0);
        textData.SortingOrder = 150;
        font.Add("main-scroll-text", textData);

        int index = 0;
        while (true)
        {
            textData.Text = text.Substring(index, 32);
            yield return new WaitForSeconds(cooldown);

            index++;
            index %= (text.Length - 32);
        }
    }
}
