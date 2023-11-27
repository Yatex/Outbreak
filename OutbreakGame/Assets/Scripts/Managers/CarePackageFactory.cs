using UnityEngine;

public class CarePackageFactory
{

#region Private properties
    static private CarePackageSpawnerManager Instance;
#endregion

    public void CreateCarePackage(GameObject[] SpawnerMarkers, GameObject[] CarePackages)
    {
        var spawnLocation = SpawnerMarkers[Random.Range(0, SpawnerMarkers.Length)];
        
        GameObject newCarePackage = GameObject.Instantiate(CarePackages[Random.Range(0,CarePackages.Length)], spawnLocation.transform.position, spawnLocation.transform.rotation);
    }
}