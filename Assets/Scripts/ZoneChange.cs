using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneChange : MonoBehaviour
{
    Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        
    }

    public void ChangeToCocktail()
    {
        mainCamera.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    public void ChangeToClient()
    {
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
