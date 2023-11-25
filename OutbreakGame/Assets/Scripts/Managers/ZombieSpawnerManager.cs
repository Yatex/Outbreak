using UnityEngine;

class ZombieSpawnerManager : MonoBehaviour
{
    #region Public properties
    public int TotalZombiesThisWave;
    public bool AreSpawnerActives = true;
    public bool GoldZombieSpawned = false;
    public bool GoldZombieKilled = false;
    public GameObject[] Zombies = new GameObject[0];
    public float TimeBetweenSpawns = 1f;
    public GameObject[] SpawnerMarkers;
    #endregion

    #region Private properties
    private int AliveZombies = 0;
    static private ZombieSpawnerManager Instance;
    private float TimeOfLastSpawn = 0f;
    private int SpawnedZombiesThisWave = 0;
    private ZombieFactory zombieFactory;
    private int Wave;
    #endregion

    private void Awake()
    {
        if(Instance != null) Destroy(this);
        Instance = this;
    }

    void Start()
    {
        zombieFactory = new ZombieFactory();
        Wave = 1;
        TotalZombiesThisWave = 5;
        Zombie.OnZombieDeath += OnZombieDeath;
    }

    public void OnZombieDeath(){
        AliveZombies -= 1;
        if (AliveZombies == 0 || GoldZombieKilled)
        {
            if (GoldZombieKilled) {
                SpawnedZombiesThisWave = AliveZombies;
            }
            Wave += 1;
            TotalZombiesThisWave = (int)(5 * Wave);
            SpawnedZombiesThisWave = 0;
            GoldZombieSpawned = false;
            GoldZombieKilled = false;
            TimeOfLastSpawn = Time.time;
        }
    }

    void Update(){
        if (ShouldSpawnZombie())
        {
            if (Random.Range(0, 10) < 2 && !GoldZombieSpawned && SpawnedZombiesThisWave > 5)
            {
                zombieFactory.CreateGoldZombie(SpawnerMarkers, Zombies);
                GoldZombieSpawned = true;
                AliveZombies += 1;
                SpawnedZombiesThisWave += 1;
                TimeOfLastSpawn = Time.time;
                return;
            }
            zombieFactory.CreateZombie(SpawnerMarkers, Zombies);
            AliveZombies += 1;
            SpawnedZombiesThisWave += 1;
            TimeOfLastSpawn = Time.time;
        }
    }

    private bool ShouldSpawnZombie() => Time.time - TimeOfLastSpawn > TimeBetweenSpawns && SpawnedZombiesThisWave < TotalZombiesThisWave;
}