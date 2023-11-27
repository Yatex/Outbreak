using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZombieSpawnerManager : MonoBehaviour
{
    #region Public properties

    public int TotalZombiesThisWave;
    public bool AreSpawnerActives = true;
    public bool GoldZombieSpawned = false;
    public bool GoldZombieKilled = false;
    public GameObject[] Zombies = new GameObject[0];
    public float TimeBetweenSpawns = 1f;
    public GameObject[] SpawnerMarkers;

    public AudioSource waveEndAudioSource;
    public AudioClip waveEndAudioClip;

    public int Wave;
    static public ZombieSpawnerManager Instance;

    public int ZombiesKilled;
    #endregion

    #region Private properties
    private int AliveZombies = 0;
    private float TimeOfLastSpawn = 0f;
    private int SpawnedZombiesThisWave = 0;
    private ZombieFactory zombieFactory;
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
        ZombiesKilled = 0;
        UI_WaveUpdater(Wave);
    }

    public void OnZombieDeath(){
        AliveZombies -= 1;
        ZombiesKilled += 1;
        if ((AliveZombies == 0 && SpawnedZombiesThisWave == TotalZombiesThisWave) || GoldZombieKilled)
        {
            if (GoldZombieKilled) {
                AliveZombies = 0;
                SpawnedZombiesThisWave = TotalZombiesThisWave;
            }
            Wave += 1;
            TotalZombiesThisWave = (int)(5 * Wave);
            SpawnedZombiesThisWave = 0;
            GoldZombieSpawned = false;
            GoldZombieKilled = false;
            TimeOfLastSpawn = Time.time;
            TimeBetweenSpawns = TimeBetweenSpawns/Wave;
            PlayWaveEndAudio();
            UI_WaveUpdater(Wave);
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

    private void PlayWaveEndAudio()
    {
        if (waveEndAudioSource != null && waveEndAudioClip != null)
        {
            waveEndAudioSource.clip = waveEndAudioClip;
            waveEndAudioSource.Play();
        }
    }

    public void UI_WaveUpdater(int Wave_number)
    {
        EventsManager.instance.WaveChange(Wave_number);
    }
    private bool ShouldSpawnZombie() => Time.time - TimeOfLastSpawn > TimeBetweenSpawns && SpawnedZombiesThisWave < TotalZombiesThisWave;
}