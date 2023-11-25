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

    [SerializeField] public Camera cam;
    [SerializeField] public float shootingRange = 100f;
    [SerializeField] public float nextTimeToShoot = 0f;
    [SerializeField] public float fireCharge = 15f;

    public float giveDamageOf = 10f;
    [SerializeField] public float punchRange = 3f;

    [SerializeField] public float punchCharge = 150f;

    [SerializeField]  public float nextTimeToPunch = 0f;

    [SerializeField] public ParticleSystem muzzleSpark;
    [SerializeField] public GameObject woodedEffect;
    [SerializeField] public GameObject bloodEffect;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bulletHit;

    private void Start(){
        Reload();
    }

    public virtual void Punch()
    {

        if(Input.GetButton("v") && !Input.GetButton("Fire2"))
        {
            Invoke("PunchZombie", 1);
            
        }
        
    }

    public virtual void PunchZombie()
    {
        nextTimeToPunch = Time.time + 1.6f / punchCharge;
            RaycastHit hitInfo;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, punchRange))
                {
                ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
                if (hitInfo.transform.name == "StrongZombie(Clone)" || hitInfo.transform.name == "WeakZombie(Clone)" || hitInfo.transform.name == "Zombie1(Clone)")
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(Damage);
                    GameObject bloodGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(bloodGo, 1f);
                }
        }
    }

    public virtual void Attack(){
        if(_bulletCount > 0 && Input.GetButton("Fire1") && Input.GetButton("Fire2") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1.6f / fireCharge;
            Shoot();
            _bulletCount--;
            UI_AmmoUpdater();
        }
        
    }

    private void Shoot()
    {
        muzzleSpark.Play();
        RaycastHit hitInfo;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            if (hitInfo.transform.name == "StrongZombie(Clone)" || hitInfo.transform.name == "WeakZombie(Clone)" || hitInfo.transform.name == "Zombie1(Clone)")
            {
                IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                damageable?.TakeDamage(Damage);
                GameObject bloodGo = Instantiate(bloodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(bloodGo, 1f);

            }
            else
            {
                GameObject woodGo = Instantiate(woodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(woodGo, 1f);
            }


        }
    }

    public void Reload(){
        _bulletCount = MagSize;
        UI_AmmoUpdater();
    } 

    public void UI_AmmoUpdater()
    {
        EventsManager.instance.AmmoChange(_bulletCount, _stats.MagSize);
    }
    public void EmptyMagazine()
    {
        _bulletCount = 0;
    }
}
