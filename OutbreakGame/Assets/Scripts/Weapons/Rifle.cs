using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Stats")]
    [SerializeField]  public Camera cam;
    [SerializeField]  public float giveDamageOf = 30f;
    [SerializeField]  public float shootingRange = 100f;
    [SerializeField]  public float nextTimeToShoot = 0f;
    [SerializeField] public float fireCharge = 15f;

    [SerializeField] public ParticleSystem muzzleSpark;
    [SerializeField] public GameObject woodedEffect;
    [SerializeField] public GameObject bloodEffect;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Input.GetButton("Fire2") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1.6f/fireCharge;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleSpark.Play();
        RaycastHit hitInfo;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
           
            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();

         

            if (hitInfo.transform.name == "StrongZombie(Clone)" || hitInfo.transform.name == "WeakZombie(Clone)")
            {
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
}
