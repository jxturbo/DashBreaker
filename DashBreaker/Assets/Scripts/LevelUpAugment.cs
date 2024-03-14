using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;


public class LevelUpAugment : MonoBehaviour
{
    public Modifiers[] modifiers;
    public GameObject[] modifierCards;
    public GameObject[] currentCardHolder;
    public GameObject listHolder;
    public GameObject[] listOfCards;
    public Modifiers[] selectedModifiers; // Array to hold the selected modifiers
    private int currentIndex = 0;
    public PlayerController player;
    public PlayerMovement playerMove;
    
    public float x, y, z;
    public GameObject[] cardbacks;
    public bool cardBackIsActive = false;
    public int timer;
    public GameObject specialMeter;

    [System.Serializable]
    public class Modifiers
    {
        public string attributeName;
        public string description;
        public Sprite accompanyingSprite;
        public float moveSpeedMultiplier = 1.1f;
        public float damageAmountIncrease = 1f;
        public float maxDistanceMultiplier = 1.5f;
        public float cooldownMultiplier = 1f;
        public float expMultiplier;
        public bool isAbility;
        public int AbilityId;
    }

    void Start()
    {
        // Get the transform component of the parentGameObject
        Transform listHolderTransform = listHolder.transform;

        // Initialize the listOfCards array with the number of children
        listOfCards = new GameObject[listHolderTransform.childCount];

        // Populate the listOfCards array with the children of parentGameObject
        for (int i = 0; i < listHolderTransform.childCount; i++)
        {
            // Store each child in the listOfCards array
            listOfCards[i] = listHolderTransform.GetChild(i).gameObject;
        }
    }

