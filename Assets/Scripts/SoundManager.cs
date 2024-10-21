using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {  get; private set; }
    private AudioSource source;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        source = GetComponent<AudioSource>();
    }    
    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
    public void ChangeSoundVolume(float _change)
    {
        float currentVolume = PlayerPrefs.GetFloat("SoundVolume");
        currentVolume += _change;
        if(currentVolume>1)
        {
            currentVolume = 0;
        }
        else if(currentVolume<0) 
        {
            currentVolume = 1;
        }
        source.volume = currentVolume;
        PlayerPrefs.SetFloat("SoundVolume",currentVolume);
    }
}
