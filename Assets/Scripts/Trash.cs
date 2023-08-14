using UnityEngine;
public class Trash : MonoBehaviour
{
    [Tooltip("How much this trash will bring the cleanliness bar down")]
    public int _dirtiness;


    float _speed;
    [SerializeField] float slowSpeed = 1;
    public float normalSpeed = 5;

    [SerializeField] GameObject _player;
    [SerializeField] PlayerMove playerMove;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        playerMove = _player.GetComponent<PlayerMove>();
    }
    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddTorque(Random.Range(-100,100), Random.Range(-100, 100), Random.Range(-100, 100));
        }
    }
    private void Update()
    {
        if (playerMove.timeSlowerPower)
        {
            _speed = slowSpeed;
        }
        else
        {
            _speed = normalSpeed;
        }
        transform.position += new Vector3(0f, -_speed * Time.deltaTime, 0f);
    }
}