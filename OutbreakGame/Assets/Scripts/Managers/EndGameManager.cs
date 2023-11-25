using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndGameManager : MonoBehaviour
{

    [SerializeField] private Text _zombiesKilled;

    // Start is called before the first frame update
    void Start()
    {
    //    _zombiesKilled.text = $"{GlobalData.instance.ZombiesKilled} zombies killed";
        _zombiesKilled.text = "Kill Zombies";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
