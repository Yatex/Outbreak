using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarePackageSpawnerManager : MonoBehaviour
{
    #region Public properties
    public GameObject[] CarePackages = new GameObject[0];
    public GameObject[] SpawnerMarkers = new GameObject[0];
    static public CarePackageSpawnerManager Instance;
    #endregion

    #region Private properties
    private int WaveCarePackageSpawned = 1;
    private int Wave;
    private CarePackageFactory carePackageFactory;
    #endregion

    private void Awake()
    {
        Debug.Log("CarePackageSpawnerManager Awake");
        if(Instance != null) Destroy(this);
        Instance = this;
    }

    void Start()
    {
        Debug.Log("CarePackageSpawnerManager Start");
        Wave = ZombieSpawnerManager.Instance.Wave;
        carePackageFactory = new CarePackageFactory(); //TODO: Fix this
    }

    void Update(){
        if (ShouldSpawnCarePackage())
        {
            Debug.Log("CarePackageSpawnerManager Update");
            carePackageFactory.CreateCarePackage(SpawnerMarkers, CarePackages);
            WaveCarePackageSpawned += 1;
        }
        Wave = ZombieSpawnerManager.Instance.Wave;
    }

    private bool ShouldSpawnCarePackage()
    {
        if(WaveCarePackageSpawned == Wave)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}