using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] Button[] _levelButtons;
    [SerializeField] Slider[] _stars;
    [SerializeField] TextMeshProUGUI[] _levelCompleteText;
    [SerializeField] int _levelCleared;
    [SerializeField] IntestitialAd _ia;

    private void Awake()
    {
        _levelCompleteText = new TextMeshProUGUI[_levelButtons.Length];
        _stars = new Slider[_levelButtons.Length];
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            _levelCompleteText[i] = _levelButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

    }

    private void OnEnable()
    {
        _levelCleared = PlayerPrefs.GetInt("LevelsPassed", 0);
        for (int i = 0; i < _levelButtons.Length; i++)
        {


            if (i <= _levelCleared)
            {
                if (_levelButtons[i] != null)
                    _levelButtons[i].interactable = true;
                if (i < _levelCleared)
                {
                    _levelCompleteText[i] = _levelButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    if (_levelCompleteText[i] != null)
                        _levelCompleteText[i].text = "Level " + (i + 1).ToString() + " Completed";
                    _stars[i] = _levelButtons[i].GetComponentInChildren<Slider>();
                    if (_stars[i] != null)
                    {
                        _stars[i].maxValue = 3;
                        _stars[i].value = PlayerPrefs.GetInt((i+1).ToString(), 0);
                    }
                }
            }
            else
            {
                if (_levelButtons[i] != null)
                    _levelButtons[i].interactable = false;
                if (_levelCompleteText[i] != null)
                    _levelCompleteText[i].text = "Level " + (i + 1).ToString();
            }
        }
    }
}