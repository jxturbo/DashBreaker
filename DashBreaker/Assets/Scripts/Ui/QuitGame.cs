using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public GameObject exitCheck;
    public GameObject cardList;
    void Update()
    {
        // Check for the Escape key (KeyCode.Escape), not Getkeycode(escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        // Use SetActive to toggle the GameObject's active state
        exitCheck.SetActive(!exitCheck.activeSelf);
        if (exitCheck.activeSelf)
        {
            // Freeze time
            Time.timeScale = 0f;
        }
        else
        {
            cardList.SetActive(false);
            // Unpause time
            Time.timeScale = 1f;
        }
    }
    public void ExitGame()
    {
        // Close the application (this won't work in the Unity Editor)
        Debug.Log("Game quit");
        Application.Quit();
    }
    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Home");
    }
}
