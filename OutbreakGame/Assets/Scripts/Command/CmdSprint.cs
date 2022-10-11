using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CmdSprint : ICommand
{
    //propiedades
    private IMoveable _moveable;
    private Vector3 _direction;

    public CmdSprint(IMoveable moveable, Vector3 direction)
    {
        _moveable = moveable;
        _direction = direction;
    }

    public void Execute() => _moveable.Travel(_direction);
}
