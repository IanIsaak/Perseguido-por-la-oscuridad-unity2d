using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    private const string VolumePrefKey = "GameVolume";

    private void Start()
    {
        float defaultVolume = 0.0f;
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, defaultVolume);
        audioMixer.SetFloat("Volume", savedVolume);

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }
    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }
}