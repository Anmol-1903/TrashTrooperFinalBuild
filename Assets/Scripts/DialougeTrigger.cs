using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] dialouges;
    [SerializeField] GameObject canvas_;
    int i;

    private void Start()
    {
        for (int j = 0; j < dialouges.Length; j++)
        {
            dialouges[j].SetActive(false);
        }
        i = 0;
        dialouges[0].SetActive(true);
        Time.timeScale = 0;
    }
    public void NextDialouge()
    {
        i++;
        for (int j = 0; j < dialouges.Length; j++)
        {
            if (j == i)
            {
                dialouges[j].SetActive(true);
            }
            else
            {
                dialouges[j].SetActive(false);
            }
        }
        canvas_.SetActive(false);
    }
}
