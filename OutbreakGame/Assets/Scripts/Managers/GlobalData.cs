using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalData : MonoBehaviour {
    static public GlobalData instance;

    public int ZombiesKilled = 0;

    private void Awake(){
        if(instance != null) Destroy(this.gameObject);
        instance = this;
    
        DontDestroyOnLoad(this);
    }




}