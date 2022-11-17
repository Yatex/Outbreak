using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdLantern : ICommand
{
    private Light _lantern;

    public CmdLantern(Light lantern){
        _lantern = lantern;
    }
    public void Execute(){
        if(_lantern.enabled) _lantern.enabled = false;
        else _lantern.enabled = true;
    } 
}
