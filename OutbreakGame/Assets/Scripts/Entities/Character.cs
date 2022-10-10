using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    private MovementController _movementController;

    [SerializeField] private List<Gun> _guns;
    [SerializeField] private Gun _currentGun;

    [SerializeField] private KeyCode _moveForward = KeyCode.W;
    [SerializeField] private KeyCode _moveBackward= KeyCode.S;
    [SerializeField] private KeyCode _moveLeft = KeyCode.A;
    [SerializeField] private KeyCode _moveRight = KeyCode.D;
    
    [SerializeField] private KeyCode _attack = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reload = KeyCode.R;

    [SerializeField] private KeyCode _weaponSlot1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode _weaponSlot2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode _setVictory = KeyCode.Return;
    [SerializeField] private KeyCode _setDefeat = KeyCode.Backspace;


    /*  COMANDOS    */
    private CmdMovement _cmdMoveForward;
    private CmdMovement _cmdMoveBack;
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
        _cmdRotateLeft = new CmdRotation(_movementController, Vector3.left);
        _cmdRotateRight = new CmdRotation(_movementController, Vector3.right);
        _cmdAttack = new CmdAttack(_currentGun);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked; //or Cursor.lockState = CursorLockMode.None;

        if (Input.GetKey(_moveForward)) EventQueueManager.instance.AddCommand(_cmdMoveForward);
        if(Input.GetKey(_moveBackward)) EventQueueManager.instance.AddCommand(_cmdMoveBack);
        EventQueueManager.instance.AddCommand(_cmdRotateLeft);
        EventQueueManager.instance.AddCommand(_cmdRotateRight);
        if(Input.GetKey(_moveLeft)) EventQueueManager.instance.AddCommand(_cmdRotateLeft);
        if(Input.GetKey(_moveRight)) EventQueueManager.instance.AddCommand(_cmdRotateRight);

        if (Input.GetKeyDown(_reload)) _currentGun.Reload();
        if(Input.GetKeyDown(_attack)) EventQueueManager.instance.AddCommand(_cmdAttack);

        if(Input.GetKeyDown(_weaponSlot1)) ChangeWeapon(0);
        if(Input.GetKeyDown(_weaponSlot2)) ChangeWeapon(1);
        if(Input.GetKeyDown(_setVictory)) EventsManager.instance.EventGameOver(true);
        if(Input.GetKeyDown(_setDefeat)) GetComponent<IDamageable>().TakeDamage(20);
        
    }

    private void ChangeWeapon(int index){
        foreach (var gun in _guns)
        {
            gun.gameObject.SetActive(false);
            _currentGun = _guns[index];
            _currentGun.gameObject.SetActive(true);
            _cmdAttack = new CmdAttack(_currentGun);
        }
    }
}
