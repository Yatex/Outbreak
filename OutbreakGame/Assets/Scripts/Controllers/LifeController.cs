using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class LifeController : MonoBehaviour, IDamageable
{
    public float MaxLife => GetComponent<Actor>().ActorStats.MaxLife;

    private float _currentLife;


    public void Start(){
        _currentLife = MaxLife;
    }

    public void TakeDamage(float damage) {
        Debug.Log("toy re duro");
        _currentLife -= damage;
        if(_currentLife <=0) Die();
    }

    public void Die() => Destroy(this.gameObject);

    private void OnDestroy()
    {
        EventsManager.instance.EventGameOver(false);
    }
}
