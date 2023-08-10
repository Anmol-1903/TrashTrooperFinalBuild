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
        Main();
    }
    public void Main()
    {
        TurnOffAllGameObjects();
        _mainMenu.SetActive(true);
    }
    public void Play()
    {
        TurnOffAllGameObjects();
        _levelSelector.SetActive(true);
    }
    public void Settings()
    {
        TurnOffAllGameObjects();
        _settings.SetActive(true);
    }
    public void Credits()
    {
        TurnOffAllGameObjects();
        _credits.SetActive(true);
    }
    public void Quit()
    {
        TurnOffAllGameObjects();
        _quit.SetActive(true);
    }
    public void Upgrades()
    {
        TurnOffAllGameObjects();
        _upgrades.SetActive(true);
    }

    public void LoadingScreen(int _levelNumber)
    {
        TurnOffAllGameObjects();
        _asynLoader.SetActive(true);
        StartCoroutine(LoadLevel(_levelNumber));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void TurnOffAllGameObjects()
    {
        _mainMenu.SetActive(false);
        _asynLoader.SetActive(false);
        _levelSelector.SetActive(false);
        _upgrades.SetActive(false);
        _settings.SetActive(false);
        _credits.SetActive(false);
        _quit.SetActive(false);
    }
    IEnumerator LoadLevel(int _levelNumber)
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(_levelNumber);
        while (!_operation.isDone)
        {
            float _progress = Mathf.Clamp01(_operation.progress / .9f);
            _progressBar.value = _progress;
            yield return null;
        }
    }
}