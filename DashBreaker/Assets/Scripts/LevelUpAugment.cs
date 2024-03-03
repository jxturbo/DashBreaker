using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpAugment : MonoBehaviour
{
    public Modifiers[] modifiers;
    public GameObject[] modifierCards;
    public PlayerController player;
    public float currentExpMultiplier;

    [System.Serializable]
    public class Modifiers
    {
        public string attributeName;
        public string description;
        public Image contextImage;
        public float moveSpeedMultiplier = 1.1f;
        public int damageAmountIncrease = 1;
        public float maxDistanceMultiplier = 1.5f;
        public float cooldownMultiplier = 1f;
        public float expMultiplier;
    }

    void ApplyModifiers(Modifiers modifiers)
    {
        if (modifiers.moveSpeedMultiplier != 0f)
            player.moveSpeed *= modifiers.moveSpeedMultiplier;

        if (modifiers.damageAmountIncrease != 0)
            player.damageAmount += modifiers.damageAmountIncrease;

        if (modifiers.maxDistanceMultiplier != 0f)
            player.maxDistance *= modifiers.maxDistanceMultiplier;

        if (modifiers.cooldownMultiplier != 0f)
            player.cooldown *= modifiers.cooldownMultiplier;

        if (modifiers.expMultiplier != 0f)
            currentExpMultiplier += modifiers.expMultiplier;
    }

    public void SelectModifier(int index)
    {
        if (index >= 0 && index < modifiers.Length)
        {
            ApplyModifiers(modifiers[index]);
        }
        else
        {
            Debug.LogError("Invalid index provided.");
        }
    }
}
