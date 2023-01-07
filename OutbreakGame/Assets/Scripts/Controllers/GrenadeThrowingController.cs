using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowing : MonoBehaviour
{
    [SerializeField] private GameObject _grenadePrefab;
    [SerializeField] private float _throwForce = 40f;
    [SerializeField] private KeyCode _throwKey = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_throwKey)) ThrowGrenade();
    }

    private void ThrowGrenade(){
        GameObject grenade = Instantiate(_grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * _throwForce);

    }
}
