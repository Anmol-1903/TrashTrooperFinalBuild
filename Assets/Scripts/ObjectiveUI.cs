using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] Animator _objectiveUI;
    [SerializeField] GameObject _objectiveCanvas, UI_manager;
    [SerializeField] TextMeshProUGUI _Objectivetext;
    [TextArea(5,5)]
    [SerializeField] string[] _line;
    [SerializeField] float _typingspeed;
    TrashSpawner _trashSpawner;
    [SerializeField] Button _objective_btn;
    [SerializeField] int _gameEndTimer;
    GameManager _gameManager;
    private int index;
    private bool _iscomplete;
    private void Awake()
    {
        _trashSpawner = FindObjectOfType<TrashSpawner>();
        _gameManager = FindObjectOfType<GameManager>(); 
    }
    void Start()
    {
        _objective_btn.interactable = false;
        UI_manager.SetActive(false);
        _Objectivetext.text = string.Empty;
        index = 0;
        Invoke("TextCalling", 0.5f);
    }

    // Update is called once per frame
    public void Objective()
    {
        _objectiveUI.SetBool("isReturn", true);
        Destroy(_objectiveCanvas, 2f);
        Invoke("Delay", 1f);
    }
    void Delay()
    {
        UI_manager.SetActive(true);
        _trashSpawner.enabled = true;
        _gameManager._gameEndCounter = _gameEndTimer;
    }
    void TextCalling()
    {
        StartCoroutine(Text());
    }
    IEnumerator Text()
    {
        foreach (char c in _line[index].ToString().ToCharArray())
        {

            _Objectivetext.text += c;
            if (_Objectivetext.text.Length == _line[index].Length)
            {
                _objective_btn.interactable = true;
            }
            yield return new WaitForSeconds(_typingspeed);
        }
    }
}
