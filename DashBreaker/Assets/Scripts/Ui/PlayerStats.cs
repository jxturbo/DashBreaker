using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI atkSpeedText;
    public TextMeshProUGUI maxDistanceText;
    public TextMeshProUGUI cooldownText;
    public TextMeshProUGUI expMultiplier;
    public PlayerController playerCtrl;
    public PlayerMovement playerMove;
    public Health playerHp;

    // Update is called once per frame
    void Update()
    {
        UpdateStats(playerHp.currentHealth, playerHp.maxHealth, playerCtrl.damageAmount,
            playerMove.currentSpeed, playerCtrl.moveSpeed, playerCtrl.maxDistance, playerCtrl.cooldown, playerCtrl.currentExpMultiplier);
    }

    public void UpdateStats(float currentHp, float maxHp, float atk, float moveSpeed, float atkSpeed, float maxDistance, float cooldown, float expMult)
    {
        UpdateStatText(hpText, "HP:", $"{currentHp}/{maxHp}");
        UpdateStatText(atkText, "ATK:", atk.ToString());
        UpdateStatText(moveSpeedText, "Move Spd:", moveSpeed.ToString());
        UpdateStatText(atkSpeedText, "Atk Spd:", atkSpeed.ToString());
        UpdateStatText(maxDistanceText, "Max Dist:", maxDistance.ToString());
        UpdateStatText(cooldownText, "Cooldown:", cooldown.ToString("0.0"));
        UpdateStatText(expMultiplier, "Exp Mult:", "x" + expMult.ToString("0.0"));
    }

    void UpdateStatText(TextMeshProUGUI textComponent, string label, string value)
    {
        if (textComponent != null)
        {
            // Calculate the length of the label and value
            int labelLength = label.Length;
            int valueLength = value.Length;

            // Calculate the number of spaces needed to ensure a total length of 20 characters
            int spacesToAdd = 20 - labelLength - valueLength;

            // Adjust the spacing to ensure each line has a consistent length of 20 characters
            textComponent.text = $"{label}{new string(' ', spacesToAdd)}{value}";
        }
    }
}
