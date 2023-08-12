using UnityEngine;
using UnityEngine.UI;
public class UpgradeSelector : MonoBehaviour
{
    [Header("Set the Lock on/off respective to the toggle")]
    [SerializeField] Upgrades[] _upgradeToggles;
    [SerializeField] bool allOff;
    [SerializeField] int _upgradeNumber;            // Use This Number To Apply Upgrade To Player
    private void OnEnable()
    {
        for (int i = 0; i < _upgradeToggles.Length; i++)
        {
            if (PlayerPrefs.GetInt("LevelsPassed") >= _upgradeToggles[i]._levelNeededToBeCleared)
            {
                _upgradeToggles[i].isAvailable = true;
            }
            else
            {
                _upgradeToggles[i].isAvailable = false;
            }
            _upgradeToggles[i]._upgrade.interactable = _upgradeToggles[i].isAvailable;
            if(PlayerPrefs.GetInt("SelectedPowerUp") == i + 1)
            {
                _upgradeToggles[i]._upgrade.isOn = true;
            }
            else
            {
                _upgradeToggles[i]._upgrade.isOn = false;
            }
        }
    }
    public void UpdateToggle()
    {
        allOff = true;
        for (int i = 0; i < _upgradeToggles.Length; i++)
        {
            if (_upgradeToggles[i]._upgrade.isOn)
            {
                _upgradeNumber = i + 1;
                allOff = false;
            }
        }
        if (allOff)
        {
            _upgradeNumber = 0;
        }
        PlayerPrefs.SetInt("SelectedPowerUp", _upgradeNumber);
    }
}
[System.Serializable]
public class Upgrades
{
    public Toggle _upgrade;
    public bool isAvailable;
    public int _levelNeededToBeCleared;
}