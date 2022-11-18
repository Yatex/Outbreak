using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHit : MonoBehaviour
{
    // Start is called before the first frame update

    public float ObjectHealth = 30f;

    public void ObjectHitDamage(float amount)
    {
        ObjectHealth -= amount;
        if(ObjectHealth <= 0f)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    
}
