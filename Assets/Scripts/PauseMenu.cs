using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    IntestitialAd _ia;
    bool _adRunning = false , _playAd = false ;
    public static PauseMenu Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI manager is empty");
            }
            return _instance;
        }
    }
    private static PauseMenu _instance;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] GameObject _HUD;
    [SerializeField] GameObject _lvlComplete, _nextlvlPanel;
    [SerializeField] Slider _progressBar;
    [SerializeField] float _timer;
    [SerializeField] SettingsMenu _sm;
    private void Awake()
    {
        _ia = FindObjectOfType<IntestitialAd>();
        _instance = this;
    }
    private void Start()
    {
        _pauseMenu.SetActive(false);
        _loadingScreen.SetActive(false);
        _HUD.SetActive(true);
        if(_sm != null)
        {
        _sm.GetController();
        _sm.GetMusic();
        _sm.GetAudio();
        }
    }
    private void Update()
    {
        if (!_adRunning && _playAd)
        {
            _ia.ShowAd();
            _adRunning = true;
            _playAd = false;
        }
    }
    public void Resume()
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _HUD.SetActive(true);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        _pauseMenu.SetActive(true);
        _HUD.SetActive(false);
    }
    public void GoToMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }
    public void RestartLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }
    IEnumerator LoadLevel(int _levelNumber)
    {
        _pauseMenu.SetActive(false);
        _HUD.SetActive(false);
        _loadingScreen.SetActive(true);
        AsyncOperation _operation = SceneManager.LoadSceneAsync(_levelNumber);
        while (!_operation.isDone)
        {
            float _progress = Mathf.Clamp01(_operation.progress / .9f);
            _progressBar.value = _progress;
            yield return null;
        }
    }
    
    public void CallLvlComplete()
    {
        if (_HUD.activeInHierarchy)
        {
            _HUD.SetActive(false);
            _lvlComplete.SetActive(true);
        }
        StartCoroutine(enumerator());
        
    }
    IEnumerator enumerator()
    {
        while (_lvlComplete.transform.GetChild(0).gameObject.activeInHierarchy)
        {
            yield return null;
        }
        _playAd = true;
        yield return new WaitForEndOfFrame();
        NextLevelPanel();

    }
    public void NextLevelPanel()
    {
        _nextlvlPanel.SetActive(true);
        _lvlComplete.SetActive(false);
    }
}