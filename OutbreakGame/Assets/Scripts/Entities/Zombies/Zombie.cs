using UnityEngine;

public abstract class Zombie : Actor
{
    // TODO: change this
    public float hitRadius = 1f;
    public float damage = 1f;
    private LifeController lifeController;
    private ZombieController movementController;


    void Start(){
        lifeController = GetComponent<LifeController>();
        movementController = GetComponent<ZombieController>();
    }

    void Update(){
        var characters = Object.FindObjectsOfType<Character>();
        double minDistance = Mathf.Infinity;
        Character target = null;
        foreach (var c in characters)
        {
            var posibleTarget = c.transform.position;
            if (minDistance > Vector3.Distance(transform.position,posibleTarget))
            {
                target = c;
            }
        }
        if (target != null)
        {
            var targetPosition = target.transform.position;
            if (Vector3.Distance(transform.position,  targetPosition) > hitRadius)
            {
                EventQueueManager.instance.AddCommand(new CmdMovement(
                    movementController, 
                    targetPosition,
                    targetPosition
                ));
            } else {
                EventQueueManager.instance.AddCommand(new CmdDamage(target.GetComponent<IDamageable>(), damage));
            }
        }
    }
}
