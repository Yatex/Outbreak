using UnityEngine;

class ZombieSpawnerManager : MonoBehaviour
{
    #region Public properties
    public int TotalZombiesThisWave;
    public bool AreSpawnerActives = true;
    public GameObject[] Zombies = new GameObject[0];
    public float TimeBetweenSpawns = 100;
    public GameObject[] SpawnerMarkers;
    #endregion

    #region Private properties
    private int AliveZombies = 0;
    static private ZombieSpawnerManager Instance;
    private float TimeOfLastSpawn = 0f;
    private int SpawnedZombiesThisWave = 0;
    #endregion

    private void Awake()
    {
        if(Instance != null) Destroy(this);
        Instance = this;
    }

    public void OnZombieDeath(){
        AliveZombies -= 1;
    }

    void Update(){
        if (ShouldSpawnZombie())
        {
            var spawnLocation = SpawnerMarkers[Random.Range(0, SpawnerMarkers.Length - 1)];

            Instantiate(Zombies[0], spawnLocation.transform.position, spawnLocation.transform.rotation);
            AliveZombies += 1;
            SpawnedZombiesThisWave += 1;
            TimeOfLastSpawn = Time.time;
        }
    }

    private bool ShouldSpawnZombie() => Time.time - TimeOfLastSpawn > TimeBetweenSpawns && SpawnedZombiesThisWave < TotalZombiesThisWave;
}