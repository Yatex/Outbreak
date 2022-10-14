using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class LifeController : MonoBehaviour, IDamageable
{
    public float MaxLife => GetComponent<Actor>().ActorStats.MaxLife;

    private float _currentLife;

    public float CurrentLife;


    public void Start(){
        _currentLife = MaxLife;
        if(name == "Soldier"){
            EventsManager.instance.CharacterLifeChange(_currentLife, MaxLife);
        }
    }

    public void TakeDamage(float damage) {
        _currentLife -= damage;
        if(name == "Soldier"){
            EventsManager.instance.CharacterLifeChange(_currentLife, MaxLife);
        }
        if(_currentLife <=0) Die();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (GetComponent<Actor>() is Character)
        {
            EventsManager.instance.EventGameOver(false);
        }
    }
}
