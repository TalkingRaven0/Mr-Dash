using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredTextComponent : MonoBehaviour
{
    private TriggeredTextInterface[] comps;

    private void Awake()
    {
        comps = GetComponents<TriggeredTextInterface>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(TriggeredTextInterface t in comps)
            {
                t.show();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            foreach (TriggeredTextInterface t in comps)
            {
                t.remove();
            }
        }
    }
}
