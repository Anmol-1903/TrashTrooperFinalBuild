using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class LevelScroller : MonoBehaviour
{
    [SerializeField] GameObject scrollBar;
    public int currentScroll = 0;
    float scrollPos = 0;
    float[] pos;
    float distance;
    public float dest;
    private void Update()
    {
        pos = new float[transform.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
        for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, dest, 0.1f);
            }
        }
    }
    public void NextLevelScroll()
    {
        if (currentScroll < transform.childCount - 1)
            currentScroll++;
        dest = pos[currentScroll];
        for (int i = 0; i < pos.Length; i++)
        {
            transform.GetChild(i).localScale = new Vector2(1f, 1f);
            transform.GetChild(i).gameObject.SetActive(true);
            for (int j = 0; j < pos.Length; j++)
            {
                if (j != currentScroll)
                {
                    transform.GetChild(j).localScale = new Vector2(0.75f, 0.75f);
                    transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
    }
    public void LastLevelScroll()
    {
        if (currentScroll > 0)
            currentScroll--;
        dest = pos[currentScroll];
        for (int i = 0; i < pos.Length; i++)
        {
            transform.GetChild(i).localScale = new Vector2(1f, 1f);
            transform.GetChild(i).gameObject.SetActive(true);
            for (int j = 0; j < pos.Length; j++)
            {
                if (j != currentScroll)
                {
                    transform.GetChild(j).localScale = new Vector2(0.75f, 0.75f);
                    transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
    }
}