using UnityEngine;
public class Inventory : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _offset;
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        transform.position = _target.transform.position + _offset;
    }
}