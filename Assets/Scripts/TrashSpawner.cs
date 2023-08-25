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
    [Range(0,100)]
    [SerializeField] int _drySpawnChance = 50;

    GameObject _trashToSpawn;
    private void Awake()
    {
        _spawnPositions = GameObject.FindGameObjectsWithTag("SpawnLocation");
        
    }
    private void Start()
    {
        Invoke("SpawnTrash", 5f);
    }
    void SpawnTrash()
    {
        _selectedSpawnPosition = _spawnPositions[Random.Range(0, _spawnPositions.Length)].transform;
        int rand = Random.Range(1, 101);
        if(rand <= _drySpawnChance)
        {
            _trashToSpawn = _dryTrashPrefabs[Random.Range(0, _dryTrashPrefabs.Length)];
        }
        else
        {
            _trashToSpawn = _wetTrashPrefabs[Random.Range(0, _wetTrashPrefabs.Length)];
        }
        Instantiate(_trashToSpawn, _selectedSpawnPosition.position, Quaternion.identity, _container);
        _selectedTimeBetweenTrashSpawn = _timeBetweenTrashSpawn[Random.Range(0, _timeBetweenTrashSpawn.Length)];
        Invoke("SpawnTrash", _selectedTimeBetweenTrashSpawn);
    }
}