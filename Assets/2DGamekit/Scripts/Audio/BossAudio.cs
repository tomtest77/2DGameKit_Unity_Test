using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class BossAudio : MonoBehaviour
{
    [Header("FMOD Events for boss")]
    public EventReference gunnerStepEvent;
    public EventReference gunnerIdleEvent;
    public EventReference gunnerAttackEvent;

    [Header("FMOD Studio Event Emmiter References")]
    public StudioEventEmitter gunnerStepEmitter;
    public StudioEventEmitter gunnerIdleEmitter;
    public StudioEventEmitter gunnerAttackEmitter;

    // Start is called before the first frame update
    void Start()
    {
        // Referencing the Events to the Studio Event Emitters
        gunnerStepEmitter.EventReference = gunnerStepEvent;
        gunnerIdleEmitter.EventReference = gunnerIdleEvent;
        gunnerAttackEmitter.EventReference = gunnerAttackEvent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
