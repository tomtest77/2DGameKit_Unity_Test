using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
//using UnityEngine.Tilemaps;

public class EnemyAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Instantiating FMOD events
        footstepInstance = RuntimeManager.CreateInstance(footstepEvent);
        idleLoopInstance = RuntimeManager.CreateInstance(idleLoopEvent);

    }

    // Update is called once per frame
    void Update()
    {
        // Update the attributes3d with the current transform data.
        attributes3d = RuntimeUtils.To3DAttributes(transform);

        // Updating instance's position
        footstepInstance.set3DAttributes(attributes3d);
        idleLoopInstance.set3DAttributes(attributes3d);
    }

    public void OnDisable()
    {
        StopSounds();
        ReleaseEvents();
    }

    public void OnDestroy()
    {
        StopSounds();
        ReleaseEvents();
    }

    public void StopSounds()
    {
        footstepInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        idleLoopInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void ReleaseEvents()
    {
        footstepInstance.release();
        idleLoopInstance.release();
    }

    [Header("FMOD Events for enemy")]
    public EventReference footstepEvent;
    public EventReference idleLoopEvent;

    // One shots
    public EventReference detectEvent;
    public EventReference attackEvent;
    public EventReference painEvent;
    public EventReference deathEvent;

    // If we want to have access to data in specific events they have to be instantiated (as opposed to one-shots - play and forget)
    public EventInstance footstepInstance;
    public EventInstance idleLoopInstance;

    // Variable for storing position of the game object
    private FMOD.ATTRIBUTES_3D attributes3d;

    public bool isIdleLoopPlaying = false;

    /// <summary>
    /// Plays a footstep sound.
    /// </summary>
    public void PlayIdleLoop()
    {
        if (!idleLoopInstance.isValid() || AudioManager.Instance.IsStopped(idleLoopInstance))
        {
            idleLoopInstance.start();
            isIdleLoopPlaying = true;
        }
    }

    public void StopIdleLoop()
    {
        idleLoopInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        isIdleLoopPlaying = false;
    }

    /// <summary>
    /// Plays a footstep sound.
    /// </summary>
    public void PlayFootstep()
    {
        //// Checks what is the current surface and sets the local parameter
        //if (surface != null)
        //{
        //    string surfaceString = surface.ToString();

        //    if (surfaceString == "TilesetRockRules (UnityEngine.RuleTile)")
        //    {
        //        footstepInstance.setParameterByName("FloorType", 0f);
        //    }
        //    else
        //    {
        //        footstepInstance.setParameterByName("FloorType", 1f);
        //    }
        //}
        footstepInstance.set3DAttributes(attributes3d);
        footstepInstance.start();
    }

    public void PlayDetect()
    {
        AudioManager.Instance.PlaySound(detectEvent, transform.position);
    }

    public void PlayAttack()
    {
        AudioManager.Instance.PlaySound(attackEvent, transform.position);
    }

    public void PlayPain()
    {
        AudioManager.Instance.PlaySound(painEvent, transform.position);
    }

    public void PlayDeath()
    {
        idleLoopInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        AudioManager.Instance.PlaySound(deathEvent, transform.position);
    }

}
