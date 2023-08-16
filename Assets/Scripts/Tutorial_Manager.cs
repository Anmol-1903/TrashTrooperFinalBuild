using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Tutorial_Manager : MonoBehaviour
{
    [SerializeField] GameObject Trash1, Trash2, Trash3;
    [SerializeField] GameObject Trash1Pause, Trash2Pause;
    [SerializeField] GameObject _hello;
    [SerializeField] Transform _player, _dustbin1, _dustbin2;
    private void Start()
    {
        DeactivateAllObjects();
        _hello.SetActive(true);
        Trash1.GetComponent<Trash>().normalSpeed = 0f;
        Trash2.GetComponent<Trash>().normalSpeed = 0f;
        Trash3.GetComponent<Trash>().normalSpeed = 0f;

    }
    private void Update()
    {
        if (Vector3.Distance(_player.position, _dustbin1.position) < 2f)
        {
            Submitted1stTrash();
        }
        if (Vector3.Distance(_player.position, _dustbin2.position) < 2f)
        {
            Submitted2ndTrash();
        }
    }
    public void HelloTrooper()
    {
        DeactivateAllObjects();
        if (Trash1 != null)
        {
            Trash1.GetComponent<Trash>().normalSpeed = 5f;
        }
        Trash1Pause.SetActive(true);
    }
    public void Submitted1stTrash()
    {
        DeactivateAllObjects();
        if (Trash2 != null)
        {
            Trash2.GetComponent<Trash>().normalSpeed = 5f;
        }
        Trash2Pause.SetActive(true);
    }
    public void Submitted2ndTrash()
    {
        if (Trash3 != null)
        {
            Trash3.GetComponent<Trash>().normalSpeed = 5f;
        }

    }
    public void DeactivateAllObjects()
    {
        _hello.SetActive(false);
        Trash1Pause.SetActive(false);
        Trash2Pause.SetActive(false);
    }
}