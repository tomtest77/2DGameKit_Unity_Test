using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

[RequireComponent(typeof(Slider))]
public class FMODVCASliderLink : MonoBehaviour
{
    [Tooltip("Has to be the exact name of your VCA")]
    public string vcaName;

    protected Slider m_Slider;

    void Awake()
    {
        vcaName = "VCA_" + vcaName;
        m_Slider = GetComponent<Slider>();

        float value;
        value = AudioManager.Instance.GetVCAVolume(vcaName);

        m_Slider.value = value;
        m_Slider.onValueChanged.AddListener(SliderValueChange);
    }


    void SliderValueChange(float value)
    {
        AudioManager.Instance.SetVCAVolume(vcaName, value);
    }
}
