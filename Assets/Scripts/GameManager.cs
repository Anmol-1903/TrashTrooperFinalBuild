using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    TrashDeSpawner TDS;

    [SerializeField] float _gameDuration;
    [SerializeField] float _counter;
    [SerializeField] float _minCleanlinessValue;

    [SerializeField] GameObject _restartPanel;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _HUD;
    [SerializeField] GameObject _nextLevelPanel;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Slider _progressBar;
    private void Awake()
    {
        TDS = GameObject.FindObjectOfType<TrashDeSpawner>();
    }
    private void Start()
    {
        _counter = _gameDuration;
    }
    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("LevelsPassed"));
        if (_counter <= 0)
        {
            if (Time.timeScale > 0.25f)
            {
                Time.timeScale -= Time.deltaTime;
            }
            else
            {
                _nextLevelPanel.SetActive(true);
                if (PlayerPrefs.GetInt("LevelsPassed") < SceneManager.GetActiveScene().buildIndex)
                {
                    PlayerPrefs.SetInt("LevelsPassed", SceneManager.GetActiveScene().buildIndex);
                }
                Time.timeScale = 0;
            }
        }
        else
        {
            _counter -= Time.deltaTime;
        }
        if (TDS._cleanliness < _minCleanlinessValue)
        {
            if (Time.timeScale > 0.25f)
            {
                Time.timeScale -= Time.deltaTime;
            }
            else
            {
                _restartPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void RestartLevel()
    {
        TurnOffAllGameObjects();
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelUsingIndex(SceneManager.GetActiveScene().buildIndex));
    }
    public void NextLevel(string level)
    {
        TurnOffAllGameObjects();
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(level));
    }
    void TurnOffAllGameObjects()
    {
        _loadingScreen.SetActive(false);
        _pauseMenu.SetActive(false);
        _HUD.SetActive(false);
        _nextLevelPanel.SetActive(false);
        _restartPanel.SetActive(false);
    }
    IEnumerator LoadLevelUsingIndex(int _buildIndex)
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(_buildIndex);
        while (!_operation.isDone)
        {
            float _progress = Mathf.Clamp01(_operation.progress / .9f);
            _progressBar.value = _progress;
            yield return null;
        }
    }
    IEnumerator LoadLevel(string level)
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(level);
        while (!_operation.isDone)
        {
            float _progress = Mathf.Clamp01(_operation.progress / .9f);
            _progressBar.value = _progress;
            yield return null;
        }
    }
}