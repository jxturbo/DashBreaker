using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstCrash : MonoBehaviour
{
    public float pulseForce = 10f; // Adjust the force as needed
    public float damageAmount = 10;
    public float animationLength;
    private AudioSource audio;
    public AudioClip boom;
    public bool Active;
    public Animator anim;
    public PlayerController player;

    private List<Collider2D> damagedEnemies = new List<Collider2D>();

    private void Start()
    {
        GameObject sfxObject = GameObject.Find("SFX");
        // Get the AudioSource component attached to the "SFX" GameObject
        //audio = sfxObject.GetComponent<AudioSource>();
        Animator animator = GetComponent<Animator>();
        //audio.PlayOneShot(boom);
    }

    public void ActivateBurstCrash()
    {
        damageAmount = player.damageAmount * 0.5f;
        Active = true;
        // Clear the list of damaged enemies when the burst crash is activated
        damagedEnemies.Clear();
    }

    public void DeactivateBurstCrash()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Active)
        {
            if (other.tag == "Enemy" && !damagedEnemies.Contains(other))
            {
                Debug.Log(other.gameObject.name);
                // Check if the object has a Rigidbody2D component
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    // Calculate the direction from the object to the burst crash
                    Vector2 forceDirection = (other.transform.position - transform.position).normalized;
                    // Apply gravitational force to push the object away
                    rb.AddForce(forceDirection * pulseForce * rb.mass);
                    IDamageable damageable = other.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        // Add the enemy to the list of damaged enemies
                        damagedEnemies.Add(other);
                        damageable.TakeDamage(damageAmount);
                    }
                }
            }
        }
    }
}
