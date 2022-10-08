using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveable
{

    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    
    public void Travel(Vector3 direction) => transform.Translate(direction * Time.deltaTime * Speed);

    public void Rotate(Vector3 direction) => transform.Rotate(direction * Time.deltaTime * RotationSpeed);
}
