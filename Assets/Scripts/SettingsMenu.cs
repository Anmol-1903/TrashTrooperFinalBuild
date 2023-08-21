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

    [SerializeField] Toggle _audioButton;
    [SerializeField] Toggle _musicButton;

    int muteAudio = 1;
    int muteMusic = 1;

    public void OnEnable()
    {
        GetController();
        GetMusic();
        GetAudio();
    }
    public void GetController()
    {
        if (PlayerPrefs.GetInt("controllerType") == 1)
        {
            controller.isOn = true;
        }
        else if (PlayerPrefs.GetInt("controllerType") == 0)
        {
            controller.isOn = false;
        }
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
    public void GetMusic()
    {
        _musicSlider.maxValue = sliderDivision;
        muteMusic = PlayerPrefs.GetInt("MuteMusic", 1);
        if (PlayerPrefs.GetInt("MuteMusic", 1) == 0)
        {
            _musicButton.isOn = true;
            _musicSlider.value = 0;
        }
        else
        {
            _musicButton.isOn = false;
            _musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue", 43);
        }
        _musicMixer.SetFloat("Music", (_musicSlider.value * muteMusic * (100 / sliderDivision)) - 80);
    }
    public void GetAudio()
    {
        _audioSlider.maxValue = sliderDivision;
        muteAudio = PlayerPrefs.GetInt("MuteAudio",1);
        if (PlayerPrefs.GetInt("MuteAudio",1) == 0)
        {
            _audioButton.isOn = true;
            _audioSlider.value = 0;
        }
        else
        {
            _audioButton.isOn = false;
            _audioSlider.value = PlayerPrefs.GetFloat("AudioSliderValue",70);
        }
        _audioMixer.SetFloat("Audio", (_audioSlider.value * muteAudio * (100 / sliderDivision)) - 80);
    }
    public void SetMusic(float volume)
    {
        _musicMixer.SetFloat("Music", (volume * muteMusic * (100 / sliderDivision)) - 80);
    }
    public void MuteMusic()
    {
        if (_musicButton.isOn)
        {
            PlayerPrefs.SetFloat("MusicSliderValue", _musicSlider.value);
            _musicSlider.value = 0;
            muteMusic = 0;
            _musicSlider.interactable = false;
        }
        else
        {
            muteMusic = 1;
            _musicSlider.interactable = true;
            _musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue");
        }
        PlayerPrefs.SetInt("MuteMusic", muteMusic);
        _musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue") * muteMusic;
    }
    public void SetAudio(float volume)
    {
        _audioMixer.SetFloat("Audio", (volume * muteAudio * (100 / sliderDivision)) - 80);
    }
    public void MuteAudio()
    {
        if (_audioButton.isOn)
        {
            PlayerPrefs.SetFloat("AudioSliderValue", _audioSlider.value);
            _audioSlider.value = 0;
            muteAudio = 0;
            _audioSlider.interactable = false;
        }
        else
        {
            _audioSlider.value = PlayerPrefs.GetFloat("AudioSliderValue");
            muteAudio = 1;
            _audioSlider.interactable = true;
        }
        PlayerPrefs.SetInt("MuteAudio", muteAudio);
        _audioMixer.SetFloat("Audio", (PlayerPrefs.GetFloat("AudioSliderValue") * muteAudio * (100 / sliderDivision)) - 80);
    }
    public void Save()
    {
        if (!_audioButton.isOn)
        {
            PlayerPrefs.SetFloat("AudioSliderValue", _audioSlider.value);
        }
        if (!_musicButton.isOn)
        {
            PlayerPrefs.SetFloat("MusicSliderValue", _musicSlider.value);
        }
    }
}