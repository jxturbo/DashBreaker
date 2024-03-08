using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class EnemyCount : MonoBehaviour
{
    public TextMeshProUGUI enemyCountText; // Reference to the TextMeshPro component
    public GameObject[] enemies; // Reference to your array of enemies
    public bool endless;

    void Start()
    {
        if(!endless)
        {
            // Initialize your array of enemies (assuming you have enemies in the scene)
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            UpdateEnemyCountText(0f);
        }
        else if(endless)
        {
            UpdateEnemyKillCountText(0f);
        }
    }

    public void UpdateEnemyCountText(float test)
    {
        // Update the text to display the count
        enemyCountText.text = test + "/" + enemies.Length.ToString();
    }
    public void UpdateEnemyKillCountText(float test)
    {
        enemyCountText.text = test.ToString();
    }
}
