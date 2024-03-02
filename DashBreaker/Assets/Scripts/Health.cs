using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int maxHealth = 1;
    public int currentHealth;
    public bool isEnemy;
    private GameObject player;
    public float ExperiencePoints;
    private bool doubleCheck;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        // Reduce health by the damage amount
        currentHealth -= damage;
        // Check if health is zero or below
        if (currentHealth <= 0)
        {
            if(isEnemy && !doubleCheck)
            {
                doubleCheck = true;
                PlayerController playerCtrl = player.GetComponent<PlayerController>();
                playerCtrl.GainExp(ExperiencePoints);
                // Destroy the GameObject if health is zero or below
                Destroy(gameObject);
            }
        }
    }
}
