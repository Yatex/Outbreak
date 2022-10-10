using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "gunStats", menuName = "Stats/Gun", order = 0)]
public class GunStats : ScriptableObject
{
    [SerializeField] private GunStatValues _gunStatsValues;

    public GameObject BulletPrefab => _gunStatsValues.BulletPrefab;
    public int Damage => _gunStatsValues.Damage;
    public int MagSize => _gunStatsValues.MagSize;
    public float ShotCooldown => _gunStatsValues.ShotCooldown;
}

[System.Serializable]
public struct GunStatValues
{
    public GameObject BulletPrefab;
    public int Damage;
    public int MagSize;
    public float ShotCooldown;


}