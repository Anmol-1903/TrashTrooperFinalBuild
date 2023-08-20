using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] Slider _progressBar;
    [SerializeField] GameObject _levelSelector;
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _settings;
    [SerializeField] GameObject _upgrades;
    [SerializeField] GameObject _credits;
    [SerializeField] GameObject _quit;
    [SerializeField] GameObject _asynLoader;
    private void Start()
    {
        PlayerPrefs.GetInt("tutorialLevelPlayed", 0);
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
        if (PlayerPrefs.GetInt("tutorialLevelPlayed") == 0)
        {  //0 = not player | 1 = played
            SceneManager.LoadScene(4);
        }

        if (_levelSelector != null)
        {
            TurnOffAllGameObjects();
            _levelSelector.SetActive(true);
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
    }
    IEnumerator LoadLevel(int _levelNumber)
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(_levelNumber);
        PlayerPrefs.SetInt("tutorialLevelPlayed", 1);
        while (!_operation.isDone && _progressBar != null)
        {
            float _progress = Mathf.Clamp01(_operation.progress / .9f);
            _progressBar.value = _progress;
            yield return null;
        }
    }
}