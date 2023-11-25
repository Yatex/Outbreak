using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdPunch : ICommand
{
    private IGun _gun;

    public CmdPunch(IGun gun){
        _gun = gun;
    }
    public void Execute()=> _gun.Punch();
}
