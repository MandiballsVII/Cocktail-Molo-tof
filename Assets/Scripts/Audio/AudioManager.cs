using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private EventInstance musicEventInstance;

    private readonly List<EventInstance> musicInstances = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayOneShot(EventReference sound, Vector3 worldpos)
    {
        RuntimeManager.PlayOneShot(sound, worldpos);
    }

    public void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateInstance(musicEventReference);
        musicInstances.Add(musicEventInstance);
        musicEventInstance.start();
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    public void StopMusic()
    {
        musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    public void StopAllMusic()
    {
        foreach (EventInstance instance in musicInstances)
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            instance.release();
        }

        musicInstances.Clear();
    }
}
