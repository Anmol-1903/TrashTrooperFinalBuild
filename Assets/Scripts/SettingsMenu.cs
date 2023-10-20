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
    public void OnEnable()
    {
        if(controller)
            GetController();
        GetMusic();
        GetAudio();
    }
    public void GetController()
    {
        if (controller)
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
    }
    public void SetController()
    {
        if (controller)
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
    }
    public void GetMusic()
    {
        if (PlayerPrefs.GetFloat("MUSIC", _musicSlider.minValue) == _musicSlider.minValue)
        {
            _musicButton.isOn = true;
            _musicSlider.value = 0;
        }
        else
        {
            _musicButton.isOn = false;
            _musicSlider.value = PlayerPrefs.GetFloat("MUSIC", 0);
        }
        _musicMixer.SetFloat("Music", Mathf.Log10(_musicSlider.value) * 20);
    }
    public void GetAudio()
    {
        if (PlayerPrefs.GetFloat("AUDIO", _audioSlider.minValue) == _audioSlider.minValue)
        {
            _audioButton.isOn = true;
            _audioSlider.value = 0;
        }
        else
        {
            _audioButton.isOn = false;
            _audioSlider.value = PlayerPrefs.GetFloat("AUDIO", 0);
        }
        _audioMixer.SetFloat("Audio", Mathf.Log10(_audioSlider.value * 20));
    }
    public void SetMusic(float volume)
    {
        _musicMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MUSIC", _musicSlider.value);
    }
    public void MuteMusic()
    {
        if (_musicButton.isOn)
        {
            PlayerPrefs.SetFloat("MusicSliderValue", _musicSlider.value);
            _musicSlider.value = 0;
            _musicSlider.interactable = false;
        }
        else
        {
            _musicSlider.interactable = true;
            _musicSlider.value = PlayerPrefs.GetFloat("MusicSliderValue");
        }
        _musicMixer.SetFloat("Music", Mathf.Log10(_musicSlider.value) * 20);
        PlayerPrefs.SetFloat("MUSIC", _musicSlider.value);
    }
    public void SetAudio(float volume)
    {
        _audioMixer.SetFloat("Audio", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("AUDIO", _audioSlider.value);
    }
    public void MuteAudio()
    {
        if (_audioButton.isOn)
        {
            PlayerPrefs.SetFloat("AudioSliderValue", _audioSlider.value);
            _audioSlider.value = 0;
            _audioSlider.interactable = false;
        }
        else
        {
            _audioSlider.value = PlayerPrefs.GetFloat("AudioSliderValue");
            _audioSlider.interactable = true;
        }
        _audioMixer.SetFloat("Audio", Mathf.Log10(_audioSlider.value) * 20);
        PlayerPrefs.SetFloat("AUDIO", _audioSlider.value);
    }
}