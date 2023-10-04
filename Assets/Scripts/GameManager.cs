using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public enum Objective
    {
        None,
        Cleanliness,
        SurviveTime,
        TrashCount,
        Boss,
    }
    #region Ranking

    public Objective objective;

    [SerializeField] float _cleanlinessForStar1;
    [SerializeField] float _cleanlinessForStar2;
    [SerializeField] float _cleanlinessForStar3;
    [SerializeField] float _gameEndTimer;

    [SerializeField] float _surviveTimeForStar1;
    [SerializeField] float _surviveTimeForStar2;
    [SerializeField] float _surviveTimeForStar3;
    [SerializeField] float _minCleanlinessValue;

    [SerializeField] int _trashCountForStar1;
    [SerializeField] int _trashCountForStar2;
    [SerializeField] int _trashCountForStar3;
    [SerializeField] BetterCatchSystem BCS;

    [SerializeField] bool _bossDead;

    #endregion
    float _gameEndCounter;
    int stars;

    TrashDeSpawner TDS;
    IntestitialAd _ia;
    
    [SerializeField] AudioClip _levelComplete;

    [SerializeField] float _gameDuration;
    [SerializeField] float _counter;

    [SerializeField] GameObject _restartPanel;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _HUD;
    [SerializeField] GameObject _nextLevelPanel;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Slider _realtimeProgressBar;
    [SerializeField] Slider _progressBar;
    [SerializeField] Slider _stars;
    [SerializeField] TextMeshProUGUI _gameTimer;
    bool _adRunning = false;



    private void Awake()
    {
        TDS = FindObjectOfType<TrashDeSpawner>();
        _ia = FindObjectOfType<IntestitialAd>();
    }
    private void Start()
    {
        if (objective == Objective.Cleanliness)
            _gameEndCounter = _gameEndTimer;
        else if (objective == Objective.SurviveTime)
            _gameEndCounter = 0;
        else if (objective == Objective.TrashCount)
        {
            _gameEndCounter = 0;
            BCS = FindObjectOfType<BetterCatchSystem>();
        }
        else if(objective == Objective.Boss)
        {
            _gameEndCounter = _gameEndTimer;
            _bossDead = false;
        }
    }
    private void Update()
    {
        if (objective == Objective.Cleanliness)
        {
            _gameTimer.text = (((int)_gameEndCounter) / 60).ToString("D2") + " : " + (((int)_gameEndCounter) % 60).ToString("D2");
            if (_gameEndCounter <= 0 && TDS._cleanliness > 0)
            {
                if (Time.timeScale > 0.2f)
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
                    if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 0) < stars)
                    {
                        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), stars);
                    }
                    Time.timeScale = 0;
                }
            }
            else
            {
                _gameEndCounter -= Time.deltaTime;
            }
            if (TDS._cleanliness >= _cleanlinessForStar3)
            {
                stars = 3;
            }
            else if (TDS._cleanliness >= _cleanlinessForStar2)
            {
                stars = 2;
            }
            else if (TDS._cleanliness >= _cleanlinessForStar1)
            {
                stars = 1;
            }
            else if (TDS._cleanliness < _cleanlinessForStar1)
            {
                stars = 0;
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
        else if (objective == Objective.SurviveTime)
        {
            _gameTimer.text = (((int)_gameEndCounter) / 60).ToString("D2") + " : " + (((int)_gameEndCounter) % 60).ToString("D2");
            if (TDS._cleanliness >= _minCleanlinessValue && _gameEndCounter < _surviveTimeForStar3)
            {
                _gameEndCounter += Time.deltaTime;
                if (_gameEndCounter >= _surviveTimeForStar3)
                {
                    stars = 3;
                }
                else if (_gameEndCounter >= _surviveTimeForStar2)
                {
                    stars = 2;
                }
                else if (_gameEndCounter >= _surviveTimeForStar1)
                {
                    stars = 1;
                }
                else
                {
                    stars = 0;
                }
            }
            else if (TDS._cleanliness >= _minCleanlinessValue && _gameEndCounter >= _surviveTimeForStar3)
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
                    if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name, 0) < stars)
                    {
                        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, stars);
                    }
                    Time.timeScale = 0;
                }
            }
            else
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
        else if (objective == Objective.TrashCount)
        {
            _gameTimer.text = (((int)_gameEndCounter) / 60).ToString("D2") + " : " + (((int)_gameEndCounter) % 60).ToString("D2");
            if (TDS._cleanliness > 0 && _gameEndCounter > 0)
            {
                _gameEndCounter -= Time.deltaTime;
                if (BCS._dry_Trashcan >= _trashCountForStar3 && BCS._wet_Trashcan >= _trashCountForStar3)
                {
                    stars = 3;
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
                            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 0) < stars)
                            {
                                PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), stars);
                            }
                            Time.timeScale = 0;
                        }
                    }
                }
                else if (BCS._dry_Trashcan >= _trashCountForStar2 && BCS._wet_Trashcan >= _trashCountForStar2)
                {
                    stars = 2;
                }
                else if (BCS._dry_Trashcan >= _trashCountForStar1 && BCS._wet_Trashcan >= _trashCountForStar1)
                {
                    stars = 1;
                }
                else
                {
                    stars = 0;
                }
            }
            else if (TDS._cleanliness <= 0)
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
        else if (objective == Objective.Boss)
        {
            _gameTimer.text = (((int)_gameEndCounter) / 60).ToString("D2") + " : " + (((int)_gameEndCounter) % 60).ToString("D2");
            if(_gameEndCounter <= 0 || TDS._cleanliness < _minCleanlinessValue)
            {
                stars = 0;
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
            else if(_bossDead && _gameEndCounter > 0 && TDS._cleanliness >= _minCleanlinessValue)
            {
                if (Time.timeScale > 0.2f)
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
                    stars = 3;
                    AudioManager.Instance.BG_Music(_levelComplete);
                    if (PlayerPrefs.GetInt("LevelsPassed") < SceneManager.GetActiveScene().buildIndex)
                    {
                        PlayerPrefs.SetInt("LevelsPassed", SceneManager.GetActiveScene().buildIndex);
                    }
                    if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex.ToString(), 0) < stars)
                    {
                        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex.ToString(), stars);
                    }
                    Time.timeScale = 0;
                }
            }
            else
            {
                _gameEndCounter -= Time.deltaTime;
            }
        }
        _stars.value = stars;
        _realtimeProgressBar.value = stars;
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
    public void BossKilled()
    {
        _bossDead = true;
    }
}