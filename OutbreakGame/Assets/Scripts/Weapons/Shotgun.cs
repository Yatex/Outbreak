using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{
    [SerializeField] private int _bulletInShell = 7;

    public override void Attack(){
        for (int i = 0; i < _bulletInShell; i++){
            var bullet = Instantiate(
                BulletPrefab,
                transform.position + Random.insideUnitSphere * 1, 
                transform.rotation);
            bullet.name = "Shotgun Bullet";
            bullet.GetComponent<Bullet>().SetOwner(this);
            
        }
    }
   
}
