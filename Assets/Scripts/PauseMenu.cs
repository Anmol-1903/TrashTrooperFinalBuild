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
    private void Start()
    {
        _pauseMenu.SetActive(false);
        _loadingScreen.SetActive(false);
        _HUD.SetActive(true);
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
    public void GoToScene(int sceneIndex)
    {
        _pauseMenu.SetActive(false);
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(sceneIndex));
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