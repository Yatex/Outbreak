using UnityEngine;

[RequireComponent(typeof(Actor))]
public class ZombieController : MonoBehaviour, IMoveable
{
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    
    public void Travel(Vector3 target) => MoveAtSpeed(target, Speed);
    public void Sprint(Vector3 target) => MoveAtSpeed(target, Speed * 1.5f);

    public void Rotate(Vector3 target) => transform.LookAt(target);

    #region PrivateMethods
    private void MoveAtSpeed(Vector3 target, float Speed) => transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            Time.deltaTime * Speed
        );
    #endregion

}
