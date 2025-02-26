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

    #region VARIABLES
    [Header("Persistent Music & Ambience")]
    [SerializeField] private EventReference loadingMusic;
    [SerializeField] private EventReference deathMusic;

    [Header("Scene Music and Ambience")]
    [SerializeField] private List<SceneAudioData> sceneAudioDataList;
    private Dictionary<string, SceneAudioData> sceneAudioDataDict;

    private EventInstance currentSceneMusicInstance;
    private EventInstance currentSceneAmbienceInstance;

    private EventInstance loadingMusicInstance;
    private EventInstance deathMusicInstance;

    // Variables for assessing ThreatLevel
    public int numberOfMonstersDetectedBy = 0;
    private bool detectedByMonster = false;
    private bool detectedByMonsterOld = false;
    #endregion


    #region INITIALIZATION
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

    // Start is called before the first frame update
    void Start()
    {
        // Convert list to dictionary for quick lookups
        sceneAudioDataDict = new Dictionary<string, SceneAudioData>();
        foreach (var data in sceneAudioDataList)
        {
            if (!sceneAudioDataDict.ContainsKey(data.SceneName))
            {
                sceneAudioDataDict.Add(data.SceneName, data);
            }
            else
            {
                Debug.LogWarning($"Duplicate scene name detected: {data.SceneName}");
            }
        }

        // Load persistent events
        loadingMusicInstance = RuntimeManager.CreateInstance(loadingMusic);
        deathMusicInstance = RuntimeManager.CreateInstance(deathMusic);
    }
    #endregion


    // Update is called once per frame
    void Update()
    {
        //if (numberOfMonstersDetectedBy > 0)
        //{
        //    detectedByMonster = true;
        //}
        //else
        //{
        //    detectedByMonster = false;
        //}

        //if (detectedByMonster != detectedByMonsterOld)
        //{
        //    if (detectedByMonster)
        //    {
        //        AudioManager.Instance.SetGlobalParameter("ThreatLevel", 1f);
        //    }
        //    else
        //    {
        //        AudioManager.Instance.SetGlobalParameter("ThreatLevel", 0f);
        //    }

        //    detectedByMonsterOld = detectedByMonster;
        //}

        ////Debug.Log(numberOfMonstersDetectedBy);
    }

    #region METHODS
    public void PlaySceneAudio(string sceneName)
    {
        // Stop previous music with fade-out, but let FMOD handle cleanup
        if (currentSceneMusicInstance.isValid())
        {
            currentSceneMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentSceneMusicInstance = default; // Clear reference
        }

        if (currentSceneAmbienceInstance.isValid())
        {
            currentSceneAmbienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentSceneAmbienceInstance = default;
        }

        // Retrieve the new scene's audio data
        if (sceneAudioDataDict.TryGetValue(sceneName, out SceneAudioData data))
        {
            // Create new music instance
            currentSceneMusicInstance = RuntimeManager.CreateInstance(data.Music);
            currentSceneMusicInstance.start();
            currentSceneMusicInstance.release(); // FMOD will auto-cleanup after stopping

            // Create new ambience instance
            currentSceneAmbienceInstance = RuntimeManager.CreateInstance(data.Ambience);
            currentSceneAmbienceInstance.start();
            currentSceneAmbienceInstance.release();
        }
        else
        {
            Debug.LogWarning($"Scene '{sceneName}' not found in MusicManager.");
        }
    }

    public void PlayLoadingScreen()
    {
        StopCurrentAudio();
        loadingMusicInstance.start();
    }

    public void PlayDeathScreen()
    {
        StopCurrentAudio();
        deathMusicInstance.start();
    }

    private void StopCurrentAudio()
    {
        if (currentSceneMusicInstance.isValid())
        {
            currentSceneMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentSceneMusicInstance = default;
        }

        if (currentSceneAmbienceInstance.isValid())
        {
            currentSceneAmbienceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentSceneAmbienceInstance = default;
        }
    }
    #endregion
}

// A class used to store Music and Ambiences EventReferences for scenes
[System.Serializable]
public class SceneAudioData
{
    public string SceneName;
    public EventReference Music;
    public EventReference Ambience;
}
