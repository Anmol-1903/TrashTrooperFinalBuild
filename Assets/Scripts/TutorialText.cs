using System.Collections;
using UnityEngine;
using TMPro;
public class TutorialText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI texts;
    [TextArea(10, 10)]
    [SerializeField] string[] lines;
    [SerializeField] float text_speed;
    [SerializeField] Tutorial_Manager TM;

    private int index;

    private void Start()
    {
        texts.text = string.Empty;
        index = 0;
        StartText();
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
            TM = GetComponentInParent<Tutorial_Manager>();
            TM.HelloTrooper();
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
}