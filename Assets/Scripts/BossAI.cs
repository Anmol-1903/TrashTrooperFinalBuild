using UnityEngine;
public class BossAI : MonoBehaviour
{
    [SerializeField] Transform _spawnPoint;
    [SerializeField] GameObject[] _trashPrefabs;
    GameObject _selectedTrashPrefab;
    public void ThrowTrash()
    {
        _selectedTrashPrefab = _trashPrefabs[Random.Range(0, _trashPrefabs.Length)];
        if(Random.Range(0,2) == 0)
        {
            Instantiate(_selectedTrashPrefab, _spawnPoint.position, Quaternion.identity);
        }
    }
}