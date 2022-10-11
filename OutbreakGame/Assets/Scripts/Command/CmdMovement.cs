using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CmdMovement : ICommand {
    //propiedades
    private IMoveable _moveable;
    private Vector3 _direction;
    private Vector3 _rotation;

    public CmdMovement(IMoveable moveable, Vector3 direction){
        _moveable = moveable;
        _direction = direction;
    }
    public CmdMovement(IMoveable moveable, Vector3 direction, Vector3 rotation){
        _moveable = moveable;
        _direction = direction;
        _rotation = rotation;
    }

    public void Execute(){
         _moveable.Travel(_direction);
        if (_rotation != null)
        {
             _moveable.Rotate(_rotation);
        }
    }
}
