using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]

public class Bullet : MonoBehaviour, IBullet
{
    public float LifeTime => _lifeTime;
    [SerializeField] private float _lifeTime = 5;

    public float Speed => _speed;
    [SerializeField] private float _speed = 0.1f;
    
    public Gun Gun => _gun;
    [SerializeField] private Gun _gun;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bulletHit;

    public void Travel() => transform.Translate(-Vector3.forward);

    public void SetOwner(Gun gun) => _owner = gun;
    [SerializeField] private Gun _owner;

    public Rigidbody Rigidbody => _rigigbody;
    [SerializeField] private Rigidbody _rigigbody;

    public Collider Collider => _collider;
    [SerializeField] private Collider _collider;

    [SerializeField] private List<int> _layerTarget;

    public void OnTriggerEnter(Collider collider){
        if(_layerTarget.Contains(collider.gameObject.layer)){
            IDamageable damageable = collider.GetComponent<IDamageable>();
            damageable?.TakeDamage(_gun.Damage);
            audioSource.clip = bulletHit;
            audioSource.Play();
            Destroy(this.gameObject);
        }  
    }

    private void Start(){
        _rigigbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _collider.isTrigger = true;
        _rigigbody.useGravity = false;
        _rigigbody.isKinematic = true;
        _rigigbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
    }

    private void Update(){
        _lifeTime -= Time.deltaTime;
        if(_lifeTime <= 0) Destroy(this.gameObject);
        Travel();
    }
}