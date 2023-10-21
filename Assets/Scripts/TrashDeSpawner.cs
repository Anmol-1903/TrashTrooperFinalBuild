using UnityEngine;
using UnityEngine.UI;
public class TrashDeSpawner : MonoBehaviour
{
    [SerializeField] AudioClip _failClip;
    [SerializeField] Slider _cleanlinessMeter;
    [SerializeField] int _maximumValue;
    [SerializeField] float _healPerAmount;
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
    public void HealNature(float amount)
    {
        _cleanliness += (amount * _healPerAmount);
        if (_cleanliness > _cleanlinessMeter.maxValue)
        {
            _cleanliness = _cleanlinessMeter.maxValue;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WetWaste") || other.CompareTag("DryWaste"))
        {
            if (other.GetComponent<Trash>())
            {
                _cleanliness -= other.GetComponent<Trash>()._dirtiness;
            }
            AudioManager.Instance.TrashFallDown(_failClip);
            Destroy(other.gameObject);
        }
    }

    public void SetCleanliness(float amount)
    {
        _cleanliness -= amount;
    }
}