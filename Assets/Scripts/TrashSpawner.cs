using UnityEngine;
public class TrashSpawner : MonoBehaviour
{
    GameObject[] _spawnPositions;
    Transform _selectedSpawnPosition;

    [SerializeField] Transform _container;

    [SerializeField] float[] _timeBetweenTrashSpawn;
    float _selectedTimeBetweenTrashSpawn;

    [SerializeField] GameObject[] _dryTrashPrefabs;
    [SerializeField] GameObject[] _wetTrashPrefabs;

    [Header("WetSpawnChance will be (100 - DrySpawnChance)")]
    [Range(0, 100)]
    [SerializeField] int _drySpawnChance = 50;

    GameObject _trashToSpawn;


    [SerializeField] GameObject[] _flowerPotPrefabs;
    [SerializeField] float _potGracePeriod;
    [SerializeField] float _potSpawnInterval;
    [SerializeField] int _potSpawnChance = 50;
    GameObject _potToSpawn;
    private void Awake()
    {
        _spawnPositions = GameObject.FindGameObjectsWithTag("SpawnLocation");

    }
    private void Start()
    {
        Invoke("SpawnTrash", 2f);
        if (_flowerPotPrefabs.Length != 0 && _flowerPotPrefabs != null)
            Invoke("SpawnFlowerPot", _potGracePeriod);
    }
    void SpawnTrash()
    {
        int rand = Random.Range(1, 101);
        if (rand <= _drySpawnChance)
        {
            _trashToSpawn = _dryTrashPrefabs[Random.Range(0, _dryTrashPrefabs.Length)];
        }
        else
        {
            _trashToSpawn = _wetTrashPrefabs[Random.Range(0, _wetTrashPrefabs.Length)];
        }
        _selectedSpawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Length)].transform;
        Instantiate(_trashToSpawn, _selectedSpawnPosition.position, Quaternion.identity, _container);
        _selectedTimeBetweenTrashSpawn = _timeBetweenTrashSpawn[Random.Range(0, _timeBetweenTrashSpawn.Length)];
        Invoke("SpawnTrash", _selectedTimeBetweenTrashSpawn);
    }
    void SpawnFlowerPot()
    {
        int rand = Random.Range(1, 101);
        if (rand <= _potSpawnChance)
        {
            _selectedSpawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Length)].transform;
            _potToSpawn = _flowerPotPrefabs[Random.Range(0, _flowerPotPrefabs.Length)];
            Instantiate(_potToSpawn, _selectedSpawnPosition.position, Quaternion.identity, _container);
        }
        Invoke("SpawnFlowerPot", _potSpawnInterval);
    }
}