using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] float _gameDuration;
    [SerializeField] float _counter;

    [SerializeField] GameObject _nextLevelPanel;
    [SerializeField] GameObject _loadingScreen;
    [SerializeField] Slider _progressBar;
    private void Start()
    {
        _counter = _gameDuration;
    }
    private void Update()
    {
        if (_counter <= 0)
        {
            Time.timeScale = 0f;
            _nextLevelPanel.SetActive(true);
        }
        else
        {
            _counter -= Time.deltaTime;
        }
    }
    public void NextLevel(string level)
    {
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadLevel(level));
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