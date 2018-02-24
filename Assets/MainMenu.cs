﻿using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}