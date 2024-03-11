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
    public float x, y, z;
    public GameObject[] cardbacks;
    public bool cardBackIsActive = false;
    public int timer;

    [System.Serializable]
    public class Modifiers
    {
        public string attributeName;
        public string description;
        public Sprite accompanyingSprite;
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
        {
            player.moveSpeed += modifiers.moveSpeedMultiplier * player.baseMoveSpeed;
            playerMove.currentSpeed += modifiers.moveSpeedMultiplier * playerMove.currentSpeed;
        }

        if (modifiers.damageAmountIncrease != 0)
            player.damageAmount += modifiers.damageAmountIncrease;

        if (modifiers.maxDistanceMultiplier != 0f)
            player.maxDistance += modifiers.maxDistanceMultiplier * player.baseMaxDistance;

        if (modifiers.cooldownMultiplier != 0f)
            player.cooldown += modifiers.cooldownMultiplier * player.baseCooldown;

        if (modifiers.expMultiplier != 0f)
            currentExpMultiplier += modifiers.expMultiplier * currentExpMultiplier;
        ResetAllCards();
        gameObject.SetActive(false);
        GlobalVariableHolder.timePaused = false;
    }

    void ApplyPowerup(Modifiers modifier)
    {
        switch (modifier.AbilityId)
        {
            case 1:
                player.BurstCrashActive = true;
                break;
            case 2:
                player.PerfectionistActive = true;
                ApplyModifiers(modifier);
                break;
            case 3:
                player.VampirismActive = true;
                break;
            case 4:
                player.TimeStopActive = true;
                break;
            default:
                // Handle cases where ID is not in range 1 to 4
                break;
        }
        ResetAllCards();
        gameObject.SetActive(false);
        GlobalVariableHolder.timePaused = false;
    }

    // Method to reset the rotation of the card and set the cardback to active
    void ResetAllCards()
    {
        for (int i = 0; i < modifierCards.Length; i++)
        {
            ResetCard(modifierCards[i], cardbacks[i], i);
        }
    }

    void ResetCard(GameObject card, GameObject cardback, int index)
    {
        card.transform.rotation = Quaternion.identity; // Reset rotation
        cardback.SetActive(true); // Set cardback to active

        string buttonName = "ToClick" + index; // Construct the button name using the index
        Button button = GameObject.Find(buttonName)?.GetComponent<Button>(); // Find the button by name

        if (button != null)
        {
            button.interactable = false; // Set the interactable property of the Button to false
        }
        else
        {
            Debug.LogError("Button with name " + buttonName + " not found.");
        }
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
                if (!modifier.isAbility)
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
            contextImage.sprite = modifiers[randomIndex].accompanyingSprite;
        }

        // Start flipping the cards after setting them up
        StartFlip();
    }

    public void StartFlip()
    {
        StartCoroutine(CalculateFlip());
    }

    IEnumerator CalculateFlip()
    {
        for (int i = 0; i < modifierCards.Length; i++)
        {
            yield return StartCoroutine(FlipCard(modifierCards[i], cardbacks[i], i));
            yield return new WaitForSeconds(0.25f); // Delay between flipping each card
        }
    }

    IEnumerator FlipCard(GameObject card, GameObject cardback, int index)
    {
        string buttonName = "ToClick" + index; // Construct the button name using the index
        Button button = GameObject.Find(buttonName)?.GetComponent<Button>(); // Find the button by name

        if (button != null)
        {
            for (int i = 0; i < 90; i++)
            {
                yield return new WaitForSeconds(0.01f);
                card.transform.Rotate(new Vector3(x, y, z));
                timer++;
                if (timer == 45 || timer == -45)
                {
                    
                    cardback.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Button with name " + buttonName + " not found.");
        }

        timer = 0;
        button.interactable = true; // Set the interactable property of the Button
    }



}
