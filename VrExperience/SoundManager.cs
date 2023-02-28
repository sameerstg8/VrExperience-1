using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    public AudioSource themeAs;
    public AudioSource sfxAs;
    public AudioSource actionsAs;
    public List<AudioClip> themeAudioClips = new();
    public List<AudioClip> sfxAudioClips = new();
    public List<AudioClip> actionAudioClips = new();
    private void Awake()
    {
        _instance = this;
    }
    public void PlayTheme(int index)
    {
        themeAs.PlayOneShot(themeAudioClips[index]);
    } public void PlaySfx(int index)
    {
        sfxAs.PlayOneShot(themeAudioClips[index]);
    } public void PlayAction(int index)
    {
        actionsAs.PlayOneShot(themeAudioClips[index]);
    } public void PlayTheme(AudioClip clip)
    {
        themeAs.PlayOneShot(clip);
    } public void PlaySfx(AudioClip clip)
    {
        sfxAs.PlayOneShot(clip);
    } public void PlayAction(AudioClip clip)
   {
        actionsAs.PlayOneShot(clip);
    }
}
