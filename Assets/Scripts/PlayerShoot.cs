using UnityEngine;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float _cooldown = 5f;
    [SerializeField] GameObject[] _prefabs;
    [SerializeField] GameObject _shootButton;

    float _countdown;
    bool _canShoot;
    
    BetterCatchSystem _cs;
    GameObject _selectedPrefab;
    private void Awake()
    {
        _cs = FindObjectOfType<BetterCatchSystem>();
    }
    private void Start()
    {
        _countdown = _cooldown;
    }
    private void Update()
    {
        if ((_cs._dry_capacity > 0 || _cs._wet_capacity > 0) && _canShoot)
        {
            _shootButton.SetActive(true);
        }
        else
        {
            _shootButton.SetActive(false);
        }
        if (_countdown < 0)
        {
            _canShoot = true;
        }
        else
        {
            _countdown -= Time.deltaTime;
        }
    }
    public void Shoot()
    {
        if (_canShoot)
        {
            _selectedPrefab = _prefabs[Random.Range(0, _prefabs.Length)];
            GameObject temp = Instantiate(_selectedPrefab, transform.position, Quaternion.identity);
            _countdown = _cooldown;
            _canShoot = false;
            if(temp != null)
            {
                Destroy(temp, 10f);
            }
        }
    }
}