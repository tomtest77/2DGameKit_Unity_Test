using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    // This is a music and ambience manager
    public static MusicManager Instance { get; private set; }

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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("FMOD Events")]
    public EventReference startMusicEvent;
    public EventReference startAmbienceEvent;

    public EventReference zone01MusicEvent;
    public EventReference zone01AmbienceEvent;

    public EventReference zone02MusicEvent;
    public EventReference zone02AmbienceEvent;

    public EventReference zone03MusicEvent;
    public EventReference zone03AmbienceEvent;

    public EventReference zone04MusicEvent;
    public EventReference zone04AmbienceEvent;

    public EventReference zone05MusicEvent;
    public EventReference zone05AmbienceEvent;

    public void PlayAmbienceAndMusic(string currentZone)
    {

    }
}
