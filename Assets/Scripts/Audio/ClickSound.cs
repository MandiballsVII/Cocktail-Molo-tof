using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public void PlayClickSound()
    {
        AudioManager.Instance.PlayOneShot(FMOD_Events.Instance.Click, Vector3.zero);
    }
}
