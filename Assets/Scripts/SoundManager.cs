using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource[] sourcePool;

    public AudioClip pincer;

    public void Play(AudioClip _clip)
    {
        foreach (AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.clip = _clip;
                source.Play();
                break;
            }
        }
    }    

    public void Play(AudioClip _clip, float _minPitch, float _maxPitch)
    {
        foreach(AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.pitch = Random.Range(_minPitch, _maxPitch);
                source.clip = _clip;
                source.Play();
                break;
            }
        }
    }

    public void Play(AudioClip _clip, float _pan)
    {
        foreach (AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.panStereo = _pan;
                source.clip = _clip;
                source.Play();
                break;
            }
        }
    }

    public void Play(AudioClip _clip, float _minPitch, float _maxPitch, float _pan)
    {
        foreach (AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.pitch = Random.Range(_minPitch, _maxPitch);
                source.panStereo = _pan;
                source.clip = _clip;
                source.Play();
                break;
            }
        }
    }
}
