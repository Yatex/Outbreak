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
        if (rb != null)
        {
            rb.drag = fallSpeed;
            rb.isKinematic = false; // Make the care package static
            rb.useGravity = true;
        }
    }

    void Update()
    {
        // Move the care package downward
        //transform.Translate(Vector3.down * descentSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Care package collided with " + other.name);
        // Check if the care package collides with the player
        if (other.CompareTag("Player"))
        {
            // Restore ammo and destroy the care package
            _currentGun.Ammo = ammoRestoreAmount;
            _currentGun.UI_AmmoUpdater();

            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            // Stop further movement or take any other necessary action
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Stop the care package from moving
                rb.isKinematic = true; // Make the care package static
                rb.useGravity = false;
                transform.position += new Vector3(0f, 0.15f, 0f);
                
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Care package collided with " + collision.gameObject.name);
        // Check if the care package collides with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Stop further movement or take any other necessary action
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Stop the care package from moving
                rb.isKinematic = true; // Make the care package static
            }
        }
    }
}
