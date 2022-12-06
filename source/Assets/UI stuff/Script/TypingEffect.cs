using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour, TriggeredTextInterface
{
    private string text;
    private TextMeshProUGUI tmp;
    private bool typing;
    private Coroutine running;
    [SerializeField]
    private float sec_to_wait = 0.1f;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        text = tmp.text;
        tmp.text = null;
    }

    private void Update()
    {
        if (!typing && tmp.text != null)
        {
            if (GetComponent<FadeScript>() != null)
            {
                if (GetComponent<CanvasGroup>().alpha == 0)
                    tmp.text = null;
                else
                    return;
            }
            else
                tmp.text = null;
        }
    }

    IEnumerator start()
    {
        foreach(char c in text)
        {
            tmp.text += c;
            yield return new WaitForSeconds(sec_to_wait);
        }
    }

    public void show()
    {
        tmp.text = null;
        running = StartCoroutine(start());
        typing = true;
    }

    public void remove()
    {
        StopCoroutine(running);
        typing = false;
    }
}
