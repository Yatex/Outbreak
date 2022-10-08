using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    Gun Gun {get;}
    float LifeTime { get;}
    float Speed { get;}
    Rigidbody Rigidbody {get;}
    void Travel();
    void OnTriggerEnter(Collider collider);
    void SetOwner(Gun gun);
}