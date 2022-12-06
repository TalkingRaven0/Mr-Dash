using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Rendering;

public class FadeScript : MonoBehaviour,TriggeredTextInterface
{
    private CanvasGroup cg;
    private bool _in;
    private float value;
    public float fadespeed = 1;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (_in)
            fade_in();
        else
            fade_out();
    }

    void fade_in()
    {
        if(value < 1)
            value += fadespeed * Time.deltaTime;
        cg.alpha = 1-((1-value)*(1-value));
    }

    void fade_out()
    {
        if (value > 0)
            value -= fadespeed * Time.deltaTime;
        cg.alpha = 1 - ((1 - value) * (1 - value));

    }

    public void show()
    {
        _in = true;
    }

    public void remove()
    {
        _in = false;
    }
}
