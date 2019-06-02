using System.Collections;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private IEnumerator coroutine;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        coroutine = BlinkCursor(0.25f);
        StartCoroutine(coroutine);
    }

    private IEnumerator BlinkCursor(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            if (rend.enabled == true)
            {
                rend.enabled = false;
            }
            else
            {
                rend.enabled = true;
            }
        }
    }
}
