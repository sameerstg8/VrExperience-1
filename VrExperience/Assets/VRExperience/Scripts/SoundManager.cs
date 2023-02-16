using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    public AudioSource themeAs;
    public AudioSource sfxAs;
    public AudioSource actionsAs;
    private void Awake()
    {
        _instance = this;
    }
    public void PlaySound(SoundType soundType,AudioClip clip)
    {
        if (soundType == SoundType.Theme)
        {
            PlayTheme(clip);
        } if (soundType == SoundType.Sfx)
        {
            PlaySfx(clip);

        }
        if (soundType == SoundType.Action)
        {
            PlayAction(clip);

        }
    }
    public void StopSound(SoundType soundType)
    {
       /* if (soundType == SoundType.Theme)
        {
            themeAs.Stop();        }
        if (soundType == SoundType.Sfx)
        {
            sfxAs.Stop();
        }
        if (soundType == SoundType.Action)
        {
            actionsAs.Stop();
        }*/
    }
    void FadeStop()
    {

    }
    void PlayTheme(AudioClip clip)
    {
        
        themeAs.clip = clip;
        themeAs.loop = true;
        themeAs.Play();
    }  void PlaySfx(AudioClip clip)
    {
        sfxAs.PlayOneShot(clip);
    }  void PlayAction(AudioClip clip)
   {
        actionsAs.PlayOneShot(clip);
    }
}
