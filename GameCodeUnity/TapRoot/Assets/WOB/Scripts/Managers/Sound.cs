using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    private AudioSource source;

    //public ClosedCaption caption; // this will require a time stamp mark for longer sounds / Narrations

    public string caption; // Temp until feature implemented

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play(float volume)
    {
        if (source)
        {
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
            source.Play();
        }
        else
        {
            Debug.LogWarning("Source bad");
        }
    }

    public void Stop()
    {
        source.Stop();
    }

}
