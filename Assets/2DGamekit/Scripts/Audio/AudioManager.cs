using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    // Variable for assessing ThreatLevel
    public int numberOfMonstersDetectedBy = 0;
    private bool detectedByMonster = false;
    private bool detectedByMonsterOld = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfMonstersDetectedBy > 0)
        {
            detectedByMonster = true;
        }
        else
        {
            detectedByMonster = false;
        }

        if (detectedByMonster != detectedByMonsterOld)
        {
            if (detectedByMonster)
            {
                SetGlobalParameter("ThreatLevel", 1f);
            }
            else
            {
                SetGlobalParameter("ThreatLevel", 0f);
            }

            detectedByMonsterOld = detectedByMonster;
        }

        Debug.Log(numberOfMonstersDetectedBy);
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
        bool isBankLoaded = RuntimeManager.HasBankLoaded("Master");
        Debug.Log($"Bank Loaded: {isBankLoaded}");
        RuntimeManager.LoadBank("MusicAmbience");
        isBankLoaded = RuntimeManager.HasBankLoaded("MusicAmbience");
        Debug.Log($"Bank Loaded: {isBankLoaded}");
    }

    // Play a one-shot sound event
    public void PlaySound(EventReference eventReference, Vector3 position)
    {
        RuntimeManager.PlayOneShot(eventReference, position);
    }

    // Set a global parameter (e.g., master volume)
    public void SetGlobalParameter(string parameterName, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }

    public bool IsStopped(EventInstance fmodInstance)
    {
        PLAYBACK_STATE state;
        fmodInstance.getPlaybackState(out state);
        return state == PLAYBACK_STATE.STOPPED;
    }
}
