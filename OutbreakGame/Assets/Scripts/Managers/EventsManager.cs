using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    static public EventsManager instance;

    private void Awake()
    {
        if(instance != null) Destroy(this);
        instance = this;
    }

    public event Action<bool> OnGameOver;

    public void EventGameOver(bool isVictory){
        if(OnGameOver != null) OnGameOver(isVictory);

    }

}
