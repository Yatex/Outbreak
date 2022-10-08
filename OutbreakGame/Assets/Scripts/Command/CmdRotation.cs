using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CmdRotation : ICommand {
    //propiedades
    private IMoveable _moveable;
    private Vector3 _direction;

    public CmdRotation(IMoveable moveable, Vector3 direction){
        _moveable = moveable;
        _direction = direction;
    }

    public void Execute() => _moveable.Rotate(_direction);
}
