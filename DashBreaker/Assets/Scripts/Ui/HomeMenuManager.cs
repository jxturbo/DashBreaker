using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenuManager : MonoBehaviour
{
    public GameObject HomeScreen;


    public void StartLevel(float level)
    {
        SceneManager.LoadScene("GameLevel");
    }

    public void Exit()
    {
        // Close the application
        Debug.Log("Game quit");
        Application.Quit();
    }
}
