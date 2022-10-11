using UnityEngine;

[RequireComponent(typeof(Actor))]
public class ZombieController : MonoBehaviour, IMoveable
{
    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    
    public void Travel(Vector3 target) => transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            Time.deltaTime * Speed
        );

    public void Rotate(Vector3 target) => transform.LookAt(target);
}
