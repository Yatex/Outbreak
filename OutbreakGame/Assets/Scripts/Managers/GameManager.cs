using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private bool _isGameOver = false;
    [SerializeField] private bool _isVictory = false;
    [SerializeField] private Text _gameoverMessage;

    void Start()
    {
        EventsManager.instance.OnGameOver += OnGameOver;
        _gameoverMessage.text = string.Empty;
    }

    private void OnGameOver(bool isVictory){
        _isGameOver = true;
        _isVictory = isVictory;

        _gameoverMessage.text = isVictory ? "VICTORIA" : "DERROTA";
        _gameoverMessage.color = isVictory? Color.green : Color.red;
    }
}
