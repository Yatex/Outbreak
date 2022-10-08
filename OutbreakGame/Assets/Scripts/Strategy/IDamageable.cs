using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    float MaxLife {get;}
    void TakeDamage(float damage);
    void Die();
}
