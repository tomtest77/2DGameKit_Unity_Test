using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAudio : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private string surfaceOld;
    
    [Header("FMOD Events")]
    public EventReference footstepEvent;
    public EventReference jumpEvent;
    public EventReference meleeEvent;
    public EventReference rangedEvent;
    public EventReference deathEvent;

    /// <summary>
    /// Plays a footstep sound.
    /// </summary>
    public void PlayFootstep(TileBase surface = null)
    {
        // Checks if the surface has changed
        if (surface != null)
        {
            string surfaceString = surface.ToString();

            if (surfaceString == "TilesetRockRules (UnityEngine.RuleTile)")
            {
                //setParameterByName("PlrFloorType", 1f);
                RuntimeManager.StudioSystem.setParameterByName("PlrFloorType", 0f);
            }
            else
            {
                RuntimeManager.StudioSystem.setParameterByName("PlrFloorType", 1f);
            }
        }

        // Plays a one-shot sound at the player's current position.
        AudioManager.Instance.PlaySound(footstepEvent.Path, transform.position);
    }

    /// <summary>
    /// Plays the jump sound.
    /// </summary>
    public void PlayJump()
    {
        AudioManager.Instance.PlaySound(jumpEvent.Path, transform.position);
    }

    /// <summary>
    /// Plays the attack sound.
    /// </summary>
    public void PlayMelee()
    {
        AudioManager.Instance.PlaySound(meleeEvent.Path, transform.position);
    }

    /// <summary>
    /// Plays the attack sound.
    /// </summary>
    public void PlayRanged()
    {
        AudioManager.Instance.PlaySound(rangedEvent.Path, transform.position);
    }

    /// <summary>
    /// Plays the death sound.
    /// </summary>
    public void PlayDeath()
    {
        AudioManager.Instance.PlaySound(deathEvent.Path, transform.position);
    }
}