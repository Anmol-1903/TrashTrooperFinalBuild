using UnityEngine;
using UnityEngine.SceneManagement;
public class Trash : MonoBehaviour
{
    [Tooltip("How much this trash will bring the cleanliness bar down")]
    public int _dirtiness;

    float _speed;
    public float slowSpeed = 1;
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
        if(SceneManager.GetActiveScene().name.Equals("Tutorial") && transform.position.y <= 1)
        {
            return;
        }
        transform.position += new Vector3(0f, -_speed * Time.deltaTime, 0f);
    }
}