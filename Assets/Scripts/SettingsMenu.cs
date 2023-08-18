using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer _musicMixer;
    [SerializeField] AudioMixer _audioMixer;

    [SerializeField] Toggle controller;

    [Tooltip("1-100")]
    [SerializeField] int sliderDivision;

    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _audioSlider;

    public void OnEnable()
    {
        if(PlayerPrefs.GetInt("controllerType") == 1)
        {
            controller.isOn = true;
        }
        else if(PlayerPrefs.GetInt("controllerType") == 0)
        {
            controller.isOn = false;
        }
        _musicSlider.maxValue = sliderDivision;
        _audioSlider.maxValue = sliderDivision;
        _musicMixer.GetFloat("Music", out float music);
        _musicSlider.value = (music + 80) / (100 / sliderDivision);
        _audioMixer.GetFloat("Audio", out float audio);
        _audioSlider.value = (audio + 80) / (100 / sliderDivision);
    }
    public void SetController()
    {
        if (controller.isOn)
        {
            PlayerPrefs.SetInt("controllerType", 1);
        }
        else
        {
            PlayerPrefs.SetInt("controllerType", 0);
        }
    }
    public void SetMusic(float volume)
    {
        _musicMixer.SetFloat("Music", (volume * (100 / sliderDivision)) - 80);
    }
    public void SetAudio(float volume)
    {
        _audioMixer.SetFloat("Audio", (volume * (100 / sliderDivision)) - 80);
    }
}