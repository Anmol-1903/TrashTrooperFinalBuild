using UnityEngine;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    [SerializeField] Button[] _levelButtons;
    [SerializeField] int _levelCleared;

    private void OnEnable()
    {
        _levelCleared = PlayerPrefs.GetInt("LevelsPassed", 0);
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            if(i <= _levelCleared)
            {
                if(_levelButtons[i] != null)
                _levelButtons[i].interactable = true;
            }
            else
            {
                if(_levelButtons[i] != null)
                _levelButtons[i].interactable = false;
            }
        }
    }
}