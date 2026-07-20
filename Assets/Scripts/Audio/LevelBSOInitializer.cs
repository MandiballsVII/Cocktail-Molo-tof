using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBSOInitializer : MonoBehaviour
{
    void Start()
    {
        //AudioManager.Instance.StopMusic();
        AudioManager.Instance.InitializeMusic(FMOD_Events.Instance.MainMenu);
    }

}