    void ApplyModifiers(Modifiers modifiers)
    {
        bool isNewModifier = true;
        foreach (Modifiers selectedModifier in selectedModifiers)
        {
            if (selectedModifier == modifiers)
            {
                isNewModifier = false;
                break;
            }
        }
        if (modifiers.moveSpeedMultiplier != 0f)
        {
            player.moveSpeed += modifiers.moveSpeedMultiplier * player.baseMoveSpeed;
            playerMove.currentSpeed += modifiers.moveSpeedMultiplier * playerMove.currentSpeed;
            if(isNewModifier && !modifiers.isAbility)
            {
                modifiers.moveSpeedMultiplier = modifiers.moveSpeedMultiplier * 0.2f;
            }
        }

        if (modifiers.damageAmountIncrease != 0f)
        {
            player.damageAmount += modifiers.damageAmountIncrease;
            if(isNewModifier && !modifiers.isAbility)
            {
                modifiers.damageAmountIncrease = modifiers.damageAmountIncrease * 0.2f;
            }
        }   

        if (modifiers.maxDistanceMultiplier != 0f)
        {
            player.maxDistance += modifiers.maxDistanceMultiplier * player.baseMaxDistance;
            if(isNewModifier && !modifiers.isAbility)
            {
                modifiers.maxDistanceMultiplier = modifiers.maxDistanceMultiplier * 0.2f;
            }
        }

        if (modifiers.cooldownMultiplier != 0f)
        {
            player.cooldown += modifiers.cooldownMultiplier * player.baseCooldown;
            if(isNewModifier && !modifiers.isAbility)
            {
                modifiers.cooldownMultiplier = modifiers.cooldownMultiplier * 0.2f;
            }
        }

        if (modifiers.expMultiplier != 0f)
        {
            player.currentExpMultiplier += modifiers.expMultiplier * player.currentExpMultiplier;
            if(isNewModifier && !modifiers.isAbility)
            {
                modifiers.expMultiplier = modifiers.expMultiplier * 0.2f;
            }
        }

        if(!modifiers.isAbility)
        {
            ResetAllCards();
            AddToCurrentCards(modifiers);
        }
        if(!modifiers.isAbility)
        {
            // Access the attributeName of the modifier
            string currentAttributeName = modifiers.attributeName;

            // Split the attributeName to separate the number part
            string[] parts = currentAttributeName.Split(' ');

            // Initialize the number to 0 if there is no number or parsing fails
            float number = 0;
            if (parts.Length > 1)
            {
                // Try to parse the number part
                if (!float.TryParse(parts[1], out number))
                {
                    Debug.LogWarning("Failed to parse the number in attributeName: " + currentAttributeName);
                    number = 0; // Set number to 0 if parsing fails
                }
            }

            // Increment the number by 1
            number++;
            // Update the attributeName with the incremented number
            if (number >= 1)
            {
                // Add the current bonus modifier to the description if the number is 1
                if (modifiers.moveSpeedMultiplier != 0f)
                {
                    string moveSpeedMultiplierString = " (Spd: + " + (modifiers.moveSpeedMultiplier * number).ToString() + ")";
                    if (modifiers.description.Contains("Spd: +"))
                    {
                        // Replace existing Move Speed multiplier with the current one
                        modifiers.description = Regex.Replace(modifiers.description, @"\(Spd: + \d+(?:\.\d+)?\)", moveSpeedMultiplierString);
                    }
                    else
                    {
                        modifiers.description += moveSpeedMultiplierString;
                    }
                }

                if (modifiers.damageAmountIncrease != 0f)
                {
                    string damageAmountIncreaseString = " (Dmg: + " + (modifiers.damageAmountIncrease * number).ToString() + ")";
                    if (modifiers.description.Contains("Dmg: +"))
                    {
                        // Replace existing Damage Increase multiplier with the current one
                        modifiers.description = Regex.Replace(modifiers.description, @"\(Dmg: + \d+(?:\.\d+)?\)", damageAmountIncreaseString);
                    }
                    else
                    {
                        modifiers.description += damageAmountIncreaseString;
                    }
                }

                if (modifiers.maxDistanceMultiplier != 0f)
                {
                    string maxDistanceMultiplierString = " (Max Distance: + " + (modifiers.maxDistanceMultiplier * number).ToString() + ")";
                    if (modifiers.description.Contains("Max Distance: +"))
                    {
                        // Replace existing Max Distance multiplier with the current one
                        modifiers.description = Regex.Replace(modifiers.description, @"\(Max Distance: + \d+(?:\.\d+)?\)", maxDistanceMultiplierString);
                    }
                    else
                    {
                        modifiers.description += maxDistanceMultiplierString;
                    }
                }

                if (modifiers.cooldownMultiplier != 0f)
                {
                    string cooldownMultiplierString = " (CD: + " + (modifiers.cooldownMultiplier * number).ToString() + ")";
                    if (modifiers.description.Contains("CD: +"))
                    {
                        // Replace existing Cooldown multiplier with the current one
                        modifiers.description = Regex.Replace(modifiers.description, @"\(CD: + \d+(?:\.\d+)?\)", cooldownMultiplierString);
                    }
                    else
                    {
                        modifiers.description += cooldownMultiplierString;
                    }
                }

                if (modifiers.expMultiplier != 0f)
                {
                    string expMultiplierString = " (Exp: + " + (modifiers.expMultiplier * number).ToString() + ")";
                    if (modifiers.description.Contains("Exp: +"))
                    {
                        // Replace existing Exp Multiplier with the current one
                        modifiers.description = Regex.Replace(modifiers.description, @"\(Exp: + \d+(?:\.\d+)?\)", expMultiplierString);
                    }
                    else
                    {
                        modifiers.description += expMultiplierString;
                    }
                }
            }
            // Update the attributeName with the incremented number
            modifiers.attributeName = parts[0] + " +" + number;

        }

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
                specialMeter.SetActive(true);
                break;
            default:
                // Handle cases where ID is not in range 1 to 4
                break;
        }
        ResetAllCards();
        AddToCurrentCards(modifier);
        GlobalVariableHolder.timePaused = false;
    }

    // Method to reset the rotation of the card and set the cardback to active
    void ResetAllCards()
    {
        for (int i = 0; i < modifierCards.Length; i++)
        {
            ResetCard(modifierCards[i], cardbacks[i], i);
        }
        gameObject.SetActive(false);
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
                    
                }
                else
                {
                    ApplyPowerup(modifier);
                    RemoveModifier(modifier);
                }
                
                break;
            }
        }
        
    }

    public void AddToCurrentCards(Modifiers modifier)
    {
        // Ensure there are available slots in the selectedModifiers array
        if (currentIndex >= 0 && currentIndex < selectedModifiers.Length)
        {
            // Check if the modifier is already selected
            bool isNewModifier = true;
            foreach (Modifiers selectedModifier in selectedModifiers)
            {
                if (selectedModifier == modifier)
                {
                    isNewModifier = false;
                    break;
                }
            }

            // If it's a new modifier, add it to the selectedModifiers array
            if (isNewModifier)
            {
                currentCardHolder[currentIndex].SetActive(true);
                // Add the modifier to the selectedModifiers array
                selectedModifiers[currentIndex] = modifier;

                // Update the TextMeshPro component in the child of the card
                TextMeshProUGUI textComponent = currentCardHolder[currentIndex].transform.Find("Multiplier").GetComponent<TextMeshProUGUI>();
                textComponent.text =  "x1";

                // Get the Image component of the child of the specified child index
                Transform cardHolderTransform = currentCardHolder[currentIndex].transform;
                Image imageComponent = null;

                // Iterate through the children of the current card holder object
                foreach (Transform child in cardHolderTransform)
                {
                    // Attempt to get the Image component from the child
                    imageComponent = child.GetComponent<Image>();

                    // If an Image component is found, break the loop
                    if (imageComponent != null)
                    {
                        imageComponent.sprite = modifier.accompanyingSprite;
                        break;
                    }
                }
                listOfCards[currentIndex].SetActive(true);
                TextMeshProUGUI attributeNameText = listOfCards[currentIndex].transform.Find("Title").GetComponent<TextMeshProUGUI>();
                attributeNameText.text = modifier.attributeName;

                // Set the description text
                TextMeshProUGUI descriptionText = listOfCards[currentIndex].transform.Find("Description").GetComponent<TextMeshProUGUI>();
                descriptionText.text = modifier.description;

                // Set the context image sprite
                Image contextImage = listOfCards[currentIndex].transform.Find("ContextImage").GetComponent<Image>();
                contextImage.sprite = modifier.accompanyingSprite;
                currentIndex++;
            }
            else
            {
                // Update the TextMeshPro component in the child of the card
                TextMeshProUGUI textComponent = currentCardHolder[currentIndex].transform.Find("Multiplier").GetComponent<TextMeshProUGUI>();
                textComponent.text =  "x" + currentIndex;
                TextMeshProUGUI attributeNameText = listOfCards[currentIndex].transform.Find("Title").GetComponent<TextMeshProUGUI>();
                attributeNameText.text = modifier.attributeName;
            }
        }
        else
        {
            Debug.LogWarning("No available slots in the selectedModifiers array.");
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
