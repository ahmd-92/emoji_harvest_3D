using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum SoundFXType
{
    collect,
    hooverButton,
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioClip collectSFX;
    public AudioClip hooverButtonSFX;


    private void Awake()
    {
        instance = this;
    }

    public void PlaySoundFX(SoundFXType type)
    {
        if (!GamePreferences.IsSoundOn()) return;

        audioSource.volume = 1f;
        switch (type)
        {
            case SoundFXType.collect:
                audioSource?.PlayOneShot(collectSFX);
                break;
            case SoundFXType.hooverButton:
                audioSource.volume = 0.3f;
                audioSource?.PlayOneShot(hooverButtonSFX);
                break;
            default:
#if UNITY_EDITOR
                Debug.Log("SoundFXType not available");
#endif
                break;
        }
        
        

    }



}
