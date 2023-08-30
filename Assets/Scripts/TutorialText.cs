using System.Collections;
using UnityEngine;
using TMPro;
public class TutorialText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI texts;
    [TextArea(10, 10)]
    [SerializeField] string[] lines;
    [SerializeField] float text_speed;
    [SerializeField] TUT_Manager TM;
    [SerializeField] GameObject _controlsUI, _trash1;
    [SerializeField] GameObject UIManager, MechanicsCanvas, _hello;
    Trash _trash;

    private int index;

    private void Awake()
    {
        _trash = FindFirstObjectByType<Trash>();
    }

    private void Start()
    {
        _hello.SetActive(true);
        UIManager.SetActive(false);
        MechanicsCanvas.SetActive(false);
        texts.text = string.Empty;
        index = 0;
        StartText();

        _controlsUI.SetActive(false);
        _trash1.SetActive(false);
    }

    void StartText()
    {
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToString().ToCharArray())
        {
            texts.text += c;
            yield return new WaitForSecondsRealtime(text_speed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            texts.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            _hello.SetActive(false);
            UIManager.SetActive(true);
            MechanicsCanvas.SetActive(true);
            _trash1.SetActive(true);
        }
    }
    public void NextDialouge()
    {
        if (texts.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            texts.text = lines[index];
        }
    }
    public void SkipTutorialText()
    {
        _hello.SetActive(false);
        UIManager.SetActive(true);
        MechanicsCanvas.SetActive(true);
        _trash1.SetActive(true);
    }
}