using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IGun
{
    [SerializeField] private GunStats _stats;
    public GameObject BulletPrefab => _stats.BulletPrefab;

    public int MagSize => _stats.MagSize;
    public int Damage => _stats.Damage;

    public int BulletCount => _bulletCount;
    [SerializeField] protected int _bulletCount;

    private void Start(){
        Reload();
    }

    public virtual void Attack(){
        if(_bulletCount > 0)
        {
            var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.name = "Bullet";
            bullet.GetComponent<Bullet>().SetOwner(this);
            _bulletCount--;
        }
        
    }

    public void Reload() => _bulletCount = MagSize;

    public void EmptyMagazine()
    {
        _bulletCount = 0;
    }
}
