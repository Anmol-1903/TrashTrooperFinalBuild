using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float _maxHealth = 3;
    float _health = 3;

    [SerializeField] Slider _healthbar;
    private void Start()
    {
        _health = _maxHealth;
        _healthbar.maxValue = _maxHealth;
        _healthbar.value = _maxHealth;
    }

    public void TakeDamage(float _amount)
    {
        _health -= _amount;
        _healthbar.value = _health;
        if(_health <= 0)
        {
            //Player Die
        }
    }
}