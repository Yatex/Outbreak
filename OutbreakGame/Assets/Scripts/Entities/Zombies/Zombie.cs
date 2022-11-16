using UnityEngine;

public abstract class Zombie : Actor
{
    public ZombieStats ZombieStats => _zombieStats;
    [SerializeField] private ZombieStats _zombieStats;
    private LifeController lifeController;
    private ZombieController zombieController;
    private float timeSinceAttack = 0;

    private bool IsDead = false;
    private Animator animator => GetComponent<Animator>();
    private Character target;


    void Start(){
        lifeController = GetComponent<LifeController>();
        zombieController = GetComponent<ZombieController>();
    }

    void Update(){
        if (lifeController.CurrentLife > 0.0)
        {
            var characters = Object.FindObjectsOfType<Character>();
            double minDistance = Mathf.Infinity;
            target = null;
            foreach (var c in characters)
            {
                var posibleTarget = c.transform.position;
                if (minDistance > Vector3.Distance(transform.position, posibleTarget))
                {
                    target = c;
                }
            }
            if (target != null)
            {
                var targetPosition = target.transform.position;
                if (Vector3.Distance(transform.position, targetPosition) > ZombieStats.HitRadius)
                {
                    animator.SetBool("isPunching", false);
                    animator.SetBool("isWalking", true);
                    EventQueueManager.instance.AddCommand(new CmdMovement(
                        zombieController,
                        targetPosition,
                        targetPosition
                    ));
                }
                else
                {
                    if (timeSinceAttack + ZombieStats.TimeBetweenAttacks + 1 < Time.time)
                    {
                        animator.SetBool("isWalking", false);
                        animator.SetBool("isPunching", true);
                        Invoke("ZombieDealDamage", 2);
                        timeSinceAttack = Time.time;
                    }
                }
            }
        }
        else
        {
            if(!IsDead){
                GlobalData.instance.ZombiesKilled++;
                IsDead = true;
            }

            animator.SetBool("isWalking", false);
            animator.SetBool("isPunching", false);
            animator.SetBool("died", true);
        }
    }

    private void ZombieDealDamage()
    {
        EventQueueManager.instance.AddCommand(new CmdDamage(zombieController, target.GetComponent<IDamageable>()));
    }
}
