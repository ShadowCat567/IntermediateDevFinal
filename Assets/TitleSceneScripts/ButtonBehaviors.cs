using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviors : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("You have exited");
        Application.Quit();
    }

    public void StartGame()
    {
        //start the game
        Debug.Log("Go to game start screen");
    }
}
