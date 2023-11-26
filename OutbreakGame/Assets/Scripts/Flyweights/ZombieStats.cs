using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieStats", menuName = "Stats/Zombie", order = 0)]
public class ZombieStats : ScriptableObject
{
    [SerializeField] private ZombieStatValues _statValues;

    public float HitRadius => _statValues.HitRadius;
    public float Damage => _statValues.Damage;
    public float TimeBetweenAttacks => _statValues.TimeBetweenAttacks;

}

[System.Serializable]
public struct ZombieStatValues
{
    public float HitRadius;
    public float Damage;
    public float TimeBetweenAttacks;
    
}