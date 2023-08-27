using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    [SerializeField] Button[] _levelButtons;
    [SerializeField] TextMeshProUGUI[] _levelCompleteText;
    [SerializeField] int _levelCleared;
    [SerializeField] IntestitialAd _ia;

    private void Awake()
    {
        _levelCompleteText = new TextMeshProUGUI[_levelButtons.Length];
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
                if(i < _levelCleared)
                {
                if (_levelCompleteText[i] != null)
                    _levelCompleteText[i].text = "Level " + (i + 1).ToString() + " Completed";
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