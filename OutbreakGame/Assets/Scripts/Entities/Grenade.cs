using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private float _delay = 5f;
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _force = 1500f;
    [SerializeField] private float _damage = 5000f;

    
    private float _countdown;
    private bool _hasExploded;
    private bool _hasSounded;

    void Start()
    {
        _countdown = _delay;

    }

    void Update()
    {

        _countdown -= Time.deltaTime;
        if(_countdown <= 2.5 && !_hasSounded){
            AudioSource aus = GetComponent<AudioSource>();
            aus.Play();
            _hasSounded = true;

        }
        if (_countdown <= 2 && !_hasExploded)
        {

            _hasExploded = true;
            Explode();
        }
        if(_countdown <= 0) Destroy(gameObject);
    }

    private void Explode(){
        Debug.Log("Explode");

        Instantiate(_explosionPrefab, transform.position, transform.rotation);

        //audioSource.Play();

        Collider[] _colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in _colliders) {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if(rb != null) {
                rb.AddExplosionForce(_force, transform.position, _radius);
            }

            LifeController lc = GetComponent<LifeController>();
            if(lc != null){
                lc.TakeDamage(_damage);
            }
        }

        GetComponent<Renderer>().enabled = false;
        //Destroy(gameObject);

    }
}
