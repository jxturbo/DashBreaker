using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour, IDamageable
{
    public float maxHealth = 1;
    public float currentHealth;
    public bool isEnemy;
    private GameObject player;
    public Animator PlayerAnim;
    public GameObject Trail; 
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

    public void TakeDamage(float damage)
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
                playerCtrl.killcount++;

                // Find the animator component in the child GameObject
                Animator childAnimator = GetComponentInChildren<Animator>();
                if (childAnimator != null)
                {
                    // Set the "Death" trigger in the child animator
                    childAnimator.SetTrigger("Death");

                    // Wait for the animation to finish before destroying the GameObject
                    StartCoroutine(WaitForAnimationFinish(childAnimator));
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else if(!isEnemy)
            {
                PlayerController playerCtrl = player.GetComponent<PlayerController>();
                PlayerMovement playerMove = player.GetComponent<PlayerMovement>();

                playerCtrl.enabled = false;
                playerMove.enabled = false;
                PlayerAnim.SetBool("Die",true);
                Trail.SetActive(false);
            }
        }
    }

    IEnumerator WaitForAnimationFinish(Animator animator)
    {
        // Wait for the end of the current animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the GameObject after the animation finishes
        Destroy(gameObject);
    }
}
