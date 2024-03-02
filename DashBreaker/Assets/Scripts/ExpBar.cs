using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpBar : MonoBehaviour
{
    public Image expFillImage; // Reference to the Image component representing the exp fill
    private PlayerController playerCtrl; // Reference to the PlayerController script
    private float expPercentage;
    public TextMeshProUGUI levelText; // Reference to the TextMeshProUGUI component for displaying the level

    void Start()
    {
        // Find the GameObject with the PlayerController script attached
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assuming "Player" tag is used for the object with PlayerController script
        if (player != null)
        {
            // Get the PlayerController script component
            playerCtrl = player.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("No GameObject with 'PlayerController' script found!");
        }
    }

    void Update()
    {
        // Calculate the exp percentage
        expPercentage = playerCtrl.currentExp / playerCtrl.maxExp;

        // Set the fill amount of the exp fill image based on the exp percentage
        expFillImage.fillAmount = expPercentage;

        // Update the level text
        levelText.text = "Lvl: " + playerCtrl.level.ToString();
        


    }
}
