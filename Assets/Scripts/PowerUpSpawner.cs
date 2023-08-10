using UnityEngine;
public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] float[] timer_For_PowerUps_To_Spawn;
    [SerializeField] float counter;
    [SerializeField] GameObject[] PowerUps;
    [SerializeField] GameObject[] spawnPositions;

    GameObject PowerUptoSpawn;
    private Transform currentSpawmnPoint;

    private void Awake()
    {
        spawnPositions = GameObject.FindGameObjectsWithTag("SpawnLocation");
    }

    private void Update()
    {
        PowerUpSpawningSystem();
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
            counter = timer_For_PowerUps_To_Spawn[Random.Range(0, timer_For_PowerUps_To_Spawn.Length)];
        }

    }

}
