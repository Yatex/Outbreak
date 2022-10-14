using UnityEngine;

[RequireComponent(typeof(Zombie))]
public class ZombieController : MonoBehaviour, IMoveable, IDamager
{
    #region Public props
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    public float Damage => GetComponent<Zombie>().ZombieStats.Damage;

    #endregion
    public void Travel(Vector3 target) => MoveAtSpeed(target, Speed);
    public void Sprint(Vector3 target) => MoveAtSpeed(target, Speed * 1.5f);

    public void Rotate(Vector3 target) => transform.LookAt(target);

    public void DealDamage(IDamageable damageable) => damageable.TakeDamage(Damage);

    #region PrivateMethods
    private void MoveAtSpeed(Vector3 target, float Speed) => transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            Time.deltaTime * Speed
        );
    #endregion

}
