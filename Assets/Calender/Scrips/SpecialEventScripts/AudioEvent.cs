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
    [Header("General Audio")]
    public TypeOfAudioEvent eventType;
    public AudioClip clipToPlay;
    public float audioVolume;

    [Header("Audio Connected")]
    public Rigidbody2D rigidBodyOfPlayOnMoveSource;

    AudioSource audioSource;

    [Header("Random Audio")]
    public int randomChecksToPlayAudioPerHour;
    public float randomizedSoundPercentage;
    // Start is called before the first frame update

    //Want to make sure we arent looping or playing on start
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    //Allows use to choose different audio events
    void CheckWhichAudioEventToPlay(TypeOfAudioEvent type)
    {
        switch(type)
        {
            case TypeOfAudioEvent.OnObjectMovement:
                PlayOnObjectMovement();
                break;
            case TypeOfAudioEvent.Persistent:
                PlayPersistentAudio();
                break;
            case TypeOfAudioEvent.Random:
                PlayAudioRandom();
                break;

        }
    }
    //Allows audio to play when bound to an objects movement
    void PlayOnObjectMovement()
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

    //Plays audio for entire day
    void PlayPersistentAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = true;
            audioSource.PlayOneShot(clipToPlay, audioVolume);
        }
    }
    //Randomly plays an audio clip
    void PlayAudioRandom()
    {
        //Player can select how many times per hour an audio clip is attempted to be played
        float checkInterval = Mathf.Round((Clock.Instance().minutesInHour - 1) / randomChecksToPlayAudioPerHour);
        if (Clock.Instance().minutes == checkInterval)
        {
            if (randomizedSoundPercentage > 1.0f)
                randomizedSoundPercentage = 1.0f;

            //Player can set how likly an audio clip is played when checked
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

    void Update()
    {
        if (GetComponent<DayScript>().enabled == true)
        {
            CheckWhichAudioEventToPlay(eventType);
        }
    }
}
