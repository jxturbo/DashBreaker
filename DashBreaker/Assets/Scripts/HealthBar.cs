using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthFillImage; // Reference to the Image component representing the health fill
    private Health healthScript; // Reference to the Health script
    private float healthPercentage;

    void Start()
    {
        // Find the GameObject with the Health script attached
        GameObject healthObject = GameObject.FindGameObjectWithTag("Player"); // Assuming "Player" tag is used for the object with Health script
        if (healthObject != null)
        {
            // Get the Health script component
            healthScript = healthObject.GetComponent<Health>();
        }
        else
        {
            Debug.LogError("No GameObject with 'Health' script found!");
        }
    }

    void Update()
    {
        // Calculate the health percentage
        healthPercentage = (float)healthScript.currentHealth / healthScript.maxHealth;

        // Set the fill amount of the health fill image based on the health percentage
        healthFillImage.fillAmount = healthPercentage;
    }
}
