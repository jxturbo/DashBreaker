using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Make sure to include this line

public class Reset : MonoBehaviour
{
    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the "R" key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reset the current scene
            ResetLevel();
        }
    }

    // Function to reset the level
    void ResetLevel()
    {
        Time.timeScale = 1f;
        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}
