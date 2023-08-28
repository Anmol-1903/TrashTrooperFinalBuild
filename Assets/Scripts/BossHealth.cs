using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _damagePerShot = 10f;
    [SerializeField] Slider _bossBar;
    float _currentHealth;
    float _apparentHealth;
    private void Start()
    {
        if (_bossBar)
        {
            _currentHealth = _maxHealth;
            _apparentHealth = _maxHealth;
            _bossBar.maxValue = _maxHealth;
        }
    }
    private void Update()
    {
        if (_bossBar)
        {
            _apparentHealth = Mathf.Lerp(_apparentHealth, _currentHealth, Time.deltaTime * 2);
            _bossBar.value = _apparentHealth;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            DealDamage(_damagePerShot);
        }
    }
    public void DealDamage(float _damage)
    {
        _currentHealth -= _damage;
        if(_currentHealth <= 0)
        {
            //Boss Ded
        }
    }
}