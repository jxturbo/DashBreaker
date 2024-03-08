using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndUI : MonoBehaviour
{
    public string currentLevel = "";
    public string homePage = "";
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentLevel);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(homePage);
    }
}
