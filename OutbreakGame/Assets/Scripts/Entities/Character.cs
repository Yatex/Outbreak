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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip rifle_shot;
    [SerializeField] private AudioClip empty_gunshot;
    [SerializeField] private AudioClip reload;


    [SerializeField] private List<Gun> _guns;
    [SerializeField] private Gun _currentGun;
    [SerializeField] private Animator animator;

    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBackward= KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;
    [SerializeField] private KeyCode _jump = KeyCode.Space;

    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
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
    private CmdJump _cmdJump;
    private CmdRotation _cmdRotateLeft;
    private CmdRotation _cmdRotateRight;
    private CmdAttack _cmdAttack;

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
        _cmdJump = new CmdJump(_movementController, Vector3.up);
        camThirdPerson = GameObject.Find("ThirdPersonCineMachine");
        camThirdPerson.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        camAim = GameObject.Find("AimCineMachine");
        camAim.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        camThirdPerson.SetActive(true);
        lifeController = GetComponent<LifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeController.CurrentLife > 0.0)
        {
            Cursor.lockState = CursorLockMode.Locked; //or Cursor.lockState = CursorLockMode.None;

            //TODO: Luego habria que verificar si muere en el lifeController
            if(Input.GetKey(KeyCode.Y)) GetComponent<IDamageable>().TakeDamage(20); //EventsManager.instance.EventGameOver(false);
            /*  *   *   *   *   *   *   *   *   *   */
            if (Input.GetKey(_moveForward))
            {
                EventQueueManager.instance.AddCommand(_cmdMoveForward);
                if (Input.GetButton("Sprint")) 
                {
                    EventQueueManager.instance.AddCommand(_cmdSprintForward);
                    if (Input.GetKeyDown(_jump))
                    {
                        EventQueueManager.instance.AddCommand(_cmdJump);
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
                    if (Input.GetKeyDown(_attack))
                    {
                        EventQueueManager.instance.AddCommand(_cmdAttack);
                        ShootAudio();
                    }
                }
                else if (Input.GetKeyDown(_jump))
                {
                    EventQueueManager.instance.AddCommand(_cmdJump);
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
                    if (Input.GetKeyDown(_attack))
                    {
                        EventQueueManager.instance.AddCommand(_cmdAttack);
                        ShootAudio();
                    }
                    //camThirdPerson.SetActive(false);
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
                        EventQueueManager.instance.AddCommand(_cmdJump);
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
                    if (Input.GetKeyDown(_attack))
                    {
                        EventQueueManager.instance.AddCommand(_cmdAttack);
                        ShootAudio();
                    }
                    //camThirdPerson.SetActive(false);
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
                        EventQueueManager.instance.AddCommand(_cmdJump);
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
                    if (Input.GetKeyDown(_attack))
                    {
                        EventQueueManager.instance.AddCommand(_cmdAttack);
                        ShootAudio();
                    }
                    //camThirdPerson.SetActive(false);
                }
                else
                {
                    Walk();
                    //camThirdPerson.SetActive(true);
                }
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
                        if (Input.GetKeyDown(_attack))
                        {
                            EventQueueManager.instance.AddCommand(_cmdAttack);
                            ShootAudio();
                        }
                    }
                }
                else if (Input.GetKey(_reload))
                {
                    Reload();
                    //camThirdPerson.SetActive(true);
                }
                else if (Input.GetKeyDown(_jump))
                {
                    EventQueueManager.instance.AddCommand(_cmdJump);
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
        }

    }

    private void ShootAudio()
    {
        if(_currentGun.BulletCount > 0)
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
        animator.SetBool("Walk", true);
        animator.ResetTrigger("Jump");
    }

    private void Idle()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Running", false);
        animator.SetBool("IdleAim", false);
        animator.SetBool("Idle", true);
        animator.ResetTrigger("Jump");
        animator.SetBool("Reloading", false);
    }

    private void Sprint()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Running", true);
        animator.ResetTrigger("Jump");
    }

    private void Jump()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Running", false);
        // animator.SetTrigger("Jump");
    }

    private void Aim()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("RifleWalk", false);
        animator.SetBool("IdleAim", true);
        animator.SetBool("Reloading", false);
    }

    private void AimWalk()
    {
        animator.SetBool("RifleWalk", true);
        animator.SetBool("Walk", false);
        animator.SetBool("IdleAim", false);
        animator.SetBool("Reloading", false);
        animator.SetBool("Idle", false);
    }

    private void Reload()
    {
        _currentGun.EmptyMagazine();
        animator.SetBool("Reloading", true);
        animator.SetBool("RifleWalk", false);
        animator.SetBool("IdleAim", false);
        audioSource.clip = reload;
        audioSource.Play();
        Invoke("ReloadAmmo", 3f);
    }

    private void Dead()
    {
        animator.SetBool("Dead", true);
    }

    private void ReloadAmmo()
    {
        _currentGun.Reload();
    }
}
