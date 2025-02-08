using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.Tilemaps;

public class AudioEventPost : MonoBehaviour
{

    [Tooltip("Choose an FMOD event")]
    public EventReference audioObject;
    // public string audioObject;

    public void PlayAudioEvent(TileBase surface = null)
    {
        //FMOD.Studio.System.setParameterByName("PlrFloorType")

        AudioManager.Instance.PlaySound(audioObject.Path, transform.position);
    }

}
