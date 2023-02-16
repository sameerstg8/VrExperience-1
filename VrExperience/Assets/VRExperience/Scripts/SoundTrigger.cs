using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{

    public ConditionOfSoundTrigger triggerCondition;
    public SoundType soundType;
    public AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            if (triggerCondition == ConditionOfSoundTrigger.ByTrigger)
            {
                SoundManager._instance.PlaySound(soundType, clip);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (triggerCondition == ConditionOfSoundTrigger.ByCollision)
            {
                SoundManager._instance.PlaySound(soundType, clip);
            }
        }
        
    }  private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Player"))
        {
            if (triggerCondition == ConditionOfSoundTrigger.ByTrigger)
            {
                SoundManager._instance.StopSound(soundType);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (triggerCondition == ConditionOfSoundTrigger.ByCollision)
            {
                SoundManager._instance.StopSound(soundType);
            }
        }
        
    }
}
public enum ConditionOfSoundTrigger
{
    ByTrigger,ByCollision
}
public enum SoundType
{
    Action,Sfx,Theme
}
