using TMPro;
using UnityEngine;
public class Cannon : MonoBehaviour
{
    [SerializeField] int _fillAmount = 5;
    [SerializeField] int _currentFillAmount = 0;
    [SerializeField] float _shootForce;
    [SerializeField] GameObject[] _trashBall;
    [SerializeField] GameObject _spawnPos;
    [SerializeField] GameObject _target;
    [SerializeField] AudioClip _fire_sfx_first_half;
    [SerializeField] AudioClip _fire_sfx_second_half;
    TextMeshProUGUI _text;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void CollectTrash(int amt)
    {
        _currentFillAmount += amt;
        if (_currentFillAmount >= _fillAmount)
        {
            _anim.SetTrigger("Shoot");
            _currentFillAmount = 0;
        }
        if (_text)
            _text.text = _currentFillAmount.ToString() + "/" + _fillAmount;
    }
    public void ShootTrashAtAunty()
    {
        GameObject temp = Instantiate(_trashBall[Random.Range(0, _trashBall.Length)], _spawnPos.transform.position, Quaternion.identity);
        temp.AddComponent<Rigidbody>().useGravity = false;
        temp.AddComponent<SphereCollider>().isTrigger = true;
        Rigidbody rb = temp.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddTorque(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            rb.velocity = direction * _shootForce;
        }
        if (_text)
            _text.text = _currentFillAmount.ToString() + "/" + _fillAmount;
    }
    public void ShootSFX1()
    {
        AudioManager.Instance.TrashDispose(_fire_sfx_first_half);
    }
    public void ShootSFX2()
    {
        AudioManager.Instance.TrashDispose(_fire_sfx_second_half);
    }
}