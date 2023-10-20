using UnityEngine;
public class Cannon : MonoBehaviour
{
    [SerializeField] int _fillAmount = 5;
    [SerializeField] int _currentFillAmount = 0;
    [SerializeField] float _shootForce;
    [SerializeField] GameObject[] _trashBall;
    [SerializeField] GameObject _spawnPos;
    [SerializeField] GameObject _target;
    [SerializeField] AudioSource _fire_sfx;

    Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void CollectTrash(int amt)
    {
        _currentFillAmount += amt;
        if (_currentFillAmount >= _fillAmount)
        {
            _anim.SetTrigger("Shoot");
            _fire_sfx.Play();
            _currentFillAmount = 0;
        }
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
            rb.velocity = direction * _shootForce * Time.deltaTime;
        }
    }
}