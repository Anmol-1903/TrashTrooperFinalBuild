using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _damagePerShot = 10f;
    [SerializeField] Slider _bossBar;
    [SerializeField] float _currentHealth;
    float _apparentHealth;
    private void Start()
    {
        _currentHealth = _maxHealth;
        _apparentHealth = _maxHealth;
        if (_bossBar)
        {
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
        Debug.Log("ouch");
        if(_currentHealth <= 0)
        {
            Debug.Log("dead");
            //Boss Ded
        }
    }
}