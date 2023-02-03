using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public static event Action<String> closedCaption;

    public static AudioManager instance = null;
    [SerializeField]public  bool muted = false;
    [SerializeField] [Range(0f, 1.0f)] float fxAudioVolume = .5f;
    [SerializeField] [Range(0f, 1.0f)] float bgAudioVolume = .75f;
    [SerializeField] Sound[] sounds;

    bool soundEnabled = true;

    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        // Load Sound volume from storage
        bgAudioVolume = Storage.GetBGLevel();
        fxAudioVolume = Storage.GetFXLevel();
        soundEnabled = Storage.GetAudioEnabled();

    }

    public void PlaySound(string _name)
    {
        if (_name == String.Empty)
        {
            Debug.LogError("No Audio Title Requested");
            return;
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {

                if (sounds[i].loop)
                {
                    if (soundEnabled)
                    {
                        sounds[i].Play(bgAudioVolume);
                    }
                }
                else
                {
                    if (soundEnabled)
                    {
                        sounds[i].Play(fxAudioVolume);
                    }

                    if (Storage.GetClosedCaptionEnabled())
                    {
                        closedCaption?.Invoke(sounds[i].caption);
                    }
                }

                return;
            }
        }

        // no sound with _name
        Debug.LogError("Audio Manager: Sound not found in list, " + _name);
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Stop();
        }
        return;
    }

}
