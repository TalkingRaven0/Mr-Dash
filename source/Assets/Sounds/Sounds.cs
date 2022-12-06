using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sounds

{
    public string Name;

    [HideInInspector]
    public int ID;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

}
