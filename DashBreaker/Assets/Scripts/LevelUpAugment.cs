using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpAugment : MonoBehaviour
{
    public Modifiers[] modifiers;
    public GameObject[] modifierCards;
    public PlayerController player;
    public PlayerMovement playerMove;
    public float currentExpMultiplier;

    [System.Serializable]
    public class Modifiers
    {
        public string attributeName;
        public string description;
        public Image contextImage;
        public float moveSpeedMultiplier = 1.1f;
        public float damageAmountIncrease = 1;
        public float maxDistanceMultiplier = 1.5f;
        public float cooldownMultiplier = 1f;
        public float expMultiplier;
        public bool isAbility;
        public int AbilityId;
    }

    void ApplyModifiers(Modifiers modifiers)
    {
        if (modifiers.moveSpeedMultiplier != 0f)
            player.moveSpeed += modifiers.moveSpeedMultiplier * player.moveSpeed;
            playerMove.currentSpeed += modifiers.moveSpeedMultiplier * playerMove.currentSpeed;

        if (modifiers.damageAmountIncrease != 0)
            player.damageAmount += modifiers.damageAmountIncrease * player.damageAmount;

        if (modifiers.maxDistanceMultiplier != 0f)
            player.maxDistance += modifiers.maxDistanceMultiplier * player.maxDistance ;

        if (modifiers.cooldownMultiplier != 0f)
            player.cooldown += modifiers.cooldownMultiplier * player.cooldown;

        if (modifiers.expMultiplier != 0f)
            currentExpMultiplier += modifiers.expMultiplier * currentExpMultiplier;
        gameObject.SetActive(false);
        Time.timeScale = 1f;

    }

    void ApplyPowerup(Modifiers modifier)
    {
        switch(modifier.AbilityId)
        {
            case 1:
                player.BurstCrashActive = true;
                break;
            case 2:
                // Apply powerup for ID 2
                break;
            case 3:
                // Apply powerup for ID 3
                break;
            case 4:
                // Apply powerup for ID 4
                break;
            default:
                // Handle cases where ID is not in range 1 to 4
                break;
        }
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SelectCard(int cardId)
    {
        // Find the TextMeshProUGUI component for the attribute name in the clicked card
        TextMeshProUGUI attributeNameText = modifierCards[cardId].transform.Find("Title").GetComponent<TextMeshProUGUI>();
        
        // Get the attribute name from the text component
        string attributeName = attributeNameText.text;

        // Iterate through modifiers to find the one with matching attributeName
        foreach (Modifiers modifier in modifiers)
        {
            if (modifier.attributeName == attributeName)
            {
                if(!modifier.isAbility)
                {
                    ApplyModifiers(modifier);
                    // Remove the modifier from the array
                    //RemoveModifier(modifier);
                }
                else
                {
                    ApplyPowerup(modifier);
                }
                break;
            }
        }
    }

    void RemoveModifier(Modifiers modifierToRemove)
    {
        List<Modifiers> tempList = new List<Modifiers>(modifiers);
        tempList.Remove(modifierToRemove);
        modifiers = tempList.ToArray();
    }


    public void ShowCards()
    {
        // Create a list to keep track of selected modifiers
        List<int> selectedIndexes = new List<int>();

        // Iterate through each modifier card
        for (int i = 0; i < modifierCards.Length; i++)
        {
            // Choose a random modifier that hasn't been selected yet
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, modifiers.Length);
            } while (selectedIndexes.Contains(randomIndex));

            // Add the selected index to the list
            selectedIndexes.Add(randomIndex);

            // Set the attribute name text
            TextMeshProUGUI attributeNameText = modifierCards[i].transform.Find("Title").GetComponent<TextMeshProUGUI>();
            attributeNameText.text = modifiers[randomIndex].attributeName;

            // Set the description text
            TextMeshProUGUI descriptionText = modifierCards[i].transform.Find("Description").GetComponent<TextMeshProUGUI>();
            descriptionText.text = modifiers[randomIndex].description;

            // Set the context image sprite
            Image contextImage = modifierCards[i].transform.Find("ContextImage").GetComponent<Image>();
            //contextImage.sprite = modifiers[randomIndex].contextImage.sprite;
        }
    }

}
