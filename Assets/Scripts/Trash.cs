using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Trash : MonoBehaviour
{
    [Tooltip("How much this trash will bring the cleanliness bar down")]
    public int _dirtiness;

    public bool IsCollected = false;

    float _speed;
    public float slowSpeed = 1f;
    public float normalSpeed = 5f;
    public float fastSpeed = 10f;
    public float attractionForce = 25f;
    public float attractionRange = 10f;

    float force;
    
    [SerializeField] GameObject _player;
    [SerializeField] Transform _target;
    [SerializeField] PlayerMove playerMove;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        playerMove = _player.GetComponent<PlayerMove>();
        _target = _player.transform.GetChild(1).transform;
    }
    private void Start()
    {
        if(gameObject.CompareTag("DryWaste") || gameObject.CompareTag("WetWaste"))
        {
            force = attractionForce;
        }
        else
        {
            force = 0;
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddTorque(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100));
        }
    }
    private void Update()
    {
        if (playerMove.timeSlowerPower)
        {
            _speed = slowSpeed;
        }
        else if (playerMove.timefaster)
        {
            _speed = fastSpeed;
        }
        else
        {
            _speed = normalSpeed;
        }
        if (SceneManager.GetActiveScene().name.Equals("Tutorial") && transform.position.y <= 1)
        {
            return;
        }


        if (playerMove.magnetPowerupActive)
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, _target.position);

            // Check if the trash is within the attraction range
            if (distanceToPlayer <= attractionRange)
            {
                // Calculate the direction to the player
                Vector3 directionToPlayer = (_target.position - transform.position).normalized;

                // Apply an attraction force towards the player
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(directionToPlayer * force, ForceMode.Acceleration);
            }
        }


        transform.position += new Vector3(0f, -_speed * Time.deltaTime, 0f);
    }
    private void OnDrawGizmos()
    {
        // Visualize the attraction range with a wire sphere in the editor
        if (gameObject.CompareTag("DryWaste") || gameObject.CompareTag("WetWaste"))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attractionRange);
        }
    }
}