using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] GameObject _HUD;
    [SerializeField] Slider _progressBar;

    [SerializeField] private AudioSource _pause;
    private void Start()
    {
        _pauseMenu.SetActive(false);
        _loadingScreen.SetActive(false);
        _HUD.SetActive(true);
    }
    public void Resume()
    {
        _pause.Play();
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _HUD.SetActive(true);
    }
    public void Pause()
    {
        if( _pause != null)
        {
            _pause.Pause();
        }
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
}