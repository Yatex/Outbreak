using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private bool _isGameOver = false;

    void Start()
    {
        EventsManager.instance.OnGameOver += OnGameOver;
    }

    private void OnGameOver(bool isVictory){
        Cursor.lockState = CursorLockMode.None;
        Invoke("LoadEndGameScene", 3);
    }
    private void LoadEndGameScene() => SceneManager.LoadScene("EndGame");
}
