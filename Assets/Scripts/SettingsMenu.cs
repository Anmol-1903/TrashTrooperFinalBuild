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
    private void Update()
    {
        Debug.Log(PlayerPrefs.GetFloat("AudioSliderValue") * muteAudio);
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
        _musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue");
        muteMusic = PlayerPrefs.GetInt("MuteMusic");
        _musicMixer.SetFloat("Music", (_musicSlider.value * muteMusic * (100 / sliderDivision)) - 80);
        if (PlayerPrefs.GetInt("MuteMusic") == 0)
        {
            _musicButton.isOn = true;
        }
        else
        {
            _musicButton.isOn = false;
        }
    }
    public void GetAudio()
    {
        _audioSlider.maxValue = sliderDivision;
        _audioSlider.value = PlayerPrefs.GetFloat("AudioSliderValue");
        muteAudio = PlayerPrefs.GetInt("MuteAudio");
        _audioMixer.SetFloat("Audio", (_audioSlider.value * muteAudio * (100 / sliderDivision)) - 80);
        if (PlayerPrefs.GetInt("MuteAudio") == 0)
        {
            _audioButton.isOn = true;
        }
        else
        {
            _audioButton.isOn = false;
        }
    }
    public void SetMusic(float volume)
    {
        _musicMixer.SetFloat("Music", (volume * muteMusic * (100 / sliderDivision)) - 80);
        PlayerPrefs.SetFloat("MusicSliderValue", volume);
    }
    public void SetAudio(float volume)
    {
        _audioMixer.SetFloat("Audio", (volume * muteAudio * (100 / sliderDivision)) - 80);
        PlayerPrefs.SetFloat("AudioSliderValue", volume);
    }
    public void MuteMusic()
    {
        if (_musicButton.isOn)
        {
            muteMusic = 0;
            _musicSlider.interactable = false;
        }
        else
        {
            muteMusic = 1;
            _musicSlider.interactable = true;
        }
        PlayerPrefs.SetInt("MuteMusic", muteMusic);
        SetMusic(PlayerPrefs.GetFloat("MusicSliderValue"));
        _musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue") * muteMusic;
    }
    public void MuteAudio()
    {
        if (_audioButton.isOn)
        {
            muteAudio = 0;
            _audioSlider.interactable = false;
        }
        else
        {
            muteAudio = 1;
            _audioSlider.interactable = true;
        }
        PlayerPrefs.SetInt("MuteAudio", muteAudio);
        SetAudio(PlayerPrefs.GetFloat("AudioSliderValue"));
        _audioSlider.value = PlayerPrefs.GetFloat("AudioSliderValue") * muteAudio;
    }
}