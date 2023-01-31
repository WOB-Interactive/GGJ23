using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    [SerializeField]public  bool muted = false;
    [SerializeField] [Range(0f, 1.0f)] float fxAudioVolume = .5f;
    [SerializeField] [Range(0f, 1.0f)] float bgAudioVolume = .75f;
    [SerializeField] Sound[] sounds;

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

    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {

                if (sounds[i].loop)
                {
                    sounds[i].Play(bgAudioVolume);
                }
                else
                {
                    sounds[i].Play(fxAudioVolume);
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
