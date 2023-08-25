using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    TrashDeSpawner TDS;
    IntestitialAd _ia;
    
    [SerializeField] AudioClip _levelComplete;

    [SerializeField] float _gameDuration;
    [SerializeField] float _counter;
    [Range(0,100)]
    [SerializeField] float _minCleanlinessValue;

    [SerializeField] GameObject _restartPanel;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _HUD;
    [SerializeField] GameObject _nextLevelPanel;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Slider _progressBar;

    [SerializeField] private TextMeshProUGUI _gameTimer;
    bool _adRunning = false;
    //[SerializeField] AudioClip _ingamebgclip;


    private void Awake()
    {
        TDS = FindObjectOfType<TrashDeSpawner>();
        _ia = FindObjectOfType<IntestitialAd>();
    }
    private void Start()
    {
        _counter = _gameDuration;
        Application.targetFrameRate = -1;
    }
    private void Update()
    {
        _gameTimer.text = ((int)_counter)/60 + " : " + ((int)_counter) % 60;
        if (_counter <= 0)
        {
            if (Time.timeScale > 0.25f)
            {
                Time.timeScale -= Time.deltaTime;
            }
            else
            {
                if (!_adRunning)
                {
                    _ia.ShowAd();
                    _adRunning = true;
                }
                _nextLevelPanel.SetActive(true);
                AudioManager.Instance.BG_Music(_levelComplete);
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
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(level));
        TurnOffAllGameObjects();
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
            //AudioManager.Instance.BG_Music(_ingamebgclip);
        }
    }
}