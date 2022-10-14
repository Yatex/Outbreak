using UnityEngine;

[RequireComponent(typeof(Actor))]
public class MovementController : MonoBehaviour, IMoveable
{

    [SerializeField] public Transform targetCam;

    public float Speed => GetComponent<Actor>().ActorStats.MovementSpeed;
    public float RotationSpeed => GetComponent<Actor>().ActorStats.RotationSpeed;
    
    public void Travel(Vector3 direction) => transform.Translate(direction * Time.deltaTime * Speed);

    public void Sprint(Vector3 direction) => transform.Translate(direction * Time.deltaTime * Speed * 2);

    public void Rotate(Vector3 direction) => transform.rotation = Quaternion.Euler(0, targetCam.rotation.eulerAngles.y, 0);
    //transform.Rotate(direction * Time.deltaTime * RotationSpeed);
}
