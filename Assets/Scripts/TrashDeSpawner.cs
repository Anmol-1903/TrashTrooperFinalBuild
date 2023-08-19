using UnityEngine;
using UnityEngine.UI;
public class TrashDeSpawner : MonoBehaviour
{
    [SerializeField] AudioClip _failClip;
    [SerializeField] Slider _cleanlinessMeter;
    [SerializeField] int _maximumValue;
    public float _cleanliness;
    private void Start()
    {
        _cleanlinessMeter.maxValue = _maximumValue;
        _cleanliness = _maximumValue;
        _cleanlinessMeter.value = _cleanliness;
    }
    private void Update()
    {
        _cleanlinessMeter.value = Mathf.Lerp(_cleanlinessMeter.value, _cleanliness, Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WetWaste") || other.CompareTag("DryWaste"))
        {
            _cleanliness -= other.GetComponent<Trash>()._dirtiness;
            AudioManager.Instance.TrashFallDown(_failClip);
            Destroy(other.gameObject);
        }
    }
}