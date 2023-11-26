using UnityEngine;

public class ZombieFactory
{

#region Private properties
    static private ZombieSpawnerManager Instance;
#endregion

    public void CreateZombie(GameObject[] SpawnerMarkers, GameObject[] Zombies)
    {
        var spawnLocation = SpawnerMarkers[Random.Range(0, SpawnerMarkers.Length)];
        
        GameObject newZombie = GameObject.Instantiate(Zombies[Random.Range(0,Zombies.Length - 1)], spawnLocation.transform.position, spawnLocation.transform.rotation);
    }

    public void CreateGoldZombie(GameObject[] SpawnerMarkers, GameObject[] Zombies)
    {
        var spawnLocation = SpawnerMarkers[Random.Range(0, SpawnerMarkers.Length)];
        GameObject.Instantiate(Zombies[2], spawnLocation.transform.position, spawnLocation.transform.rotation);
    }
}