using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMenuManager : MonoBehaviour
{
    public GameObject LevelSelect;
    public GameObject HomeScreen;


    public void LevelSelectSwitch()
    {
        LevelSelect.SetActive(true);
        HomeScreen.SetActive(false);
    }
    public void StartLevel(float level)
    {
        switch (level)
        {
            case 0:
                SceneManager.LoadScene("Tutorial");
                break;
            case 1:
                SceneManager.LoadScene("Speed");
                break;
            case 2:
                SceneManager.LoadScene("Pull");
                break;
            case 3:
                SceneManager.LoadScene("Chaos");
                break;
            case 4:
                SceneManager.LoadScene("Boss");
                break;
            default:
                Debug.LogError("Invalid level value. No corresponding scene found.");
                break;
        }
    }
    public void StartEndless()
    {
        SceneManager.LoadScene("Endless");
    }
    public void Exit()
    {
        // Close the application
        Debug.Log("Game quit");
        Application.Quit();
    }
}
