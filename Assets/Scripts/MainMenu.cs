using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider _progressBar;
    [SerializeField] GameObject _levelSelector;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _settings;
    [SerializeField] GameObject _resetGame;
    [SerializeField] GameObject _upgrades;
    [SerializeField] GameObject _credits;
    [SerializeField] GameObject _quit;
    [SerializeField] GameObject _asynLoader;

   

    [SerializeField] AudioMixer _musicMixer;
    [SerializeField] AudioMixer _audioMixer;


    private void Start()
    {
        if (_musicMixer != null)
            _musicMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music", -9));
        if (_audioMixer != null)
            _audioMixer.SetFloat("Audio", PlayerPrefs.GetFloat("Audio", 0));
        Main();
    }
    public void Main()
    {
        if (_mainMenu != null)
        {
            TurnOffAllGameObjects();
            _mainMenu.SetActive(true);
        }
    }
    public void Play()
    {
        if (PlayerPrefs.GetInt("HitCountKey", 0) == 0)
        {
            StartCoroutine(LoadLevelByName("Tutorial"));
        }
        else
        {
            if (_levelSelector != null)
            {
                TurnOffAllGameObjects();
                _levelSelector.SetActive(true);
            }
        }
    }
    public void Settings()
    {
        if (_settings != null)
        {
            TurnOffAllGameObjects();
            _settings.SetActive(true);
        }
    }
    public void Credits()
    {
        if (_credits != null)
        {
            TurnOffAllGameObjects();
            _credits.SetActive(true);
        }
    }
    public void Quit()
    {
        if (_quit != null)
        {
            TurnOffAllGameObjects();
            _quit.SetActive(true);
        }
    }
    public void Upgrades()
    {
        if (_upgrades != null)
        {
            TurnOffAllGameObjects();
            _upgrades.SetActive(true);
        }
    }

    public void LoadingScreen(int _levelNumber)
    {
        TurnOffAllGameObjects();
        if (_asynLoader != null)
            _asynLoader.SetActive(true);
        StartCoroutine(LoadLevel(_levelNumber));
    }
    public void PlayTutorial()
    {
        TurnOffAllGameObjects();
        if (_asynLoader != null)
            _asynLoader.SetActive(true);
        StartCoroutine(LoadLevelByName("Tutorial"));
    }
    public void ClearAllPlayerPrefs()
    {
        //TurnOffAllGameObjects();
        _resetGame.SetActive(true);
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(LoadLevelByName("MainMenu"));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void TurnOffAllGameObjects()
    {
        if (_mainMenu != null)
            _mainMenu.SetActive(false);
        if (_asynLoader != null)
            _asynLoader.SetActive(false);
        if (_levelSelector != null)
            _levelSelector.SetActive(false);
        if (_upgrades != null)
            _upgrades.SetActive(false);
        if (_settings != null)
            _settings.SetActive(false);
        if (_credits != null)
            _credits.SetActive(false);
        if (_quit != null)
            _quit.SetActive(false);
        if (_resetGame != null)
            _resetGame.SetActive(false);
    }
    IEnumerator LoadLevel(int _levelNumber)
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(_levelNumber);
        while (!_operation.isDone && _progressBar != null)
        {
            float _progress = Mathf.Clamp01(_operation.progress / .9f);
            _progressBar.value = _progress;
            yield return null;
        }
    }
    IEnumerator LoadLevelByName(string _SceneName)
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(_SceneName);
        while (!_operation.isDone && _progressBar != null)
        {
            float _progress = Mathf.Clamp01(_operation.progress / .9f);
            _progressBar.value = _progress;
            yield return null;
        }
    }
    public void openurl(string url)
    {
        Application.OpenURL(url);
    }
}