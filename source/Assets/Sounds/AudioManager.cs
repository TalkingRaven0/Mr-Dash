using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sounds[] sound;

    private void Awake()
    {
        foreach(Sounds s in sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.ID = Animator.StringToHash(s.Name);

            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }
    }

    public void Play(string name)
    {
        int id = Animator.StringToHash(name);

        Sounds s =Array.Find(sound, sound => sound.ID == id);
        if (s == null)
            return;
        s.source.Play();
    }

    public void playlooped(string name)
    {
        int id = Animator.StringToHash(name);

        Sounds s = Array.Find(sound, sound => sound.ID == id);
        if (s == null)
            return;
        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void forcestop(string name)
    {
        int id = Animator.StringToHash(name);

        Sounds s = Array.Find(sound, sound => sound.ID == id);
        if (s.source.isPlaying)
            s.source.Stop();
    }

}
