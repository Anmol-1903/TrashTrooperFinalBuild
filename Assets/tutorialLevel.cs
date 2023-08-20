using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class tutorialLevel : MonoBehaviour
{

    [SerializeField] private int counter = 0;
    [SerializeField] private int _alpha = 0;
    [SerializeField] Button tutSceneloader;

    [SerializeField] GameObject _notCompleted;
    [SerializeField] GameObject _completed;

    private const string HitCountKey = "HitCountKey"; // 1

    private void OnEnable()
    {
        if(PlayerPrefs.GetInt(HitCountKey,0) == 0)
        {
        _notCompleted.SetActive(true);
        _completed.SetActive(false);

        }
        else
        {
            _notCompleted.SetActive(false);
            _completed.SetActive(true);
        }
    }







    /*
        private void Start()
        {
            _completed.enabled = true;
            _notCompleted.enabled = false;

            if (PlayerPrefs.HasKey(HitCountKey)) // 2
            {
                // Read the hit count from the PlayerPrefs.
                counter = PlayerPrefs.GetInt(HitCountKey, 0); // 3

            }
        }

        private void Update()
        {
            if (counter == 0)
            {
                tutSceneloader.enabled= true;
                tutSceneloader.image.color = Color.white;
                _notCompleted.enabled = true;
                _completed.enabled = false;
                //normal image = not played yet
            }
            else
            {
                tutSceneloader.image.color = Color.gray;
                tutSceneloader.enabled = false;
                _completed.enabled = true;
                _notCompleted.enabled = false;
                //disabled image = played
            }
        }
    */
    public void TutLoader()
    {
        SceneManager.LoadScene("Tutorial");
        
        // Set and save the hit count before ending the game.
        // 5
        PlayerPrefs.SetInt(HitCountKey, 1); // 4
        //PlayerPrefs.Save();
    }


}