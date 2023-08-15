using UnityEngine;
public class Inventory : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] Vector3 _offset;
    [SerializeField] bool _takeCopyFromScene;
    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Start()
    {
        if (_takeCopyFromScene)
        {
            _offset = transform.position - _target.position;
        }
    }
    private void LateUpdate()
    {
        transform.position = _target.transform.position + _offset;
    }
}