using UnityEngine;
using UnityEngine.UI;
public class Tut_trashRespawner : MonoBehaviour
{
    TrashDeSpawner _trashDespawner;
    [SerializeField] float _damage;


    private void Awake()
    {
        _trashDespawner = FindObjectOfType<TrashDeSpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dry") || other.CompareTag("Wet"))
        {

            _trashDespawner.SetCleanliness(_damage);
            other.transform.position = new Vector3(Random.Range(-8.5f,9.3f) , Random.Range(16f,35f), 0);
        }
    }
}
