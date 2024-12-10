using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public Button exitButton;
    public Button startButton;
    public Button creditsButton;
    public Button skipButton;

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /* public void ShowCredits()
     {
         SceneManager.LoadScene(X);
     }

     public void CloseCredits()
     {
         SceneManager.LoadScene(X);
     }*/
}