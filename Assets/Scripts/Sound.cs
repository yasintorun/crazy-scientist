using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    [Range(0, 1)]
    public float volume;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
