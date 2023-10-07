using UnityEngine;
using System.Collections;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    [SerializeField] Animator _objectiveUI;
    [SerializeField] GameObject _objectiveCanvas, _trashSpawner , UI_manager;
    [SerializeField] TextMeshProUGUI _Objectivetext;
    [TextArea(5,5)]
    [SerializeField] string[] _line;
    [SerializeField] float _typingspeed;

    private int index;

    void Start()
    {
        _trashSpawner.SetActive(false);
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
        _trashSpawner.SetActive(true);
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
            yield return new WaitForSeconds(_typingspeed);
        }
    }
}
