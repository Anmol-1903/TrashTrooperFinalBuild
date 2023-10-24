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
        if (other.CompareTag("BulletL"))
        {
            DealDamage(_damagePerShot, true);
        }
        else if (other.CompareTag("BulletR"))
        {
            DealDamage(_damagePerShot, false);
        }
        Destroy(other.gameObject);
    }
    public void DealDamage(float _damage, bool left)
    {
        _currentHealth -= 10;
        if (_currentHealth <= 0)
        {
            GameManager GM = FindObjectOfType<GameManager>();
            GM.BossKilled();
        }
        FindObjectOfType<BossAI>().AuntyDamage(left, _currentHealth <= 0);
    }
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }
}