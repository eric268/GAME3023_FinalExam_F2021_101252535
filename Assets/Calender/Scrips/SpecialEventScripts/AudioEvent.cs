using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfAudioEvent
{
    OnObjectMovement,
    Persistent,
    Random,
}

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(DayScript))]
public class AudioEvent : MonoBehaviour
{
    public TypeOfAudioEvent eventType;
    public AudioClip clipToPlay;
    public Rigidbody2D rigidBodyOfPlayOnMoveSource;
    public float audioVolume;
    AudioSource audioSource;

    public int randomChecksToPlayAudioPerHour;
    public float randomizedSoundPercentage;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    void CheckWhichAudioEventToPlay(TypeOfAudioEvent type)
    {
        switch(type)
        {
            case TypeOfAudioEvent.OnObjectMovement:
                PlayOnPlayerMovement();
                break;
            case TypeOfAudioEvent.Persistent:
                PlayPersistentAudio();
                break;
            case TypeOfAudioEvent.Random:
                PlayAudioRandom();
                break;

        }
    }

    void PlayOnPlayerMovement()
    {
        float playerVelocity = rigidBodyOfPlayOnMoveSource.velocity.magnitude;
        if (playerVelocity > 0.01f)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clipToPlay, audioVolume);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void PlayPersistentAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(clipToPlay, audioVolume);
        }
    }

    void PlayAudioRandom()
    {
        float checkInterval = Mathf.Round((Clock.Instance().minutesInHour - 1) / randomChecksToPlayAudioPerHour);
        if (Clock.Instance().minutes == checkInterval)
        {
            if (randomizedSoundPercentage > 1.0f)
                randomizedSoundPercentage = 1.0f;

            float ranNum = Random.Range(0.0f, 1.0f);
            if (ranNum > (1 - randomizedSoundPercentage))
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(clipToPlay, audioVolume);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<DayScript>().enabled == true)
        {
            CheckWhichAudioEventToPlay(eventType);
        }
    }
}
