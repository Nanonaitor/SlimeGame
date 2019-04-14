using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameScene = "GameTest"; //default

    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
