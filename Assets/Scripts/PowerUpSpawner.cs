using UnityEngine;
public class PowerUpSpawner : MonoBehaviour
{
    [Header("Inclusive")]
    [SerializeField] int min_Timer_For_PowerUps_To_Spawn;
    [Header("Exclusive")]
    [SerializeField] int max_Timer_For_PowerUps_To_Spawn;
    [Header("Initial Time")]
    [SerializeField] float counter;
    [SerializeField] GameObject[] PowerUps;
    GameObject[] spawnPositions;

    GameObject PowerUptoSpawn;
    private Transform currentSpawmnPoint;

    private void Awake()
    {
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnLocation");
    }

    private void Update()
    {
        if(PowerUps.Length != 0 && PowerUps != null)
        {
            PowerUpSpawningSystem();
        }
    }

    void PowerUpSpawningSystem()
    {
        if(counter > 0)
        {
                counter -= Time.deltaTime;
        }
        else
        {
            currentSpawmnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)].transform;
            PowerUptoSpawn = PowerUps[Random.Range(0, PowerUps.Length)];
            Instantiate(PowerUptoSpawn, currentSpawmnPoint.position, Quaternion.identity);
            counter = Random.Range(min_Timer_For_PowerUps_To_Spawn, max_Timer_For_PowerUps_To_Spawn);
        }

    }

}
