using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFMOD();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeFMOD()
    {
        // Load banks, initialize runtime settings, etc.
        RuntimeManager.LoadBank("Master");
        RuntimeManager.LoadBank("MusicAmbience");
    }

    // Play a one-shot sound event
    public void PlaySound(string eventPath, Vector3 position)
    {
        RuntimeManager.PlayOneShot(eventPath, position);
    }

    // Set a global parameter (e.g., master volume)
    public void SetGlobalParameter(string parameterName, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }
}
