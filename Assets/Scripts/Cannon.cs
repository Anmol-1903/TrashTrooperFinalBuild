using UnityEngine;
public class Cannon : MonoBehaviour
{
    [SerializeField] int _fillAmount = 5;
    [SerializeField] int _currentFillAmount = 0;
    [SerializeField] float _shootForce;
    [SerializeField] GameObject[] _trashBall;
    [SerializeField] GameObject _spawnPos;
    [SerializeField] GameObject _target;
    public void CollectTrash(int amt)
    {
        _currentFillAmount += amt;
        if (_currentFillAmount >= _fillAmount)
        {
            ShootTrashAtAunty();
            _currentFillAmount = 0;
        }
    }
    void ShootTrashAtAunty()
    {
        GameObject temp = Instantiate(_trashBall[Random.Range(0, _trashBall.Length)], _spawnPos.transform.position, Quaternion.identity);
        Debug.Log("Instantiated");
        Rigidbody rb = temp.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("moved"); 
            Vector3 direction = (_target.transform.position - transform.position).normalized;
            rb.velocity = direction * _shootForce;
        }
    }
}