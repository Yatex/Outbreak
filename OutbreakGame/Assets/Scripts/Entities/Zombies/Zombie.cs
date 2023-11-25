using UnityEngine;

public abstract class Zombie : Actor
{
    public delegate void ZombieDeathHandler();
    public static event ZombieDeathHandler OnZombieDeath;
    public ZombieStats ZombieStats => _zombieStats;
    [SerializeField] private ZombieStats _zombieStats;
    private LifeController lifeController;
    public ZombieController zombieController;
    private float timeSinceAttack = 0;
    private CapsuleCollider capsuleCollider;

    private bool IsDead = false;
    private Animator animator => GetComponent<Animator>();
    private Character target;

    public string targetTag = "Zombie";

    private Rigidbody rb;


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
                    animator.SetBool("died", false);
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
                        animator.SetBool("died", false);
                        animator.SetBool("isWalking", false);
                        animator.SetBool("isPunching", true);
                        Invoke("ZombieDealDamage", 1.4f);
                        timeSinceAttack = Time.time;
                    }
                }
            }
        }
        else
        {
            if (!IsDead){
                rb = gameObject.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                capsuleCollider = GetComponent<CapsuleCollider>();
                capsuleCollider.enabled = false;
                IsDead = true;
                animator.SetBool("isWalking", false);
                animator.SetBool("isPunching", false);
                animator.SetBool("died", true);
                string objectName = gameObject.name;
                if (objectName == "Zombie1(Clone)")
                {
                    GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag(targetTag);
                    foreach (GameObject obj in objectsToDestroy)
                    {
                        if(obj != null){
                            obj.GetComponent<Zombie>().lifeController.TakeDamage(1000f);
                        }
                    }
                }
                OnZombieDeath?.Invoke();
            }
        }
    }

    private void ZombieDealDamage()
    {
        EventQueueManager.instance.AddCommand(new CmdDamage(zombieController, target.GetComponent<IDamageable>()));
    }
}
