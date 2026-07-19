using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMOD_Events : MonoBehaviour
{
    /// Music /// 
    [field: Header("Music")]
    [field: SerializeField] public EventReference MainMenu { get; private set; }
    [field: SerializeField] public EventReference Gameplay { get; private set; }

    [field: Header("SoundFX")]
    [field: SerializeField] public EventReference AgarrarBotella { get; private set; }
    [field: SerializeField] public EventReference AgitarCoctelera { get; private set; }
    [field: SerializeField] public EventReference ArrastrarJarra { get; private set; }
    [field: SerializeField] public EventReference Click { get; private set; }
    [field: SerializeField] public EventReference CucharaRemover { get; private set; }
    [field: SerializeField] public EventReference DestaponarBotella { get; private set; } //Este esta por si hace falta, lo grabé asi random
    [field: SerializeField] public EventReference Gato { get; private set; }
    [field: SerializeField] public EventReference GrunidoCliente { get; private set; }
    [field: SerializeField] public EventReference GrunidoIntermedio { get; private set; }
    [field: SerializeField] public EventReference GrunidoNegativo { get; private set; }
    [field: SerializeField] public EventReference GrunidoPositivo { get; private set; }
    [field: SerializeField] public EventReference MinijuegoExito { get; private set; }
    [field: SerializeField] public EventReference MinijuegoFallo { get; private set; }
    [field: SerializeField] public EventReference PasarPagina { get; private set; }
    [field: SerializeField] public EventReference PonerDecoracion { get; private set; }
    [field: SerializeField] public EventReference VerterLiquido { get; private set; }
    [field: SerializeField] public EventReference WhooshCambioPantalla { get; private set; }




    ///////////      DontDestroyOnLoad      ///////////

    public static FMOD_Events Instance { get; private set; }

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
}
