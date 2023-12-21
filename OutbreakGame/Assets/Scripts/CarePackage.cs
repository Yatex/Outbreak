using UnityEngine;

public class CarePackage : MonoBehaviour
{
    public int ammoRestoreAmount = 120;
    public float fallSpeed = 1000f;

    [SerializeField] private Gun _currentGun;

    void Start()
    {
        // Set the initial downward velocity to control the fall speed
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move the care package downward
        //transform.Translate(Vector3.down * descentSpeed * Time.deltaTime);
    }

    

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Care package collided with " + collision.gameObject.name);
        // Check if the care package collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Restore ammo and destroy the care package
            _currentGun.Ammo = ammoRestoreAmount;
            _currentGun.UI_AmmoUpdater();

            Destroy(gameObject);
        }
    }
}
