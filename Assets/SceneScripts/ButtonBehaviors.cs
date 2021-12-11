using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviors : MonoBehaviour
{
    public void ExitGame()
    {
       // Debug.Log("You have exited");
        Application.Quit();
    }

    public void StartGame()
    {
        //start the game
        SceneManager.LoadScene("EnvironmentScene");
    }

    public void MainMenu()
    {
        //go to the title screen
       // Debug.Log("go to title screen");
        SceneManager.LoadScene("TitleScreen");
    }
}
