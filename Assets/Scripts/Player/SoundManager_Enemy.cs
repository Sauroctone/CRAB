using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_Enemy : MonoBehaviour {

    public AudioSource[] sourcePool;

	public AudioClip attackSFX;

    public void Play(AudioClip _clip, float _volume)
    {
        foreach (AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.volume = _volume;
                source.pitch = 1;
                source.clip = _clip;
                source.Play();
                break;
            }
        }
    }

    public void Play(AudioClip _clip, float _volume, float _minPitch, float _maxPitch)
    {
        foreach(AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.volume = _volume;
                source.pitch = Random.Range(_minPitch, _maxPitch);
                source.clip = _clip;
                source.Play();
                break;
            }
        }
    }

    public void Play(AudioClip _clip, float _volume, float _pan)
    {
        foreach (AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.volume = _volume;
                source.pitch = 1;
                source.panStereo = _pan;
                source.clip = _clip;
                source.Play();
                break;
            }
        }
    }

    public void Play(AudioClip _clip, float _volume, float _minPitch, float _maxPitch, float _pan)
    {
        foreach (AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.volume = _volume;
                source.pitch = Random.Range(_minPitch, _maxPitch);
                source.panStereo = _pan;
                source.clip = _clip;
                source.Play();
                break;
            }
        }
    }
}
