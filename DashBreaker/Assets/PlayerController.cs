using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float damageAmount = 10f;
    public float maxDistance;
    public float cooldown = 1f;
    public LayerMask enemyMask;
    public bool cooldownEnabled;

    public Camera mainCamera;

    void Start()
    {
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
            Vector3 mousePosition = Input.mousePosition;
            Vector2 clickedPoint = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y));
            // Draw a line from player to the clicked spot
            Debug.DrawLine(transform.position, clickedPoint, Color.red, 2f);

            // Move player to the clicked spot
            StartCoroutine(PerformDashAttack(clickedPoint));

            // Perform raycast if needed (optional)
            RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                // Handle hit object
            }
        }
    }

    IEnumerator PerformDashAttack(Vector2 targetPosition)
    {
        cooldownEnabled = true;
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
        yield return new WaitForSeconds(cooldown);
        cooldownEnabled = false;
    }





    // void DamageObjectsInPath(Vector3 targetPosition)
    // {
    //     // Check for objects between player and target position
    //     Vector3 direction = (targetPosition - transform.position).normalized;
    //     float maxDistance = Vector3.Distance(transform.position, targetPosition);
    //     RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, maxDistance, enemyMask);

    //     // Damage each object in the path
    //     foreach (RaycastHit hit in hits)
    //     {
    //         IDamageable damageable = hit.collider.GetComponent<IDamageable>();
    //         if (damageable != null)
    //         {
    //             damageable.TakeDamage(damageAmount);
    //         }
    //     }
    // }
}
