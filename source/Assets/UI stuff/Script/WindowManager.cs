using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    private Canvas[] canvaslist;

    private void Awake()
    {
        canvaslist = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach(Canvas c in canvaslist)
        {
            c.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        foreach (Canvas c in canvaslist)
        {
            if (c.gameObject.name == "WithLevelSelect")
                c.gameObject.SetActive(true);
        }
    }

    public void OpenWindowSolo(string s)
    {
        foreach (Canvas c in canvaslist)
        {
            if (c.gameObject.name == s)
                c.gameObject.SetActive(true);
            else
                c.gameObject.SetActive(false);
        }
    }
}
