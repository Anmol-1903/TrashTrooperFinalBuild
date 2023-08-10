using UnityEngine;
using UnityEngine.UI;
public class TrashDeSpawner : MonoBehaviour
{
    [SerializeField] Slider _cleanlinessMeter;
    [SerializeField] int _maximumValue;
    float _apparentValue;
    private void Start()
    {
        _cleanlinessMeter.maxValue = _maximumValue;
        _apparentValue = _maximumValue;
        _cleanlinessMeter.value = _apparentValue;
    }
    private void Update()
    {
        _cleanlinessMeter.value = Mathf.Lerp(_cleanlinessMeter.value, _apparentValue, Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WetWaste") || other.CompareTag("DryWaste"))
        {
            _apparentValue -= other.GetComponent<Trash>()._dirtiness;
            Destroy(other.gameObject);
        }
    }
}