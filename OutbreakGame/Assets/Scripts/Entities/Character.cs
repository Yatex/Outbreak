using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Character : Actor
{
    private MovementController _movementController;
    private GameObject camThirdPerson;
    private GameObject camAim;
    private LifeController lifeController;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rigidBody;
    public float nextTimeToShoot = 0f;
    public float nextTimeToPunch = 0f;
    public float fireCharge = 15f;

    public float punchCharge = 150f;

    public float punchRange = 3f;

    private bool isReloading = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip rifle_shot;
    [SerializeField] private AudioClip empty_gunshot;
    [SerializeField] private AudioClip reload;


    [SerializeField] private List<Gun> _guns;
    [SerializeField] private Gun _currentGun;
    [SerializeField] private Animator animator;
    [SerializeField] private Light lantern;

    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBackward= KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;
    [SerializeField] private KeyCode _jump = KeyCode.Space;
    [SerializeField] private KeyCode _lantern = KeyCode.F;

    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _punch = KeyCode.V;
    [SerializeField] private KeyCode _aim = KeyCode.Mouse1;
    [SerializeField] private KeyCode _reload = KeyCode.R;

    [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode _setVictory = KeyCode.Return;
    [SerializeField] private KeyCode _setDefeat = KeyCode.Backspace;


    /*  COMANDOS    */
    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBack;
    private CmdMovement _cmdMoveLeft;
    private CmdMovement _cmdMoveRight;
    private CmdSprint _cmdSprintForward;
    private CmdRotation _cmdRotateLeft;
    private CmdRotation _cmdRotateRight;
    private CmdAttack _cmdAttack;
    private CmdPunch _cmdPunch;
    private CmdLantern _cmdLantern;

    // Start is called before the first frame update
    void Start()
    {
        _movementController = GetComponent<MovementController>();
        ChangeWeapon(0);

        _cmdMoveForward = new CmdMovement(_movementController, Vector3.forward);
        _cmdMoveBack = new CmdMovement(_movementController, Vector3.back);
        _cmdMoveLeft = new CmdMovement(_movementController, Vector3.left);
        _cmdMoveRight = new CmdMovement(_movementController, Vector3.right);
        _cmdSprintForward = new CmdSprint(_movementController, Vector3.forward);
        _cmdRotateLeft = new CmdRotation(_movementController, Vector3.left);
        _cmdRotateRight = new CmdRotation(_movementController, Vector3.right);
        _cmdAttack = new CmdAttack(_currentGun);
        _cmdLantern = new CmdLantern(lantern);
        _cmdPunch = new CmdPunch(_currentGun);

        camThirdPerson = GameObject.Find("ThirdPersonCineMachine");
        camThirdPerson.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        camAim = GameObject.Find("AimCineMachine");
        camAim.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        camThirdPerson.SetActive(true);
        lifeController = GetComponent<LifeController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isReloading)
        {
            if (lifeController.CurrentLife > 0.0)
            {
                Cursor.lockState = CursorLockMode.Locked; //or Cursor.lockState = CursorLockMode.None;

                //TODO: Luego habria que verificar si muere en el lifeController
                if(Input.GetKey(KeyCode.Y)) GetComponent<IDamageable>().TakeDamage(20); //EventsManager.instance.EventGameOver(false);
                /*  *   *   *   *   *   *   *   *   *   */
                if(Input.GetKey(_moveForward))
                {
                    EventQueueManager.instance.AddCommand(_cmdMoveForward);
                    if (Input.GetButton("Sprint")) 
                    {
                        EventQueueManager.instance.AddCommand(_cmdSprintForward);
                        if (Input.GetKeyDown(_jump))
                        {
                            Jump();
                            //camThirdPerson.SetActive(true);
                        }
                        else
                        {
                            Sprint();
                            //camThirdPerson.SetActive(true);
                        }
                    }
                    else if (Input.GetKey(_aim))
                    {
                        AimWalk();
                        //camThirdPerson.SetActive(false);
                        if (Input.GetKey(_attack) && Time.time >= nextTimeToShoot)
                        {
                            ShootAudio();
                            nextTimeToShoot = Time.time + 1.6f / fireCharge;
                            EventQueueManager.instance.AddCommand(_cmdAttack);
                        }
                    }
                    else if(Input.GetKey(_punch))
                        {
                            Punch();
                            nextTimeToPunch = Time.time + 1.6f / punchCharge;
                            EventQueueManager.instance.AddCommand(_cmdPunch);
                        }
                    else if (Input.GetKeyDown(_jump))
                    {
                        Jump();
                        //camThirdPerson.SetActive(true);
                    }
                    else
                    {
                        Walk();
                        //camThirdPerson.SetActive(true);
                    }

                }
                
                if(Input.GetKey(_moveBackward))
                {
                    EventQueueManager.instance.AddCommand(_cmdMoveBack);
                    if (Input.GetKey(_aim))
                    {
                        AimWalk();
                        if (Input.GetKey(_attack) && Time.time >= nextTimeToShoot)
                        {
                            ShootAudio();
                            nextTimeToShoot = Time.time + 1.6f / fireCharge;
                            EventQueueManager.instance.AddCommand(_cmdAttack);
                        }
                        //camThirdPerson.SetActive(false);
                    }
                    else if(Input.GetKey(_punch))
                        {
                            Punch();
                            nextTimeToPunch = Time.time + 1.6f / punchCharge;
                            EventQueueManager.instance.AddCommand(_cmdPunch);
                        }
                    else
                    {
                        Walk();
                        //camThirdPerson.SetActive(true);
                    }
                }
                EventQueueManager.instance.AddCommand(_cmdRotateLeft);
                EventQueueManager.instance.AddCommand(_cmdRotateRight);
                if (Input.GetKey(_moveLeft))
                {
                    EventQueueManager.instance.AddCommand(_cmdMoveLeft);
                    EventQueueManager.instance.AddCommand(_cmdRotateLeft);
                    if (Input.GetButton("Sprint") && Input.GetKey(_moveForward))
                    {
                        if (Input.GetKeyDown(_jump))
                        {
                            Jump();
                            //camThirdPerson.SetActive(true);
                        }
                        else
                        {
                            Sprint();
                            //camThirdPerson.SetActive(true);
                        }
                    }
                    else if (Input.GetKey(_aim))
                    {
                        AimWalk();
                        if (Input.GetKey(_attack) && Time.time >= nextTimeToShoot)
                        {
                            ShootAudio();
                            nextTimeToShoot = Time.time + 1.6f / fireCharge;
                            EventQueueManager.instance.AddCommand(_cmdAttack);
                        }
                        //camThirdPerson.SetActive(false);
                    }
                    else if(Input.GetKey(_punch))
                        {
                            Punch();
                            nextTimeToPunch = Time.time + 1.6f / punchCharge;
                            EventQueueManager.instance.AddCommand(_cmdPunch);
                        }
                    else
                    {
                        Walk();
                        //camThirdPerson.SetActive(true);
                    }
                }

            
                if (Input.GetKey(_moveRight))
                {
                    EventQueueManager.instance.AddCommand(_cmdMoveRight);
                    EventQueueManager.instance.AddCommand(_cmdRotateRight);
                    if (Input.GetButton("Sprint") && Input.GetKey(_moveForward))
                    {
                        if (Input.GetKeyDown(_jump))
                        {
                            Jump();
                            //camThirdPerson.SetActive(true);
                        }
                        else
                        {
                            Sprint();
                            //camThirdPerson.SetActive(true);
                        }
                    }
                    else if (Input.GetKey(_aim))
                    {
                        AimWalk();
                        if (Input.GetKey(_attack) && Time.time >= nextTimeToShoot)
                        {
                            ShootAudio();
                            nextTimeToShoot = Time.time + 1.6f / fireCharge;
                            EventQueueManager.instance.AddCommand(_cmdAttack);
                        }
                        //camThirdPerson.SetActive(false);
                    }
                    else if(Input.GetKey(_punch))
                        {
                            Punch();
                            nextTimeToPunch = Time.time + 1.6f / punchCharge;
                            EventQueueManager.instance.AddCommand(_cmdPunch);
                        }
                    else
                    {
                        Walk();
                        //camThirdPerson.SetActive(true);
                    }
                }
                
                if(Input.GetKeyDown(_lantern)){
                    EventQueueManager.instance.AddCommand(_cmdLantern);
                }
            

                if(Input.GetKeyDown(_weaponSlot1))
                {
                    ChangeWeapon(0);
                }
            
                if(Input.GetKeyDown(_weaponSlot2))
                {
                    ChangeWeapon(1);
                }
            
                if(Input.GetKeyDown(_setVictory))
                {
                    EventsManager.instance.EventGameOver(true);
                }
            
                if(Input.GetKeyDown(_setDefeat))
                {
                    GetComponent<IDamageable>().TakeDamage(20);
                }

                if (!Input.GetKey(_moveLeft) && !Input.GetKey(_moveRight) && !Input.GetKey(_moveForward) && !Input.GetKey(_moveBackward))
                {
                    if (Input.GetKey(_aim))
                    {
                        //camThirdPerson.SetActive(false);
                        if (Input.GetKey(_reload))
                        {
                            Reload();
                        }
                        else
                        {
                            Aim();
                            if (Input.GetKey(_attack) && Time.time >= nextTimeToShoot)
                            {
                                ShootAudio();
                                nextTimeToShoot = Time.time + 1.6f / fireCharge;
                                EventQueueManager.instance.AddCommand(_cmdAttack); 
                            }
                        }
                    }
                    else if(Input.GetKey(_punch))
                        {
                            Punch();
                            nextTimeToPunch = Time.time + 1.6f / punchCharge;
                            EventQueueManager.instance.AddCommand(_cmdPunch);
                        }
                    else if (Input.GetKey(_reload))
                    {
                        Reload();
                        //camThirdPerson.SetActive(true);
                    }
                    else if (Input.GetKeyDown(_jump))
                    {
                        Jump();
                        //camThirdPerson.SetActive(true);
                    }
                    else
                    {
                        Idle();
                        //camThirdPerson.SetActive(true);
                    }
                }
            }
            else
            {
                Dead();
                Cursor.lockState = CursorLockMode.None;
                EventsManager.instance.EventGameOver(true);
            }
        }
    }

    private void ShootAudio()
    {
        if(_currentGun.BulletCount > 0 && _currentGun.Ammo > 0)
        {
            audioSource.clip = rifle_shot;
            
        }
        else
        {
            audioSource.clip = empty_gunshot;
        }
        audioSource.Play();
    }

    private void ChangeWeapon(int index){
        foreach (var gun in _guns)
        {
            gun.gameObject.SetActive(false);
            _currentGun = _guns[index];
            _currentGun.gameObject.SetActive(true);
            _currentGun.Reload();
            _cmdAttack = new CmdAttack(_currentGun);

        }
    }

    private void Walk()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Running", false);
        animator.SetBool("RifleWalk", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Walk", true);
        animator.ResetTrigger("Jump");
    }

    private void Punch()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Running", false);
        animator.SetBool("RifleWalk", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Punch", true);
        animator.ResetTrigger("Jump");
    }

    private void Idle()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Running", false);
        animator.SetBool("IdleAim", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Idle", true);
        animator.ResetTrigger("Jump");
        animator.SetBool("Reloading", false);
    }

    private void Sprint()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Running", true);
        animator.ResetTrigger("Jump");
    }

    private void Jump()
    {

        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Running", false);
        animator.SetBool("Punch", false);
        animator.SetTrigger("Jump");
        capsuleCollider.height = 0.9f;
        rigidBody.useGravity = false;
        Invoke("reSizeCollider", 0.5f);
    }

    private void reSizeCollider()
    {
        capsuleCollider.height = 1.8f;
        rigidBody.useGravity = true;
    }

    private void Aim()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("RifleWalk", false);
        animator.SetBool("Punch", false);
        animator.SetBool("IdleAim", true);
        animator.SetBool("Reloading", false);
    }

    private void AimWalk()
    {
        animator.SetBool("RifleWalk", true);
        animator.SetBool("Walk", false);
        animator.SetBool("IdleAim", false);
        animator.SetBool("Reloading", false);
        animator.SetBool("Punch", false);
        animator.SetBool("Idle", false);
    }

    private void Reload()
    {
        if(_currentGun.Ammo > 0 && _currentGun.BulletCount < _currentGun.Ammo){
            isReloading = true;
            _currentGun.EmptyMagazine();
            animator.SetBool("Reloading", true);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("Punch", false);
            animator.SetBool("IdleAim", false);
            audioSource.clip = reload;
            audioSource.Play();
            StartCoroutine(WaitForReload());
        }
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(3f); // Adjust this time based on your reload animation duration
        animator.SetBool("Reloading", false); // End reloading state, allowing movement again
        _currentGun.Reload();
        isReloading = false;
    }

    private void Dead()
    {
        animator.SetBool("RifleWalk", false);
        animator.SetBool("Punch", false);
        animator.SetBool("IdleAim", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Dead", true);
    }

    private void ReloadAmmo()
    {
        _currentGun.Reload();
    }
}
