using System.Collections;
using UnityEngine;

public class DasBoot : MonoBehaviour
{
    IEnumerator Start()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        while (y > 16)
        {
            y = y - 1;
            transform.position = new Vector3(x, y);

            yield return null;
        }
    }
}
