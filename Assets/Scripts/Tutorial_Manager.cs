using UnityEngine;
public class Tutorial_Manager : MonoBehaviour
{
    [SerializeField] GameObject Trash1, Trash2, Trash3;
    [SerializeField] GameObject _hello;
    [SerializeField] Transform _player, _dustbin1, _dustbin2;
    [SerializeField] GameObject canvas1, canvas2, canvas3;
    [SerializeField] GameObject tutorialEnd;
    [SerializeField] PlayerMove playerMove;
    private void Start()
    {
        DeactivateAllObjects();
        _hello.SetActive(true);
        Trash1.GetComponent<Trash>().normalSpeed = 0f;
        Trash2.GetComponent<Trash>().normalSpeed = 0f;
        Trash3.GetComponent<EggFallDown>()._speed = 0f;
    }
    private void Update()
    {
        if (Trash1 != null && Trash1.transform.position.y <= 1f)
        {
            canvas1.SetActive(false);
            canvas2.SetActive(true);
        }
        else
        {
            canvas2.SetActive(false);
        }
        if (Trash1 == null && canvas3 != null)
        {
            canvas3.SetActive(true);
            canvas2.SetActive(false);
        }
        if (Vector3.Distance(_player.position, _dustbin1.position) < 2f)
        {
            Submitted1stTrash();
        }
        if (Vector3.Distance(_player.position, _dustbin2.position) < 2f)
        {
            Submitted2ndTrash();
        }
        if (Trash3 == null && Vector3.Distance(_player.position, _dustbin2.position) < 2f)
        {
            PlayerPrefs.SetInt("HitCountKey", 1);
            tutorialEnd.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    public void HelloTrooper()
    {
        DeactivateAllObjects();
        canvas1.SetActive(true);
        if (Trash1 != null)
        {
            Trash1.GetComponent<Trash>().normalSpeed = 5f;
        }
    }
    public void Submitted1stTrash()
    {
        DeactivateAllObjects();
        if (Trash2 != null)
        {
            if (canvas3 != null)
            {
                canvas3.SetActive(false);
            }
            canvas3 = null;
            Trash2.GetComponent<Trash>().normalSpeed = 5f;
        }
    }
    public void Submitted2ndTrash()
    {
        if (Trash3 != null)
        {
            Trash3.GetComponent<EggFallDown>()._speed = 10f;
        }

    }
    public void DeactivateAllObjects()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(false);
        if (canvas3 != null)
        {
            canvas3.SetActive(false);
        }
        _hello.SetActive(false);
    }
    
}