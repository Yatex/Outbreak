using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _lifebar;
    [SerializeField] private Text _ammo;

    private void Start(){
        EventsManager.instance.OnAmmoChange += OnAmmoChange;
       // EventsManager.instance.OnCharacterLifeChange += OnCharacterLifeChange;
    }

    private void OnAmmoChange(int currentAmmo, int maxAmmo){
        _ammo.text = $"{currentAmmo}/{maxAmmo}";
    }
   /*
    private void OnCharacterLifeChange(float currentLife, float maxLife){
        _lifebar.fillAmount = currentLife/maxLife;
        
    }
    */
}
