using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    static public EventsManager instance;

    private void void Awake()
    {
        if(instance != null) Destroy(this);
        instance = this;
    }

    #region GAME_OVER
    public event Action<bool> OnGameOver;
    public void EventGameOver(bool isVictory){
        if(OnGameOver != null) OnGameOver(isVictory);
    }
    #endregion

    #region UI_EVENTS
    public event Action<int, int> OnAmmoChange;
    public void AmmoChange(int currentAmmo, int maxAmmo){
        if(OnAmmoChange != null) OnAmmoChange(currentAmmo, maxAmmo);    //TODO: Crean AmmoChangeActivator en Gun.cs
    }
    #endregion


}
