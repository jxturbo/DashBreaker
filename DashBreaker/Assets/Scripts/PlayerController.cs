using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Base DashAttack stats ")]
    public float baseMoveSpeed = 10f;
    public float basedamageAmount = 10;
    public float baseMaxDistance;
    public float baseCooldown = 1f;

    [Header("DashAttack stats ")]
    public float moveSpeed = 10f;
    public float damageAmount = 10;
    public float maxDistance;
    public float cooldown = 1f;

    public LayerMask enemyMask;
    public bool cooldownEnabled;
    private Vector3 mousePosition;
    public bool isInvincible;

    [Header("Misc ")]
    public Health playerHp;
    private Camera mainCamera;

    [Header("Exp ")]
    public float currentExp = 0f;
    public float maxExp = 100f;
    public int level = 1;
    public float MaxExpIncreaseAmount = 50f;
    public GameObject AttributeSelectScreen;

    [Header("Powerups ")]
    public bool BurstCrashActive;
    public GameObject BurstHolder;
    public bool PerfectionistActive;
    public float perfectionistStacks;
    public float currentBonusModifier = 0.1f;
    public bool VampirismActive;

    void Start()
    {
        moveSpeed = baseMoveSpeed;
        damageAmount = basedamageAmount;
        maxDistance = baseMaxDistance;
        cooldown = baseCooldown;
        mainCamera = Camera.main;
    }

    void Update()
    {
        HandlePlayerInput();
    }

    void HandlePlayerInput()
    {
        // Check for mouse click
        if (Input.GetMouseButtonDown(0) && !cooldownEnabled)
        {
            // Convert the mouse position from screen space to world space (2D)
            mousePosition = Input.mousePosition;
            Vector2 clickedPoint = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y));
            // Draw a line from player to the clicked spot
            Debug.DrawLine(transform.position, clickedPoint, Color.red, 2f);

            // Move player to the clicked spot
            StartCoroutine(PerformDashAttack(clickedPoint));
            DamageObjectsInPath(clickedPoint);

        }
    }

    IEnumerator PerformDashAttack(Vector2 targetPosition)
    {
        cooldownEnabled = true;
        isInvincible = true;
        // Calculate the distance between the current position and the target position
        float originalDistance = Vector2.Distance(transform.position, targetPosition);
        // Calculate the duration of the movement based on the original distance
        float duration = originalDistance / moveSpeed;
        float startTime = Time.time;
        Vector2 startPosition = transform.position;

        while (Time.time < startTime + duration)
        {
            // Calculate the current distance traveled
            float currentDistance = Vector2.Distance(transform.position, startPosition);

            // If the current distance exceeds the maximum distance, stop the movement
            if (currentDistance >= maxDistance)
            {
                break;
            }

            // Calculate the interpolation factor
            float t = (Time.time - startTime) / duration;
            // Move the player
            transform.position = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        //performs burst crash at end of attack
        if(BurstCrashActive)
        {
            BurstHolder.SetActive(true);
            BurstHolder.GetComponent<BurstCrash>().ActivateBurstCrash();
        }
        isInvincible = false;
        yield return new WaitForSeconds(cooldown);
        cooldownEnabled = false;
        
    }

    void DamageObjectsInPath(Vector2 targetPosition)
    {
        // Check for objects between player and target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, maxDistance, enemyMask);

        // Damage each object in the path
        foreach (RaycastHit2D hit in hits)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
                // 30% chance to heal the player for 10% of max HP
                if (Random.value < 0.3f && playerHp.currentHealth <= playerHp.maxHealth && VampirismActive) // Random.value returns a float between 0.0 and 1.0
                {
                    playerHp.currentHealth += playerHp.maxHealth * 0.1f;
                    if(playerHp.currentHealth > playerHp.maxHealth)
                    {
                        playerHp.currentHealth = playerHp.maxHealth;
                    }
                }
            }
        }
        if(hits.Length > 0 && PerfectionistActive && perfectionistStacks <= 10f)
        {
            perfectionistStacks += 1f;
            damageAmount += currentBonusModifier * basedamageAmount;
            cooldown -= currentBonusModifier * baseCooldown;
            maxDistance += currentBonusModifier * baseMaxDistance;
        }
        else if(hits.Length == 0 && PerfectionistActive)
        {
            damageAmount -= currentBonusModifier * basedamageAmount * perfectionistStacks;
            cooldown += currentBonusModifier * baseCooldown * perfectionistStacks;
            maxDistance -= currentBonusModifier * baseMaxDistance * perfectionistStacks;
            perfectionistStacks = 0f;
        }
    }

    public void GainExp(float amount)
    {
        currentExp += amount;
        // Ensure currentExp does not exceed maxExp
        currentExp = Mathf.Min(currentExp, maxExp);
        UpdateExpBar();
    }

    void UpdateExpBar()
    {
        if (currentExp >= maxExp)
        {
            level++;
            currentExp = 0f;
            maxExp += MaxExpIncreaseAmount;
            //more or less creates a upwards curve to make things more difficult
            MaxExpIncreaseAmount += MaxExpIncreaseAmount;
            AttributeSelectScreen.SetActive(true);
            LevelUpAugment levelup = AttributeSelectScreen.GetComponent<LevelUpAugment>();
            Invoke("PauseTime", 0.3f);
            levelup.ShowCards();
        }

    }
    void PauseTime()
    {
        Time.timeScale = 0f;
    }
}
