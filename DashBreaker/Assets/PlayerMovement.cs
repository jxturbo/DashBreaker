using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Use Rigidbody2D for 2D physics
    public float speed = 3f;
    private float currentSpeed = 3f;
    private bool isRunning;
    private bool isMoving;

    public SpriteRenderer playerSprite;
    public Animator playerAnim;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
    }
    //wasd to move
    //shift to sprint
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0 )
        {
            //playerAnim.SetBool("Walking", true);
            // Simple left-right movement vector in 2D
            Vector2 move = new Vector2(x, y);
            // Move the player in accordance with the vector smoothly in 2D space
            rb.velocity = new Vector2(move.x * currentSpeed, move.y * currentSpeed);
            isMoving = true;
            // Flip the sprite renderer if moving left
            if (x < 0)
            {
                if (playerSprite != null)
                    playerSprite.flipX = true;
            }
            else if (x > 0) // Reset the flip if moving right
            {
                if (playerSprite != null)
                    playerSprite.flipX = false;
            }
        }
        else
        {
            //playerAnim.SetBool("Walking", false);
            isMoving = false;
            rb.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //playerAnim.SetBool("Running", true);
            isRunning = true;
            currentSpeed = speed * 2f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            //playerAnim.SetBool("Running", false);
            isRunning = false;
            currentSpeed = speed;
        }
    }
}
