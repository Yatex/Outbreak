using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonLogic : MonoBehaviour
{
    public void LoadMenuScene() => SceneManager.LoadScene("MainMenu");
    public void LoadLevelScene() => SceneManager.LoadScene("Scene_A");
    public void LoadSettingsScene() => Debug.Log("Settings screen");
    public void CloseGame() => Application.Quit();
}
