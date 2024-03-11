using UnityEngine;
using UnityEngine.UI;

public class SpecialBar : MonoBehaviour
{
    public Image specialFillImage; // Reference to the Image component representing the special fill
    private PlayerController playerCtrl; // Reference to the player controller script
    private Animator anim;

    void Start()
    {
        // Find the GameObject with the PlayerController script attached
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // Assuming "Player" tag is used for the object with PlayerController script
        if (playerObject != null)
        {
            // Get the PlayerController script component
            playerCtrl = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("No GameObject with 'PlayerController' script found!");
        }
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerCtrl != null)
        {
            // Calculate the special cooldown percentage
            float specialPercentage = 1f - (playerCtrl.timeStopCooldownTimer / playerCtrl.timeStopCooldown);

            // Clamp the percentage between 0 and 1
            specialPercentage = Mathf.Clamp01(specialPercentage);

            // Set the fill amount of the special fill image based on the special percentage
            specialFillImage.fillAmount = specialPercentage;

            // Set the SpecialReady boolean parameter of the animator based on the special fill amount
            bool specialReady = specialPercentage >= 1f;
            anim.SetBool("SpecialReady", specialReady);
        }
    }
}
