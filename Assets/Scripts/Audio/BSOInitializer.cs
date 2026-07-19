using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSOInitializer : MonoBehaviour
{
    void Start()
    {
        AudioManager.Instance.InitializeMusic(FMOD_Events.Instance.MainMenu);
    }

}
