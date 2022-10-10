using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGun 
{
    GameObject BulletPrefab { get; }
    int MagSize { get; }
    int BulletCount { get; }

    void Attack();
    void Reload();
    
}
